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

        public Bullet( float _xPos, float _yPos )
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

        public void Draw()
        {

        }

    }
}
