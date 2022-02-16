using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class FuelTank : Pickup
{
    Scene scene;
    public FuelTank(Scene iScene, string filename = "square.png") : base(filename,iScene)
    {
        Console.WriteLine("attempt fuel spawn" + Time.time);
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
        Console.WriteLine("spawn succes");
    }
    public override void OnPickUp()
    {
        scene.player.lastFuel = Time.time;
    }
    public override void Delete()
    {
        scene.fuelTanks.Remove(this);
        scene.RemoveChild(this);
        Console.WriteLine("deleted fuel tank" + scene.fuelTanks.Count);
        LateDestroy();
    }
}