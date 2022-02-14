using GXPEngine;

public class DestroyAnimation : AnimationSprite
{
    //Class global
    bool alreadyCycledThrough = false;
    float speed;

    public DestroyAnimation(string pFilename, int pRow, int pCol, float pSpeed = 0f) : base(pFilename, pRow, pCol, -1, false, false)
    {
        SetOrigin(width / 2, height / 2);
        SetCycle(0, 5);
        scale = 3f;
        speed = pSpeed;
    }

    void Update()
    {
        int clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);

        x -= speed * clampedDeltaTime;

        if (currentFrame == 1) //If passed first animation frame, remember that
        {
            alreadyCycledThrough = true;
        }
        else if (currentFrame == 0 && alreadyCycledThrough) //If new animation cycle starts, destroy
        {
            LateDestroy();
        }
        Animate(0.01f * clampedDeltaTime);
    }
}
