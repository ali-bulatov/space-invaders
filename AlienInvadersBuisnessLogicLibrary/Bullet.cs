using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;



namespace AlienInvadersBuisnessLogic
{
    public class Bullet
    {
        /// <summary>
        /// Main properties of a bullet
        /// </summary>
        protected double _xPos, _yPos;
        protected bool isAlive;
        protected float Velocity;
        protected Image bullet;

        /// <summary>
        /// Internal constructor for the bullet class
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="_bullet"></param>
        public Bullet( double xPos, double yPos, Image _bullet )
        {
            _xPos = xPos;
            _yPos = yPos;
            isAlive = false;
            Velocity = 300.0f;
            bullet = _bullet;
        }

        /// <summary>
        /// Checks to see if bullet is Alive
        /// </summary>
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        /// <summary>
        /// X position of bullet
        /// </summary>
        public double XPos
        {
            get { return _xPos; }
            set { _xPos = value; }
        }

        /// <summary>
        /// Y position of bullet
        /// </summary>
        public double YPos
        {
            get { return _yPos; }
            set { _yPos = value; }
        }

        /// <summary>
        /// Update method that constantly checks to see whether the bullet is alive or not + is in charge
        /// if moving the bullet.
        /// </summary>
        /// <param name="elapsedTime">Float</param>
        /// <returns>If bullet is alive or not</returns>
        public virtual bool Update(float elapsedTime)
        {
            _yPos -= Velocity * elapsedTime;
            Canvas.SetTop(bullet, _yPos);
            //If bullet y position is less than 0, then it is also not alive.
            if (_yPos < 0 - bullet.Height)
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
        public virtual bool Draw(double xPosition, double yPosition)
        {
            //If the bullet is not alive, then can be able to draw bullet
            if(IsAlive == false)
            {
                bullet.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Canvas.SetLeft(bullet, xPosition);
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
        /// <param name="alienImageList">Is a List of alien images</param>
        /// <param name="motherShipImage">Image of mothership</param>
        /// <returns></returns>
        public virtual byte Collide(List<Image> alienImageList, Image motherShipImage)
        {
            byte index = 0;
            //Goes through each alien
            foreach(Image alien in alienImageList)
            {
                //Checks to see the visibiity of the alien is true
                if(alien.Visibility == Windows.UI.Xaml.Visibility.Visible)
                {
                    //Collision code for Bullet to Alien
                    if (_xPos < (Canvas.GetLeft(alien) + alien.Width) && (_xPos + bullet.Width) > Canvas.GetLeft(alien) && _yPos < (Canvas.GetTop(alien) + alien.Height) && (_yPos + bullet.Height) > Canvas.GetTop(alien))
                    {
                        //Removes alien
                        return index;
                    }
                }
                index++;
            }
            //Checks to see the vsibility of the mother ship is true
            if (motherShipImage.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                //Collision code for Bullet to Mother Ship
                if (_xPos < (Canvas.GetLeft(motherShipImage) + motherShipImage.Width) && (_xPos + bullet.Width) > Canvas.GetLeft(motherShipImage) && _yPos < (Canvas.GetTop(motherShipImage) + motherShipImage.Height) && (_yPos + bullet.Height) > Canvas.GetTop(motherShipImage))
                {
                    return 55;
                }
            }
            //Represents the bullet has not hit anything
            return 255;
        }

        /// <summary>
        /// Resets position of the bullet to be invisible but always aligned with the player.
        /// </summary>
        public void ResetPosition()
        {
            _xPos = 0;
            _yPos = 0;
            Canvas.SetLeft(bullet, _xPos);
            Canvas.SetTop(bullet, _yPos);
            isAlive = false;
            bullet.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

    }
}
