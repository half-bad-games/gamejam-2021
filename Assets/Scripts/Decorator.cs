using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct Stats
{
    public int speed;
    public int damage;
    public float health;
    public int agility;
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

class FinDecorator : Decorator
{
    public FinDecorator() { }

    public FinDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.agility += 10;
        return this.stats;
    }
}

class ScaleDecorator : Decorator
{
    public ScaleDecorator() { }

    public ScaleDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.health += 10;
        return this.stats;
    }
}

class PaddlesDecorator : Decorator
{
    public PaddlesDecorator() { }

    public PaddlesDecorator(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.speed += 10;
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
        decorators.Add(new FinDecorator());
        decorators.Add(new PaddlesDecorator());
        decorators.Add(new ScaleDecorator());
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
