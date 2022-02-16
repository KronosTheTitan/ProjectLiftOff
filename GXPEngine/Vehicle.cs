using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Vehicle : Sprite
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
                if(gameObject is Asteroid && scene != null)
                {
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

    public virtual void whenHit()
    {
        health--;
        if (this is Player)
            scene.hud.UpdateHealth(-1);
        if (health <= 0)
        {
            if (!(this is Player))
            {
                LateDestroy();
            } 
        }
    }
}
