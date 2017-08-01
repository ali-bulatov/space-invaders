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

        private List<Alien> _alienList;

        private MotherShip _motherShip;

        private List<Shield> _shieldList;

        private GameDifficulty _difficulty;

        private Color _colorOption;

        private byte _imageOption;

        private List<Image> _alienImageList;

        /// <summary>
        /// Represents the constructor of the game that is being played.
        /// </summary>
        /// <param name="difficulty">Represents the difficulty of the game.</param>
        /// <param name="colorOption">Represents the color of the player selected as an option.</param>
        /// <param name="imageOption">Represents the image option of the player.</param>
        public Game(GameDifficulty difficulty, Color colorOption, byte imageOption, Image playerImage, List<Image> alienImageList)
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
            _alienList = new List<Alien>();
            //TODO: FIX WIDTH TO CANVAS.ACTUALWIDTH.
            _motherShip = new MotherShip(720, 0.25,_randomizer);
            _shieldList = new List<Shield>();
            _difficulty = difficulty;
            _alienImageList = alienImageList;
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

        public int AlienCount
        {
            get
            {
                return _alienList.Count;
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
            //TODO: TALK TO PARTNER ABOUT CHANGING ALIEN POSITION.
            for (int alienImageIndex = 0; alienImageIndex < _alienImageList.Count; alienImageIndex++)
            {
                _alienList.Add(new Alien(0.25, _alienImageList[alienImageIndex]));
            }
            //Give three random aliens a bullet from the list to start.

            for (int alienCount = 0; alienCount < 3; alienCount++)
            {
                _bulletList.Add(new EnemyBullet());
                int randomIndex = _randomizer.Next(44, 54);
                //TODO: Make the _enemyBullet available.
                _alienList[randomIndex]._enemyBullet = _bulletList[alienCount];
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
            _alienList = new List<Alien>();
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

        public double IncreaseSpeed(int alienCount)
        {
            double speed;
            foreach (Alien alien in _alienList)
            {
                if (alien != null)
                {
                    speed = alien.Speed;
                }

            }
            return; 
        }

        public void DespawnAliens(int alienNum)
        {
            //Pop the alien object out of the list.
            Alien selectedAlien = _alienList[alienNum];
            //Move the alien offscreen.
            Canvas.SetLeft(selectedAlien._uiAlien, 0);
            Canvas.SetTop(selectedAlien._uiAlien, 0);
            //Set the alien visibility to false.
            selectedAlien._uiAlien.Visibility = Visibility.Collapsed;
            //TODO: IMPLEMENT.
            //Add a null refernce to the list.
            _alienList[alienNum] = null;
            //Destroy the Alien Object.
            //TODO: ASK PARTNER FOR THAT.
            //Check to see if there are any more aliens.
            bool isClear = true;
                foreach (Alien alien in _alienList)
                {
                    if (alien == null)
                    {
                        isClear = false;
                    }

                }

            if (isClear)
            {
                ResetRound();
                return;
            }
            
        }

        public void ShiftAliens()
        {
            if (_alienList[0].Direction == Direction.Left)
            {
                foreach (Alien alien in _alienList)
                {
                    if (alien != null)
                    {
                        bool isHittingEdge = alien.MoveHorizontal();
                        if (isHittingEdge)
                        {
                            AdvanceAliens();
                            break;
                        }
                    }

                }
            }
            else
            {
                for (int leftAlienIndex = 0; leftAlienIndex < 55; leftAlienIndex += 11)
                {
                    for (int alienIndex = (10 + leftAlienIndex); alienIndex >= leftAlienIndex; alienIndex--)
                    {
                        if (_alienList[alienIndex] != null)
                        {
                            bool isHittingEdge = _alienList[alienIndex].MoveHorizontal();
                            if (isHittingEdge)
                            {
                                AdvanceAliens();
                                break;
                            }
                        }
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
            if (_player.CanMove == true)
            {
                _player.CanMove = false;
            }
            else
            {
                _player.CanMove = true;
            }
        }

        public void Save()
        {
            //Save the list of objects into the file.

        }

        public void AdvanceAliens()
        {
            foreach (Alien alienCell in _alienList)
            {
                if (alienCell != null)
                {
                    alienCell.MoveVertical();
                    if (alienCell.Direction == Direction.Left)
                    {
                        alienCell.Direction = Direction.Right;
                    }
                    else
                    {
                        alienCell.Direction = Direction.Left;
                    }
                }

            }
            
            //Check to see if any aliens have hit the bottom of the screen.
            //Kill the player.

        }
    }
}
