using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Player : Vehicle
{
    float speed = 1f;
    public Player(int iHealth,string filename) : base(iHealth,filename)
    {
        rotation = 90;
    }
    public override void Update()
    {
        MovePlayer();
        base.Update();
    }

    void MovePlayer()
    {
        if (y > 0+ (width / 2) && Input.GetKey(Key.UP))
            y -= speed * Time.deltaTime;
        if (y < 600-(width/2) && Input.GetKey(Key.DOWN))
            y += speed * Time.deltaTime;
    }
    void Shoot()
    {
        
    }
}
