using GXPEngine;
using System;

class Particle : Sprite
{
    //Object reference passed by constructor
    Scene activeScene;

    //Class globals
    float lifeTime;
    float scaleSteps;
    float speed;
    float moveLeftSpeed;

    public Particle(string pFileName, float pScaleSteps, float pAngle, float pSpeed, float pLifeTime, Scene pActiveScene, float pColorR, float pColorG, float pColorB, float pAlpha, float pMoveLeftSpeed) : base(pFileName, false, false)
    {
        SetOrigin(width / 2, height / 2);
        activeScene = pActiveScene;
        speed = pSpeed;
        scaleSteps = pScaleSteps;
        lifeTime = pLifeTime;
        moveLeftSpeed = pMoveLeftSpeed;
        alpha = pAlpha;
        Turn(pAngle);
        SetColor(pColorR, pColorG, pColorB);
    }

    void Update()
    {
        float clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);

        lifeTime -= clampedDeltaTime;
        if (lifeTime <= 0)
        {
            LateDestroy(); //Destroy after life time endet
        }

        scale += scaleSteps; //Change scale

        Move(speed * clampedDeltaTime, 0);
        if (activeScene.playerAlive)
        {
            x -= moveLeftSpeed * clampedDeltaTime;
        }
    }
}
