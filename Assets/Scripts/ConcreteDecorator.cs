using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
