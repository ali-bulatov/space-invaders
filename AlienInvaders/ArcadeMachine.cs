﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienInvaders
{
    public class ArcadeMachine
    {
        private int _credits;

        private List<List<string>> _scoreList;

        private Game _game;
        private bool newscore;

        public ArcadeMachine()
        {
            _credits = 0;
            _scoreList = new List<List<string>>();
            _game = null;
        }

        public void LoadScores()
        {
            var TopScores = File.ReadLines("scorelog.txt")
                .Select(scoreline => int.Parse(scoreline))
                .OrderByDescending(score => score)
                .Take(10);
        }

        public void SaveScores()
        {
            string fileName = @"scorelog.txt";
            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(newscore);
            }
        }

        public void ClearGame()
        {

        }

        public void AddCredits()
        {

        }

        public void ShowHighScores()
        {
            var TopScores = File.ReadLines("scorelog.txt")
                .Select(scoreline => int.Parse(scoreline))
                .OrderByDescending(score => score)
                .Take(1);
        }
    }
}
