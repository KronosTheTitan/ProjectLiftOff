using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Bullet : Sprite
{
    public Vehicle shooter;

    public Bullet(float iX, float iY, float direction, Vehicle iShooter, string fileName = CoreParameters.playerPath + "laser.png") : base(fileName)
    {
        shooter = iShooter;
        SetOrigin(width / 2, height / 2);
        SetXY(iX + shooter.width / 2, iY);
        SetScaleXY(0.5f, 0.5f);
        rotation = direction;
    }
    void Update()
    {
        Move(CoreParameters.bulletSpeed * Time.deltaTime, 0);
        if (x < -10 || x > 810)
            Destroy();
    }
}
