using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class EnemyDecoratorFactory
{
    Component component;
    Playable baseComponent;
    List<Decorator> decorators;

    public EnemyDecoratorFactory(Component component, Playable baseComponent)
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
    public Decorator generate(int n)
    {
        Decorator dec = null;

        for (var i = 0; i < n + 1; i++)
        {
            var lastDec = dec;
            dec = System.Activator.CreateInstance(
                decorators[Random.Range(0, this.decorators.Count)].GetType()
            ) as Decorator;

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
