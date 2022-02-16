using GXPEngine;
using System;

public class Emitter : GameObject
{
    //Object references, passed by Constructor
    Scene activeScene;

    //Class globals
    string filename;
    float lifeTime;
    float moveLeftSpeed;

    float minAngle;
    float maxAngle;
    float minSpeed;
    float maxSpeed;

    float startScaleMin;
    float startScaleMax;
    float scaleSteps;

    float minX;
    float maxX;
    float minY;
    float maxY;

    float alpha;
    float colorR;
    float colorG;
    float colorB;

    public Emitter(string pFilename, float pLifeTime, Scene pActiveScene, float pMoveLeftSpeed = 0) : base(false)
    {
        activeScene = pActiveScene;
        filename = pFilename;
        lifeTime = pLifeTime;
        moveLeftSpeed = pMoveLeftSpeed;
    }

    public void Emit(int pParticleNumber) //Emit a set number of particles with pre randomized attributes in a pre determined range
    {
        if (activeScene != null)
        {
            Random r = new Random();

            for (int i = 0; i < pParticleNumber; i++)
            {

                float posX = (float)(r.NextDouble() * (maxX - minX) + minX);
                float posY = (float)(r.NextDouble() * (maxY - minY) + minY);
                float angle = (float)(r.NextDouble() * (maxAngle - minAngle) + minAngle);
                float speed = (float)(r.NextDouble() * (maxSpeed - minSpeed) + minSpeed);
                float randomizerScale = (float)(r.NextDouble() * (startScaleMax - startScaleMin) + startScaleMin);

                Particle particle = new Particle(filename, scaleSteps, angle, speed, lifeTime, activeScene, colorR, colorG, colorB, alpha, moveLeftSpeed);
                particle.scale = startScaleMin + randomizerScale; 
                particle.SetXY(posX, posY);
                activeScene.AddChildAt(particle, activeScene.GetChildCount());
            }
        }
    }

    public Emitter SetVelocity(float pMinAngle, float pMaxAngle, float pMinSpeed, float pMaxSpeed)
    {
        minSpeed = pMinSpeed;
        maxSpeed = pMaxSpeed;
        minAngle = pMinAngle;
        maxAngle = pMaxAngle;
        return this;
    }

    public Emitter SetSpawnPosition(float pMinX, float pMaxX, float pMinY, float pMaxY)
    {
        minX = pMinX;
        maxX = pMaxX;
        minY = pMinY;
        maxY = pMaxY;
        return this;
    }

    public Emitter SetScale(float pStartScaleMin, float pStartScaleMax, float pScaleSteps)
    {
        startScaleMin = pStartScaleMin;
        startScaleMax = pStartScaleMax;
        scaleSteps = pScaleSteps;
        return this;
    }

    public Emitter SetColors(float pColorR, float pColorG, float pColorB, float pAlpha)
    {
        colorR = pColorR;
        colorG = pColorG;
        colorB = pColorB;
        alpha = pAlpha;
        return this;
    }
}
