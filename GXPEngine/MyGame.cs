using GXPEngine;

public class MyGame : Game
{
    Scene activeScene;
    ScoreBoard activeScoreBoard;
    Hud activeHud;

    public MyGame() : base(800, 600, false, true)
    {
        //LoadScene();
        LoadScoreBoard();
    }

    public void LoadScene(string pPlayerName = "")
    {
        activeScene = new Scene();
        AddChild(activeScene);

        activeHud = new Hud(activeScene, pPlayerName);
        AddChild(activeHud);

        Pickup pickup = new Pickup("Star.png", 13, Pickup.Type.Health, activeHud, activeScene);
        pickup.SetXY(game.width, game.height / 2);
        activeScene.AddChild(pickup);
    }

    public void LoadScoreBoard()
    {
        int latestScore = activeHud != null ? activeHud.scoreCount : 0;
        string latestName = activeHud != null ? activeHud.playerName : "";

        activeScoreBoard = new ScoreBoard(latestScore, latestName);
        AddChild(activeScoreBoard);   
    }    

    void Update()
    {
        if (Input.GetKeyDown(Key.Q) && activeScene != null) //For Testing. Destroy Scene
        {
            activeScene = null;
            foreach (GameObject child in GetChildren())
            {
                child.LateDestroy();
            }
        }

        if (Input.GetKeyDown(Key.W) && activeScoreBoard != null) //For Testing. Destroy ScoreBoard
        {
            activeScoreBoard = null;
            foreach (GameObject child in GetChildren())
            {
                child.LateDestroy();
            }
        }
    }

    static void Main()
    {
        new MyGame().Start();
    }
}