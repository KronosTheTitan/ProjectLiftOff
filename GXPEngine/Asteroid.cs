using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Asteroid : Sprite
{    
    public enum Type
    {
        Normal,
        Bundle,
        Small
    }

    Type type; 

    public float speed;
    public bool playDestroyAnimation = true;

    Scene scene;

    public Asteroid(Scene iScene, float iX, float iY, Type pType) : base(pType == Type.Bundle ? "bundle.png" : "toxic_waste.png")
    {
        speed = .5f;
        scene = iScene;
        x = iX;
        y = iY;
        type = pType;
        SetOrigin(width / 2, height / 2);
        //Console.WriteLine("new asteroid");
    }

    void Update()
    {
        if (scene.playerAlive)
        {
            x -= speed * Time.deltaTime;

            if (x < -10)
            {
                //Console.WriteLine("offscreen");
                playDestroyAnimation = false;
                Delete();
            }
        }

        if (type == Type.Small)
        {
            Move(0.05f * Time.deltaTime, 0);
        }

        CollisionBullet();
    }
    
    void SpawnSmallAsteroids()
    {
        Emitter emitter = new Emitter("smoke.png", 1000, scene, speed);
        emitter.SetScale(0.02f, 0.025f, 0.001f).SetSpawnPosition(x - 30, x +30, y - 30, y + 30).SetVelocity(0, 360, 0.02f, 0.03f).SetColors(0.12f, 0.5f, 0.12f, 0.8f);
        emitter.Emit(8);

        Asteroid asteroid = new Asteroid(scene, x + 10, y + 10, Type.Small);
        asteroid.Turn(135);
        scene.AddChildAt(asteroid, scene.GetChildCount());
        asteroid = new Asteroid(scene, scene.x + x - 10, scene.y + y - 10, Type.Small);
        asteroid.Turn(320);
        scene.AddChildAt(asteroid, scene.GetChildCount());
        asteroid = new Asteroid(scene, scene.x + x - 10, scene.y + y + 10, Type.Small);
        asteroid.Turn(225);
        scene.AddChildAt(asteroid, scene.GetChildCount());
        asteroid = new Asteroid(scene, scene.x + x + 10, scene.y + y - 10, Type.Small);
        asteroid.Turn(45);
        scene.AddChildAt(asteroid, scene.GetChildCount());
    }

    void CollisionBullet()
    {
        List<Bullet> hitBullets = new List<Bullet>();

        foreach (Bullet bullet in scene.playerBullets)
        {
            if (HitTest(bullet))
            {
                hitBullets.Add(bullet);
                Delete();
            }
        }

        foreach (Bullet bullet in hitBullets)
        {
            scene.playerBullets.Remove(bullet);
            bullet.Destroy();
        }
        hitBullets.Clear();
    }

    protected override void OnDestroy()
    {
        if (scene != null && playDestroyAnimation)
        {
            Emitter emitter = new Emitter("smoke.png", 1000, scene, speed);
            emitter.SetScale(0.02f, 0.025f, 0.001f).SetSpawnPosition(x - 5, x + 5, y - 5, y + 5).SetVelocity(0, 360, 0.02f, 0.03f).SetColors(0.12f, 0.5f, 0.12f, 0.8f);
            emitter.Emit(5);

            DestroyAnimation asteroidDestroyAnimation = new DestroyAnimation("Explosion.png", 5, 1, speed, 3);
            scene.AddChildAt(asteroidDestroyAnimation, scene.GetChildCount());
            asteroidDestroyAnimation.SetXY(x, y);
        }
    }

    void Delete()
    {
        if (type == Type.Bundle)
        {
            SpawnSmallAsteroids();
        }
        scene.RemoveChild(this);
        Destroy();
    }
}
