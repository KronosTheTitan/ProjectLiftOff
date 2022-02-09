using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Player : Vehicle
{
    float speed = 1;
    public Player(int iHealth,string filename) : base(iHealth,filename)
    {
        rotation = 90;
    }
    void Update()
    {
        MovePlayer();
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
