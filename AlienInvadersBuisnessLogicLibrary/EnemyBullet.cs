using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AlienInvadersBuisnessLogic
{
    /// <summary>
    /// EnemyBullet is a class derived from the Bullet class
    /// </summary>
    public class EnemyBullet : Bullet
    {
        /// <summary>
        /// Retrieves information from Bullet class
        /// </summary>
        /// <param name="xPos">is a double</param>
        /// <param name="yPos">is a double</param>
        /// <param name="image"> is an image</param>
        public EnemyBullet(double xPos, double yPos, Image image) : base(xPos, yPos, image)
        {

        }

        /// <summary>
        /// Update method that constantly checks to see whether the bullet is alive or not + is in charge
        /// if moving the bullet.
        /// </summary>
        /// <param name="elapsedTime"></param>
        /// <returns></returns>
        public override bool Update(float elapsedTime)
        {
            _yPos += Velocity * elapsedTime;
            Canvas.SetTop(bullet, _yPos);
            //If bullet y position is less than 0, then it is also not alive.
            if (_yPos > 720 - bullet.Height)
            {
                IsAlive = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Draws the bullet center to the player
        /// </summary>
        /// <param name="xPosition">double</param>
        /// <param name="yPosition">double</param>
        /// <returns>If bullet is not alive, player is eligible to shoot (Waits for bullet)</returns>
        public override bool Draw(double xPosition, double yPosition)
        {
            //If the bullet is not alive, then can be able to draw bullet
            if (IsAlive == false)
            {
                bullet.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Canvas.SetLeft(bullet, xPosition);
                Canvas.SetTop(bullet, yPosition);
                _xPos = xPosition;
                _yPos = yPosition;
                isAlive = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Collision code which associates alien image + mothership image to bullet
        /// </summary>
        /// <param name="playerShieldList">Is a List of player shield images</param>
        /// <param name="playerImage">Image of player</param>
        /// <returns></returns>
        public override byte Collide(List<Image> playerShieldList, Image playerImage)
        {
            byte index = 0;

            //Checks to see the visibility of the player Image
            if (playerImage.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                //Collision code for Bullet to Player Image
                if (_xPos < (Canvas.GetLeft(playerImage) + playerImage.Width) && (_xPos + playerImage.Width) > Canvas.GetLeft(playerImage) && _yPos < (Canvas.GetTop(playerImage) + playerImage.Height) && (_yPos + playerImage.Height) > Canvas.GetTop(playerImage))
                {
                    //Removes player image
                    return 4;
                }
            }
            //Goes through each playerShield
            foreach(Image playerShield in playerShieldList)
            {
                //Checks to see the visibiity of the playerShield is true
                if (playerShield.Visibility == Windows.UI.Xaml.Visibility.Visible)
                {
                    //Collision code for Bullet to player Shield
                    if (_xPos < (Canvas.GetLeft(playerShield) + playerShield.Width) && (_xPos + playerShield.Width) > Canvas.GetLeft(playerShield) && _yPos < (Canvas.GetTop(playerShield) + playerShield.Height) && (_yPos + playerShield.Height) > Canvas.GetTop(playerShield))
                    {
                        //Removes playershield instance
                        return index;
                    }
                }
            }
            //Represents the bullet has not hit anything
            return 255;
        }
    }
}
