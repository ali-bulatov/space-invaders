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
        /// <summary>
        /// Main read and write properties for the ArcadeMachine
        /// </summary>
        public string PlayerName { get; set; }
        public double Score { get; set; }
        public double Time { get; set; }
        public byte Round { get; set; }

        private Game _game;

        /// <summary>
        /// Gets the game to the arcade machine so that if necessary, can start a new game
        /// </summary>
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

        /// <summary>
        /// Internal constructor that should retrieve playername, score, time, and round into ArcadeMachine class
        /// </summary>
        /// <param name="playername">This is a string</param>
        /// <param name="score">This is a double</param>
        /// <param name="time">This is a double</param>
        /// <param name="round">This is a byte</param>
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

        /// <summary>
        /// Sets game to null to restart game.
        /// </summary>
        public void ClearGame()
        {
            _game = null;
        }
    }
}
