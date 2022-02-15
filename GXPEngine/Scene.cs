using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
public class Scene : GameObject
{
    public Hud hud;
    public List<FuelTank> fuelTanks = new List<FuelTank>();
    public Player player;
    public int score = 0;
    public bool playerAlive = true;
    public float lastFuel;

    ScenePivot scenePivot;
    Asteroid[] latestAsteroids = new Asteroid[3];
    DestroyAnimation playerDestroyAnimation;
    float timeLastAsteroid = 0;
    float lastScore = CoreParameters.scoreInterval;
    float lastBoss = 0;
    bool bossFight = false;

    public Hud hud;

    public List<Pickup> fuelTanks = new List<Pickup>();
    public Scene()
    {
        player = new Player(3, CoreParameters.playerPath+"base.png",this);
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
        CheckForPlayerDeath();
        if (!bossFight)
        {
            SpawnAsteroid();
            if (Time.time > lastBoss + CoreParameters.bossScoreInterval)
            {
                BossFightStart();
            }
            UpdateScore();
            if(fuelTanks.Count < 3)
            {
                Pickup fuel = new Pickup("square.png", 0, Pickup.Type.Fuel, hud, this);
                fuelTanks.Add(fuel);
            }
        }
        else
        {
            player.lastFuel = Time.time;
        }

        if (!playerAlive)
        {
            LateDestroy();
        }
    }
    void SpawnAsteroid()
    {
        if (Time.time > timeLastAsteroid + Mathf.Clamp(CoreParameters.maxTimeBetweenAsteroids - (score / CoreParameters.scoreImpactOnDifficulty), CoreParameters.minTimeBetweenAsteroids, CoreParameters.maxTimeBetweenAsteroids))
             return;
        ///Console.WriteLine("attempt spawn");
        Asteroid asteroid = new Asteroid(this,Utils.Random(CoreParameters.minSpawnXAsteroids, CoreParameters.maxSpawnXAsteroids), player.y);
        foreach(Asteroid asteroid1 in latestAsteroids)
        {
            if(asteroid.DistanceTo(asteroid1) < Mathf.Clamp(CoreParameters.maxDistanceToOther-(score/CoreParameters.scoreImpactOnDifficulty), CoreParameters.minDistanceToOther,CoreParameters.maxDistanceToOther))
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
    void CheckForPlayerDeath()
    {
        if (player.health <= 0 && playerDestroyAnimation == null)
        {
            player.Destroy();
            playerDestroyAnimation = new DestroyAnimation(CoreParameters.playerPath + "death.png", 8, 1, 0, 8);
            AddChildAt(playerDestroyAnimation, GetChildCount());
            playerDestroyAnimation.SetXY(player.x, player.y);
        }
        else if (playerDestroyAnimation != null && playerDestroyAnimation.isOver)
        {
            playerAlive = false;
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
