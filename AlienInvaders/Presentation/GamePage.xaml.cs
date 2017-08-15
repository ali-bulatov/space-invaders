using AlienInvadersBuisnessLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
        /// <summary>
        /// Represents a timer that will move the player.
        /// </summary>
        private DispatcherTimer _playerMoveTimer;

        /// <summary>
        /// Represents a timer that will move all aliens.
        /// </summary>
        private DispatcherTimer _alienMoveTimer;

        /// <summary>
        /// Represents a timer that will move the Mothership.
        /// </summary>
        private DispatcherTimer _shipMoveTimer;

        /// <summary>
        /// Represnets a timer that will move the player bullet and check collision.
        /// </summary>
        private DispatcherTimer _bulletMoveTimer;

        /// <summary>
        /// Represents a timer that will move the first enemy bullet and check collision.
        /// </summary>
        private DispatcherTimer _enemyBulletMoveTimer;

        /// <summary>
        /// Represents a timer that will move the second enemy bullet and check collision.
        /// </summary>
        private DispatcherTimer _enemyBulletOneMoveTimer;

        /// <summary>
        /// Represents a timer that will move the third enemy bullet and check collision.
        /// </summary>
        private DispatcherTimer _enemyBulletTwoMoveTimer;

        /// <summary>
        /// Represents a timer that will count the time in the game and randomly check to spawn the mothership and update labels.
        /// </summary>
        private DispatcherTimer _clockTimer;

        /// <summary>
        /// Represents a timer that will spawn the enemy bullets to three aliens.
        /// </summary>
        private DispatcherTimer _enemyBulletSpawnTimer;

        /// <summary>
        /// Represents the arcade machine that contains the game and handles the score.
        /// </summary>
        private ArcadeMachine _arcadeMachine;

        /// <summary>
        /// Represents a list of alien images to be used.
        /// </summary>
        private List<Image> _imageList;

        /// <summary>
        /// Represents the values passed from MainPage to GamePage for the player's color, difficulty, and type.
        /// </summary>
        private int[] _passedGameValues;

        /// <summary>
        /// Represents the game itself.
        /// </summary>
        private Game _game;

        /// <summary>
        /// Represents the constructor of the GamePage.
        /// </summary>
        public GamePage()
        {
            //Initialize all components.
            this.InitializeComponent();
            //Setup the player move timer.
            _playerMoveTimer = new DispatcherTimer();
            _playerMoveTimer.Tick += OnPlayerMoveTimerTick;
            _playerMoveTimer.Interval = TimeSpan.FromMilliseconds(0.25);

            //Setup the alien move timer.
            _alienMoveTimer = new DispatcherTimer();
            _alienMoveTimer.Tick += OnAlienMoveTimerTick;
            _alienMoveTimer.Interval = TimeSpan.FromMilliseconds(100);

            //Setup the bullet move timer.
            _bulletMoveTimer = new DispatcherTimer();
            _bulletMoveTimer.Tick += OnBulletMoveTimerTick;
            _bulletMoveTimer.Interval = TimeSpan.FromMilliseconds(0.1);

            //Setup the mothership move timer.
            _shipMoveTimer = new DispatcherTimer();
            _shipMoveTimer.Tick += OnShipMoveTimerTick;
            _shipMoveTimer.Interval = TimeSpan.FromMilliseconds(0.25);

            //Setup each of the three enemy bullet move timers.
            _enemyBulletMoveTimer = new DispatcherTimer();
            _enemyBulletMoveTimer.Tick += OnEnemyBulletMoveTimerTick;
            _enemyBulletMoveTimer.Interval = TimeSpan.FromMilliseconds(1);

            _enemyBulletOneMoveTimer = new DispatcherTimer();
            _enemyBulletOneMoveTimer.Tick += OnEnemyBulletOneMoveTimerTick;
            _enemyBulletOneMoveTimer.Interval = TimeSpan.FromMilliseconds(1);

            _enemyBulletTwoMoveTimer = new DispatcherTimer();
            _enemyBulletTwoMoveTimer.Tick += OnEnemyBulletTwoMoveTimerTick;
            _enemyBulletTwoMoveTimer.Interval = TimeSpan.FromMilliseconds(1);

            //Setup the clock timer.
            _clockTimer = new DispatcherTimer();
            _clockTimer.Tick += OnClockTimerTick;
            _clockTimer.Interval = TimeSpan.FromMilliseconds(1000);

            //Setup the enemy bullet spawn timer.
            _enemyBulletSpawnTimer = new DispatcherTimer();
            _enemyBulletSpawnTimer.Tick += OnEnemyBulletSpawnTimerTick;
            _enemyBulletSpawnTimer.Interval = TimeSpan.FromMilliseconds(1000);

            //Set a new arcade machine.
            _arcadeMachine = new ArcadeMachine();
            //Set game and ImageList to null and set passedGameValues to a blank integer list.
            _game = null;
            _passedGameValues = null;
            _imageList = null;
        }

        /// <summary>
        /// Represents a method that will move an alien bullet depending on the index given for the bullet list.
        /// </summary>
        /// <param name="index">Represents the index number for getting the enemy bullet object.</param>
        private void MoveEnemyBullet(int index)
        {
            //Move the alien bullet at the index by calling update.
            bool isHit = _game.BulletList[index].Update(0.03f);
            //Check to see if the bullet has hit the end of the screen.
            if (isHit)
            {
                //Reset the position of the bullet.
                _game.BulletList[index].ResetPosition();
                //Check to see if it was the first bullet that landed.
                if (index == 0)
                {
                    //Stop the first enemy bullet timer.
                    _enemyBulletMoveTimer.Stop();                  
                }
                //Check to see if it was the second bullet that landed.
                else if (index == 1)
                {
                    //Stop the second enemy bullet timer.
                    _enemyBulletOneMoveTimer.Stop();
                }
                //Check to see if it was the third bullet that landed.
                else
                {
                    //Stop the third enemy bullet timer.
                    _enemyBulletTwoMoveTimer.Stop();
                }
                //Take the bullet away from the alien that had it initially.
                _game.TakeBullet(index);
            }
            //If it has not landed, check to see if it has collided with a target.
            byte target = _game.BulletList[index].Collide(new List<Image> { _imgShield, _imgShield1, _imgShield2, _imgShield3 }, _imgPlayer);
            //Check to see if it has hit one of the shields.
            if (target == 0 || target <= 3)
            {
                //TODO: DO SOMETHING WITH THE SHIELD.
                //_enemyBulletOneMoveTimer.Stop();
            }
            //Check to see if it hit the player.
            else if (target == 4)
            {
                //Check to see if the player has enough lives.
                bool canContinue = _game.Player.OnDeath();
                if (canContinue)
                {
                    //Reset the bullet to its position.
                    _game.BulletList[index].ResetPosition();
                    //Check to see if the first bullet hit the player.
                    if (index == 0)
                    {
                        //Stop the first enemy bullet timer.
                        _enemyBulletMoveTimer.Stop();
                    }
                    //Check to see if the second bullet hit the player.
                    else if (index == 1)
                    {
                        //Stop the second enemy bullet timer.
                        _enemyBulletOneMoveTimer.Stop();
                    }
                    //Check to see if the third bullet hit the player.
                    else
                    {
                        //Stop the first enemy bullet timer.
                        _enemyBulletTwoMoveTimer.Stop();
                    }
                    //Update the lives of the player.
                    _txtLives.Text = "Lives: " + _game.Player.Lives.ToString();
                    //take the bullet away from the alien that has it.
                    _game.TakeBullet(index);
                }
                //Check to see if the player has no lives.
                else
                {
                    //End the game.
                    EndGame();
                }
            }
            //Otherwise, just keep moving.
            else
            {
                //Do nothing and continue.
            }
        }

        /// <summary>
        /// Represents a method that will end the game and create a text file that indicates a new game.
        /// </summary>
        private async void EndGame()
        {
            //Access the local folder.
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            //Create game.txt
            StorageFile file = await folder.CreateFileAsync("game.txt", CreationCollisionOption.ReplaceExisting);
            //Write 0 to the file, indicating that there is no current game.
            FileIO.WriteTextAsync(file, "0");
            //Go to the highscore page.
            Frame.Navigate(typeof(HighScore), _arcadeMachine);
        }
        /// <summary>
        /// Represents a method for when this page is navigated to.
        /// </summary>
        /// <param name="e">Represents the data that the page recieves.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Check to see if it properly recieved the values.
            if (e.Parameter is int[])
            {
                //Get the difficulty, color option, and the tank type.
                _passedGameValues = (int[])e.Parameter;
            }
            else
            {
                //Otherwise, throw an error.
                Debug.Assert(false, "Incorrect Navigation.");
            }
            //Call the base method.
            base.OnNavigatedTo(e);
            //Set the image list to the list of all alien images.
            _imageList = new List<Image> { _imgAlien, _imgAlien1, _imgAlien2, _imgAlien3, _imgAlien4, _imgAlien5, _imgAlien6, _imgAlien7, _imgAlien8, _imgAlien9, _imgAlien10, _imgAlien11, _imgAlien12, _imgAlien13, _imgAlien14, _imgAlien15, _imgAlien16, _imgAlien17, _imgAlien18, _imgAlien19, _imgAlien20, _imgAlien21, _imgAlien22, _imgAlien23, _imgAlien24, _imgAlien25, _imgAlien26, _imgAlien27, _imgAlien28, _imgAlien29, _imgAlien30, _imgAlien31, _imgAlien32, _imgAlien33, _imgAlien34, _imgAlien35, _imgAlien36, _imgAlien37, _imgAlien38, _imgAlien39, _imgAlien40, _imgAlien41, _imgAlien42, _imgAlien43, _imgAlien44, _imgAlien45, _imgAlien46, _imgAlien47, _imgAlien48, _imgAlien49, _imgAlien50, _imgAlien51, _imgAlien52, _imgAlien53, _imgAlien54 };
            //Set the shieldList and bulletList to the list of shields and enemybullet images.
            List<Image> shieldList = new List<Image> { _imgShield, _imgShield1, _imgShield2, _imgShield3 };
            List<Image> bulletList = new List<Image> { _imgEnemyBullet, _imgEnemyBullet1, _imgEnemyBullet2 };
            //Create a new game with the values passed and the images.
            _game = new Game((GameDifficulty)_passedGameValues[0], (Color)_passedGameValues[1], (byte)_passedGameValues[2], _imgPlayer, _imgBullet, _imageList, shieldList, bulletList, _imgMotherShip);
            //Set the game of arcadeMachine = game.
            _arcadeMachine.Game = _game;
            //Initiate the game.
            _game.Play();
            //Start the alien move timer.
            _alienMoveTimer.Start();
            //Start the clock.
            _clockTimer.Start();
            //Start the enemy bullet spawn timer.
            _enemyBulletSpawnTimer.Start();

        }
        /// <summary>
        /// Represents an event handler method that will act as a clock to display the time and randomly update labels and check to see if the mothership is active or not.
        /// </summary>
        /// <param name="sender">Represents the timer object</param>
        /// <param name="e">Represents other parameters.</param>
        private void OnClockTimerTick(object sender, object e)
        {
            //Update the game time.
            _game.Time += 1;
            int seconds = _game.Time % 60;
            int minutes = _game.Time / 60;
            //Check to see if the seconds are less than 10.
            if (seconds < 10)
            {
                //Display the time.
                _txtTime.Text = $"Time: {minutes}:0{seconds}";
            }
            else
            {
                //Display the time.
                _txtTime.Text = $"Time: {minutes}:{seconds}";
            }
            //Check to see if the mothership move timer is enabled.
            if (_shipMoveTimer.IsEnabled == false)
            {
                //Check to see if the mothership chooses to spawn.
                bool canSpawn =_game.MotherShip.Spawn();
                if (canSpawn)
                {
                    //Start moving the mothership.
                    _shipMoveTimer.Start();
                }
            }
            //Update the score, level, and lives labels.
            _txtScore.Text = "Score: " + _game.GameScore.ToString();
            _txtLevel.Text = "Level: " + _game.Round.ToString();
            _txtLives.Text = "Lives: " + _game.Player.Lives.ToString();

        }
        /// <summary>
        /// Represents an event handler method that will start enemyBulletOneMoveTimer, enemyBulletMoveTimer, enemyBulletTwoMoveTimer depending on whether the enemy bullet has landed or not.
        /// </summary>
        /// <param name="sender">Represents the timer object</param>
        /// <param name="e">Represents other parameters.</param>
        private void OnEnemyBulletSpawnTimerTick(object sender, object e)
        {
            //Check to see if there are 3 alien columns left.
            if (_game.ColumnCount >= 3)
            {
                //Check to see if the first bullet is not moving.
                if (_game.BulletList[0].IsAlive == false)
                {
                    //Give the bullet to the alien and make it move.
                    _game.GiveBullet(0);
                    _enemyBulletMoveTimer.Start();
                    return;
                }
                //Check to see if the second bullet is not moving.
                else if (_game.BulletList[1].IsAlive == false)
                {
                    //Give the bullet to the alien and make it move.
                    _game.GiveBullet(1);
                    _enemyBulletOneMoveTimer.Start();
                    return;
                }
                //Check to see if the third bullet is not moving.
                else if (_game.BulletList[2].IsAlive == false)
                {
                    //Give the bullet to the alien and make it move.
                    _game.GiveBullet(2);
                    _enemyBulletTwoMoveTimer.Start();
                    return;
                }
                else
                {
                    //do nothing and continue.
                }
            }
            //Check to see if there are 2 alien columns left.
            else if (_game.ColumnCount == 2)
            {
                //Check to see if the first bullet is not moving.
                if (_game.BulletList[0].IsAlive == false)
                {
                    //Give the bullet to the alien and make it move.
                    _game.GiveBullet(0);
                    _enemyBulletMoveTimer.Start();
                    return;
                }
                //Check to see if the second bullet is not moving.
                else if (_game.BulletList[1].IsAlive == false)
                {
                    //Give the bullet to the alien and make it move.
                    _game.GiveBullet(1);
                    _enemyBulletOneMoveTimer.Start();
                    return;
                }
                else
                {
                    //Do nothing and continue.
                }
            }
            //Check to see if there is one column left.
            else if (_game.ColumnCount == 1)
            {
                //Check to see if the first bullet is not moving.
                if (_game.BulletList[0].IsAlive == false)
                {
                    //Give the bullet to the alien and make it move.
                    _game.GiveBullet(0);
                    _enemyBulletMoveTimer.Start();
                    return;
                }
                else
                {
                    //Do nothing and continue.
                }
            }
            else
            {
                //Do nothing and continue.
            }
        }

        /// <summary>
        /// Represents the timer to move the first enemy bullet.
        /// </summary>
        /// <param name="sender">Represents the timer object.</param>
        /// <param name="e">Represents other method parameters</param>
        private void OnEnemyBulletMoveTimerTick(object sender, object e)
        {
            //Move the first bullet at index 0.
            MoveEnemyBullet(0);
        }
        /// <summary>
        /// Represents the timer to move the second enemy bullet.
        /// </summary>
        /// <param name="sender">Represents the timer object.</param>
        /// <param name="e">Represents other method parameters</param>
        private void OnEnemyBulletOneMoveTimerTick(object sender, object e)
        {
            //Move the second bullet at index 1.
            MoveEnemyBullet(1);
        }

        /// <summary>
        /// Represents the timer to move the third enemy bullet.
        /// </summary>
        /// <param name="sender">Represents the timer object.</param>
        /// <param name="e">Represents other method parameters</param>
        private void OnEnemyBulletTwoMoveTimerTick(object sender, object e)
        {
            //Move the third bullet at index 2.
            MoveEnemyBullet(2);
        }

        /// <summary>
        /// Represents the timer to move the mothership across the screen.
        /// </summary>
        /// <param name="sender">Represents the timer object.</param>
        /// <param name="e">Represents other method parameters.</param>
        private void OnShipMoveTimerTick(object sender, object e)
        {
            //Move the mothership
            bool hasLanded = _game.MotherShip.Fly();
            //Check to see if the mothership hit the wall.
            if (hasLanded)
            {
                //Set the visibility of the mothership to false.
                _imgMotherShip.Visibility = Visibility.Collapsed;
                //Stop the timer.
                _shipMoveTimer.Stop();
            }
        }

        /// <summary>
        /// Represents a timer that will move the player's bullet and check collision.
        /// </summary>
        /// <param name="sender">Represents the timer object.</param>
        /// <param name="e">Represents other method parameters.</param>
        private void OnBulletMoveTimerTick(object sender, object e)
        {
            //Move the bullet and check to see if it hit the bottom of the screen.
            bool isHit = _game.Player.Bullet.Update(0.03f);
            //Check to see if it hit the bottom of the screen.
            if (isHit)
            {
                //Reset the bullet positon and stop the timer.
                _game.Player.Bullet.ResetPosition();
                _bulletMoveTimer.Stop();
            }
            //Check to see which target the bullet has hit, if any.
            byte target = _game.Player.Bullet.Collide(_imageList, _imgMotherShip);
            //Check to see if any alien was hit.
            if (target == 0 || target <= 54)
            {
                //Remove the alien and update the scroe.
                int addedscore = _game.DespawnAliens((int)target);
                int newScore = _game.UpdateScore(addedscore);
                //Set the new score on display.
                _txtScore.Text = "Score: " + newScore.ToString();
                //Reset the bullet position.
                _game.Player.Bullet.ResetPosition();
                //Stop the timer.
                _bulletMoveTimer.Stop();
            }
            //Check to see if the mothership was hit.
            else if (target == 55)
            {
                //Stop the mothership from moving.
                _imgMotherShip.Visibility = Visibility.Collapsed;
                _shipMoveTimer.Stop();
                //Update the score.
                int newScore = _game.UpdateScore(_game.MotherShip.BonusPoint);
                //Set the new score on display.
                _txtScore.Text = "Score: " + newScore.ToString();
                //Reset the bullet position and stop the timer.
                _game.Player.Bullet.ResetPosition();
                _bulletMoveTimer.Stop();
            }
            else
            {
                //Do nothing and continue.
            }
        }

        /// <summary>
        /// Represents an method for the timer that will move each alien across and down the screen.
        /// </summary>
        /// <param name="sender">Represents the timer object.</param>
        /// <param name="e">Represents other method parameters.</param>
        private void OnAlienMoveTimerTick(object sender, object e)
        {
            //Check to see if there are more than 0 aliens left.
            if (_game.AlienCount > 0)
            {
                //Move the aliens.
                _game.ShiftAliens();
                //Increase the speed depending on the number of aliens.
                _alienMoveTimer.Interval = TimeSpan.FromMilliseconds(_game.IncreaseSpeed());
            }
            //If there are no aliens left.
            else
            {
                //Reset the round.
                byte newRound = _game.ResetRound();
                //Update labels.
                _txtLevel.Text = "Level: " + newRound.ToString();
            }
        }

        /// <summary>
        /// Represents a timer that will move the player.
        /// </summary>
        /// <param name="sender">Represents the timer object.</param>
        /// <param name="e">Represents other method parameters.</param>
        private void OnPlayerMoveTimerTick(object sender, object e)
        {
            //Move the player.
            _game.Player.Move();
        }
        
        /// <summary>
        /// Represents a event handler for when the pause button is clicked.
        /// </summary>
        /// <param name="sender">represents the button.</param>
        /// <param name="e">Represents other method parameters.</param>
        private void OnPauseClicked(object sender, RoutedEventArgs e)
        {
            //Check to see if the game is not paused.
            if (_btnPause.Content.ToString() == "Pause")
            {
                //Set the pause button content to resume.
                _btnPause.Content = "Resume";
                //Stop the alien movement, clock, mothership, enemybullet spawner, and  the three enemybullet timers.
                _alienMoveTimer.Stop();
                _clockTimer.Stop();
                _shipMoveTimer.Stop();
                _enemyBulletSpawnTimer.Stop();
                _enemyBulletMoveTimer.Stop();
                _enemyBulletOneMoveTimer.Stop();
                _enemyBulletTwoMoveTimer.Stop();
                //Make the save button visible.
                _btnSave.Visibility = Visibility.Visible;
                //Pause the game.
                _game.Pause();
            }
            //Otherwise, resume the game.
            else
            {
                //Set the text to pause again.
                _btnPause.Content = "Pause";
                //Start the alien movement, clock, mothership, enemybullet spawner, and  the three enemybullet timers.
                _alienMoveTimer.Start();
                _clockTimer.Start();
                _shipMoveTimer.Start();
                _enemyBulletSpawnTimer.Start();
                _enemyBulletMoveTimer.Start();
                _enemyBulletOneMoveTimer.Start();
                _enemyBulletTwoMoveTimer.Start();
                //Make the save button invisible.
                _btnSave.Visibility = Visibility.Collapsed;
                //Resume the game.
                _game.Pause();
            }
            
        }
        /// <summary>
        /// Represents an event handler method for when the game is saved when the button is clicked.
        /// </summary>
        /// <param name="sender">Represents the button.</param>
        /// <param name="e">Represents other method parameters.</param>
        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            //Call the save method of the Game.
            _game.Save();
            //Navigate Back to MainPage.
            this.Frame.GoBack();
        }
        /// <summary>
        /// Represents a method that will check to see which key is pressed.
        /// </summary>
        /// <param name="sender">Represents the sender object.</param>
        /// <param name="e">Represents the key.</param>
        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            //Check to see which key is pressed.
            switch(e.Key)
            {
                //Check to see if the right key is pressed.
                case (Windows.System.VirtualKey.Right):
                    //Check to see if the player is facing left.
                    if (_game.Player.Direction == Direction.Left)
                    {
                        //Change the direction and start the player move timer.
                        _game.Player.Direction = Direction.Right;
                        _playerMoveTimer.Start();
                    }
                    else
                    {
                        //Otherwise, just start the timer.
                        _playerMoveTimer.Start();
                    }
                    break;
                //Check to see if the left key is pressed.
                case (Windows.System.VirtualKey.Left):
                    //Check to see if the player is facing right.
                    if (_game.Player.Direction == Direction.Right)
                    {
                        //Change the direction and start the player move timer.
                        _game.Player.Direction = Direction.Left;
                        _playerMoveTimer.Start();
                    }
                    else
                    {
                        //Otherwise, just start the timer.
                        _playerMoveTimer.Start();
                    }
                    break;
                //Check to see if the up key or fire is pressed.
                case (Windows.System.VirtualKey.Up):
                    //Check to see if the game is paused or not.
                    if (_game.Player.CanShoot)
                    {
                        //Check to see if the bullet is moving or not.
                        bool hasNotStart = _game.Player.OnShoot();
                        if (hasNotStart)
                        {
                            //If it has not moved yet, then start the timer.
                            _bulletMoveTimer.Start();
                        }
                    }
                    break;

                default:
                    //Do nothing.
                    break;
            }
        }

        /// <summary>
        /// Represents a method that will check to see which key is released.
        /// </summary>
        /// <param name="sender">represents the sender object.</param>
        /// <param name="e">Represents the key released.</param>
        private void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            //Check to see which key is released.
            switch (e.Key)
            {
                //Check to see if the right key is released.
                case (Windows.System.VirtualKey.Right):
                    //Stop the player movement timer.
                    _playerMoveTimer.Stop();
                    break;
                //Check to see if the left key is released.
                case (Windows.System.VirtualKey.Left):
                    //Stop the player movement timer.
                    _playerMoveTimer.Stop();
                    break;

                default:
                    //Do nothing.
                    break;
            }
        }
    }
}
