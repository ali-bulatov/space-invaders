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

        private Image enemyBullet;

        public EnemyBullet(double _xPos, double _yPos, Image _bullet) : base(_xPos, _yPos, _bullet)
        {

        }

        public bool DrawBullet()
        {
            if (IsAlive == false)
            {
                enemyBullet.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Canvas.SetLeft(enemyBullet, xPosition);
                xPos = xPosition;
                yPos = yPosition;
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
                if (xPosition > Canvas.GetLeft(playerImage) - enemyBullet.Width && xPosition < Canvas.GetLeft(playerImage) + enemyBullet.Width)
                {
                    if (yPosition > Canvas.GetTop(playerImage) - enemyBullet.Height && yPosition < Canvas.GetTop(playerImage) + enemyBullet.Height)
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
                    if (xPosition > Canvas.GetLeft(playerShield) - enemyBullet.Width && xPosition < Canvas.GetLeft(playerShield) + enemyBullet.Width)
                    {
                        if (yPosition > Canvas.GetTop(playerShield) - enemyBullet.Height && yPosition < Canvas.GetTop(playerShield) + enemyBullet.Height)
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
