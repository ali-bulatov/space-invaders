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
            //TODO: IMPLEMENT.
        }

        private void OnMoveClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnFireClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnPauseClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
