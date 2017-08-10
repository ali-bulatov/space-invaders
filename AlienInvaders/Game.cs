using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
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
        public Game(GameDifficulty difficulty, Color colorOption, byte imageOption, Image playerImage, List<Image> alienImageList, Image bulletImage)
        {
            //Load the basic assets of the game before playing or resuming.
            _gameScore = 0;
            _randomizer = new Random();
            _round = 1;
            _time = 0;
            _colorOption = colorOption;
            _imageOption = imageOption;
            //Set the player color 
            _player = new Player(3, _colorOption, _imageOption, playerImage, bulletImage);
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
        
        public async void Play()
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
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile savedGame = await storageFolder.GetFileAsync("game.txt");
            IList<String> fileLines = await FileIO.ReadLinesAsync(savedGame);
            if (fileLines[0] == "1")
            {
                for (int fileIndex = 1; fileIndex <= _alienImageList.Count; fileIndex++)
                {
                    if (fileLines[fileIndex] != "0 0 0 0 Right")
                    {
                        //TODO: FIX THIS IMMEDIATELY
                        _alienList.Add(new Alien(100, _alienImageList[fileIndex - 1]));
                        /*
                        _alienList[fileIndex - 1].XPosition = double.Parse(fileLines[fileIndex][0].ToString());
                        Canvas.SetLeft(_alienImageList[fileIndex - 1], _alienList[fileIndex - 1].XPosition);
                        _alienList[fileIndex - 1].YPosition = double.Parse(fileLines[fileIndex][2].ToString());
                        Canvas.SetTop(_alienImageList[fileIndex - 1], _alienList[fileIndex - 1].YPosition);
                        _alienList[fileIndex - 1].Speed = double.Parse(fileLines[fileIndex][4].ToString());
                        _alienList[fileIndex - 1].Points = byte.Parse(fileLines[fileIndex][6].ToString());
                        //TODO: FIGURE OUT CODE TO SET ENUMERATION NUMBER.
                        //_alienList[fileIndex - 1].Direction = fileLines[fileIndex][8].ToString();
                        */

                    }
                    else
                    {
                        _alienList.Add(new Alien(100, _alienImageList[fileIndex - 1]));
                        DespawnAliens(fileIndex - 1);
                    }
                    
                }
                _player.Lives = byte.Parse(fileLines[56]);
                _gameScore = int.Parse(fileLines[57]);
                _time = int.Parse(fileLines[58]);
                _player.Position = double.Parse(fileLines[59]);
                Canvas.SetLeft(_player.UiPlayer, double.Parse(fileLines[59]));
                //_difficulty = GameDifficulty.(fileLines[60]);                   
            }
            else
            {
                for (int alienImageIndex = 0; alienImageIndex < _alienImageList.Count; alienImageIndex++)
                {
                    _alienList.Add(new Alien(100, _alienImageList[alienImageIndex]));
                }
                //Give three random aliens a bullet from the list to start.

                for (int alienCount = 0; alienCount < 3; alienCount++)
                {
                    _bulletList.Add(new EnemyBullet());
                    int randomIndex = _randomizer.Next(44, 54);
                    //TODO: Make the _enemyBullet available.
                    //_alienList[randomIndex]._enemyBullet = _bulletList[alienCount];
                }
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
                    //speed = alien.Speed;
                }

            }
            switch(alienCount)
            {
                //case 45:
                    //speed -= 50;
                    //break;

                //case 
            }
            //return speed;
            return 0.0;
        }

        public int DespawnAliens(int alienNum)
        {
            //Pop the alien object out of the list.
            Alien selectedAlien = _alienList[alienNum];
            //Move the alien offscreen.
            //Canvas.SetLeft(selectedAlien.UiAlien, 0);
            //Canvas.SetTop(selectedAlien.UiAlien, 0);
            //Set the alien visibility to false.
            //selectedAlien.UiAlien.Visibility = Visibility.Collapsed;
            //Add a null refernce to the list.
            _alienList[alienNum] = null;
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
            }

            //return selectedAlien.Points;
            return 0;

        }

        public void ShiftAliens()
        {
            Alien aliveAlien = null;
            foreach (Alien alien in _alienList)
            {
                if (alien != null)
                {
                    aliveAlien = alien;
                }
            }
            if (aliveAlien.Direction == Direction.Left)
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

        public async void Save()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //Check to see if the file is available or not. You want to call the .ReplaceExisting to overwrite the current text file.
            StorageFile saveFile = await storageFolder.CreateFileAsync("game.txt", CreationCollisionOption.ReplaceExisting);

            List<String> saveItems = new List<String>();
            saveItems.Add("1");
            foreach (Alien alien in _alienList)
            {
                if (alien != null)
                {
                    //saveItems.Add(alien.XPosition.ToString() + " " + alien.YPosition.ToString() + " " + alien.Speed.ToString() + " " + alien.Points.ToString() + " " + alien.Direction.ToString());
                }
                else
                {
                    //This alien does not exist. Set it to default.
                    saveItems.Add("0 0 0 0 Right");
                }
            }
            saveItems.Add(_player.Lives.ToString());
            saveItems.Add(_gameScore.ToString());
            saveItems.Add(_time.ToString());
            saveItems.Add(_player.Position.ToString());
            saveItems.Add(_difficulty.ToString());
            //TODO: ASK PARTNER TO COMPLETE SHIELD CLASS.
            //Write text into the file.
            await FileIO.WriteLinesAsync(saveFile, saveItems);
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
