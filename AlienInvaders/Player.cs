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
        Left,
        Right
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
            //Set the position of the player.
            _alive = true;
            _lives = lives;
            //TODO: Change Speed.
            _speed = 0.25;
            _bullet = new Bullet();
            _color = color;
            _type = type;
            _direction = Direction.Left;
            _uiPlayer = _imgPlayer;
            SetImage(color, type);
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

        public void SetImage(Color color, byte type)
        {
            _color = color;
            _type = type;
            List<List<Image>> imageCombo = new List<List<Image>>();
            //TODO: Add the images into the list, each row representing a color.
            //set the image of the player to the image in the list.
            _uiPlayer = List<List<Image>>[(int)color][type];
        }
    }
}
