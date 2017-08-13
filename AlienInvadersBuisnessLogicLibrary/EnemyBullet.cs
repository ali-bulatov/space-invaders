using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AlienInvadersBuisnessLogic
{
    public class EnemyBullet : Bullet
    {

        public EnemyBullet(double xPos, double yPos, Image image) : base(xPos, yPos, image)
        {

        }

        public override bool Update(float elapsedTime)
        {
            _yPos += Velocity * elapsedTime;
            Canvas.SetTop(bullet, _yPos);
            if (_yPos > 720 - bullet.Height)
            {
                IsAlive = false;
                return true;
            }
            return false;
        }

        public override bool Draw(double xPosition, double yPosition)
        {
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
