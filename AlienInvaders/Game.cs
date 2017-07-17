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

        private List<List<Alien>> _alienList;

        private MotherShip _motherShip;

        private List<Shield> _shieldList;

        private GameDifficulty _difficulty;

        private Color _colorOption;

        private byte _imageOption;

        /// <summary>
        /// Represents the constructor of the game that is being played.
        /// </summary>
        /// <param name="difficulty">Represents the difficulty of the game.</param>
        /// <param name="colorOption">Represents the color of the player selected as an option.</param>
        /// <param name="imageOption">Represents the image option of the player.</param>
        public Game(GameDifficulty difficulty, Color colorOption, byte imageOption)
        {
            //Load the basic assets of the game before playing or resuming.
            _gameScore = 0;
            _randomizer = new Random();
            _round = 1;
            _time = 0;
            _colorOption = colorOption;
            _imageOption = imageOption;
            //Set the player color 
            _player = new Player(3, _colorOption, _imageOption);
            _bulletList = new List<EnemyBullet>();
            _alienList = new List<List<Alien>>();
            _motherShip = new MotherShip();
            _shieldList = new List<Shield>();
            _difficulty = difficulty;
        }

        public int GameScore
        {
            get
            {
                return _gameScore;
            }
        }

        public int Time
        {
            get
            {
                return _time;
            }
        }

        public byte Round
        {
            get
            {
                return _round;
            }
        }

        public void Play()
        {
            //Check to see if there is an existing game going on.
            //If so:
            //Load the file containing all of the alien objects and the player.
            //Load and Update the score.
            //Update the Player's Lives.
            //Update the Time and Round Number.
            //Set the image of the player to the loaded settings.
            //Add the aliens to the list and position them for all aliens.

            //Otherwise, simply load in the aliens and set their position.
            //Give three random aliens a bullet from the list to start.
            
        }

        public void End()
        {
            //
        }

        public void UpdateScore()
        {

        }

        public void ResetRound()
        {
            //Check to see if the player has enough lives.
            //If so, set the player position by calling the move method.
            //Decrement the number of lives.
            //Otherwise, call the End() method.
        }

        public void IncreaseSpeed()
        {

        }

        public void DespawnAliens(int alienRow, int alienColumn)
        {
            //Pop the alien object out of the list.
            //Move the alien offscreen.
            //Set the alien visibility to false.
            //Add a null refernce to the list.
            //Destroy the Alien Object.

        }

        public void ShiftAliens()
        {
            //TODO: FIX THIS METHOD.
            List<List<bool>> isHittingEdge = new List<List<bool>>();
            foreach (List<Alien> alienRow in _alienList)
            {
                foreach (Alien alienCell in alienRow)
                {
                    if (alienCell != null)
                    {
                        isHittingEdge.Add(alienCell.MoveHorizontal());
                    }

                }
            }
            foreach (List<Alien> alienRow in _alienList)
            {
                foreach (Alien alienCell in alienRow)
                {
                    if (alienCell != null)
                    {
                        alienCell.MoveVertical();
                    }

                }
            }
            //Go through all 55 aliens in the list of aliens.
            //Store the return values in the IsHittingEdge list.
            //Go through all Aliens again.
            //Check to see if the the alien is the farthest left or right and is hitting the edge of the screen.
            //Go through all aliens.
            //Move the aliens vertically.
        }

        public void Pause()
        {

        }

        public void Save()
        {
            //Save the list of objects into the file.
            //Navigate back to the MainPage.
        }
    }
}
