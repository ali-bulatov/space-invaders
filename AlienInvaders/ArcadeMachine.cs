using System;
using System.Collections.Generic;
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

        public ArcadeMachine()
        {
            _credits = 0;
            _scoreList = new List<List<string>>();
            _game = null;
        }

        public void LoadScores()
        {
            
        }

        public void SaveScores()
        {

        }

        public void ClearGame()
        {

        }

        public void AddCredits()
        {

        }

        public void ShowHighScores()
        {

        }

        public void CreateGame()
        {

        }
    }
}
