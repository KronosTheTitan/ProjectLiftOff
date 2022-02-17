using GXPEngine;

class DestroyAnimation : AnimationSprite
{
    //Interface
    bool _isOver = false;

    //Class global
    bool alreadyCycledThrough = false;
    float speed;

    public DestroyAnimation(string pFilename, int pRow, int pCol, float pSpeed = 0f, float pScale = 1) : base(pFilename, pRow, pCol, -1, false, false)
    {
        SetOrigin(width / 2, height / 2);
        SetCycle(0, 5);
        scale = pScale;
        speed = pSpeed;
    }

    //Interface
    public bool isOver
    {
        get => _isOver;
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
            _isOver = true;
        }
        Animate(0.01f * clampedDeltaTime);
    }
}
