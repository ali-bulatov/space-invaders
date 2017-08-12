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
                if (_xPos > Canvas.GetLeft(playerImage) - enemyBullet.Width && _xPos < Canvas.GetLeft(playerImage) + enemyBullet.Width)
                {
                    if (_yPos > Canvas.GetTop(playerImage) - enemyBullet.Height && _yPos < Canvas.GetTop(playerImage) + enemyBullet.Height)
                    {
                        return 4;
                    }
                }
            }
            index++;

            foreach(Image playerShield in playerShieldList)
            {
                if (playerShield.Visibility == Windows.UI.Xaml.Visibility.Visible)
                {
                    if (_xPos > Canvas.GetLeft(playerShield) - enemyBullet.Width && _xPos < Canvas.GetLeft(playerShield) + enemyBullet.Width)
                    {
                        if (_yPos > Canvas.GetTop(playerShield) - enemyBullet.Height && _yPos < Canvas.GetTop(playerShield) + enemyBullet.Height)
                        {
                            return index;
                        }
                    }
                }
            }
            return 255;
        }
    }
}
