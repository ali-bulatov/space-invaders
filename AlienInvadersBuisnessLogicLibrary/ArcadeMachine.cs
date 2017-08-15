using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AlienInvadersBuisnessLogic
{
    public class ArcadeMachine
    {
        public string PlayerName { get; set; }
        public double Score { get; set; }
        public double Time { get; set; }
        public byte Round { get; set; }

        private Game _game;

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

        public ArcadeMachine(string playername = "None", double score = 0, double time = 0, byte round = 0)
        {
            PlayerName = playername;
            Score = score;
            Time = time;
            Round = round;

            _game = null;
        }

        public ArcadeMachine()
        {

        }

        public void ClearGame()
        {
            _game = null;
        }
    }
}
