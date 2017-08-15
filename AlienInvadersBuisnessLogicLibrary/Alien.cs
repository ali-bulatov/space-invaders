using AlienInvadersBuisnessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AlienInvadersBuisnessLogic
{
    /// <summary>
    /// Class rerpresenting the alien object
    /// </summary>
    public class Alien
    {
        /// <summary>
        /// x-axis position of the alien
        /// </summary>
        private double _xPosition;
        /// <summary>
        /// y-axis position of the alien
        /// </summary>
        private double _yPosition;
        /// <summary>
        /// Moving speed of the alien
        /// </summary>
        private double _speed;
        /// <summary>
        /// Points
        /// </summary>
        private int _points;
        /// <summary>
        /// represents enemy bullet
        /// </summary>
        private EnemyBullet _enemyBullet;
        private Image _uiAlien;

        private Direction _direction;
        /// <summary>
        /// Internal alien constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public Alien(double speed, Image uiAlien, int points)
        {
            this._speed = speed;
            _points = points;
            _uiAlien = uiAlien;
            _xPosition = Canvas.GetLeft(uiAlien);
            _yPosition = Canvas.GetTop(uiAlien);
            _direction = Direction.Right;
            _enemyBullet = null;
        }
        /// <summary>
        /// xPosition property
        /// </summary>
        public double XPosition
        {
            get
            {
                return _xPosition;
            }
            set
            {
                _xPosition = value;
            }
        }
        /// <summary>
        /// yPosition property
        /// </summary>
        public double YPosition
        {
            get
            {
                return _yPosition;
            }
            set
            {
                _yPosition = value;
            }
        }
        public EnemyBullet EnemyBullet
        {
            get
            {
                return _enemyBullet;
            }
            set
            {
                _enemyBullet = value;
            }
        }
        /// <summary>
        /// Speed property
        /// </summary>
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
        /// <summary>
        /// alien image property
        /// </summary>
        public Image UiAlien
        {
            get
            {
                return _uiAlien;
            }
        }
        /// <summary>
        /// alien points property
        /// </summary>
        public int Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }
        /// <summary>
        /// direction property
        /// </summary>
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

        /// <summary>
        /// Alien shooting
        /// </summary>
        public void Shoot()
        {
            double yPosition = _yPosition;
            yPosition += _uiAlien.Height;
            double bulletPosition = _xPosition;
            bulletPosition += (_uiAlien.Width / 2);
            _enemyBullet.Draw(bulletPosition, yPosition);
        }
        /// <summary>
        /// Move alien vertically
        /// </summary>
        public void MoveVertical()
        {
            _yPosition += 5;
            Canvas.SetTop(_uiAlien, _yPosition);
        }
        /// <summary>
        /// Move alien horizontally
        /// </summary>
        public bool MoveHorizontal()
        {
            if (_direction == Direction.Right)
            {

                if (_xPosition >= 720)
                {
                    Canvas.SetLeft(_uiAlien, _xPosition);
                    return true;
                }
                else
                {
                    _xPosition += 5;
                    Canvas.SetLeft(_uiAlien, _xPosition);
                    return false;
                }
            }
            else
            {
                if (_xPosition <= 0)
                {
                    _xPosition -= 5;
                    Canvas.SetLeft(_uiAlien, _xPosition);
                    return true;
                }
                else
                {
                    _xPosition -= 5;
                    Canvas.SetLeft(_uiAlien, _xPosition);
                    return false;
                }
            }
        }
    }
}