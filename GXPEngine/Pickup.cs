using GXPEngine;

class Pickup : Sprite
{
    Scene activeScene;

    float moveSpeed = 0f;

    public Pickup(string pFileName, Scene iScene) : base(pFileName)
    {
        activeScene = iScene;
        moveSpeed = 0.5f;
    }

    public void Update()
    {
        int clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);

        if (HitTest(activeScene.player)) //If hits player, do stuff based on pickiptype
        {
            OnPickUp();
            Delete();
        }
        if(activeScene.playerAlive)
            x -= moveSpeed * Time.deltaTime; //Move pickup

        if (x + activeScene.x < 0)
        {
            Delete();
        }
    }
    public virtual void OnPickUp()
    {

    }
    public virtual void Delete()
    {

    }
}
