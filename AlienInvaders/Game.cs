using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

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
        public Game(GameDifficulty difficulty, Color colorOption, byte imageOption, Image playerImage)
        {
            //Load the basic assets of the game before playing or resuming.
            _gameScore = 0;
            _randomizer = new Random();
            _round = 1;
            _time = 0;
            _colorOption = colorOption;
            _imageOption = imageOption;
            //Set the player color 
            _player = new Player(3, _colorOption, _imageOption, playerImage);
            _bulletList = new List<EnemyBullet>();
            _alienList = new List<List<Alien>>();
            //TODO: FIX WIDTH TO CANVAS.ACTUALWIDTH.
            _motherShip = new MotherShip(720, 0.25);
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
            set
            {
                _time = value;
            }
        }

        public byte Round
        {
            get
            {
                return _round;
            }
        }

        public Player Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
            }
        }

        public Random Randomizer
        {
            get
            {
                return _randomizer;
            }
        }

        public MotherShip MotherShip
        {
            get
            {
                return _motherShip;
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
            for (int alienRowCount = 0; alienRowCount < 5; alienRowCount++)
            {
                _alienList.Add(new List<Alien>());
            }
            //TODO: TALK TO PARTNER ABOUT CHANGING ALIEN POSITION.
            //Give three random aliens a bullet from the list to start.
            for (int alienCount = 0; alienCount < 3; alienCount++)
            {
                _bulletList.Add(new EnemyBullet());
                int randRow = _randomizer.Next(0, 4);
                int randCol = _randomizer.Next(0, 11);
                //TODO: Make the _enemyBullet available.
                //_alienList[randRow][randCol]._enemyBullet = _bulletList[alienCount];
            }
            
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
            _alienList = new List<List<Alien>>();
            for (int alienRowCount = 0; alienRowCount < 5; alienRowCount++)
            {
                _alienList.Add(new List<Alien>());
            }
            //TODO: TALK TO PARTNER ABOUT CHANGING ALIEN POSITION.
            //Give three random aliens a bullet from the list to start.
            for (int alienCount = 0; alienCount < 3; alienCount++)
            {
                _bulletList.Add(new EnemyBullet());
                int randRow = _randomizer.Next(0, 4);
                int randCol = _randomizer.Next(0, 11);
                //TODO: Make the _enemyBullet available.
                //_alienList[randRow][randCol]._enemyBullet = _bulletList[alienCount];
            }

            _player.Reset();
        }

        public void IncreaseSpeed()
        {

        }

        public void DespawnAliens(int alienRow, int alienColumn)
        {
            //Pop the alien object out of the list.
            Alien selectedAlien = _alienList[alienRow][alienColumn];
            //Move the alien offscreen.
            //Canvas.SetLeft(selectedAlien._imgAlien, 0);
            //Canvas.SetTop(selectedAlien._imgAlien, 0);
            //Set the alien visibility to false.
            //selectedAlien._imgAlien.Visibility = Visibility.Collapsed;
            //TODO: IMPLEMENT.
            //Add a null refernce to the list.
            _alienList[alienRow][alienColumn] = null;
            //Destroy the Alien Object.
            foreach (List<Alien> alienRowNum in _alienList)
            {
                foreach (Alien alien in alienRowNum)
                {
                    if (alien != null)
                    {
                        return;
                    }

                }
            }
            ResetRound();
        }

        public void ShiftAliens()
        {
            foreach (List<Alien> alienRow in _alienList)
            {
                foreach (Alien alienCell in alienRow)
                {
                    if (alienCell != null)
                    {
                        //bool isHittingEdge = alienCell.MoveHorizontal();
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
            _player.CanMove = false;
        }

        public void Save()
        {
            //Save the list of objects into the file.

        }

        public int CountAliens()
        {
            int count = 0;
            foreach(List<Alien> alienRow in _alienList)
            {
                foreach(Alien alien in alienRow)
                {
                    if (alien != null)
                    {
                        count += 1;
                    }
                }
            }
            return count;
        }

    }
}
