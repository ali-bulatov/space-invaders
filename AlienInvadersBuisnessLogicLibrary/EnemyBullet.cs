using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AlienInvaders
{
    public class EnemyBullet : Bullet
    {
        public EnemyBullet(double xPos, double yPos, Image image) : base (xPos, yPos, image)
        {

        }
        private Image enemyBullet;
       
        public bool DrawBullet(double xPosition, double yPosition)
        {
            if (IsAlive == false)
            {
                enemyBullet.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Canvas.SetLeft(enemyBullet, xPosition);
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

        public override byte Collide(List<Image> playerShieldList, Image playerImage)
        {
            byte index = 0;

            if (playerImage.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                if (_xPos < (Canvas.GetLeft(playerImage) + playerImage.Width) && (_xPos + playerImage.Width) > Canvas.GetLeft(playerImage) && _yPos < (Canvas.GetTop(playerImage) + playerImage.Height) && (_yPos + playerImage.Height) > Canvas.GetTop(playerImage))
                {
                    return 4;
                }
            }
            index++;

            foreach(Image playerShield in playerShieldList)
            {
                if (playerShield.Visibility == Windows.UI.Xaml.Visibility.Visible)
                {
                    if (_xPos < (Canvas.GetLeft(playerShield) + playerShield.Width) && (_xPos + playerShield.Width) > Canvas.GetLeft(playerShield) && _yPos < (Canvas.GetTop(playerShield) + playerShield.Height) && (_yPos + playerShield.Height) > Canvas.GetTop(playerShield))
                    {
                        return index;
                    }
                }
            }
            return 255;
        }
    }
}
