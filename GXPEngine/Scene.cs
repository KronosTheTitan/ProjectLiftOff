using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Scene : GameObject
{
    Player player;
    ScenePivot scenePivot;
    Scene()
    {
        scenePivot = new ScenePivot();
    }
}
