using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Projectile : Sprite
{
    float speed;
    public Projectile(string filename,float iSpeed,float iX,float iY,float direction) : base(filename)
    {
        speed = iSpeed;
        x = iX;
        y = iY;
        rotation = direction;
    }
    void Update()
    {
        Move(speed, 0);
    }
    public virtual void OnHit()
    {

    }
}
