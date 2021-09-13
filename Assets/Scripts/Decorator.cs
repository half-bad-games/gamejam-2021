using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface Component
{
    Playable extend();
}

class PlayableAdapterComponent : Component
{
    Playable baseComponent;

    public PlayableAdapterComponent(string name)
    {
        var gameObj = GameObject.Find(name);

        if (gameObj != null)
        {
            baseComponent = gameObj.GetComponent<Playable>();
        }
    }

    public Playable extend()
    {
        return this.baseComponent;
    }
}

abstract class Decorator : Component
{
    Component component;
    protected Playable currentComponent;
    protected Playable baseComponent;

    protected Decorator() { }

    protected Decorator(Component component)
    {
        this.setComponent(component);
    }

    public void setComponent(Component component)
    {
        this.component = component;
    }

    public void setComponent(Component component, Playable baseComponent)
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
    public abstract Playable add();

    public Playable extend()
    {
        this.currentComponent = this.component.extend();
        this.loadAsset();
        return this.add();
    }
}
