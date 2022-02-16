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
    BossAnimation bossAnimation;

    public Boss(Scene iScene, string fileName = "bossBase.png") : base(10, fileName, iScene)
    {
        alpha = 0;
        scale = 4f;
        y = (game.height / game.scaleY) / 2;
        x = game.width / game.scaleX;
        bossAnimation = new BossAnimation();
        AddChild(bossAnimation);
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
            Pickup health = new Pickup(scene, Pickup.Type.Health);
            scene.AddChild(health);
            health.x = x;
            health.y = y;
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
            scene.AddChild(new Asteroid(scene, x, y+width/2, Asteroid.Type.Normal));
            scene.AddChild(new Asteroid(scene, x, y-width/2, Asteroid.Type.Normal));
            lastShot = Time.time;
        }
    }

    protected override void OnDestroy()
    {
        Emitter emitter = new Emitter("smoke.png", 1500, scene, speed);
        emitter.SetScale(0.02f, 0.025f, 0.002f).SetSpawnPosition(x - 100, x + 100, y - 100, y + 100).SetVelocity(0, 360, 0.02f, 0.04f).SetColors(0.12f, 0.5f, 0.12f, 0.8f);
        emitter.Emit(10);
    }
}