using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Player : Vehicle
{
    float speed = .75f;
    float lastShot;
    public float lastFuel = 0;
    public Player(int iHealth,string filename,Scene scene) : base(iHealth,filename,scene)
    {
        rotation = 90;
        health = 3;
        fuel = 60;
    }
    public override void Update()
    {
        MovePlayer();
        Shoot();
        UpdateFuel();
        base.Update();
    }

    void MovePlayer()
    {
        if (y > 0+ (width / 2) && Input.GetKey(Key.UP))
            y -= speed * Time.deltaTime;
        if (y < 600-(width/2) && Input.GetKey(Key.DOWN))
            y += speed * Time.deltaTime;
    }
    public override void Shoot()
    {
        if (Input.GetKey(Key.SPACE) && Time.time > lastShot + CoreParameters.playerFireSpeed)
        {
            //scene.AddChild(new Bullet(x, y, 0, this));
            //lastShot = Time.time;
        }
    }
    public void UpdateFuel()
    {
        if(Time.time > lastFuel + CoreParameters.maxTimeBetweenFuel)
        {
            health = 0;
            if (health <= 0)
            {
                if (this is Player)
                    scene.playerAlive = false;
                Destroy();
            }
        }

    }
}
