using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
