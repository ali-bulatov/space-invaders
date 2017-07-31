using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AlienInvaders
{
    /// <summary>
    /// Class rerpresenting the alien object
    /// </summary>
    class Alien
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
        private byte _points;

        private Image _uiAlien;

        private Direction _direction;
        /// <summary>
        /// Internal alien constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        internal Alien(double speed, Image uiAlien)
        {
            this._speed = speed;
            _uiAlien = uiAlien;
            _xPosition = Canvas.GetLeft(uiAlien);
            _yPosition = Canvas.GetTop(uiAlien);
            _direction = Direction.Right;

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
        /// <summary>
        /// Alien shooting
        /// </summary>
        public void Shoot()
        {

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
                    _xPosition += 5;
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