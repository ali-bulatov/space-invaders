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
    public enum Color
    {
        Red = 0,
        Yellow,
        Green,
        Blue
    }

    public enum Direction
    {
        Left,
        Right
    }

    public class Player
    {
        private double _position;

        private byte _lives;

        private double _speed;

        private Bullet _bullet;

        private Color _color;

        private Direction _direction;

        private byte _type;

        private Image _uiPlayer;

        private bool _canMove;

        public Player(Color color, byte type, Image uiPlayer, Image bulletImage, double speed)
        {
            _lives = 3;
            _speed = speed;
            _bullet = new Bullet(0, 0, bulletImage);
            _color = color;
            _type = type;
            _direction = Direction.Left;
            _uiPlayer = uiPlayer;
            _position = 0;
            _canMove = true;
            SetImage(color, type);
            
        }

        public byte Lives
        {
            get
            {
                return _lives;
            }
            set
            {
                _lives = value;
            }
        }

        public double Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Direction Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
            }
        }

        public bool CanMove
        {
            get
            {
                return _canMove;
            }
            set
            {
                _canMove = value;
            }
        }

        public Bullet Bullet
        {
            get
            {
                return _bullet;
            }
        }

        public Image UiPlayer
        {
            get
            {
                return _uiPlayer;
            }
            set
            {
                _uiPlayer = value;
            }
        }

        public double Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        public void Move()
        {
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
                    else
                    {
                        //TODO: USE ACTUALWIDTH INSTEAD.
                        Canvas.SetLeft(_uiPlayer, 720);
                        _position = 720;
                    }
                }
            }

            // Else if space the player is going to move is not enough but has some space remaining between it and the edge.
            // Move the player to the end of the screen.
            // Otherwise, if the player has hit the end of the screen.
            // Do not move the player at all.
        }

        public bool OnShoot()
        {
            //THIS CAUSES THE BULLET TO BE VISIBLE ON THE SCREEN.
            double yPosition = Canvas.GetTop(_uiPlayer);
            double bulletPosition = Canvas.GetLeft(_uiPlayer);
            bulletPosition += (_uiPlayer.Width / 2);
            bool notFired = _bullet.Draw(bulletPosition, yPosition);
            if (notFired)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OnDeath()
        {
            _lives -= 1;
            if (_lives <= 0)
            {
                return false;
            }
            else
            {
                Reset();
                return true;
            }
        }

        public void SetImage(Color color, byte type)
        {
            _color = color;
            _type = type;
            String[,] imageCombo = new String[3, 4];
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
            _uiPlayer.Source = new BitmapImage(new Uri(imageCombo[_type, (int)_color]));
        }

        public void Reset()
        {
            Canvas.SetLeft(_uiPlayer, 0);
            _position = 0;
        }
    }
}
