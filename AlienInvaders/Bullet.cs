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
        protected double _xPos, _yPos;
        protected bool isAlive;
        private float Velocity;
        private Image bullet;

        public Bullet( double xPos, double yPos, Image _bullet )
        {
            _xPos = xPos;
            _yPos = yPos;
            isAlive = false;
            Velocity = 300.0f;
            bullet = _bullet;
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public double XPos
        {
            get { return _xPos; }
            set { _xPos = value; }
        }

        public double YPos
        {
            get { return _yPos; }
            set { _yPos = value; }
        }

        public bool Update(float elapsedTime)
        {
            _yPos -= Velocity * elapsedTime;
            Canvas.SetTop(bullet, _yPos);
            if (_yPos < 0 - bullet.Height)
            {
                IsAlive = false;
                return true;
            }
            return false;
        }

        public virtual bool Draw(double xPosition, double yPosition)
        {
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

        public virtual byte Collide(List<Image> alienImageList, Image motherShipImage)
        {
            byte index = 0;

            foreach(Image alien in alienImageList)
            {
                if(alien.Visibility == Windows.UI.Xaml.Visibility.Visible)
                {
                    if (_xPos < (Canvas.GetLeft(alien) + alien.Width) && (_xPos + bullet.Width) > Canvas.GetLeft(alien) && _yPos < (Canvas.GetTop(alien) + alien.Height) && (_yPos + bullet.Height) > Canvas.GetTop(alien))
                    {
                        return index;
                    }
                }
                index++;
            }
            if (motherShipImage.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                if (_xPos < (Canvas.GetLeft(motherShipImage) + motherShipImage.Width) && (_xPos + bullet.Width) > Canvas.GetLeft(motherShipImage) && _yPos < (Canvas.GetTop(motherShipImage) + motherShipImage.Height) && (_yPos + bullet.Height) > Canvas.GetTop(motherShipImage))
                {
                    return 55;
                }
            }
            return 255;
        }

        public void ResetPosition()
        {
            _xPos = 0;
            _yPos = 0;
            Canvas.SetLeft(bullet, _xPos);
        }

    }
}
