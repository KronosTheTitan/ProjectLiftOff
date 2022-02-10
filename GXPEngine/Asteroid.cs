using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Asteroid : Sprite
{
    float speed;
    Scene scene;
    public Asteroid(Scene iScene,float iX,float iY, string filename = "circle.png") : base(filename)
    {
        speed = .5f;
        scene = iScene;
        x = iX;
        y = iY;
        SetOrigin(width / 2, height / 2);
    }
    void Update()
    {
        x -= speed * Time.deltaTime;
        if (x < -10)
            Delete();

    }
    public virtual void OnHit(GameObject contact)
    {

    }
    void Delete()
    {
        scene.RemoveChild(this);
        Destroy();
    }
}
