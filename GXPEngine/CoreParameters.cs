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
  
    public const float scoreInterval = 200;

    public const float minDistanceToOther = 200;
    public const float maxDistanceToOther = 500;

    public const float bossScoreInterval = 30000;
    public const float bossMainGunInterval = 500;
    public const float bossSideGunInterval = 400;

    public const float bulletSpeed = .3f;

    public const float playerFireSpeed = 1000f;
    public const float maxTimeBetweenFuel = 10000f;

    //Font
    public const string fontPath = "font.otf";

    //Hud
    //Health
    public const int hudHealthWidth = 60;
    public const int hudHealthHeight = 10;
    public const float hudHealthPosX = 10f;
    public const float hudHealthPosY = 30f;
    public const float hudHealthScale = 2f;

    //Fuel
    public const int hudFuelWidth = 60;
    public const int hudFuelHeight = 10;
    public const float hudFuelPosX = 15f;
    public const float hudFuelPosY = 80f;
    public const float hudFuelbarLooseOverTime = 0.01f;
    public const float hudFuelbarScale = 0.5f;

    //Score
    public const int hudScoreWidth = 100;
    public const int hudScoreHeight = 20;
    public const float hudScorePosX = 650f;
    public const float hudScorePosY = 20f;
    public const float hudScoreFontSize = 20f;

    //Pickup
    public const float pickupScale = 2f;
    public const float pickupMoveSpeed = 0.2f;
    public const int pickupHealBoost = 1;
    public const int pickupFuelBoost = 50;
    public const float pickupAnimationSpeed = 0.015f;

    //Scoreboard
    public const int playerNameMaxLength = 15;

    //Title
    public const int scoreBoardTitleWidth = 400;
    public const int scoreBoardTitleHeight = 50;
    public const float scoreBoardTitlePosX = 400f;
    public const float scoreBoardTitlePosY = 50f;
    public const float scoreBoardTitleFontSize = 30f;

    //Table
    public const int scoreTableCellWidth = 300;
    public const int scoreTableCellHeight = 50;
    public const int scoreTableNumOfRows = 6;
    public const float scoreTablePosY = 100f;
    public const float scoreTablePosX = 300f;
    public const float scoreTableFontSize = 20f;

    //SavefileName
    public const string savefileName = "SaveGame.txt";

    //StartBtn
    public const int scoreBoardStartBtnWidth = 200;
    public const int scoreBoardStartBtnHeight = 100;
    public const float scoreBoardStartBtnPosX = 500f;
    public const float scoreBoardStartBtnPosY = 500f;
    public const float scoreBoardStartBtnFontSize = 15f;

    //NewPlayerrBtn
    public const int scoreBoardNewPlayerrBtnWidth = 330;
    public const int scoreBoardNewPlayerrBtnHeight = 100;
    public const float scoreBoardNewPlayerrBtnPosX = 200f;
    public const float scoreBoardNewPlayerrBtnPosY = 500f;
    public const float scoreBoardNewPlayerrBtnFontSize = 15f;
}
