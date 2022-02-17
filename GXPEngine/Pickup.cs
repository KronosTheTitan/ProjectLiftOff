using GXPEngine;

class Pickup : Sprite
{
    public enum Type
    {
        Fuel,
        Health
    }

    Type type;

    Scene activeScene;

    float moveSpeed = 0f;
    Sound pickupSound;

    public Pickup(Scene pScene, Type pType) : base(pType == Type.Fuel ? "fuel.png" : "health.png")
    {
        activeScene = pScene;
        pickupSound = new Sound(CoreParameters.soundPath + "pickup.wav");
        moveSpeed = 0.5f;
        type = pType;
        System.Console.WriteLine(x);
    }

    public void Update()
    {
        int clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);

        if (HitTest(activeScene.player)) //If hits player, do stuff based on pickiptype
        {
            pickupSound.Play();
            switch (type)
            {
                case Type.Fuel:
                    activeScene.fuelTanks.Remove(this);
                    activeScene.hud.UpdateFuelbar(CoreParameters.pickupFuelBoost);
                    break;
                case Type.Health:
                    activeScene.player.health++;
                    activeScene.hud.UpdateHealth(1);
                    break;
            }
            Destroy();
        }

        if(activeScene.playerAlive)
            x -= moveSpeed * Time.deltaTime; //Move pickup

        if (x < 0)
        {
            activeScene.fuelTanks.Remove(this);
            Destroy();
        }
    }
}
