using System;
using System.Collections.Generic;
using GXPEngine;
class Bullet : Sprite
{
    public Vehicle shooter;

    Scene scene;

    public Bullet(float iX, float iY, float direction, Vehicle iShooter, string fileName = CoreParameters.playerPath + "laser.png", Scene pScene = null) : base(fileName)
    {
        shooter = iShooter;
        SetOrigin(width / 2, height / 2);
        SetXY(iX + shooter.width / 2, iY);
        SetScaleXY(0.5f, 0.5f);
        rotation = direction;
        scene = pScene;
        Console.WriteLine("fired shot");
    }
    public void Update()
    {
        Move(CoreParameters.bulletSpeed * Time.deltaTime, 0);

        if (x < -10 || x > game.width)
        {
            if (scene != null && scene.playerBullets.Contains(this))
            {
                scene.playerBullets.Remove(this);
            }
            Destroy();
        }
    }
}
