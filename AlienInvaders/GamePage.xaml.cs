using System;
using System.Collections.Generic;
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

        //TODO: REMOVE TEST VARIABLE:
        private Game _game;

        public GamePage()
        {
            this.InitializeComponent();
            //TODO: MUST CHANGE VALUES FOR TIMER.
            _playerMoveTimer = new DispatcherTimer();
            _playerMoveTimer.Tick += OnPlayerMoveTimerTick;
            _playerMoveTimer.Interval = TimeSpan.FromMilliseconds(100);

            _alienMoveTimer = new DispatcherTimer();
            _alienMoveTimer.Tick += OnAlienMoveTimerTick;
            _alienMoveTimer.Interval = TimeSpan.FromMilliseconds(100);

            _bulletMoveTimer = new DispatcherTimer();
            _bulletMoveTimer.Tick += OnBulletMoveTimerTick;
            _bulletMoveTimer.Interval = TimeSpan.FromMilliseconds(100);

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

            //TODO: REMOVE. THIS IS FOR TESTING PURPOSES.
            _game = new Game(GameDifficulty.Beginner, Color.Green, 1, _imgPlayer);
        }

        private void OnClockTimerTick(object sender, object e)
        {
            throw new NotImplementedException();
        }

        private void OnEnemyBulletMoveTimerTick(object sender, object e)
        {
            throw new NotImplementedException();
        }

        private void OnShipMoveTimerTick(object sender, object e)
        {
            throw new NotImplementedException();
        }

        private void OnBulletMoveTimerTick(object sender, object e)
        {
            throw new NotImplementedException();
        }

        private void OnAlienMoveTimerTick(object sender, object e)
        {
            //TODO: IMPLEMENT.
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
        }

        private void OnFireClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnPauseClicked(object sender, RoutedEventArgs e)
        {
            if (_btnPause.Content == "Pause")
            {
                _btnPause.Content = "Resume";
                _alienMoveTimer.Stop();
                _clockTimer.Stop();
                _shipMoveTimer.Stop();
                _btnSave.Visibility = Visibility.Visible;
            }
            else
            {
                _btnPause.Content = "Pause";
                _alienMoveTimer.Start();
                _clockTimer.Start();
                _shipMoveTimer.Start();
                _btnSave.Visibility = Visibility.Collapsed;
            }
            
        }
    }
}
