using AlienInvadersBuisnessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AlienInvaders
{
    /// <summary>
    /// 
    /// </summary>
    public class MotherShip
    {
        /// <summary>
        /// Current moving direction of the mothership
        /// </summary>
        private Direction _direction;
        /// <summary>
        /// Randomizer user to spawn mothership at a random time
        /// </summary>
        private Random _randomizer;
        /// <summary>
        /// The position on the x-xis where the UI for the Mother Ship is positioned.
        /// </summary>
        private double _startPosition;
        /// <summary>
        ///  The UI element used to represent the Mother Ship to the user, on the form.
        /// </summary>
        private Image _uiMotherShip;
        /// <summary>
        /// Position of the ship
        /// </summary>
        private double _position;
        /// <summary>
        /// bonus points given for killing the otherShip
        /// </summary>
        private int _bounsPoint;
        /// <summary>
        /// Speed of the ship
        /// </summary>
        private double _speed;
        /// <summary>
        /// internal constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public MotherShip(double position, double speed, Random randomizer, Image uiMotherShip)
        {
            this._position = position;
            this._speed = speed;
            this._randomizer = randomizer;
            _uiMotherShip = uiMotherShip;
            _bounsPoint = 500;
        }
        /// <summary>
        /// Bonus points constructor
        /// </summary>
        public int BonusPoint
        {
            get
            {
                return _bounsPoint;
            }
        }
        /// <summary>
        /// Move the ship
        /// </summary>
        public bool Fly()
        {
            //if left --> move left
            //check whether is has hit the edge return true if it did
            //check the direction of the ship
            if (_direction == Direction.Left)
            {
                if (_position >= 720)
                {
                    _position += 4;
                    // set position
                    Canvas.SetLeft(_uiMotherShip, _position);
                    return true;
                }
                else
                {
                    _position += 4;
                    // set position
                    Canvas.SetLeft(_uiMotherShip, _position);
                    return false;
                }
            }
            else
            {
                if (_position <= 0)
                {
                    _position -= 4;
                    // set position
                    Canvas.SetLeft(_uiMotherShip, _position);
                    return true;
                }
                else
                {
                    _position -= 4;
                    // set position
                    Canvas.SetLeft(_uiMotherShip, _position);
                    return false;
                }
            }
        }
        /// <summary>
        /// Spawns the mothership randomly returns bool value
        /// </summary>
        /// <returns></returns>
        public bool Spawn()
        {
            //check whether it is visible or not
            if (_uiMotherShip.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                return true;
            }
            else
            {
                // choose a random number from 1 to 25
                int randomNumber = _randomizer.Next(1, 26);
                // if random number is equal to 25 show Mother Ship
                if (randomNumber == 25)
                {
                    _uiMotherShip.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    // generate a random position 0 or 1
                    int randomPostiion = _randomizer.Next(0, 2);
                    // reset position of the alien
                    ResetPosition(randomPostiion);
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Reset location of the ship
        /// </summary>
        public void ResetPosition(int randomPosition)
        {
            // if random position equals to 0 move left
            if (randomPosition == 0)
            {
                _direction = Direction.Left;
                _position = 0;
                _startPosition = 0;
                // set position
                Canvas.SetLeft(_uiMotherShip, _startPosition);
            }
            // move right (random number =1)
            else
            {
                _direction = Direction.Right;
                _position = 720;
                _startPosition = 720;
                // set position
                Canvas.SetLeft(_uiMotherShip, _startPosition);
            }
        }
    }
}
