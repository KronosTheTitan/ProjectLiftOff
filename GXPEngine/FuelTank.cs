using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class FuelTank : Sprite
{
    float speed;
    Scene scene;
    public FuelTank(Scene iScene, string filename = "square.png") : base(filename)
    {
        //Console.WriteLine("attempt fuel spawn" + Time.time);
        speed = .5f;
        scene = iScene;
        scene.AddChild(this);
        scene.fuelTanks.Add(this);
        SetOrigin(width / 2, height / 2);
        x = 1000;
        y = Utils.Random(0 + width / 2, 600 - width / 2);
        GameObject[] gameObject = GetCollisions();
        if (gameObject.Length > 0)
        {
            Delete();
            return;
        }
        //Console.WriteLine("spawn succes");
    }
    void Update()
    {
        if (scene.playerAlive)
        {
            x -= speed * Time.deltaTime;
            if (x < -10)
                Delete();
        }
    }
    public void Delete()
    {
        scene.fuelTanks.Remove(this);
        scene.RemoveChild(this);
        //Console.WriteLine("deleted fuel tank" + scene.fuelTanks.Count);
        LateDestroy();
    }
}