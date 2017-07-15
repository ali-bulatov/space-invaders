using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AlienInvaders
{
    public enum Color
    {
        Red = 1,
        Yellow,
        Green,
        Blue
    }

    public enum Direction
    {
        left,
        right
    }

    public class Player
    {
        private double _position;

        private bool _alive;

        private byte _lives;

        private double _speed;

        private Bullet _bullet;

        private Color _color;

        private Direction _direction;

        private byte _type;

        private Image _uiPlayer;

        public Player(Byte lives, Color color, byte type)
        {

        }

        public void Move()
        {

        }

        public void OnShoot()
        {

        }

        public void OnDeath()
        {

        }

        public void Respawn()
        {

        }

        public void SetImage()
        {

        }
    }
}
