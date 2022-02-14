using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Scene : GameObject
{
    public Player player;
    ScenePivot scenePivot;
    Asteroid[] latestAsteroids = new Asteroid[3];
    float timeLastAsteroid = 0;
    float lastScore = CoreParameters.scoreInterval;
    int score = 0;
    public bool playerAlive = true;
    float lastBoss = 0;
    bool bossFight = false;
    public Scene()
    {
        player = new Player(3, "triangle.png",this);
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
        if (!bossFight)
        {
            SpawnAsteroid();
            if (Time.time > lastBoss + CoreParameters.bossScoreInterval)
            {
                BossFightStart();
            }
            UpdateScore();
        }

        if (!playerAlive)
        {
            LateDestroy();
        }
    }
    void SpawnAsteroid()
    {
        if (Time.time > timeLastAsteroid + Mathf.Clamp(CoreParameters.maxTimeBetweenAsteroids - score, CoreParameters.minTimeBetweenAsteroids, CoreParameters.maxTimeBetweenAsteroids))
             return;
        ///Console.WriteLine("attempt spawn");
        Asteroid asteroid = new Asteroid(this,Utils.Random(CoreParameters.minSpawnXAsteroids, CoreParameters.maxSpawnXAsteroids), player.y);
        foreach(Asteroid asteroid1 in latestAsteroids)
        {
            if(asteroid.DistanceTo(asteroid1) < Mathf.Clamp(CoreParameters.maxDistanceToOther-score, CoreParameters.minDistanceToOther,CoreParameters.maxDistanceToOther))
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
    void UpdateScore()
    {
        if (!playerAlive)
            return;
        if (Time.time > lastScore + CoreParameters.scoreInterval)
        {
            lastScore = Time.time;
            score++;
        }
    }
    void BossFightStart()
    {
        bossFight = true;
        AddChild(new Boss(this));
    }
    public void BossFightEnd()
    {
        bossFight = false;
        lastBoss = Time.time;
        timeLastAsteroid = Time.time;

        for (int i = 0; i < latestAsteroids.Length; i++)
        {
            latestAsteroids[i] = new Asteroid(this, 1000, Utils.Random(0, 600));
        }

        AddChild(latestAsteroids[0]);
        AddChild(latestAsteroids[1]);
        AddChild(latestAsteroids[2]);
    }

    protected override void OnDestroy()
    {
        ((MyGame)game).LoadScoreBoard();
    }
}
