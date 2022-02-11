using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class CoreParameters
{
    public const float maxTimeBetweenAsteroids = 10000;
    public const float minTimeBetweenAsteroids = 1000;

    public const float minSpeedAsteroids = .5f;
    public const float maxSpeedAsteroids = 15;
    public const float minSpeedFromScore = 0;
    public const float maxSpeedFromScore = 5;

    public const float minSpawnXAsteroids = 1000;
    public const float maxSpawnXAsteroids = 2500;

    public const float minDistanceToOther = 350;
    public const float maxDistanceToOther = 1000;

    public const float scoreInterval = 200;

    public const float bossScoreInterval = 30000;
    public const float bossMainGunInterval = 500;
    public const float bossSideGunInterval = 400;

    public const float bulletSpeed = .3f;

    public const float playerFireSpeed = 1000f;
    public const float maxTimeBetweenFuel = 10000f;
}
