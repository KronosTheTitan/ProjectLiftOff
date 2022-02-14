using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using GXPEngine;

class ScoreBoard : GameObject
{
    List<string> names = new List<string>();
    List<int> scores = new List<int>();
    int latestScore;
    string latestName;
    bool typingNewName = false;
    float typeIndicatorTime;
    bool showTpeindicator = false;

    EasyDraw startBtn;
    EasyDraw newPlayerBtn;

    public ScoreBoard(int pLatestScore = 0, string pLatestName = "") : base()
    {
        latestScore = pLatestScore;
        latestName = pLatestName;

        LoadData("SaveGame.txt");
        SaveGame(CoreParameters.savefileName);
        LoadData("SaveGame.txt");

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
        Font font = Utils.LoadFont(CoreParameters.fontPath, CoreParameters.scoreBoardTitleFontSize);
        title.TextFont(font);
        title.TextAlign(CenterMode.Min, CenterMode.Center);
        title.Fill(Color.White);
        title.Text("Score Board");
        title.SetXY(CoreParameters.scoreBoardTitlePosX, CoreParameters.scoreBoardTitlePosY);
        AddChild(title);
    }

    void CreateStartButton()
    {
        startBtn = new EasyDraw(CoreParameters.scoreBoardStartBtnWidth, CoreParameters.scoreBoardStartBtnHeight, false);
        startBtn.SetOrigin(startBtn.width / 2, startBtn.height / 2);
        Font font = Utils.LoadFont(CoreParameters.fontPath, CoreParameters.scoreBoardStartBtnFontSize);
        startBtn.TextFont(font);
        startBtn.TextAlign(CenterMode.Min, CenterMode.Center);
        startBtn.Fill(Color.White);
        startBtn.Text("Start Game");
        startBtn.SetXY(CoreParameters.scoreBoardStartBtnPosX, CoreParameters.scoreBoardStartBtnPosY);
        startBtn.Rect(startBtn.x, startBtn.y, startBtn.width, startBtn.height);
        AddChild(startBtn);
    }

    void CreateNewPlayerButton()
    {
        newPlayerBtn = new EasyDraw(CoreParameters.scoreBoardNewPlayerrBtnWidth, CoreParameters.scoreBoardNewPlayerrBtnHeight, false);
        newPlayerBtn.SetOrigin(newPlayerBtn.width / 2, newPlayerBtn.height / 2);
        Font font = Utils.LoadFont(CoreParameters.fontPath, CoreParameters.scoreBoardNewPlayerrBtnFontSize);
        newPlayerBtn.TextFont(font);
        newPlayerBtn.TextAlign(CenterMode.Min, CenterMode.Center);
        newPlayerBtn.Fill(Color.White);
        newPlayerBtn.Text(latestName == "" ? "New Player" : latestName);
        newPlayerBtn.SetXY(CoreParameters.scoreBoardNewPlayerrBtnPosX, CoreParameters.scoreBoardNewPlayerrBtnPosY);
        AddChild(newPlayerBtn);
    }

    void CreateTable()
    {
        CreateCell(true, "Players", 0);
        CreateCell(false, "Scores", 0);

        int lastIndex = 0;
        foreach (string name in names)
        {
            //Names = Column 1
            CreateCell(true, name, names.IndexOf(name) + 1);
            //Sores = Column 2
            CreateCell(false, scores[names.IndexOf(name)].ToString(), names.IndexOf(name) + 1);

            lastIndex = names.IndexOf(name) + 1;
        }

        //Last column with stats from last game
        if (latestScore != 0 && latestName != "")
        {
            CreateCell(true, "You Score", lastIndex + 1);
            CreateCell(false, latestScore.ToString(), lastIndex + 1);
        }
    }

    void CreateCell(bool isFirstColumn, string cellText, int index)
    {
        EasyDraw cell = new EasyDraw(CoreParameters.scoreTableCellWidth, CoreParameters.scoreTableCellHeight);
        cell.SetOrigin(cell.width / 2, cell.height / 2);
        cell.SetXY(isFirstColumn ? CoreParameters.scoreTablePosX : CoreParameters.scoreTablePosX + CoreParameters.scoreTableCellWidth,
            CoreParameters.scoreTablePosY + index * CoreParameters.scoreTableCellHeight);
        Font font = Utils.LoadFont(CoreParameters.fontPath, CoreParameters.scoreTableFontSize);
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
                names.Clear();
                scores.Clear();
                while (line != null)
                {
                    int splitPos = line.IndexOf('=');
                    if (splitPos >= 0)
                    {
                        string key = line.Substring(0, splitPos);
                        string value = line.Substring(splitPos + 1);
                        string[] numbers = value.Split(',');

                        switch (key) //Read all values from saveFile
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

    /**
    void CheckForMultipleRecord(int pRecordNumber)
    {
        string newLatestName = pRecordNumber == 0 ? latestName : latestName.Substring(latestName.Length-3) + "("+pRecordNumber.ToString()+")";

        if (names.Contains(latestName))
        {
            CheckForMultipleRecord(pRecordNumber+1);
        } else
        {
            latestName = newLatestName;
            return;
        }
    }
    **/

    void SaveGame(string pFilename)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(pFilename))
            {
                bool shouldSve = false;
                int index = 0;

                //CheckForMultipleRecord(0);

                foreach (int score in scores)
                {
                    if (latestScore > score)
                    {
                        //Insert at position in table
                        shouldSve = true;
                        index = scores.IndexOf(score);
                        break;
                    }
                }

                if (shouldSve)
                {
                    //Insert between
                    scores.Insert(index, latestScore);
                    names.Insert(index, latestName);
                }
                else
                {
                    //Insert at the end
                    scores.Add(latestScore);
                    names.Add(latestName);
                }

                if (names.Count > CoreParameters.scoreTableNumOfRows) {
                    scores.RemoveRange(CoreParameters.scoreTableNumOfRows, scores.Count - CoreParameters.scoreTableNumOfRows);
                    names.RemoveRange(CoreParameters.scoreTableNumOfRows, names.Count - CoreParameters.scoreTableNumOfRows);
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
        ((MyGame)game).LoadScene(latestName);
    }

    void Update()
    {
        int clampedDeltaTime = Mathf.Min(Time.deltaTime, 40);

        if (Input.GetMouseButtonUp(0))
        {
            //Check for Click on Start Game btn
            if (Input.mouseX > startBtn.x - startBtn.width / 2 && Input.mouseX < startBtn.x + startBtn.width / 2
                && Input.mouseY > startBtn.y - startBtn.height / 2 && Input.mouseY < startBtn.y + startBtn.height / 2)
            {
                if (latestName != "" && !names.Contains(latestName))
                {
                    LateDestroy();
                }
                else if (names.Contains(latestName))
                {
                    newPlayerBtn.Text("Name already taken!", true);
                }
                else
                {
                    newPlayerBtn.Text("Create Player Name!", true);
                }
            }

            //Check for Click on New Player btn
            if (Input.mouseX > newPlayerBtn.x - newPlayerBtn.width / 2 && Input.mouseX < newPlayerBtn.x + newPlayerBtn.width / 2
                && Input.mouseY > newPlayerBtn.y - newPlayerBtn.height / 2 && Input.mouseY < newPlayerBtn.y + newPlayerBtn.height / 2)
            {
                typingNewName = true;
                newPlayerBtn.Text("", true);
                latestName = "";
            }
        }

        if (typingNewName)
        {
            if (latestName.Length < CoreParameters.playerNameMaxLength)
            {
                //Register typing
                latestName += (Input.GetKeyDown(Key.A) ? "A" : "");
                latestName += (Input.GetKeyDown(Key.B) ? "B" : "");
                latestName += (Input.GetKeyDown(Key.C) ? "C" : "");
                latestName += (Input.GetKeyDown(Key.D) ? "D" : "");
                latestName += (Input.GetKeyDown(Key.E) ? "E" : "");
                latestName += (Input.GetKeyDown(Key.F) ? "F" : "");
                latestName += (Input.GetKeyDown(Key.G) ? "G" : "");
                latestName += (Input.GetKeyDown(Key.H) ? "H" : "");
                latestName += (Input.GetKeyDown(Key.I) ? "I" : "");
                latestName += (Input.GetKeyDown(Key.J) ? "J" : "");
                latestName += (Input.GetKeyDown(Key.K) ? "K" : "");
                latestName += (Input.GetKeyDown(Key.L) ? "L" : "");
                latestName += (Input.GetKeyDown(Key.M) ? "M" : "");
                latestName += (Input.GetKeyDown(Key.N) ? "N" : "");
                latestName += (Input.GetKeyDown(Key.O) ? "O" : "");
                latestName += (Input.GetKeyDown(Key.P) ? "P" : "");
                latestName += (Input.GetKeyDown(Key.Q) ? "Q" : "");
                latestName += (Input.GetKeyDown(Key.R) ? "R" : "");
                latestName += (Input.GetKeyDown(Key.S) ? "S" : "");
                latestName += (Input.GetKeyDown(Key.T) ? "T" : "");
                latestName += (Input.GetKeyDown(Key.U) ? "U" : "");
                latestName += (Input.GetKeyDown(Key.V) ? "V" : "");
                latestName += (Input.GetKeyDown(Key.W) ? "W" : "");
                latestName += (Input.GetKeyDown(Key.X) ? "X" : "");
                latestName += (Input.GetKeyDown(Key.Y) ? "Y" : "");
                latestName += (Input.GetKeyDown(Key.Z) ? "Z" : "");
                latestName += (Input.GetKeyDown(Key.ONE) ? "1" : "");
                latestName += (Input.GetKeyDown(Key.TWO) ? "2" : "");
                latestName += (Input.GetKeyDown(Key.THREE) ? "3" : "");
                latestName += (Input.GetKeyDown(Key.FOUR) ? "4" : "");
                latestName += (Input.GetKeyDown(Key.FIVE) ? "5" : "");
                latestName += (Input.GetKeyDown(Key.SIX) ? "6" : "");
                latestName += (Input.GetKeyDown(Key.SEVEN) ? "7" : "");
                latestName += (Input.GetKeyDown(Key.EIGHT) ? "8" : "");
                latestName += (Input.GetKeyDown(Key.NINE) ? "9" : "");
                latestName += (Input.GetKeyDown(Key.ZERO) ? "0" : "");
            }

            if (Input.GetKeyDown(Key.BACKSPACE))
            {
                latestName = latestName.Substring(0, latestName.Length - 1);
            }

            if (typeIndicatorTime > 500)
            {
                typeIndicatorTime = 0;
                showTpeindicator = !showTpeindicator;
            }

            typeIndicatorTime += clampedDeltaTime;

            if (Input.GetKeyDown(Key.ENTER))
            {
                typingNewName = false;
                showTpeindicator = false;
            }

            newPlayerBtn.Text(latestName + (showTpeindicator ? "|" : ""), true);
        }
    }
}
