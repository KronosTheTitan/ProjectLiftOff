using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using GXPEngine;

class ScoreBoard : GameObject
{
    List<string> names = new List<string>();
    List<int> scores = new List<int>();
    Font font;
    int latestScore;
    string latestName;

    public ScoreBoard(int pLatestScore = 0, string pLatestName = "") : base()
    {
        font = Utils.LoadFont(CoreParameters.fontPath, CoreParameters.scoreBoardTitleFontSize);

        latestScore = pLatestScore;
        latestName = pLatestName;

        CreateTitle();

        CreateTable();

        CreateStartButton();

        CreateNewPlayerButton();
    }

    void CreateTitle()
    {
        //Scoreboard title
        EasyDraw title = new EasyDraw(CoreParameters.scoreBoardTitleWidth, CoreParameters.scoreBoardTitleHeight, false);
        title.SetOrigin(title.width / 2, title.height / 2);
        title.TextFont(font);
        title.TextAlign(CenterMode.Min, CenterMode.Center);
        title.Fill(Color.White);
        title.Text("Score Board");
        title.SetXY(CoreParameters.scoreBoardTitlePosX, CoreParameters.scoreBoardTitlePosY);
        AddChild(title);
    }

    void CreateStartButton()
    {
        if (latestName != "")
        {
            //Calls ((MyGame)game).LoadScene(latestName);
        }
        //TODO create start button
    }

    void CreateNewPlayerButton()
    {
        //TODO Set playername
    }

    void CreateTable()
    {
        LoadData("SaveGame.txt");
        
        CreateCell(true, "Players", 0);
        CreateCell(false, "Scores", 0);

        foreach (string name in names)
        {
            //Names = Column 1
           CreateCell(true, name, names.IndexOf(name)+1);
        }

        foreach (int score in scores)
        {
            //Sores = Column 2
            CreateCell(false, score.ToString(), scores.IndexOf(score)+1);
        }

        //TODO create last row with global values
    }

    void CreateCell(bool isFirstColumn, string cellText, int index)
    {
        EasyDraw cell = new EasyDraw(CoreParameters.scoreTableCellWidth, CoreParameters.scoreTableCellHeight);
        cell.SetOrigin(cell.width / 2, cell.height / 2);
        cell.SetXY(isFirstColumn ? CoreParameters.scoreTablePosX : CoreParameters.scoreTablePosX + CoreParameters.scoreTableCellWidth,
            CoreParameters.scoreTablePosY + index * CoreParameters.scoreTableCellHeight);
        cell.TextFont(font);
        cell.TextAlign(CenterMode.Min, CenterMode.Center);
        cell.Fill(Color.White);
        cell.Text(cellText);
        AddChild(cell);
    }

    void LoadData(string pFilename)
    {
        if (!File.Exists(pFilename))
        {
            Console.WriteLine("No save file found!");
            return;
        }
        try
        {
            using (StreamReader reader = new StreamReader(pFilename))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    int splitPos = line.IndexOf('=');
                    if (splitPos >= 0)
                    {
                        string key = line.Substring(0, splitPos);
                        string value = line.Substring(splitPos + 1);
                        string[] numbers = value.Split(',');
                        switch (key) //Read all values from saveFile into PlayerData
                        {
                            case "names":
                                foreach (string number in numbers)
                                {
                                    names.Add(number);
                                }
                                break;
                            case "scores":
                                foreach (string number in numbers)
                                {
                                    scores.Add(int.Parse(number));
                                }
                                break;
                        }
                    }
                    line = reader.ReadLine();
                }
                reader.Close();
                Console.WriteLine("File from {0} loaded successfull!", pFilename);
            }
        }
        catch (Exception error)
        {
            Console.WriteLine("Error while loading save file: {0}", error.Message);
        }
    }

    void SaveGame(string pFilename, int pNewScore = 0, string pNewName = "")
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(pFilename))
            {
                if (pNewName != "")
                {
                    names.Add(pNewName);
                    scores.Add(pNewScore);
                }

                writer.WriteLine("names=" + string.Join<string>(",", names));
                writer.WriteLine("scores=" + string.Join<int>(",", scores));
               
                writer.Close();
            }
        }
        catch (Exception error)
        {
            Console.WriteLine("Error while writing save file: {0}", error.Message);
        }
    }

    protected override void OnDestroy()
    {
        SaveGame(CoreParameters.savefileName, latestScore, latestName);
    }
}
