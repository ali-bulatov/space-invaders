using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AlienInvadersBuisnessLogic
{
    [Serializable()]
    public class ArcadeMachine : ISerializable
    {
        public string PlayerName { get; set; }
        public double Score { get; set; }
        public double Time { get; set; }
        public byte Round { get; set; }

        //private List<List<string>> _scoreList;

        private Game _game;
        //private bool newscore;

        public ArcadeMachine(string playername = "None", double score = 0, double time = 0, byte round = 0)
        {
            PlayerName = playername;
            Score = score;
            Time = time;
            Round = round;


            //_scoreList = new List<List<string>>();
            _game = null;
        }

        public override string ToString()
        {
            return string.Format("{0}                    {1}                    {2}                    {4}", PlayerName, Score, Time, Round);
        }

        public void ClearGame()
        {
            _game = null;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PlayerName", PlayerName);
            info.AddValue("Score", Score);
            info.AddValue("Time", Time);
            info.AddValue("Round", Round);
        }

        public ArcadeMachine(SerializationInfo info, StreamingContext context)
        {
            PlayerName = (string)info.GetValue("PlayerScore", typeof(string));
            Score = (double)info.GetValue("Score", typeof(double));
            Time = (double)info.GetValue("Time", typeof(double));
            Round = (byte)info.GetValue("Weight", typeof(byte));

        }
    }
}
