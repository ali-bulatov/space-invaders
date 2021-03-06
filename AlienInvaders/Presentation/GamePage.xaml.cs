﻿using AlienInvadersBuisnessLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AlienInvaders
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private DispatcherTimer _playerMoveTimer;

        private DispatcherTimer _alienMoveTimer;

        private DispatcherTimer _shipMoveTimer;

        private DispatcherTimer _bulletMoveTimer;

        private DispatcherTimer _enemyBulletMoveTimer;

        private DispatcherTimer _enemyBulletOneMoveTimer;

        private DispatcherTimer _enemyBulletTwoMoveTimer;

        private DispatcherTimer _clockTimer;

        private DispatcherTimer _enemyBulletSpawnTimer;

        private ArcadeMachine _arcadeMachine;

        private List<Image> _imageList;

        private int[] _passedGameValues;

        private Game _game;

        public GamePage()
        {
            this.InitializeComponent();
            //TODO: MUST CHANGE VALUES FOR TIMER.
            _playerMoveTimer = new DispatcherTimer();
            _playerMoveTimer.Tick += OnPlayerMoveTimerTick;
            _playerMoveTimer.Interval = TimeSpan.FromMilliseconds(0.25);

            _alienMoveTimer = new DispatcherTimer();
            _alienMoveTimer.Tick += OnAlienMoveTimerTick;
            _alienMoveTimer.Interval = TimeSpan.FromMilliseconds(100);

            _bulletMoveTimer = new DispatcherTimer();
            _bulletMoveTimer.Tick += OnBulletMoveTimerTick;
            _bulletMoveTimer.Interval = TimeSpan.FromMilliseconds(0.1);

            _shipMoveTimer = new DispatcherTimer();
            _shipMoveTimer.Tick += OnShipMoveTimerTick;
            _shipMoveTimer.Interval = TimeSpan.FromMilliseconds(0.25);

            _enemyBulletMoveTimer = new DispatcherTimer();
            _enemyBulletMoveTimer.Tick += OnEnemyBulletMoveTimerTick;
            _enemyBulletMoveTimer.Interval = TimeSpan.FromMilliseconds(1);

            _enemyBulletOneMoveTimer = new DispatcherTimer();
            _enemyBulletOneMoveTimer.Tick += OnEnemyBulletOneMoveTimerTick;
            _enemyBulletOneMoveTimer.Interval = TimeSpan.FromMilliseconds(1);

            _enemyBulletTwoMoveTimer = new DispatcherTimer();
            _enemyBulletTwoMoveTimer.Tick += OnEnemyBulletTwoMoveTimerTick;
            _enemyBulletTwoMoveTimer.Interval = TimeSpan.FromMilliseconds(1);

            _clockTimer = new DispatcherTimer();
            _clockTimer.Tick += OnClockTimerTick;
            _clockTimer.Interval = TimeSpan.FromMilliseconds(1000);

            _enemyBulletSpawnTimer = new DispatcherTimer();
            _enemyBulletSpawnTimer.Tick += OnEnemyBulletSpawnTimerTick;
            _enemyBulletSpawnTimer.Interval = TimeSpan.FromMilliseconds(1000);

            _arcadeMachine = new ArcadeMachine();
            _game = null;
            _passedGameValues = new int[2];
        }

        private void MoveEnemyBullet(int index)
        {
            bool isHit = _game.BulletList[index].Update(0.03f);
            if (isHit)
            {
                _game.BulletList[index].ResetPosition();
                if (index == 0)
                {
                    _enemyBulletMoveTimer.Stop();                  
                }
                else if (index == 1)
                {
                    _enemyBulletOneMoveTimer.Stop();
                }
                else
                {
                    _enemyBulletTwoMoveTimer.Stop();
                }
                _game.TakeBullet(index);
            }
            byte target = _game.BulletList[index].Collide(new List<Image> { _imgShield, _imgShield1, _imgShield2, _imgShield3 }, _imgPlayer);
            if (target == 0 || target <= 3)
            {
                //TODO: DO SOMETHING WITH THE SHIELD.
                //_enemyBulletOneMoveTimer.Stop();
            }
            else if (target == 4)
            {
                bool canContinue = _game.Player.OnDeath();
                if (canContinue)
                {
                    _game.BulletList[index].ResetPosition();
                    if (index == 0)
                    {
                        _enemyBulletMoveTimer.Stop();
                    }
                    else if (index == 1)
                    {
                        _enemyBulletOneMoveTimer.Stop();
                    }
                    else
                    {
                        _enemyBulletTwoMoveTimer.Stop();
                    }
                    _txtLives.Text = "Lives: " + _game.Player.Lives.ToString();
                    _game.TakeBullet(index);
                }
                else
                {
                    EndGame();
                }
            }
            else
            {
                //Do nothing and continue.
            }
        }

        private void EndGame()
        {
            Frame.Navigate(typeof(HighScore), _arcadeMachine);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is int[])
            {
                _passedGameValues = (int[])e.Parameter;
            }
            else
            {
                Debug.Assert(false, "Incorrect Navigation.");
            }
            base.OnNavigatedTo(e);
            _imageList = new List<Image> { _imgAlien, _imgAlien1, _imgAlien2, _imgAlien3, _imgAlien4, _imgAlien5, _imgAlien6, _imgAlien7, _imgAlien8, _imgAlien9, _imgAlien10, _imgAlien11, _imgAlien12, _imgAlien13, _imgAlien14, _imgAlien15, _imgAlien16, _imgAlien17, _imgAlien18, _imgAlien19, _imgAlien20, _imgAlien21, _imgAlien22, _imgAlien23, _imgAlien24, _imgAlien25, _imgAlien26, _imgAlien27, _imgAlien28, _imgAlien29, _imgAlien30, _imgAlien31, _imgAlien32, _imgAlien33, _imgAlien34, _imgAlien35, _imgAlien36, _imgAlien37, _imgAlien38, _imgAlien39, _imgAlien40, _imgAlien41, _imgAlien42, _imgAlien43, _imgAlien44, _imgAlien45, _imgAlien46, _imgAlien47, _imgAlien48, _imgAlien49, _imgAlien50, _imgAlien51, _imgAlien52, _imgAlien53, _imgAlien54 };
            List<Image> shieldList = new List<Image> { _imgShield, _imgShield1, _imgShield2, _imgShield3 };
            List<Image> bulletList = new List<Image> { _imgEnemyBullet, _imgEnemyBullet1, _imgEnemyBullet2 };
            _game = new Game((GameDifficulty)_passedGameValues[0], (Color)_passedGameValues[1], (byte)_passedGameValues[2], _imgPlayer, _imgBullet, _imageList, shieldList, bulletList, _imgMotherShip);
            _arcadeMachine.Game = _game;
            _game.Play();
            _alienMoveTimer.Start();
            _clockTimer.Start();
            _enemyBulletSpawnTimer.Start();
            _txtScore.Text = "Score: " + _game.GameScore.ToString();
            _txtLevel.Text = "Level: " + _game.Round.ToString();
            _txtLives.Text = "Lives: " + _game.Player.Lives.ToString();

        }
        private void OnClockTimerTick(object sender, object e)
        {
            _game.Time += 1;
            int seconds = _game.Time % 60;
            int minutes = _game.Time / 60;
            if (seconds < 10)
            {
                _txtTime.Text = $"Time: {minutes}:0{seconds}";
            }
            else
            {
                _txtTime.Text = $"Time: {minutes}:{seconds}";
            }

            if (_shipMoveTimer.IsEnabled == false)
            {
                bool canSpawn =_game.MotherShip.Spawn();
                if (canSpawn)
                {
                    _shipMoveTimer.Start();
                }
            }

        }

        private void OnEnemyBulletSpawnTimerTick(object sender, object e)
        {
            if (_game.ColumnCount >= 3)
            {
                if (_game.BulletList[0].IsAlive == false)
                {
                    _game.GiveBullet(0);
                    _enemyBulletMoveTimer.Start();
                    return;
                }
                else if (_game.BulletList[1].IsAlive == false)
                {
                    _game.GiveBullet(1);
                    _enemyBulletOneMoveTimer.Start();
                    return;
                }
                else if (_game.BulletList[2].IsAlive == false)
                {
                    _game.GiveBullet(2);
                    _enemyBulletTwoMoveTimer.Start();
                    return;
                }
                else
                {

                }
            }
            else if (_game.ColumnCount == 2)
            {
                if (_game.BulletList[0].IsAlive == false)
                {
                    _game.GiveBullet(0);
                    _enemyBulletMoveTimer.Start();
                    return;
                }
                else if (_game.BulletList[1].IsAlive == false)
                {
                    _game.GiveBullet(1);
                    _enemyBulletOneMoveTimer.Start();
                    return;
                }
                else
                {

                }
            }
            else if (_game.ColumnCount == 1)
            {
                if (_game.BulletList[0].IsAlive == false)
                {
                    _game.GiveBullet(0);
                    _enemyBulletMoveTimer.Start();
                    return;
                }
                else
                {

                }
            }
            else
            {

            }
        }

        private void OnEnemyBulletMoveTimerTick(object sender, object e)
        {
            MoveEnemyBullet(0);
        }

        private void OnEnemyBulletOneMoveTimerTick(object sender, object e)
        {
            MoveEnemyBullet(1);
        }

        private void OnEnemyBulletTwoMoveTimerTick(object sender, object e)
        {
            MoveEnemyBullet(2);
        }

        private void OnShipMoveTimerTick(object sender, object e)
        {
            //Move the mothership
            bool hasLanded = _game.MotherShip.Fly();
            //Check to see if the mothership hit the wall.
            //Set the visibility of the mothership to false.
            //Reset the position of the mothership.
            if (hasLanded)
            {
                _imgMotherShip.Visibility = Visibility.Collapsed;
                _shipMoveTimer.Stop();
            }
            //Stop the timer.
        }

        private void OnBulletMoveTimerTick(object sender, object e)
        {
            bool isHit = _game.Player.Bullet.Update(0.03f);
            if (isHit)
            {
                _game.Player.Bullet.ResetPosition();
                _bulletMoveTimer.Stop();
            }
            byte target = _game.Player.Bullet.Collide(_imageList, _imgMotherShip);
            if (target == 0 || target <= 54)
            {
                int addedscore = _game.DespawnAliens((int)target);
                int newScore = _game.UpdateScore(addedscore);
                //Set the new score on display.
                _txtScore.Text = "Score: " + newScore.ToString();
                _game.Player.Bullet.ResetPosition();
                _bulletMoveTimer.Stop();
            }
            else if (target == 55)
            {
                //Do something with the mothership.
                //Reset the bullet position.
                _imgMotherShip.Visibility = Visibility.Collapsed;
                _shipMoveTimer.Stop();
                _game.Player.Bullet.ResetPosition();
                _bulletMoveTimer.Stop();
            }
            else
            {
                //Do nothing and continue.
            }
        }

        private void OnAlienMoveTimerTick(object sender, object e)
        {
            if (_game.AlienCount > 0)
            {
                _game.ShiftAliens();
                _alienMoveTimer.Interval = TimeSpan.FromMilliseconds(_game.IncreaseSpeed());
            }
            else
            {
                byte newRound = _game.ResetRound();
                _txtLevel.Text = "Level: " + newRound.ToString();
            }
        }

        private void OnPlayerMoveTimerTick(object sender, object e)
        {
            _game.Player.Move();
        }
        
        private void OnPauseClicked(object sender, RoutedEventArgs e)
        {
            if (_btnPause.Content.ToString() == "Pause")
            {
                _btnPause.Content = "Resume";
                _alienMoveTimer.Stop();
                _clockTimer.Stop();
                _shipMoveTimer.Stop();
                _enemyBulletSpawnTimer.Stop();
                _enemyBulletMoveTimer.Stop();
                _enemyBulletOneMoveTimer.Stop();
                _enemyBulletTwoMoveTimer.Stop();
                _btnSave.Visibility = Visibility.Visible;
                _game.Pause();
            }
            else
            {
                _btnPause.Content = "Pause";
                _alienMoveTimer.Start();
                _clockTimer.Start();
                _shipMoveTimer.Start();
                _enemyBulletSpawnTimer.Start();
                _enemyBulletMoveTimer.Start();
                _enemyBulletOneMoveTimer.Start();
                _enemyBulletTwoMoveTimer.Start();
                _btnSave.Visibility = Visibility.Collapsed;
                _game.Pause();
            }
            
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            //Call the save method of the Game.
            //Navigate Back to MainPage.
            _game.Save();
            this.Frame.GoBack();
        }
        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            switch(e.Key)
            {
                case (Windows.System.VirtualKey.Right):
                    if (_game.Player.Direction == Direction.Left)
                    {
                        _game.Player.Direction = Direction.Right;
                        _playerMoveTimer.Start();
                    }
                    else
                    {
                        _playerMoveTimer.Start();
                    }
                    break;

                case (Windows.System.VirtualKey.Left):
                    if (_game.Player.Direction == Direction.Right)
                    {
                        _game.Player.Direction = Direction.Left;
                        _playerMoveTimer.Start();
                    }
                    else
                    {
                        _playerMoveTimer.Start();
                    }
                    break;

                case (Windows.System.VirtualKey.Up):
                    bool hasNotStart = _game.Player.OnShoot();
                    if (hasNotStart)
                    {
                        _bulletMoveTimer.Start();
                    }
                    break;
                
                default:
                    //Do nothing.
                    break;
            }
        }

        private void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case (Windows.System.VirtualKey.Right):
                    _playerMoveTimer.Stop();
                    break;

                case (Windows.System.VirtualKey.Left):
                    _playerMoveTimer.Stop();
                    break;

                default:
                    //Do nothing.
                    break;
            }
        }
    }
}
