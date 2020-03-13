using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace AlienInvadersBuisnessLogic
{
    /// <summary>
    /// Represents an enumeration for the color of the player. By Nelson Shaw
    /// </summary>
    public enum Color
    {
        //Red is set to 0, yellow 1, green 2, and blue 3.
        Red = 0,
        Yellow,
        Green,
        Blue
    }

    /// <summary>
    /// Represents an enumeration for the direction of an object. By Nelson Shaw
    /// </summary>
    public enum Direction
    {
        //Left is 0, right is 1.
        Left,
        Right
    }

    /// <summary>
    /// Represents a player that can move across the screen. By Nelson Shaw
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Represents player position.
        /// </summary>
        private double _position;

        /// <summary>
        /// Represents player lives.
        /// </summary>
        private byte _lives;

        /// <summary>
        /// Represents player speed.
        /// </summary>
        private double _speed;

        /// <summary>
        /// Represents the bullet the alien has.
        /// </summary>
        private Bullet _bullet;

        /// <summary>
        /// Represents player color.
        /// </summary>
        private Color _color;

        /// <summary>
        /// Represents player direction.
        /// </summary>
        private Direction _direction;

        /// <summary>
        /// Represents the player tank type.
        /// </summary>
        private byte _type;

        /// <summary>
        /// Represents the ui player image.
        /// </summary>
        private Image _uiPlayer;

        /// <summary>
        /// Represents if the palyer can move.
        /// </summary>
        private bool _canMove;

        /// <summary>
        /// Represents if the player can shoot.
        /// </summary>
        private bool _canShoot;

        /// <summary>
        /// Represents the constructor for the player.
        /// </summary>
        /// <param name="color">Represents the color option.</param>
        /// <param name="type">Represents the tank type.</param>
        /// <param name="uiPlayer">Represents the player image.</param>
        /// <param name="bulletImage">Represents the image for the player bullet.</param>
        /// <param name="speed">Represents the player speed.</param>
        public Player(Color color, byte type, Image uiPlayer, Image bulletImage, double speed)
        {
            //The player has 3 lives.
            _lives = 3;
            //Set player speed.
            _speed = speed;
            //Set the bullet of the player.
            _bullet = new Bullet(0, 0, bulletImage);
            //Set the color and tank type.
            _color = color;
            _type = type;
            //Set the direction to left initially.
            _direction = Direction.Left;
            //Set the player image.
            _uiPlayer = uiPlayer;
            //Set the position to 0 to default.
            _position = 0;
            //Allow the player to shoot and move.
            _canMove = true;
            _canShoot = true;
            //Set the image for the player.
            SetImage(color, type);
            
        }

        /// <summary>
        /// Represents a property to get and set the lives of the player.
        /// </summary>
        public byte Lives
        {
            get
            {
                //Return lives.
                return _lives;
            }
            set
            {
                //Set the new life.
                _lives = value;
            }
        }

        /// <summary>
        /// Represents a property to set and get the player position.
        /// </summary>
        public double Position
        {
            get
            {
                //Return the player position.
                return _position;
            }
            set
            {
                //Set the player position.
                _position = value;
            }
        }
        /// <summary>
        /// Represents a property that will get and set the direction of the player.
        /// </summary>
        public Direction Direction
        {
            get
            {
                //Returns the direction.
                return _direction;
            }
            set
            {
                //Set the direction.
                _direction = value;
            }
        }

        /// <summary>
        /// Represents a property that will get and set the canmove field.
        /// </summary>
        public bool CanMove
        {
            get
            {
                //Return the boolean.
                return _canMove;
            }
            set
            {
                //set the boolean.
                _canMove = value;
            }
        }

        /// <summary>
        /// Represents a property that will get and set the player shooting boolean.
        /// </summary>
        public bool CanShoot
        {
            get
            {
                //Return the boolean.
                return _canShoot;
            }
            set
            {
                //Set the boolean.
                _canShoot = value;
            }
        }

        /// <summary>
        /// Rerpresents a property that will only get the bullet of hte player.
        /// </summary>
        public Bullet Bullet
        {
            get
            {
                //Return the bullet.
                return _bullet;
            }
        }

        /// <summary>
        /// Represnets a property that will get and set the image of the player.
        /// </summary>
        public Image UiPlayer
        {
            get
            {
                //Return the image.
                return _uiPlayer;
            }
            set
            {
                //Set the image.
                _uiPlayer = value;
            }
        }
        /// <summary>
        /// Represents a property that will get and set the player speed.
        /// </summary>
        public double Speed
        {
            get
            {
                //Return the speed.
                return _speed;
            }
            set
            {
                //Set the speed.
                _speed = value;
            }
        }

        /// <summary>
        /// Represnets a method that will move the player depending on direction.
        /// </summary>
        public void Move()
        {
            //Check to see if the player can move.
            if (_canMove)
            {
                //Check to see which direction the player is currently facing.
                if (_direction == Direction.Left)
                {
                    //Check to see if the player will be able to have the space on the screen to move or not.
                    if (_position - (8 * _speed) > 0)
                    {
                        //If so, move the player.
                        double location = Canvas.GetLeft(_uiPlayer);
                        location -= (8 * _speed);
                        Canvas.SetLeft(_uiPlayer, location);
                        _position -= (8 * _speed);
                    }
                    //Otherwise, don't move the player.
                    else
                    {
                        Canvas.SetLeft(_uiPlayer, 0);
                        _position = 0;
                    }
                }
                else
                {
                    //Check to see if the player will be able to have the space on the screen to move or not.
                    if (_position + (8 * _speed) < 720)
                    {
                        //If so, move the player.
                        double location = Canvas.GetLeft(_uiPlayer);
                        location += (8 * _speed);
                        Canvas.SetLeft(_uiPlayer, location);
                        _position += (8 * _speed);
                    }
                    //Otherwise, don't move the player.
                    else
                    {
                        Canvas.SetLeft(_uiPlayer, 720);
                        _position = 720;
                    }
                }
            }
        }

        /// <summary>
        /// Represents a method that will shoot the bullet of the player.
        /// </summary>
        /// <returns>Represents whether the player can shoot or not.</returns>
        public bool OnShoot()
        {
            //Get the y position of the player.
            double yPosition = Canvas.GetTop(_uiPlayer);
            //Get the middle of the player image.
            double bulletPosition = Canvas.GetLeft(_uiPlayer);
            bulletPosition += (_uiPlayer.Width / 2);
            //Draw the bullet and check to see if it is moving.
            bool notFired = _bullet.Draw(bulletPosition, yPosition);
            if (notFired)
            {
                //If it is not moving ,return true.
                return true;
            }
            else
            {
                //The bullet is moving.
                return false;
            }
        }

        /// <summary>
        /// Represents a method that will decrement player lives and check to see if the player has lives left.
        /// </summary>
        /// <returns></returns>
        public bool OnDeath()
        {
            //Decrement lives.
            _lives -= 1;
            //Check to see if the player has lives.
            if (_lives <= 0)
            {
                //The player doesn't have any more.
                return false;
            }
            else
            {
                //The player does have more lives. Reset the position of the player.
                Reset();
                return true;
            }
        }

        /// <summary>
        /// Represents a method that will set the image of the player depending on the values on MainPage selected.
        /// </summary>
        /// <param name="color">Represents the color type.</param>
        /// <param name="type">Represents tank type.</param>
        public void SetImage(Color color, byte type)
        {
            //Get the color and type.
            _color = color;
            _type = type;
            //Create a new list of image sources, representing all possible images.
            String[,] imageCombo = new String[3, 4];
            //Add the image sources.
            imageCombo[0, 0] = "ms-appx:///Assets/SpR1.png";
            imageCombo[0, 1] = "ms-appx:///Assets/SpY1.png";
            imageCombo[0, 2] = "ms-appx:///Assets/SpG1.png";
            imageCombo[0, 3] = "ms-appx:///Assets/SpB1.png";
            imageCombo[1, 0] = "ms-appx:///Assets/SpR2.png";
            imageCombo[1, 1] = "ms-appx:///Assets/SpY2.png";
            imageCombo[1, 2] = "ms-appx:///Assets/SpG2.png";
            imageCombo[1, 3] = "ms-appx:///Assets/SpB2.png";
            imageCombo[2, 0] = "ms-appx:///Assets/SpR3.png";
            imageCombo[2, 1] = "ms-appx:///Assets/SpY3.png";
            imageCombo[2, 2] = "ms-appx:///Assets/SpG3.png";
            imageCombo[2, 3] = "ms-appx:///Assets/SpB3.png";
            //Set the player image based on what was entered.
            _uiPlayer.Source = new BitmapImage(new Uri(imageCombo[_type, (int)_color]));
        }

        /// <summary>
        /// Represents a method to reset player position.
        /// </summary>
        public void Reset()
        {
            //Reset the position of the image and the player.
            Canvas.SetLeft(_uiPlayer, 0);
            _position = 0;
        }
    }
}
