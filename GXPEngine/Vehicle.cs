using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Vehicle : Sprite
{
    int health;
    public Vehicle(int iHealth,string fileName) : base(fileName)
    {
        health = iHealth;
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
            }
        }
    }
    public virtual void whenHit()
    {
        health--;
        if (health <= 0)
            Destroy();
    }
}
