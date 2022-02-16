using GXPEngine;
using System;

class ScreenShaker : GameObject
{
    float shakeTimer;
    float shakeIntensity;
    Scene activeScene;
    float oldX;
    float oldY;

    public ScreenShaker(float pShakeTimer, float pShakeIntensity, Scene pActiveScene) : base(false)
    {
        activeScene = pActiveScene;
        shakeTimer = pShakeTimer;
        shakeIntensity = pShakeIntensity;
        oldX = activeScene.x;
        oldY = activeScene.y;
    }

    void Update()
    {
        float clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);
        Random r = new Random();

        if (shakeTimer > 0)
        {
            shakeTimer -= clampedDeltaTime;
            activeScene.x += (float)((r.NextDouble() * 2 * shakeIntensity) - shakeIntensity);
            activeScene.y += (float)((r.NextDouble() * 2 * shakeIntensity) - shakeIntensity);
        }
        else
        {
            activeScene.x = oldX;
            activeScene.y = oldY;
            LateDestroy();
        }
    }
}
