using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;



namespace AlienInvaders
{
    public class Bullet
    {
        private Rectangle rect;
        protected double xPos, yPos;
        protected bool isAlive;
        private float Velocity;
        private Image bullet;
        public Bullet( double _xPos, double _yPos, Image _bullet )
        {
            xPosition = xPos = _xPos;
            yPosition = yPos = _yPos;
            isAlive = false;
            Velocity = 300.0f;
            bullet = _bullet;
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public double xPosition
        {
            get { return xPos; }
            set { xPos = value; }
        }

        public double yPosition
        {
            get { return yPos; }
            set { yPos = value; }
        }

        public bool Update(float elapsedTime)
        {
            yPos -= Velocity * elapsedTime;
            Canvas.SetTop(bullet, yPos);
            if (yPosition < 0 - bullet.Height)
            {
                IsAlive = false;
                return true;
            }
            return false;
        }

        public bool Draw(double xPosition, double yPosition)
        {
            if(IsAlive == false)
            {
                bullet.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Canvas.SetLeft(bullet, xPosition);
                Canvas.SetTop(bullet, yPosition);
                xPos = xPosition;
                yPos = yPosition;
                return true;
            }
            return false;
        }

        public bool Draw(float elapsedTime)
        {
            if (IsAlive == false)
            {
                bullet.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Canvas.SetLeft(bullet, xPosition);
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

        public virtual byte Collide(List<Image> alienImageList, Image motherShipImage)
        {
            byte index = 0;

            foreach(Image alien in alienImageList)
            {
                if(alien.Visibility == Windows.UI.Xaml.Visibility.Visible)
                {
                    if (xPosition > Canvas.GetLeft(alien) - bullet.Width && xPosition < Canvas.GetLeft(alien) + bullet.Width)
                    {
                        if (yPosition > Canvas.GetTop(alien) - bullet.Height && yPosition < Canvas.GetTop(alien) + bullet.Height)
                        {
                            return index;
                        }
                    }
                }
                index++;
            }
            if (motherShipImage.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                if (xPosition > Canvas.GetLeft(motherShipImage) - bullet.Width && xPosition < Canvas.GetLeft(motherShipImage) + bullet.Width)
                {
                    if (yPosition > Canvas.GetTop(motherShipImage) - bullet.Height && yPosition < Canvas.GetTop(motherShipImage) + bullet.Height)
                    {
                        return 55;
                    }
                }
            }
            return 255;
        }

    }
}
