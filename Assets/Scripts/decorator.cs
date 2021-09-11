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

class PlayerAdapterComponent : Component
{
    Stats stats;

    public PlayerAdapterComponent(Player p)
    {
        this.stats = new Stats();
        this.stats.health = p.health;
    }

    public Stats extend()
    {
        return this.stats;
    }
}

abstract class Decorator : Component
{
    Component component;
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

    public Stats extend()
    {
        this.stats = this.component.extend();
        return this.add();
    }

    public abstract Stats add();
}

class Spear : Decorator
{
    public Spear() { }

    public Spear(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.damage += 10;
        return this.stats;
    }
}

class Fin : Decorator
{
    public Fin() { }

    public Fin(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.agility += 10;
        return this.stats;
    }
}

class Scale : Decorator
{
    public Scale() { }

    public Scale(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.health += 10;
        return this.stats;
    }
}

class Paddles : Decorator
{
    public Paddles() { }

    public Paddles(Component component) : base(component) { }

    public override Stats add()
    {
        this.stats.speed += 10;
        return this.stats;
    }
}

class DecoratorFactory
{
    Component component;
    List<Decorator> decorators;

    public DecoratorFactory(Component component)
    {
        this.component = component;
        
        this.decorators = new List<Decorator>();

        decorators.Add(new Spear());
        decorators.Add(new Fin());
        decorators.Add(new Paddles());
        decorators.Add(new Scale());
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
                dec.setComponent(lastDec);
            }
        }

        return dec;
    }
}
