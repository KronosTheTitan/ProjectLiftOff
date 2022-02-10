using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Scene : GameObject
{
    Player player;
    ScenePivot scenePivot;
    Asteroid[] latestAsteroids = new Asteroid[3];
    float timeLastAsteroid = 0;
    int score = 0;
    public Scene()
    {
        player = new Player(5, "triangle.png");
        player.SetXY(100, 600 / 2);
        AddChild(player);
        scenePivot = new ScenePivot();
        AddChild(scenePivot);

        for(int i =0; i < latestAsteroids.Length; i++)
        {
            latestAsteroids[i] = new Asteroid(this, 1000, Utils.Random(0, 600));
        }

        AddChild(latestAsteroids[0]);
        AddChild(latestAsteroids[1]);
        AddChild(latestAsteroids[2]);
    }
    void Update()
    {
        SpawnAsteroid();
    }
    void SpawnAsteroid()
    {
        if (Time.time > timeLastAsteroid + (Mathf.Clamp(CoreParameters.maxTimeBetweenAsteroids - score, CoreParameters.minTimeBetweenAsteroids, CoreParameters.maxTimeBetweenAsteroids)))
             return;
        Asteroid asteroid = new Asteroid(this,Utils.Random(CoreParameters.minSpawnXAsteroids, CoreParameters.maxSpawnXAsteroids), player.y);
        foreach(Asteroid asteroid1 in latestAsteroids)
        {
            if(asteroid.DistanceTo(asteroid1) < CoreParameters.minDistanceToOther)
            {
                asteroid.Destroy();
                return;
            }
        }
        GameObject[] gameObject=asteroid.GetCollisions();
        if(gameObject.Length > 0)
        {
            asteroid.Destroy();
            return;
        }
        latestAsteroids[0] = latestAsteroids[1];
        latestAsteroids[1] = latestAsteroids[2];
        latestAsteroids[2] = asteroid;
        AddChild(asteroid);
        timeLastAsteroid = Time.time;
    }
}
