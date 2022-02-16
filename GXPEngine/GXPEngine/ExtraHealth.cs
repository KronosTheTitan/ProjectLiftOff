using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class ExtraHealth : Pickup
{
    Scene scene;
    public ExtraHealth(string fileName,Scene iScene) : base(fileName, iScene)
    {
        scene = iScene;
    }
    public override void OnPickUp()
    {
        scene.player.health++;
        scene.hud.UpdateHealth(1);
    }
    public override void Delete()
    {
        LateDestroy();
    }
}
