using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class Vehicle : Sprite
{
    public int health;
    public Scene scene;

    public Vehicle(int iHealth,string fileName, Scene iScene) : base(fileName)
    {
        health = iHealth;
        scene = iScene;
        SetOrigin(width / 2, height / 2);
    }
    public virtual void Shoot()
    {

    }
    public virtual void Update()
    {
        GameObject[] collisions = GetCollisions();
        if(collisions.Length > 0)
        {
            foreach(GameObject gameObject in collisions)
            {
                if(gameObject is Asteroid)
                {
                    CreateAsteroidDestroyAnimation(((Asteroid)gameObject));

                    ScreenShaker screenShaker = new ScreenShaker(300, 3, (Scene)parent);
                    AddChild(screenShaker);

                    whenHit();

                    //Console.WriteLine("Collision!");
                    gameObject.LateDestroy();
                }
                if(gameObject is Bullet)
                {
                    Bullet bullet = (Bullet)gameObject;
                    if(bullet.shooter != this)
                    {
                        whenHit();
                        //Console.WriteLine("Collision!");
                        gameObject.LateDestroy();
                    }
                }
            }
        }
    }

    void CreateAsteroidDestroyAnimation(Asteroid pAsteroid)
    {
        if ((Scene)parent != null)
        {
            Emitter emitter = new Emitter("smoke.png", 1000, (Scene)parent, pAsteroid.speed);
            emitter.SetScale(0.02f, 0.025f, 0.001f).SetSpawnPosition(pAsteroid.x - 5, pAsteroid.x + 5, pAsteroid.y - 5, pAsteroid.y + 5).SetVelocity(0, 360, 0.02f, 0.03f).SetColors(0.12f, 0.5f, 0.12f, 0.8f);
            emitter.Emit(5);

            DestroyAnimation asteroidDestroyAnimation = new DestroyAnimation("Explosion.png", 5, 1, pAsteroid.speed, 3);
            parent.AddChildAt(asteroidDestroyAnimation, parent.GetChildCount());
            asteroidDestroyAnimation.SetXY(pAsteroid.x, pAsteroid.y);
        }
    }

    public virtual void whenHit()
    {
        health--;
        if (this is Player)
            scene.hud.UpdateHealth(-1);
        if (health <= 0)
        {
            if (!(this is Player))
            {
                Destroy();
            } 
        }
    }
}
