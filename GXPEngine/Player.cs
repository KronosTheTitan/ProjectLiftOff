using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Player : Vehicle
{
    float speed = .75f;
    float lastShot;
    public int health;
    public int fuel;
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
            scene.AddChild(new Bullet(x, y, 0, this));
            lastShot = Time.time;
        }
    }
}
