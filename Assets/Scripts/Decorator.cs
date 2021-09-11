using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct Stats
{
    public int speed;
    public int damage;
    public float health;
    public int agility;
    public int vision;
}

interface Component
{
    Stats extend();
}

class GameObjectAdapterComponent : Component
{
    Stats stats;

    dynamic baseComponent;

    public GameObjectAdapterComponent(string name)
    {
        var obj = GameObject.Find(name);

        if (obj)
        {
            baseComponent = obj.GetComponent(System.Type.GetType(name));

            this.stats = new Stats();
            this.stats.health = baseComponent.health;
        }
    }

    public Stats extend()
    {
        return this.stats;
    }
}

abstract class Decorator : Component
{
    Component component;
    protected dynamic baseComponent;
    protected Stats stats;

    protected Decorator() { }

    protected Decorator(Component component)
    {
        this.setComponent(component);
    }

    public void setComponent(Component component)
    {
        this.component = component;
    }

    public void setComponent(Component component, dynamic baseComponent)
    {
        this.setComponent(component);
        this.baseComponent = baseComponent;
    }

    void loadAsset()
    {
        var name = this.GetType().Name.Split(
            new string[] {"Decorator"},
            System.StringSplitOptions.None
        )[0];

        var obj = Resources.Load<GameObject>($"Prefab/{name}");

        if (obj)
        {
            var clone = GameObject.Instantiate(obj);
            
            if (clone != null && this.baseComponent != null)
            {
                clone.transform.SetParent(this.baseComponent.transform);
            }
        }
    }

    public Stats extend()
    {
        this.stats = this.component.extend();
        this.loadAsset();
        return this.add();
    }

    public abstract Stats add();
}

class SpearDecorator : Decorator
{
    public SpearDecorator() { }

    public SpearDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.damage += 10;
        return this.stats;
    }
}

class FishTailDecorator : Decorator
{
    public FishTailDecorator() { }

    public FishTailDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.speed += 20;
        return this.stats;
    }
}

class TapedSwordDecorator : Decorator
{
    public TapedSwordDecorator() { }

    public TapedSwordDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.damage += 20;
        return this.stats;
    }
}

class LightSaberDecorator : Decorator
{
    public LightSaberDecorator() { }

    public LightSaberDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.damage += 100;
        return this.stats;
    }
}

class EyesDecorator : Decorator
{
    public EyesDecorator() { }

    public EyesDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.vision += 5;
        return this.stats;
    }
}

class WhiskersDecorator : Decorator
{
    public WhiskersDecorator() { }

    public WhiskersDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.agility += 5;
        return this.stats;
    }
}

class ThirdEyeDecorator : Decorator
{
    public ThirdEyeDecorator() { }

    public ThirdEyeDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.vision += 10;
        return this.stats;
    }
}

class SlimeDecorator : Decorator
{
    public SlimeDecorator() { }

    public SlimeDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.speed += 10;
        return this.stats;
    }
}

class ForceFieldDecorator : Decorator
{
    public ForceFieldDecorator() { }

    public ForceFieldDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.health += 100;
        return this.stats;
    }
}

class DecoratorFactory
{
    Component component;
    dynamic baseComponent;
    List<Decorator> decorators;

    public DecoratorFactory(Component component, dynamic baseComponent)
    {
        this.component = component;
        this.baseComponent = baseComponent;
        
        this.decorators = new List<Decorator>();

        decorators.Add(new SpearDecorator());
        decorators.Add(new FishTailDecorator());
        decorators.Add(new TapedSwordDecorator());
        decorators.Add(new LightSaberDecorator());
        decorators.Add(new EyesDecorator());
        decorators.Add(new WhiskersDecorator());
        decorators.Add(new ThirdEyeDecorator());
        decorators.Add(new SlimeDecorator());
        decorators.Add(new ForceFieldDecorator());
    }

    // Generate a composition of random of Decorators
    public Decorator generate(int n)
    {
        Decorator dec = null;

        for (var i = 0; i < n; i++)
        {
            var lastDec = dec;
            dec = System.Activator.CreateInstance(decorators[Random.Range(0, this.decorators.Count)].GetType()) as Decorator;

            if (i == 0)
            {
                dec.setComponent(this.component);
            }
            else
            {
                dec.setComponent(lastDec, this.baseComponent);
            }
        }

        return dec;
    }
}
