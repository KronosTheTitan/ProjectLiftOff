using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class Scene : GameObject
{
    public Player player;
    public int score = 0;
    public bool playerAlive = true;
    public Hud hud;
    public List<FuelTank> fuelTanks = new List<FuelTank>();

    public List<Bullet> playerBullets = new List<Bullet>();
    ScenePivot scenePivot;
    Asteroid[] latestAsteroids = new Asteroid[3];
    DestroyAnimation playerDestroyAnimation;
    float timeLastAsteroid = 0;
    float lastScore = CoreParameters.scoreInterval;
    float lastBoss = 0;
    bool bossFight = false;

    public Scene()
    {
        player = new Player(3, "triangle.png", this);
        player.SetXY(100, 600 / 2);
        AddChild(player);
        scenePivot = new ScenePivot();
        AddChild(scenePivot);
        timeLastAsteroid = Time.time;

        for (int i = 0; i < latestAsteroids.Length; i++)
        {
            latestAsteroids[i] = new Asteroid(this, 1000, Utils.Random(0, 600), Asteroid.Type.Normal);
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
            if (fuelTanks.Count < 3)
            {
                FuelTank fuel = new FuelTank(this);
            }
        }
        else
        {
            player.lastFuel = Time.time;
        }
    }
    void SpawnAsteroid()
    {
        if (Time.time > timeLastAsteroid + Mathf.Clamp(CoreParameters.maxTimeBetweenAsteroids - score, CoreParameters.minTimeBetweenAsteroids, CoreParameters.maxTimeBetweenAsteroids))
             return;
        //Console.WriteLine("attempt spawn");
        Asteroid asteroid = new Asteroid(this,Utils.Random(CoreParameters.minSpawnXAsteroids, CoreParameters.maxSpawnXAsteroids), player.y, Asteroid.Type.Bundle);
        foreach(Asteroid asteroid1 in latestAsteroids)
        {
            if (asteroid.DistanceTo(asteroid1) < Mathf.Clamp(CoreParameters.maxDistanceToOther - score, CoreParameters.minDistanceToOther, CoreParameters.maxDistanceToOther))
            {
                asteroid.playDestroyAnimation = false;
                asteroid.Destroy();
                return;
            }
        }
        GameObject[] gameObject = asteroid.GetCollisions();
        if (gameObject.Length > 0)
        {
            asteroid.playDestroyAnimation = false;
            asteroid.Destroy();
            return;
        }
        latestAsteroids[0] = latestAsteroids[1];
        latestAsteroids[1] = latestAsteroids[2];
        latestAsteroids[2] = asteroid;
        AddChild(asteroid);
        timeLastAsteroid = Time.time;
    }

    void CheckForPlayerDeath()
    {
        if (player.health <= 0 && playerDestroyAnimation == null)
        {
            player.Destroy();
            playerDestroyAnimation = new DestroyAnimation(CoreParameters.playerPath + "death.png", 8, 1, 0, 8);
            AddChildAt(playerDestroyAnimation, GetChildCount());
            playerDestroyAnimation.SetXY(player.x, player.y);
            playerAlive = false;
            Emitter emitter = new Emitter("smoke.png", 500, this, 0);
            emitter.SetScale(0.03f, 0.035f, 0.001f).SetSpawnPosition(x - 20, x + 20, y - 20, y + 20).SetVelocity(0, 360, 0.02f, 0.03f).SetColors(0.2f, 0.2f, 0.2f, 0.8f);
            emitter.Emit(4);
        }
        else if (playerDestroyAnimation != null && playerDestroyAnimation.isOver)
        {
            LateDestroy();
        }
    }

    void UpdateScore()
    {
        if (playerAlive && Time.time > lastScore + CoreParameters.scoreInterval)
        {
            lastScore = Time.time;
            score++;
            hud.UpdateScore(1);
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
            latestAsteroids[i] = new Asteroid(this, 1000, Utils.Random(0, 600), Asteroid.Type.Normal);
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
