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
        if (activeScoreBoard != null) //Destroy scoreboard
        {
            foreach (GameObject child in GetChildren())
            {
                child.LateDestroy();
            }
        }

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

        if (activeScene != null) //Destroy scene
        {
            foreach (GameObject child in GetChildren())
            {
                child.LateDestroy();
            }
        }

        activeScoreBoard = new ScoreBoard(latestScore, latestName);
        AddChild(activeScoreBoard);   
    }    

    void Update()
    {
        if (Input.GetKeyDown(Key.Q)) //For Testing. Load new Scene
        {
            foreach (GameObject child in GetChildren())
            {
                child.LateDestroy();
            }
            LoadScene();
        }

        if (Input.GetKeyDown(Key.W))
        {
            foreach (GameObject child in GetChildren()) //For Testing. Load new ScoreBoard
            {
                child.LateDestroy();
            }
            LoadScoreBoard();
        }
    }

    static void Main()
    {
        new MyGame().Start();
    }
}