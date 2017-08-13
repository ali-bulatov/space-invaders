using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AlienInvadersBuisnessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public class Shield
    {
        /// <summary>
        /// Hom many bullets needed to dissolve the shield
        /// </summary>
        private int _strength;
        /// <summary>
        /// UI representation of the shield
        /// </summary>
        private Image _uiShield;
        /// <summary>
        /// internal constructor
        /// </summary>
        /// <param name="strength"></param>
        internal Shield(int strength)
        {
            this._strength = strength;
        }
        /// <summary>
        /// Dissolve the shield
        /// </summary>
        public void Dissolve()
        {
            // Change the width of the shield
            _uiShield.Width -= 10;
        }
        /// <summary>
        /// Ressolve the shield
        /// </summary>
        public void Ressolve()
        {

        }
    }
}
