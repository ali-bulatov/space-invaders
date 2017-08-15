﻿using System;
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

        /// <summary>
        /// Represents the list of shields that the game has.
        /// </summary>
        private List<Shield> _shieldList;

        /// <summary>
        /// Represents the difficulty of the game.
        /// </summary>
        private GameDifficulty _difficulty;

        /// <summary>
        /// Represents the color option selected for the player to use.
        /// </summary>
        private Color _colorOption;

        /// <summary>
        /// Represents the tank type for the player to use.
        /// </summary>
        private byte _imageOption;

        /// <summary>
        /// Represents the list of all alien images for all 55 aliens.
        /// </summary>
        private List<Image> _alienImageList;

        /// <summary>
        /// Represents the starting x positions of the aliens.
        /// </summary>
        private double[] _alienStartXPositions;

        /// <summary>
        /// Represents the starting y positions of the aliens.
        /// </summary>
        private double[] _alienStartYPositions;
        
        /// <summary>
        ///Represents the constructor of the game being played.
        /// </summary>
        /// <param name="difficulty">Represents game difficulty</param>
        /// <param name="colorOption">Represents color option.</param>
        /// <param name="imageOption">Represets tank type.</param>
        /// <param name="playerImage">Represents the image of the player.</param>
        /// <param name="bulletImage">Represents the player bullet image.</param>
        /// <param name="alienImageList">Represents the list of alien images.</param>
        /// <param name="shieldList">Represents the list of shields.</param>
        /// <param name="bulletImageList">Represents the list of enemy bullet images.</param>
        /// <param name="motherShipImage">Represents the mothership image.</param>
        public Game(GameDifficulty difficulty, Color colorOption, byte imageOption, Image playerImage, Image bulletImage, List<Image> alienImageList, List<Image> shieldList, List<Image> bulletImageList, Image motherShipImage)
        {
            //Load the basic assets of the game before playing or resuming.
            _gameScore = 0;
            _randomizer = new Random();
            _round = 1;
            _time = 0;
            //Set the player color 
            _colorOption = colorOption;
            _imageOption = imageOption;
            //Set the difficulty.
            _difficulty = difficulty;
            //Create a new player.
            _player = new Player(_colorOption, _imageOption, playerImage, bulletImage, 0.25);
            //Add a list of aliens.
            _alienList = new List<Alien>();
            //Create a new mothership.
            _motherShip = new MotherShip(720, 0.25,_randomizer, motherShipImage);
            //Create a new list of shields.
            _shieldList = new List<Shield>();
            //Get the images of the aliens,
            _alienImageList = alienImageList;
            //Create a new bullet list and add 3 enemy bullets.
            _bulletList = new List<EnemyBullet>();
            _bulletList.Add(new EnemyBullet(0, 0, bulletImageList[0]));
            _bulletList.Add(new EnemyBullet(0, 0, bulletImageList[1]));
            _bulletList.Add(new EnemyBullet(0, 0, bulletImageList[2]));
            //Set the start x positions and y positions.
            _alienStartXPositions = new double[55];
            _alienStartYPositions = new double[55];
            //Go through each alien and get the starting positions for them for each of hte 55 aliens.
            for (int imageIndex = 0; imageIndex < 55; imageIndex++)
            {
                //Get the initial starting point of hte image.
                _alienStartXPositions[imageIndex] = Canvas.GetLeft(_alienImageList[imageIndex]);
                _alienStartYPositions[imageIndex] = Canvas.GetTop(_alienImageList[imageIndex]);
            }
        }

        /// <summary>
        /// Represents a read only property to return the game score.
        /// </summary>
        public int GameScore
        {
            get
            {
                //Return the score.
                return _gameScore;
            }
        }

        /// <summary>
        /// Represents a property that will get and set the time of the game.
        /// </summary>
        public int Time
        {
            get
            {
                //Return the time.
                return _time;
            }
            set
            {
                //Set the time.
                _time = value;
            }
        }
        /// <summary>
        /// Represents a read-only property that will get and set hte round number.
        /// </summary>
        public byte Round
        {
            get
            {
                //Return the round number.
                return _round;
            }
        }

        /// <summary>
        /// Represents a property to get and set the player object.
        /// </summary>
        public Player Player
        {
            get
            {
                //Return the player.
                return _player;
            }
            set
            {
                //Set the player.
                _player = value;
            }
        }

        /// <summary>
        /// Represents a read-only property that will get the randomizer.
        /// </summary>
        public Random Randomizer
        {
            get
            {
                //Return the randomizer.
                return _randomizer;
            }
        }
        /// <summary>
        /// Represents a read-only property to get and set the mothership.
        /// </summary>
        public MotherShip MotherShip
        {
            get
            {
                //Return the mothership.
                return _motherShip;
            }
        }
        /// <summary>
        /// Represents a property to set and get the list of enemy bullets.
        /// </summary>
        public List<EnemyBullet> BulletList
        {
            get
            {
                //Return the 3 bullets.
                return _bulletList;
            }
            set
            {
                //Set the bullets.
                _bulletList = value;
            }
        }
        
        /// <summary>
        /// Represnets a read-only property to get the number of aliens remaining.
        /// </summary>
        public int AlienCount
        {
            get
            {
                //Set the initial count to 0.
                int count = 0;
                //Go through each alien in hte list.
                foreach (Alien alien in _alienList)
                {
                    //Check to see if the alien exists.
                    if (alien != null)
                    {
                        //Add the count.
                        count++;
                    }
                }
                //Return the count.
                return count;
            }
        }
        /// <summary>
        /// Represents a read-only property to get the number of alien columns.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                //Set the initial column count to 0.
                int columnCount = 0;
                //Go through each alien column for all 11 columns.
                for (int columnIndex = 0; columnIndex < 11; columnIndex++)
                {
                    //Go through each alien cell depending on the column for all 5 aliens in the column.
                    for (int rowIndex = columnIndex; rowIndex < (columnIndex + 55); rowIndex += 11)
                    {
                        //Check to see if the alien exists in the column.
                        if (_alienList[rowIndex] != null)
                        {
                            //Add the number of columns by one.
                            columnCount += 1;
                            //Go to the next column.
                            break;
                        }
                    }
                }
                //Return the column count.
                return columnCount;
            }
        }
        /// <summary>
        /// Represents a method that will start up the game and load previously stored data if any.
        /// </summary>
        public async void Play()
        {
            //Get the local folder.
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //Set the saved game file to null.
            StorageFile savedGame = null;
            try
            {
                //Try to get the saved game.
                savedGame = await storageFolder.GetFileAsync("game.txt");
            }
            catch (FileNotFoundException)
            {
                //Create the file.
                savedGame = await storageFolder.CreateFileAsync("game.txt", CreationCollisionOption.ReplaceExisting);
                //Write 0, indicating that there is no game.
                await FileIO.WriteTextAsync(savedGame, "0");
            }
            finally
            {
                //Read the list of lines from the file.
                IList<String> fileLines = await FileIO.ReadLinesAsync(savedGame);
                //Check to see if there is a game going on.
                if (fileLines[0] == "1")
                {
                    //Go through each line for lines 1 and 55.
                    for (int fileIndex = 1; fileIndex <= _alienImageList.Count; fileIndex++)
                    {
                        //Check to see if the alien exists or not.
                        if (fileLines[fileIndex] != "Null")
                        {
                            //Add a new alien.
                            _alienList.Add(new Alien(100, _alienImageList[fileIndex - 1], 100));
                            //Split the information of the alien by the space character.
                            string[] alienInfo = fileLines[fileIndex].Split(' ');
                            //Set the x position of the alien.
                            _alienList[fileIndex - 1].XPosition = double.Parse(alienInfo[0]);
                            Canvas.SetLeft(_alienImageList[fileIndex - 1], _alienList[fileIndex - 1].XPosition);
                            //Set the y position of the alien,
                            _alienList[fileIndex - 1].YPosition = double.Parse(alienInfo[1]);
                            Canvas.SetTop(_alienImageList[fileIndex - 1], _alienList[fileIndex - 1].YPosition);
                            //Set the speed of the alien.
                            _alienList[fileIndex - 1].Speed = double.Parse(alienInfo[2]);
                            //Set the points of the alien.
                            _alienList[fileIndex - 1].Points = int.Parse(alienInfo[3]);
                            //Set the direction of the alien.
                            _alienList[fileIndex - 1].Direction = (Direction)int.Parse(alienInfo[4]);


                        }
                        //If the alien does not exist.
                        else
                        {
                            //Despawn the alien.
                            _alienList.Add(new Alien(100, _alienImageList[fileIndex - 1], 100));
                            DespawnAliens(fileIndex - 1);
                        }

                    }
                    //Set the player lives.
                    _player.Lives = byte.Parse(fileLines[56]);
                    //Set the game score.
                    _gameScore = int.Parse(fileLines[57]);
                    //Set the time.
                    _time = int.Parse(fileLines[58]);
                    //Set the round number.
                    _round = byte.Parse(fileLines[59]);
                    //Set the player position.
                    _player.Position = double.Parse(fileLines[60]);
                    //Set the player speed.
                    _player.Speed = double.Parse(fileLines[61]);
                    //Set player position.
                    Canvas.SetLeft(_player.UiPlayer, double.Parse(fileLines[60])); 
                }
                //Otherwise, if no game was saved.
                else
                {
                    //Go through and set the player speed and spawn a new alien for all 55 aliens.
                    for (int alienImageIndex = 0; alienImageIndex < _alienImageList.Count; alienImageIndex++)
                    {
                        //Check to see if the difficulty level is 1.
                        if (_difficulty == GameDifficulty.Beginner)
                        {
                            //Add a new alien. Set the player speed.
                            _alienList.Add(new Alien(1, _alienImageList[alienImageIndex], 500));
                            _player.Speed = 0.80;
                        }
                        //Check to see if the difficulty level is 2.
                        else if (_difficulty == GameDifficulty.Easy)
                        {
                            //Add a new alien. Set the player speed.
                            _alienList.Add(new Alien(1.25, _alienImageList[alienImageIndex], 250));
                            _player.Speed = 0.75;
                        }
                        //Check to see if the difficulty level is 3.
                        else if (_difficulty == GameDifficulty.Normal)
                        {
                            //Add a new alien. Set the player speed.
                            _alienList.Add(new Alien(1.5, _alienImageList[alienImageIndex], 250));
                            _player.Speed = 0.50;
                        }
                        //Check to see if the difficulty level is 4.
                        else if (_difficulty == GameDifficulty.Hard)
                        {
                            //Add a new alien. Set the player speed.
                            _alienList.Add(new Alien(1.75, _alienImageList[alienImageIndex], 100));
                            _player.Speed = 0.25;
                        }
                        //Check to see if the difficulty level is 5.
                        else
                        {
                            //Add a new alien. Set the player speed.
                            _alienList.Add(new Alien(2, _alienImageList[alienImageIndex], 100));
                            _player.Speed = 0.25;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Represents a method that will add the score to the game.
        /// </summary>
        /// <param name="addedScore">Represents the score to add.</param>
        /// <returns>Represents the new game score to return.</returns>
        public int UpdateScore(int addedScore)
        {
            //Add the score and return it.
            _gameScore += addedScore;
            return _gameScore;
        }

        /// <summary>
        /// Represents a method that will reset the round and reset player and alien positions.
        /// </summary>
        /// <returns>Represents the new round number.</returns>
        public byte ResetRound()
        {
            //Create a new list of aliens.
            _alienList = new List<Alien>();
            //Go through each alien and create a new alien and set the position of the alien for all 55 aliens.
            for (int alienImageIndex = 0; alienImageIndex < _alienImageList.Count; alienImageIndex++)
            {
                //Set the alien image visibility to true.
                _alienImageList[alienImageIndex].Visibility = Visibility.Visible;
                //Check to see if beginner dificulty.
                if (_difficulty == GameDifficulty.Beginner)
                {
                    //Add the alien.
                    _alienList.Add(new Alien(1, _alienImageList[alienImageIndex], 500));
                }
                //Check to see if easy dificulty.
                else if (_difficulty == GameDifficulty.Easy)
                {
                    //Add the alien.
                    _alienList.Add(new Alien(1.25, _alienImageList[alienImageIndex], 500));
                }
                //Check to see if normal dificulty.
                else if (_difficulty == GameDifficulty.Normal)
                {
                    //Add the alien.
                    _alienList.Add(new Alien(1.5, _alienImageList[alienImageIndex], 500));
                }
                //Check to see if hard dificulty.
                else if (_difficulty == GameDifficulty.Hard)
                {
                    //Add the alien.
                    _alienList.Add(new Alien(1.75, _alienImageList[alienImageIndex], 500));
                }
                //Check to see if intense dificulty.
                else
                {
                    //Add the alien.
                    _alienList.Add(new Alien(2, _alienImageList[alienImageIndex], 500));
                }
                //Set the default position of the given alien.
                Canvas.SetLeft(_alienList[alienImageIndex].UiAlien, _alienStartXPositions[alienImageIndex]);
                Canvas.SetTop(_alienList[alienImageIndex].UiAlien, _alienStartYPositions[alienImageIndex]);
                _alienList[alienImageIndex].XPosition = Canvas.GetLeft(_alienList[alienImageIndex].UiAlien);
                _alienList[alienImageIndex].YPosition = Canvas.GetTop(_alienList[alienImageIndex].UiAlien);
            }
            //Reset the player position.
            _player.Reset();
            //Incremenet round number and return number.
            _round += 1;
            return _round;
        }

        /// <summary>
        /// Represents a method to increase the speed of the aliens depending on how many are left.
        /// </summary>
        /// <returns>Represents the new alien timer speed.</returns>
        public double IncreaseSpeed()
        {
            //Check to see if there are 30 aliens left.
            if (AlienCount <= 30 && AlienCount > 15)
            {
                //Return speed of 75.
                return 75;
            }
            //Check to see if there are 15 aliens left.
            else if (AlienCount <= 15 && AlienCount > 10)
            {
                //Return speed 50.
                return 50;
            }
            //Check to see if there are 10 aliens left.
            else if (AlienCount <= 10 && AlienCount > 2)
            {
                //Return speed 10.
                return 10;
            }
            //Check to see if there are 2 aliens left.
            else if (AlienCount == 2)
            {
                //Return speed 5
                return 5;
            }
            //Check to see if there is 1 alien left.
            else if (AlienCount == 1)
            {
                //Return speed 0.5.
                return 0.5;
            }
            //Check if all aliens are alive.
            else
            {
                //Return normal speed.
                return 100;
            }
        }

        /// <summary>
        /// Represents a method that will despawn a given alien at a given index.
        /// </summary>
        /// <param name="alienNum">Represents the alien index.</param>
        /// <returns>Represents the number of points of the alien.</returns>
        public int DespawnAliens(int alienNum)
        {
            //Pop the alien object out of the list.
            Alien selectedAlien = _alienList[alienNum];
            //Move the alien offscreen.
            Canvas.SetLeft(selectedAlien.UiAlien, 0);
            Canvas.SetTop(selectedAlien.UiAlien, 0);
            //Set the alien visibility to false.
            selectedAlien.UiAlien.Visibility = Visibility.Collapsed;
            //Remove the enemy bullet.
            selectedAlien.EnemyBullet = null;
            //Add a null refernce to the list.
            _alienList[alienNum] = null;
            //Return the points.
            return selectedAlien.Points;
        }

        /// <summary>
        /// Represnets a method that will move all aliens horizontally and check to see if they hit the side of the screen.
        /// </summary>
        public void ShiftAliens()
        {
            //Set an alive alien as reference to check which alien is moving.
            Alien aliveAlien = null;
            //Go through 
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
                _player.CanShoot = false;
            }
            else
            {
                _player.CanMove = true;
                _player.CanShoot = true;
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

        public void GiveBullet(int bulletIndex)
        {
            Alien[] selectedAliens = new Alien[11];
            Alien bottomAlien;
            for (int rowIndex = 0; rowIndex < 11; rowIndex++)
            {
                bottomAlien = _alienList[rowIndex];
                for (int columnIndex = rowIndex; columnIndex < (rowIndex + 55); columnIndex += 11)
                {
                    if (_alienList[columnIndex] != null)
                    {
                        if (bottomAlien == null)
                        {
                            bottomAlien = _alienList[columnIndex];
                            continue;
                        }
                        if (_alienList[columnIndex].YPosition > bottomAlien.YPosition && _alienList[columnIndex].YPosition != bottomAlien.YPosition)
                        {
                            bottomAlien = _alienList[columnIndex];
                        }
                    }
                }
                selectedAliens[rowIndex] = bottomAlien;
            }
            EnemyBullet selectedBullet = _bulletList[bulletIndex];
            int index = _randomizer.Next(0, 11);
            while (true)
            {
                if (selectedAliens[index] != null)
                {
                    if (selectedAliens[index].EnemyBullet == null)
                    {
                        selectedAliens[index].EnemyBullet = selectedBullet;
                        selectedAliens[index].Shoot();
                        break;
                    }
                }
                index = _randomizer.Next(0, 11);
            }
            
        }

        public void TakeBullet (int index)
        {
            foreach (Alien alien in _alienList)
            {
                if (alien != null)
                {
                    if (alien.EnemyBullet == _bulletList[index])
                    {
                        alien.EnemyBullet = null;
                        break;
                    }
                }
            }
        }
    }
}
