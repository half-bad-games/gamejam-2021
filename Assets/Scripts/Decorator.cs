using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Stats
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

    public GameObjectAdapterComponent(string name, System.Type type)
    {
        var gameObj = GameObject.Find(name);

        if (gameObj)
        {
            baseComponent = gameObj.GetComponent(type);

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
            if (this.baseComponent != null)
            {
                var clone = GameObject.Instantiate(obj);
                clone.transform.SetParent(this.baseComponent.transform, false);
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
