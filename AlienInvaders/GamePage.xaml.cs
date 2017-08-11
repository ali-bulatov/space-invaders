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

        private DispatcherTimer _clockTimer;

        private ArcadeMachine _arcadeMachine;

        private List<Image> _imageList;

        private int[] _passedGameValues;

        //TODO: REMOVE TEST VARIABLE:
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
            _shipMoveTimer.Interval = TimeSpan.FromMilliseconds(100);

            _enemyBulletMoveTimer = new DispatcherTimer();
            _enemyBulletMoveTimer.Tick += OnEnemyBulletMoveTimerTick;
            _enemyBulletMoveTimer.Interval = TimeSpan.FromMilliseconds(100);

            _clockTimer = new DispatcherTimer();
            _clockTimer.Tick += OnClockTimerTick;
            _clockTimer.Interval = TimeSpan.FromMilliseconds(1000);

            //TODO: CHANGE TO GET THE ARCADE MACHINE FROM PREVIOUS PAGE.
            _arcadeMachine = new ArcadeMachine();
            _passedGameValues = new int[2];
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is int[])
            {
                _passedGameValues = (int[])e.Parameter;
                //TODO: REMOVE. THIS IS FOR TESTING PURPOSES.
            }
            else
            {
                Debug.Assert(false, "Incorrect Navigation.");
            }
            base.OnNavigatedTo(e);
            _passedGameValues[1] -= 1;
            _imageList = new List<Image> { _imgAlien, _imgAlien1, _imgAlien2, _imgAlien3, _imgAlien4, _imgAlien5, _imgAlien6, _imgAlien7, _imgAlien8, _imgAlien9, _imgAlien10, _imgAlien11, _imgAlien12, _imgAlien13, _imgAlien14, _imgAlien15, _imgAlien16, _imgAlien17, _imgAlien18, _imgAlien19, _imgAlien20, _imgAlien21, _imgAlien22, _imgAlien23, _imgAlien24, _imgAlien25, _imgAlien26, _imgAlien27, _imgAlien28, _imgAlien29, _imgAlien30, _imgAlien31, _imgAlien32, _imgAlien33, _imgAlien34, _imgAlien35, _imgAlien36, _imgAlien37, _imgAlien38, _imgAlien39, _imgAlien40, _imgAlien41, _imgAlien42, _imgAlien43, _imgAlien44, _imgAlien45, _imgAlien46, _imgAlien47, _imgAlien48, _imgAlien49, _imgAlien50, _imgAlien51, _imgAlien52, _imgAlien53, _imgAlien54 };
            List<Image> shieldList = new List<Image> { _imgShield, _imgShield1, _imgShield2, _imgShield3 };
            _game = new Game((GameDifficulty)_passedGameValues[0], (Color)_passedGameValues[1], 1 , _imgPlayer, _imgBullet, _imageList, shieldList);
            _game.Play();
            _alienMoveTimer.Start();
            _clockTimer.Start();
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
                int spawnNum = _game.Randomizer.Next(1, 25);
                if (spawnNum == 25)
                {
                    _shipMoveTimer.Start();
                }
            }

        }

        private void OnEnemyBulletMoveTimerTick(object sender, object e)
        {
            
        }

        private void OnShipMoveTimerTick(object sender, object e)
        {
            //Move the mothership
            _imgMotherShip.Visibility = Visibility.Visible;
            _game.MotherShip.Fly();
            //Check to see if the mothership hit the wall.
            //Set the visibility of the mothership to false.
            //Reset the position of the mothership.
            _game.MotherShip.ResetLocation();
            //Stop the timer.
            _shipMoveTimer.Stop();
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
                //newScore = _game.UpdateScore(addedscore);
                //Set the new score on display.
                //reset the bullet position
                //TODO: REMOVE THIS AND REPLACE WITH RESET POSITION.
                _game.Player.Bullet.ResetPosition();
                _bulletMoveTimer.Stop();
            }
            else if (target == 55)
            {
                //Do something with the mothership.
                //Reset the bullet position.
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
            _game.ShiftAliens();
            //Count the number of aliens in the list.
           // _alienMoveTimer.Interval = TimeSpan.FromMilliseconds(_game.IncreaseSpeed(_game.AlienCount));
        }

        private void OnPlayerMoveTimerTick(object sender, object e)
        {
            _game.Player.Move();
        }

        
        private void OnMoveClicked(object sender, RoutedEventArgs e)
        {
            if (sender == _btnLeft)
            {
                //TODO: USE DOTNOTATION WITH ARCADE MACHINE.
                if (_game.Player.Direction == Direction.Right)
                {
                    _game.Player.Direction = Direction.Left;
                    _playerMoveTimer.Start();
                }
                else
                {
                    _playerMoveTimer.Start();
                }
            }
            else
            {
                if (_game.Player.Direction == Direction.Left)
                {
                    _game.Player.Direction = Direction.Right;
                    _playerMoveTimer.Start();
                }
                else
                {
                    _playerMoveTimer.Start();
                }
            }
        }
        
        private void OnFireClicked(object sender, RoutedEventArgs e)
        {

        }
        
        private void OnPauseClicked(object sender, RoutedEventArgs e)
        {
            //TODO: FIX THIS.
            if (_btnPause.Content == "Pause")
            {
                _btnPause.Content = "Resume";
                _alienMoveTimer.Stop();
                _clockTimer.Stop();
                _shipMoveTimer.Stop();
                _btnSave.Visibility = Visibility.Visible;
                _game.Pause();
            }
            else
            {
                _btnPause.Content = "Pause";
                _alienMoveTimer.Start();
                _clockTimer.Start();
                _shipMoveTimer.Start();
                _btnSave.Visibility = Visibility.Collapsed;
                _game.Pause();
            }
            
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            //TODO: IMPLEMENT SAVING FUNCTIONALITY.
            //Call the save method of the Game.
            //Navigate Back to MainPage.
            _game.Save();
            this.Frame.GoBack();
        }
        //TODO: CHANGE THIS TO 
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
