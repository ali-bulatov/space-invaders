using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienInvaders
{
    /// <summary>
    /// 
    /// </summary>
    class MotherShip
    {
        /// <summary>
        /// Position of the ship
        /// </summary>
        private double _position;
        /// <summary>
        /// 
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
        internal MotherShip(double position,double speed)
        {
            this._position = position;
            this._speed = speed;
        }
        /// <summary>
        /// Move the ship
        /// </summary>
        public void Fly()
        {

        }
        /// <summary>
        /// Reset location of the ship
        /// </summary>
        public void ResetLocation()
        {

        }
    }
}
