using GXPEngine;

public class MyGame : Game
{
    Scene activeScene;
    ScoreBoard activeScoreBoard;
    Hud activeHud;

    public MyGame() : base(960, 540, false, true, 1920, 1080, true)
    {
        LoadScoreBoard();
    }

    public void LoadScene(string pPlayerName = "")
    {
        activeScene = new Scene();
        AddChild(activeScene);

        activeHud = new Hud(activeScene, pPlayerName);
        activeScene.AddChild(activeHud);
    }

    public void LoadScoreBoard()
    {
        int latestScore = activeHud != null ? activeHud.scoreCount : 0;
        string latestName = activeHud != null ? activeHud.playerName : "";

        activeScoreBoard = new ScoreBoard(latestScore, latestName);
        AddChild(activeScoreBoard);   
    }    

    static void Main()
    {
        new MyGame().Start();
    }
}