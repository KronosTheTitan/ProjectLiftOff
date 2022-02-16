using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class BossAnimation : AnimationSprite
{
    public BossAnimation(string fileName = "boss.png") : base(fileName, 8, 1, -1, false, false)
    {
        SetOrigin(width / 2, height / 2);
        SetCycle(0, 8);
    }

    void Update()
    {
        int clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);
        Animate(CoreParameters.playerAnimationSpeed * clampedDeltaTime);
    }
}