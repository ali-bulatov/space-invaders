using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienInvadersBuisnessLogic
{
    public class ArcadeMachine
    {

        private List<List<string>> _scoreList;

        private Game _game;
        private bool newscore;

        public ArcadeMachine()
        {
            _scoreList = new List<List<string>>();
            _game = null;
        }

        public Game Game
        {
            get
            {
                return _game;
            }
            set
            {
                _game = value;
            }
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

        public void ShowHighScores()
        {
            var TopScores = File.ReadLines("scorelog.txt")
                .Select(scoreline => int.Parse(scoreline))
                .OrderByDescending(score => score)
                .Take(1);
        }
    }
}
