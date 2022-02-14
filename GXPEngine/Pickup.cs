using GXPEngine;

class Pickup : AnimationSprite
{
    public enum Type
    {
        Health,
        Fuel
    }

    Type type;
    Hud hud;
    Scene activeScene;

    float animationSpeed;
    float moveSpeed;

    public Pickup(string pFileName, int pCol, Type pType, Hud pHud, Scene pActiceScene) : base(pFileName, pCol, 1)
    {
        SetOrigin(width / 2, height / 2);
        SetCycle(0, pCol);
        scale = CoreParameters.pickupScale;
        collider.isTrigger = true;

        hud = pHud;
        type = pType;
        activeScene = pActiceScene;
        animationSpeed = CoreParameters.pickupAnimationSpeed;
        moveSpeed = CoreParameters.pickupMoveSpeed;
    }

    void Update()
    {
        int clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);

        if (HitTest(activeScene.player)) //If hits player, do stuff based on pickiptype
        {
            switch (type)
            {
                case Type.Health:
                    hud.UpdateHealth(CoreParameters.pickupHealBoost); //Heal 1
                    break;
                case Type.Fuel:
                    hud.UpdateFuelbar(CoreParameters.pickupFuelBoost); //Add 30 to fuel
                    break;
            }
            LateDestroy();
        }

        Move(-moveSpeed * clampedDeltaTime, 0); //Move pickup

        if (x + activeScene.x < 0)
        {
            LateDestroy(); //Pickup is offscreen
        }
        
        Animate(animationSpeed * clampedDeltaTime);
    }
}
