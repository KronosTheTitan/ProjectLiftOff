using System.Drawing;
using GXPEngine;
class Hud : GameObject
{
    public int scoreCount = 0;
    public string playerName = "";

    //Hud elemets
    EasyDraw fuelbarHudElement;
    EasyDraw healthHudElement;
    EasyDraw scoreHudElement;

    Scene activeScene;
    Sprite fuelbarFill;
    float fuelbarFillAmount;

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
    }

    public void UpdateHealth(int healthBoost)
    {
        int playerHealth = activeScene.player.health; //New player health

        foreach (Sprite child in healthHudElement.GetChildren()) //Empty old healthbar
        {
            healthHudElement.RemoveChild(child);
        }

        Sprite sprite;
        for (int i = 1; i <= playerHealth; i++) //Create hearts for every liefepoint of Player. Empty hearts for the remaining difference to maxhealth
        {
            if (playerHealth >= i)
            {
                sprite = new Sprite("health.png", false, false);
            }
            else
            {
                sprite = new Sprite("healthblack.png", false, false);
            }
            sprite.SetOrigin(sprite.width / 2, sprite.height / 2);
            sprite.scale = CoreParameters.hudHealthScale;
            sprite.SetXY(i * healthHudElement.width - healthHudElement.width / 2, healthHudElement.height);
            healthHudElement.AddChild(sprite);
        }
    }

    public void UpdateFuelbar(int pFuelbarBoost)
    {
        fuelbarFillAmount = Mathf.Round(100 * (Time.time - activeScene.player.lastFuel) / CoreParameters.maxTimeBetweenFuel);
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
        UpdateHealth(0);
    }

    void CreateFuelElement()
    {
        fuelbarHudElement = new EasyDraw(CoreParameters.hudFuelWidth, CoreParameters.hudFuelHeight, false);
        fuelbarHudElement.SetOrigin(fuelbarHudElement.width / 2, fuelbarHudElement.height / 2);
        fuelbarHudElement.SetXY(CoreParameters.hudFuelPosX, CoreParameters.hudFuelPosY);
        fuelbarFill = new Sprite("fuelbar.png", false, false);
        fuelbarFill.SetOrigin(fuelbarFill.width / 2, fuelbarFill.height / 2);
        fuelbarFill.scale = CoreParameters.hudFuelbarScale;
        fuelbarFill.SetXY(fuelbarHudElement.width, fuelbarHudElement.height);
        fuelbarFill.width = Mathf.Round(100 * (Time.time - activeScene.player.lastFuel) / CoreParameters.maxTimeBetweenFuel);
        fuelbarHudElement.AddChild(fuelbarFill);
        AddChild(fuelbarHudElement);
        fuelbarFillAmount = fuelbarFill.width;
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

        //Fuel update
        if (fuelbarFill.width > 0)
        {
            int oldFillAmount = (int)fuelbarFillAmount;
            fuelbarFillAmount -= CoreParameters.hudFuelbarLooseOverTime * clampedDeltaTime;
            if (oldFillAmount != (int)fuelbarFillAmount)
            {
                fuelbarFill.width = (int)fuelbarFillAmount;
                fuelbarFill.x = oldFillAmount - fuelbarFill.width / 2;

            }
        }
        else if (fuelbarFill.width < 0)
        {
            fuelbarFill.width = 0;
        }
    }
}
