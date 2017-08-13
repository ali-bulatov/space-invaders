using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AlienInvadersBuisnessLogic
{
    /// <summary>
    /// Represents the difficulty of a game.
    /// </summary>
    public enum GameDifficulty
    {
        //Beginner is set as 1 by default.
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
        /// <summary>
        /// Represents the score of the game.
        /// </summary>
        private int _gameScore;

        /// <summary>
        /// Represents the randomizer for the game.
        /// </summary>
        private Random _randomizer;

        /// <summary>
        /// Represents the round number.
        /// </summary>
        private byte _round;

        /// <summary>
        /// Represents the time of the game.
        /// </summary>
        private int _time;

        /// <summary>
        /// Represents the player of the game.
        /// </summary>
        private Player _player;

        /// <summary>
        /// Represents the list of enemy bullets.
        /// </summary>
        private List<EnemyBullet> _bulletList;

        /// <summary>
        /// Represents 55 aliens that the game contains.
        /// </summary>
        private List<Alien> _alienList;

        /// <summary>
        /// Represents the mothership of the game.
        /// </summary>
        private MotherShip _motherShip;

        private List<Shield> _shieldList;

        private GameDifficulty _difficulty;

        private Color _colorOption;

        private byte _imageOption;

        private List<Image> _alienImageList;

        private List<Image> _bulletImageList;

        private double[] _alienStartXPositions;

        private double[] _alienStartYPositions;

        /// <summary>
        /// Represents the constructor of the game that is being played.
        /// </summary>
        /// <param name="difficulty">Represents the difficulty of the game.</param>
        /// <param name="colorOption">Represents the color of the player selected as an option.</param>
        /// <param name="imageOption">Represents the image option of the player.</param>
        public Game(GameDifficulty difficulty, Color colorOption, byte imageOption, Image playerImage, Image bulletImage, List<Image> alienImageList, List<Image> shieldList, List<Image> bulletImageList, Image motherShipImage)
        {
            //Load the basic assets of the game before playing or resuming.
            _gameScore = 0;
            _randomizer = new Random();
            _round = 1;
            _time = 0;
            _colorOption = colorOption;
            _imageOption = imageOption;
            //Set the player color 
            _difficulty = difficulty;
            _player = new Player(3, _colorOption, _imageOption, playerImage, bulletImage, 0.25);
            //_bulletList = new List<EnemyBullet>();
            _alienList = new List<Alien>();
            //TODO: FIX WIDTH TO CANVAS.ACTUALWIDTH.
            _motherShip = new MotherShip(720, 0.25,_randomizer, motherShipImage);
            _shieldList = new List<Shield>();
            _alienImageList = alienImageList;
            _bulletList = new List<EnemyBullet>();
            _bulletImageList = bulletImageList;
            _alienStartXPositions = new double[55];
            _alienStartYPositions = new double[55];
            for (int imageIndex = 0; imageIndex < 55; imageIndex++)
            {
                _alienStartXPositions[imageIndex] = Canvas.GetLeft(_alienImageList[imageIndex]);
                _alienStartYPositions[imageIndex] = Canvas.GetTop(_alienImageList[imageIndex]);
            }
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
                int count = 0;
                foreach (Alien alien in _alienList)
                {
                    if (alien != null)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        public async void Play()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile savedGame = null;
            try
            {
                savedGame = await storageFolder.GetFileAsync("game.txt");
            }
            catch (FileNotFoundException)
            {
                savedGame = await storageFolder.CreateFileAsync("game.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(savedGame, "0");
            }
            finally
            {
                IList<String> fileLines = await FileIO.ReadLinesAsync(savedGame);
                if (fileLines[0] == "1")
                {
                    for (int fileIndex = 1; fileIndex <= _alienImageList.Count; fileIndex++)
                    {
                        if (fileLines[fileIndex] != "Null")
                        {
                            _alienList.Add(new Alien(100, _alienImageList[fileIndex - 1], 100));
                            string[] alienInfo = fileLines[fileIndex].Split(' ');
                            _alienList[fileIndex - 1].XPosition = double.Parse(alienInfo[0]);
                            Canvas.SetLeft(_alienImageList[fileIndex - 1], _alienList[fileIndex - 1].XPosition);
                            _alienList[fileIndex - 1].YPosition = double.Parse(alienInfo[1]);
                            Canvas.SetTop(_alienImageList[fileIndex - 1], _alienList[fileIndex - 1].YPosition);
                            _alienList[fileIndex - 1].Speed = double.Parse(alienInfo[2]);
                            _alienList[fileIndex - 1].Points = int.Parse(alienInfo[3]);
                            _alienList[fileIndex - 1].Direction = (Direction)int.Parse(alienInfo[4]);


                        }
                        else
                        {
                            _alienList.Add(new Alien(100, _alienImageList[fileIndex - 1], 100));
                            DespawnAliens(fileIndex - 1);
                        }

                    }
                    _player.Lives = byte.Parse(fileLines[56]);
                    _gameScore = int.Parse(fileLines[57]);
                    _time = int.Parse(fileLines[58]);
                    _round = byte.Parse(fileLines[59]);
                    _player.Position = double.Parse(fileLines[60]);
                    _player.Speed = double.Parse(fileLines[61]);
                    Canvas.SetLeft(_player.UiPlayer, double.Parse(fileLines[60])); 
                }
                else
                {
                    for (int alienImageIndex = 0; alienImageIndex < _alienImageList.Count; alienImageIndex++)
                    {
                        if (_difficulty == GameDifficulty.Beginner)
                        {
                            _alienList.Add(new Alien(1, _alienImageList[alienImageIndex], 500));
                            _player.Speed = 0.80;
                        }
                        else if (_difficulty == GameDifficulty.Easy)
                        {
                            _alienList.Add(new Alien(1.25, _alienImageList[alienImageIndex], 250));
                            _player.Speed = 0.75;
                        }
                        else if (_difficulty == GameDifficulty.Normal)
                        {
                            _alienList.Add(new Alien(1.5, _alienImageList[alienImageIndex], 250));
                            _player.Speed = 0.50;
                        }
                        else if (_difficulty == GameDifficulty.Hard)
                        {
                            _alienList.Add(new Alien(1.75, _alienImageList[alienImageIndex], 100));
                            _player.Speed = 0.25;
                        }
                        else
                        {
                            _alienList.Add(new Alien(2, _alienImageList[alienImageIndex], 100));
                            _player.Speed = 0.25;
                        }
                    }
                    //Give three random aliens a bullet from the list to start.
                    //GiveBullet();
                    
                }
            }
        }

        public void End()
        {
            //
        }

        public int UpdateScore(int addedScore)
        {
            _gameScore += addedScore;
            return _gameScore;
        }

        public byte ResetRound()
        {
            _alienList = new List<Alien>();
            for (int alienImageIndex = 0; alienImageIndex < _alienImageList.Count; alienImageIndex++)
            {
                if (_difficulty == GameDifficulty.Beginner)
                {
                    _alienImageList[alienImageIndex].Visibility = Visibility.Visible;
                    _alienList.Add(new Alien(1, _alienImageList[alienImageIndex], 500));
                    Canvas.SetLeft(_alienList[alienImageIndex].UiAlien, _alienStartXPositions[alienImageIndex]);
                    Canvas.SetTop(_alienList[alienImageIndex].UiAlien, _alienStartYPositions[alienImageIndex]);
                    _alienList[alienImageIndex].XPosition = Canvas.GetLeft(_alienList[alienImageIndex].UiAlien);
                    _alienList[alienImageIndex].YPosition = Canvas.GetTop(_alienList[alienImageIndex].UiAlien);
                }
            }
            //Give three random aliens a bullet from the list to start.
            //GiveBullet();
            _player.Reset();
            _round += 1;
            return _round;
        }

        public double IncreaseSpeed()
        {
            if (AlienCount <= 30 && AlienCount > 15)
            {
                return 75;
            }
            else if (AlienCount <= 15 && AlienCount > 10)
            {
                return 50;
            }
            else if (AlienCount <= 10 && AlienCount > 2)
            {
                return 10;
            }
            else if (AlienCount == 2)
            {
                return 5;
            }
            else if (AlienCount == 1)
            {
                return 0.5;
            }
            else
            {
                return 100;
            }
        }

        public int DespawnAliens(int alienNum)
        {
            //Pop the alien object out of the list.
            Alien selectedAlien = _alienList[alienNum];
            //Move the alien offscreen.
            Canvas.SetLeft(selectedAlien.UiAlien, 0);
            Canvas.SetTop(selectedAlien.UiAlien, 0);
            //Set the alien visibility to false.
            selectedAlien.UiAlien.Visibility = Visibility.Collapsed;
            //Add a null refernce to the list.
            _alienList[alienNum] = null;
            //Destroy the Alien Object.
            return selectedAlien.Points;
        }

        public void ShiftAliens()
        {
            Alien aliveAlien = null;
            foreach (Alien alien in _alienList)
            {
                if (alien != null)
                {
                    aliveAlien = alien;
                    break;
                }
            }
            if (aliveAlien == null)
            {
                return;
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
            //Save the list of objects into the file.
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile saveFile = await folder.GetFileAsync("game.txt");
            List<String> saveItems = new List<String>();
            saveItems.Add("1");
            foreach (Alien alien in _alienList)
            {
                if (alien != null)
                {
                    int direction = (int)Enum.Parse(typeof(Direction), alien.Direction.ToString());
                    saveItems.Add(alien.XPosition.ToString() + " " + alien.YPosition.ToString() + " " + alien.Speed.ToString() + " " + alien.Points.ToString() + " " + direction.ToString());
                }
                else
                {
                    //This alien does not exist. Set it to default.
                    saveItems.Add("Null");
                }
            }
            saveItems.Add(_player.Lives.ToString());
            saveItems.Add(_gameScore.ToString());
            saveItems.Add(_time.ToString());
            saveItems.Add(_round.ToString());
            saveItems.Add(_player.Position.ToString());
            saveItems.Add(_player.Speed.ToString());
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

        public void GiveBullet()
        {
            Alien[] selectedAliens = new Alien[11];
            Alien bottomAlien = _alienList[0];
            for (int rowIndex = 0; rowIndex < 11; rowIndex++)
            {
                for (int columnIndex = rowIndex; columnIndex < (rowIndex + 55); columnIndex += 11)
                {
                    if (_alienList[columnIndex].YPosition > bottomAlien.YPosition)
                    {
                        selectedAliens[rowIndex] = _alienList[columnIndex];
                    }
                }
            }
            byte bulletCount = 0;
            while (bulletCount < 3)
            {
                int index = _randomizer.Next(0, 10);
                if (selectedAliens[index].EnemyBullet == null)
                {
                    _bulletList.Add(new EnemyBullet(0, 0, _bulletImageList[bulletCount]));
                    //TODO: Make the _enemyBullet available.
                    selectedAliens[index].EnemyBullet = _bulletList[bulletCount];
                    bulletCount++;
                }
            }
        }
    }
}
