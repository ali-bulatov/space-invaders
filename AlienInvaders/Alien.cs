using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienInvaders
{
    /// <summary>
    /// Class rerpresenting the alien object
    /// </summary>
    class Alien
    {
        /// <summary>
        /// Position of the alien
        /// </summary>
        private double _position;
        /// <summary>
        /// Moving speed of the alien
        /// </summary>
        private double _speed;
        /// <summary>
        /// Points
        /// </summary>
        private byte _points;
        /// <summary>
        /// Internal alien constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        internal Alien(double position, double speed)
        {
            this._position = position;
            this._speed = speed;
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

        }
        /// <summary>
        /// Move alien horizontally
        /// </summary>
        public void MoveHorizontal()
        {

        }
    }
}
