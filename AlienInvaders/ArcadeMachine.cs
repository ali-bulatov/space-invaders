using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienInvaders
{
    public class ArcadeMachine
    {

        private List<List<string>> _scoreList;

        private Game _game;
        //private bool newscore;

        public ArcadeMachine()
        {
            _scoreList = new List<List<string>>();
            _game = null;
        }

        public string LoadScores()
        {
            var TopScores = File.ReadLines("scorelog.txt")
                .Select(scoreline => int.Parse(scoreline))
                .OrderByDescending(score => score)
                .Take(15);

            return TopScores.ToString();
        }

        public void SaveScores(int newScore, string playerName, byte level, int time)
        {
            string fileName = @"scorelog.txt";
            using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(playerName, newScore.ToString(), level.ToString(), time.ToString());
            }
        }

        public void ClearGame()
        {
            _game = null;
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
