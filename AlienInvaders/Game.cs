using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
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
        public Game(GameDifficulty difficulty, Color colorOption, byte imageOption, Image playerImage, Image bulletImage, List<Image> alienImageList, List<Image> shieldList)
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
            //_bulletList = new List<EnemyBullet>();
            _alienList = new List<Alien>();
            //TODO: FIX WIDTH TO CANVAS.ACTUALWIDTH.
            _motherShip = new MotherShip(720, 0.25,_randomizer);
            _shieldList = new List<Shield>();
            _difficulty = difficulty;
            _alienImageList = alienImageList;
            _bulletList = new List<EnemyBullet>();
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
                        if (fileLines[fileIndex] != "0 0 0 0 Right")
                        {
                            //TODO: FIX THIS IMMEDIATELY
                            _alienList.Add(new Alien(100, _alienImageList[fileIndex - 1], 100));

                            _alienList[fileIndex - 1].XPosition = double.Parse(fileLines[fileIndex][0].ToString());
                            Canvas.SetLeft(_alienImageList[fileIndex - 1], _alienList[fileIndex - 1].XPosition);
                            _alienList[fileIndex - 1].YPosition = double.Parse(fileLines[fileIndex][2].ToString());
                            Canvas.SetTop(_alienImageList[fileIndex - 1], _alienList[fileIndex - 1].YPosition);
                            _alienList[fileIndex - 1].Speed = double.Parse(fileLines[fileIndex][4].ToString());
                            _alienList[fileIndex - 1].Points = int.Parse(fileLines[fileIndex][6].ToString());
                            //TODO: FIGURE OUT CODE TO SET ENUMERATION NUMBER.
                            //_alienList[fileIndex - 1].Direction = fileLines[fileIndex][8].ToString();


                        }
                        else
                        {
                            _alienList.Add(new Alien(100, _alienImageList[fileIndex - 1], 100));
                            //DespawnAliens(fileIndex - 1);
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
                        if (_difficulty == GameDifficulty.Beginner)
                        {
                            _alienList.Add(new Alien(1, _alienImageList[alienImageIndex], 500));
                        }
                    }
                    //Give three random aliens a bullet from the list to start.

                    for (int alienCount = 0; alienCount < 3; alienCount++)
                    {
                        _bulletList.Add(new EnemyBullet(0, 0, ));
                        //TODO: Make the _enemyBullet available.
                        Alien selectedAlien = GiveBullet();
                        selectedAlien.bullet = _bulletList[alienCount];

                    }
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
                //TODO: FIX ENEMY BULLET.
                //_bulletList.Add(new EnemyBullet(1.0, 1.0, new Image()));
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

        public int DespawnAliens(int alienNum)
        {
            //Pop the alien object out of the list.
            Alien selectedAlien = _alienList[alienNum];
            //Move the alien offscreen.
            //Canvas.SetLeft(selectedAlien._imgAlien, 0);
            //Canvas.SetTop(selectedAlien._imgAlien, 0);
            //Set the alien visibility to false.
            //selectedAlien._imgAlien.Visibility = Visibility.Collapsed;
            //TODO: IMPLEMENT.
            //Add a null refernce to the list.
            _alienList[alienNum] = null;
            //Destroy the Alien Object.
                foreach (Alien alien in _alienList)
                {
                    if (alien != null)
                    {
                        return 0;
                    }

                }
            
            ResetRound();
            //return selectedAlien.Points;
            return 0;
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
                    saveItems.Add(alien.XPosition.ToString() + " " + alien.YPosition.ToString() + " " + alien.Speed.ToString() + " " + alien.Points.ToString() + " " + alien.Direction.ToString());
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

        public Alien GiveBullet()
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
            byte bulletsLeft = 3;
            while (bulletsLeft > 0)
            {
                int index = _randomizer.Next(0, 10);
                if (selectedAliens[index].Enemy)
            }
        }
    }
}
