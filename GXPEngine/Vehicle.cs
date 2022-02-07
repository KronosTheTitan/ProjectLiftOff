using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Vehicle : Sprite
{
    public Vehicle(string fileName) : base(fileName)
    {

    }
    public virtual void Shoot()
    {

    }
    public virtual void whenHit()
    {

    }
    void OnCollision(GameObject other)
    {
        if(other is Projectile)
        {
            Projectile projectile = (Projectile)other;
            projectile.OnHit();
        }
    }
}
