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

    public ScoreBoard(int pLatestScore) : base()
    {
        font = Utils.LoadFont(CoreParameters.fontPath, CoreParameters.scoreBoardTitleFontSize);

        CreateTitle();

        CreateTable();
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

    void CreateTable()
    {
        LoadData("SaveGame.txt");

        foreach (string name in names)
        {
            //Names = Column 1
            CreateCell(true, name);
        }

        foreach (int score in scores)
        {
            //Sores = Column 2
            CreateCell(false, score.ToString());
        }

    }

    void CreateCell(bool isNameCell, string cellText)
    {
        Console.WriteLine(names.IndexOf(name));
        EasyDraw cell = new EasyDraw(CoreParameters.scoreTableCellWidth, CoreParameters.scoreTableCellHeight);
        cell.SetOrigin(cell.width / 2, cell.height / 2);
        cell.SetXY(isNameCell ? CoreParameters.scoreTablePosX : CoreParameters.scoreTablePosX + CoreParameters.scoreTableCellWidth,
            CoreParameters.scoreTablePosY + names.IndexOf(name)  + 1 * CoreParameters.scoreTableCellHeight);
        cell.TextFont(font);
        cell.TextAlign(CenterMode.Min, CenterMode.Center);
        cell.Fill(Color.White);
        cell.Text(cellText);
        cell.SetXY(CoreParameters.scoreBoardTitlePosX, CoreParameters.scoreBoardTitlePosY);
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
}
