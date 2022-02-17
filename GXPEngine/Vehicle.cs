using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Vehicle : Sprite
{
    public int health;
    public Scene scene;

    protected Sound shootSound;
    protected Sound hitSound;

    public Vehicle(int iHealth, string fileName, Scene iScene) : base(fileName)
    {
        health = iHealth;
        scene = iScene;
        SetOrigin(width / 2, height / 2);
        shootSound = new Sound(CoreParameters.soundPath + "shot.wav");
        hitSound = new Sound(CoreParameters.soundPath + "explosion.wav");
    }
    public virtual void Shoot()
    {

    }
    public virtual void Update()
    {
        GameObject[] collisions = GetCollisions();
        if (collisions.Length > 0)
        {
            foreach (GameObject gameObject in collisions)
            {
                if (gameObject is Asteroid && scene != null && this is Player)
                {
                    ScreenShaker screenShaker = new ScreenShaker(300, 3, (Scene)parent);
                    AddChild(screenShaker);

                    whenHit();

                    //Console.WriteLine("Collision!");
                    gameObject.LateDestroy();
                }
                if (gameObject is Bullet)
                {
                    Bullet bullet = (Bullet)gameObject;
                    if (bullet.shooter != this)
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
        {
            scene.hud.UpdateHealth();
            hitSound.Play();
        }
        else if (health <= 0)
        {
            LateDestroy();
        }
    }
}
