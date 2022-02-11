﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Vehicle : Sprite
{
    public int health;
    public Scene scene;
    public Vehicle(int iHealth,string fileName,Scene iScene) : base(fileName)
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
                    whenHit();
                    Console.WriteLine("Collision!");
                    gameObject.LateDestroy();
                }
                if(gameObject is Bullet)
                {
                    Bullet bullet = (Bullet)gameObject;
                    if(bullet.shooter != this)
                    {
                        whenHit();
                        Console.WriteLine("Collision!");
                        gameObject.LateDestroy();
                    }
                }
            }
        }
    }
    public virtual void whenHit()
    {
        health--;
        if (health <= 0)
        {
            if (this is Player)
                scene.playerAlive = false;
            Destroy();
        }
    }
}
