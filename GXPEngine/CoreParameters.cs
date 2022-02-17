using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class CoreParameters
{
    public const string playerPath = "player/";
    public const string soundPath = "sounds/";

    public const float maxTimeBetweenAsteroids = 10000;
    public const float minTimeBetweenAsteroids = 1000;

    public const float minSpeedAsteroids = .5f;
    public const float maxSpeedAsteroids = 5;
    public const float minSpeedFromScore = 0;
    public const float maxSpeedFromScore = 5;

    public const float minSpawnXAsteroids = 1000;
    public const float maxSpawnXAsteroids = 2500;

    public const float scoreInterval = 200;

    public const float minDistanceToOther = 200;
    public const float maxDistanceToOther = 500;

    public const float bossScoreInterval = 150;
    public const float bossMainGunInterval = 500;
    public const float bossSideGunInterval = 400;

    public const float bulletSpeed = 0.5f;

    public const float playerFireSpeed = 750f;
    public const float maxTimeBetweenFuel = 10000f;

    public const float playerAnimationSpeed = 0.015f;
    public const float playerSparkInterval = 100f;
    public const float hudFuelbarLooseOverTime = 0.02f;


    //Font
    public const string fontPath = "font.ttf";

    //Hud
    //Health
    public const int hudHealthWidth = 60;
    public const int hudHealthHeight = 10;
    public const float hudHealthPosX = 10f;
    public const float hudHealthPosY = 30f;
    public const float hudHealthScale = .2f;

    //Fuel
    public const int hudFuelWidth = 200;
    public const int hudFuelHeight = 10;
    public const float hudFuelPosX = 120;
    public const float hudFuelPosY = 80f;
    public const float hudFuelbarScale = 0.5f;

    //Score
    public const int hudScoreWidth = 100;
    public const int hudScoreHeight = 40;
    public const float hudScorePosX = 850f;
    public const float hudScorePosY = 20f;
    public const float hudScoreFontSize = 20f;

    //Pickup
    public const float pickupScale = 2f;
    public const float pickupMoveSpeed = 0.5f;
    public const int pickupHealBoost = 1;
    public const int pickupFuelBoost = 15;

    //Scoreboard
    public const int playerNameMaxLength = 15;

    //Title
    public const int scoreBoardTitleWidth = 350;
    public const int scoreBoardTitleHeight = 50;
    public const float scoreBoardTitlePosX = 480;
    public const float scoreBoardTitlePosY = 50f;
    public const float scoreBoardTitleFontSize = 30f;

    //Table
    public const int scoreTableCellWidth = 400;
    public const int scoreTableCellHeight = 50;
    public const int scoreTableNumOfRows = 6;
    public const float scoreTablePosY = 100f;
    public const float scoreTablePosX = 410f;
    public const float scoreTableFontSize = 20f;

    //SavefileName
    public const string savefileName = "SaveGame.txt";

    //StartBtn
    public const int scoreBoardStartBtnWidth = 200;
    public const int scoreBoardStartBtnHeight = 100;
    public const float scoreBoardStartBtnPosX = 800f;
    public const float scoreBoardStartBtnPosY = 500f;
    public const float scoreBoardStartBtnFontSize = 15f;

    //NewPlayerrBtn
    public const int scoreBoardNewPlayerrBtnWidth = 330;
    public const int scoreBoardNewPlayerrBtnHeight = 100;
    public const float scoreBoardNewPlayerrBtnPosX = 200f;
    public const float scoreBoardNewPlayerrBtnPosY = 500f;
    public const float scoreBoardNewPlayerrBtnFontSize = 15f;

    public static List<string> playerNames = new List<string>()
    {
        "Joey",
        "Aurelio",
        "Evan",
        "Donny",
        "Foster",
        "Dwayne",
        "Grady",
        "Quinton",
        "Darin",
        "Mickey",
        "Hank",
        "Kim",
        "Peter",
        "Jeremy",
        "Jess",
        "Jimmie",
        "Vern",
        "Pasquale",
        "Romeo",
        "Chris",
        "Dale",
        "Beau",
        "Cliff",
        "Timothy",
        "Raphael",
        "Brain",
        "Mauro",
        "Luke",
        "Myron",
        "Omar",
        "Reynaldo",
        "Major",
        "Clinton",
        "Nolan",
        "Raymond",
        "Lucien",
        "Carey",
        "Winfred",
        "Dan",
        "Abel",
        "Elliott",
        "Brent",
        "Chuck",
        "Dirk",
        "Tod",
        "Emerson",
        "Dewey",
        "Scot",
        "Enrique",
        "Al",
        "Beatrice",
        "Brandy",
        "Kathy",
        "Jane",
        "Marcy",
        "Shelly",
        "Lucy",
        "Cathy",
        "Joanna",
        "Doris",
        "Lindsay",
        "Staci",
        "Shelia",
        "Rosanne",
        "Rebecca",
        "Luz",
        "Flora",
        "Rosalie",
        "Karla",
        "Phoebe",
        "Meagan",
        "Virginia",
        "Amanda",
        "Katy",
        "Karla",
        "Deanne",
        "Pearl",
        "Christi",
        "Victoria",
        "Ola",
        "Alexandra",
        "Marina",
        "Lorraine",
        "Sybil",
        "Adeline",
        "Taylor",
        "Anita",
        "Aurora",
        "Neva",
        "Alisha",
        "Maria",
        "Erna",
        "Gwendolyn",
        "Brenda",
        "Bethany",
        "Sybil",
        "Earline",
        "June",
        "Brandy",
        "Sue"
    };

}
