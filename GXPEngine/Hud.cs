using System;
using System.Drawing;
using GXPEngine;
class Hud : GameObject
{
    public int scoreCount = 0;
    public string playerName = "";

    //Hud elemets
    EasyDraw healthHudElement;
    EasyDraw scoreHudElement;
    Sprite fuelbarFill;
    Sprite powerbarFill;

    float fuelbarFillAmount;
    float powerbarAmount;
        
    Scene activeScene;

    public Hud(Scene pActiveScene, string pName) : base()
    {
        activeScene = pActiveScene;
        activeScene.hud = this;
        playerName = pName;

        //Create hud element for health
        CreateHealthElement();

        //Create hud element for fuel
        CreateFuelElement();

        //Create hud element for score
        CreateScoreElement();

        CreatePowerElement();
    }

    public void UpdateHealth()
    {
        //int playerHealthTotal = activeScene.player.health - healthBoost;
        int playerHealth = activeScene.player.health; //New player health

        foreach (Sprite child in healthHudElement.GetChildren()) //Empty old healthbar
        {
            healthHudElement.RemoveChild(child);
        }

        for (int i = 1; i <= playerHealth; i++)
        {
            Sprite sprite = new Sprite("health.png", false, false);
            sprite.SetOrigin(sprite.width / 2, sprite.height / 2);
            sprite.scale = CoreParameters.hudHealthScale;
            sprite.SetXY(i * healthHudElement.width - healthHudElement.width / 2, healthHudElement.height);
            healthHudElement.AddChild(sprite);
        }
    }

    public void UpdateFuelbar(int pFuelbarBoost)
    {
        fuelbarFill.x += pFuelbarBoost;
        fuelbarFill.width += 2 * pFuelbarBoost;
        fuelbarFillAmount = fuelbarFill.width;
    }

    public void UpdatePowerbar(int pPowerbarBoost)
    {
        powerbarFill.x += pPowerbarBoost;
        powerbarFill.width += 2 * pPowerbarBoost;
        powerbarAmount = powerbarFill.width;
    }

    public void UpdateScore(int pScoreBoost)
    {
        scoreCount += pScoreBoost;
    }

    void CreateHealthElement()
    {
        healthHudElement = new EasyDraw(CoreParameters.hudHealthWidth, CoreParameters.hudHealthHeight, false);
        healthHudElement.SetOrigin(healthHudElement.width / 2, healthHudElement.height / 2);
        healthHudElement.SetXY(CoreParameters.hudHealthPosX, CoreParameters.hudHealthPosY);
        AddChild(healthHudElement);
        UpdateHealth();
    }

    void CreateFuelElement()
    {
        fuelbarFill = new Sprite("fuelbar.png", false, false);
        fuelbarFill.SetOrigin(fuelbarFill.width / 2, fuelbarFill.height / 2);
        fuelbarFill.SetXY(CoreParameters.hudFuelPosX, CoreParameters.hudFuelPosY);
        fuelbarFill.scale = CoreParameters.hudFuelbarScale;
        fuelbarFill.width = CoreParameters.hudFuelWidth;
        fuelbarFill.height = CoreParameters.hudFuelHeight;
        AddChild(fuelbarFill);
        fuelbarFillAmount = fuelbarFill.width;
    }

    void CreatePowerElement()
    {
        powerbarFill = new Sprite("powerbar.png", false, false);
        powerbarFill.SetOrigin(powerbarFill.width / 2, powerbarFill.height / 2);
        powerbarFill.SetXY(CoreParameters.hudFuelPosX, CoreParameters.hudFuelPosY + 20);
        powerbarFill.scale = CoreParameters.hudFuelbarScale;
        powerbarFill.width = CoreParameters.hudFuelWidth;
        powerbarFill.height = CoreParameters.hudFuelHeight;
        AddChild(powerbarFill);
        powerbarAmount = powerbarFill.width;
    }

    void CreateScoreElement()
    {
        scoreHudElement = new EasyDraw(CoreParameters.hudScoreWidth, CoreParameters.hudScoreHeight, false);
        scoreHudElement.SetXY(CoreParameters.hudScorePosX, CoreParameters.hudScorePosY);
        Font font = Utils.LoadFont(CoreParameters.fontPath, CoreParameters.hudScoreFontSize);
        scoreHudElement.TextFont(font);
        scoreHudElement.TextAlign(CenterMode.Min, CenterMode.Center);
        scoreHudElement.Fill(Color.White);
        AddChild(scoreHudElement);
        UpdateScore(0);
    }

    void Update()
    {
        int clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);

        //Score update
        scoreCount = activeScene.score;
        scoreHudElement.Text(scoreCount.ToString(), true);

        //Power update
        if (!activeScene.playerAlive)
        {
            powerbarFill.width = 0;
        }
        else
        {
            Console.WriteLine("****************START******************");
            //Fuel update
            if (fuelbarFill.width > 0 && !activeScene.bossFight)
            {
                fuelbarFillAmount -= CoreParameters.hudFuelbarLooseOverTime * clampedDeltaTime;
                Console.WriteLine("fuelbarFillAmount " + fuelbarFillAmount);
                Console.WriteLine("fuelbarFill.width - 2 " + (fuelbarFill.width - 2).ToString());
                if (fuelbarFillAmount <= fuelbarFill.width - 2)
                {
                    Console.WriteLine("go Down");
                    UpdateFuelbar(-1);
                }

                Console.WriteLine("fuelbarFill.width " + fuelbarFill.width);
                Console.WriteLine("fuelbarFill.x " + fuelbarFill.x);

            }
            else if (fuelbarFill.width <= 0)
            {
                activeScene.player.health = 0;
                fuelbarFill.width = 0;
            }

            if (powerbarFill.width < CoreParameters.hudFuelWidth && !activeScene.player.isInSpecialState)
            {
                powerbarAmount += CoreParameters.hudFuelbarLooseOverTime * clampedDeltaTime;
                if (powerbarAmount >= powerbarFill.width + 1)
                {
                    UpdatePowerbar(1);
                }
            }
            else if (powerbarFill.width >= CoreParameters.hudFuelWidth && !activeScene.player.isInSpecialState)
            {
                powerbarFill.width = CoreParameters.hudFuelWidth;
                activeScene.player.isInSpecialState = true;
            }

            if (powerbarFill.width > 0 && activeScene.player.isInSpecialState)
            {
                powerbarAmount -= CoreParameters.hudFuelbarLooseOverTime * clampedDeltaTime;
                if (powerbarAmount <= powerbarFill.width - 0.5f)
                {
                    UpdatePowerbar(-1);
                }
            }
            else if (powerbarFill.width <= 0 && activeScene.player.isInSpecialState)
            {
                powerbarFill.width = 0;
                activeScene.player.isInSpecialState = false;
            }
        }
    }
}
