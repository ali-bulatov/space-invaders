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
        private float xPos, yPos, width, height;
        private bool isAlive;
        private float Velocity;
        private Image bullet;

        public Bullet( float _xPos, float _yPos, Image bullet )
        {
            xPosition = xPos = _xPos;
            yPosition = yPos = _yPos;
            isAlive = false;
            Velocity = 300.0f;
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public float xPosition
        {
            get { return xPos; }
            set { xPos = value; }
        }

        public float yPosition
        {
            get { return yPos; }
            set { yPos = value; }
        }

        public void Update(float elapsedTime)
        {
            yPosition -= Velocity * elapsedTime;
            if (yPosition < 0 - height)
            {
                IsAlive = false;
            }
        }

        public bool Draw(float xPosition, float yPosition)
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
            else
            {
                return false;
            }
        }

        public byte Collide()
        {

            //if (a.Left > b.Left - a.Width And a.Left < b.Left + b.Width) Then
            //{
            //    If a.Top > b.Top - a.Height And a.Top < b.Top + a.Height Then
            //    {
            //        //Do something.
            //    }
            //}
        }

    }
}
