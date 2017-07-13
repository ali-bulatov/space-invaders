using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienInvaders
{
    public enum GameDifficulty
    {
        Beginner = 1,
        Easy,
        Normal,
        Hard,
        Intense
    }
    /// <summary>
    /// Represents a current game that is being played on the GamePage
    /// </summary>
    public class Game
    {
        private int _gameScore;

        private Random _randomizer;

        private byte _round;

        private int _time;

        private Player _player;

        private List<EnemyBullet> _bulletList;

        private List<Alien> _alienList;

        private MotherShip _motherShip;

        private List<Shield> _shieldList;

        private GameDifficulty _difficulty;

        public Game(GameDifficulty difficulty)
        {

        }

        public void Play()
        {

        }

        public void End()
        {

        }

        public void UpdateScore()
        {

        }

        public void ResetRound()
        {

        }

        public void IncreaseSpeed()
        {

        }

        public void DespawnAliens()
        {

        }

        public void ShiftAliens()
        {

        }
    }
}
