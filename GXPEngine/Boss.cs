using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Boss : Vehicle
{
    float speed = .3f;
    float lastShotMain;
    float lastShot;
    public Boss(Scene iScene, string fileName = "triangle.png") : base(10, fileName, iScene)
    {
        y = 300;
        x = 2000;
        SetScaleXY(6, 3);
        rotation = 270;
    }

    public override void Update()
    {
        if(x > 700)
        {
            x -= speed * Time.deltaTime;
            //Console.WriteLine(x);
        }
        else
        {
            Shoot();
        }
        base.Update();
    }
    public override void whenHit()
    {
        health--;
        if (health <= 0)
        {
            ExtraHealth Ehealth = new ExtraHealth("star.png", scene);
            scene.AddChild(Ehealth);
            Ehealth.x = x;
            Ehealth.y = y;
            scene.BossFightEnd();
            Destroy();
        }
    }
    public override void Shoot()
    {
        if(Time.time > lastShotMain + CoreParameters.bossMainGunInterval)
        {
            float dirToPlayer = Mathf.Atan2(scene.player.y - y, scene.player.x - x) * 180 / (Mathf.PI);
            scene.AddChild(new Bullet(x, y, dirToPlayer, this));
            lastShotMain = Time.time;
        }
        if(Time.time > lastShot + CoreParameters.bossSideGunInterval)
        {
            scene.AddChild(new Bullet(x, y+width/2, 180, this));
            scene.AddChild(new Bullet(x, y-width/2, 180, this));
            lastShot = Time.time;

        }
    }

}