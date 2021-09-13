using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class PlayerDecoratorFactory
{
    Component component;
    Playable baseComponent;
    List<Decorator> decorators;

    public PlayerDecoratorFactory(Component component, Playable baseComponent)
    {
        this.component = component;
        this.baseComponent = baseComponent;
        
        this.decorators = new List<Decorator>();

        decorators.Add(new MouthDecorator());
        decorators.Add(new BasicFinsDecorator());
        decorators.Add(new EyesDecorator());
        decorators.Add(new WhiskersDecorator());
        decorators.Add(new SlimeDecorator());
        decorators.Add(new SpearDecorator());
        decorators.Add(new SpikesDecorator());
        decorators.Add(new ThirdEyeDecorator());
        decorators.Add(new FishTailDecorator());
    }

    // Generate a composition of random of Decorators
    public Decorator generate(string name)
    {
        Decorator dec = null;

        for (var i = 0; i < this.decorators.Count; i++)
        {
            if (decorators[i].GetType().ToString() == name)
            {
                dec = decorators[i];
                dec.setComponent(this.component, this.baseComponent);
            }
        }

        return dec;
    }
}
