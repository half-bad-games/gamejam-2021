using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class SpearDecorator : Decorator
{
    public SpearDecorator() { }

    public SpearDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.damage += 10;
        return this.currentComponent;
    }
}

class FishTailDecorator : Decorator
{
    public FishTailDecorator() { }

    public FishTailDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.speed += 20;
        return this.currentComponent;
    }
}

class TapedSwordDecorator : Decorator
{
    public TapedSwordDecorator() { }

    public TapedSwordDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.damage += 20;
        return this.currentComponent;
    }
}

class LightSaberDecorator : Decorator
{
    public LightSaberDecorator() { }

    public LightSaberDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.damage += 100;
        return this.currentComponent;
    }
}

class EyesDecorator : Decorator
{
    public EyesDecorator() { }

    public EyesDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.vision += 2;
        return this.currentComponent;
    }
}

class WhiskersDecorator : Decorator
{
    public WhiskersDecorator() { }

    public WhiskersDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.agility += 5;
        return this.currentComponent;
    }
}

class ThirdEyeDecorator : Decorator
{
    public ThirdEyeDecorator() { }

    public ThirdEyeDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.vision += 3;
        return this.currentComponent;
    }
}

class SlimeDecorator : Decorator
{
    public SlimeDecorator() { }

    public SlimeDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.speed += 10;
        return this.currentComponent;
    }
}

class ForceFieldDecorator : Decorator
{
    public ForceFieldDecorator() { }

    public ForceFieldDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.health += 100;
        return this.currentComponent;
    }
}

class BasicFinsDecorator : Decorator
{
    public BasicFinsDecorator() { }

    public BasicFinsDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.speed += 5;
        return this.currentComponent;
    }
}

class MouthDecorator : Decorator
{
    public MouthDecorator() { }

    public MouthDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.damage += 5;
        return this.currentComponent;
    }
}

class SpikesDecorator : Decorator
{
    public SpikesDecorator() { }

    public SpikesDecorator(Component component) : base(component) { }

    public override Playable add()
    {
        this.currentComponent.stats.damage += 5;
        this.currentComponent.stats.health += 10;
        return this.currentComponent;
    }
}
