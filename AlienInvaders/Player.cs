﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
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

        private bool _canMove;

        public Player(Byte lives, Color color, byte type, Image uiPlayer)
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
            _uiPlayer = uiPlayer;
            _position = 0;
            _canMove = true;
            SetImage(color, type);
            
        }

        public Direction Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
            }
        }

        public bool CanMove
        {
            get
            {
                return _canMove;
            }
            set
            {
                _canMove = value;
            }
        }

        public void Move()
        {
            if (_canMove)
            {
                //Check to see which direction the player is currently facing.
                if (_direction == Direction.Left)
                {
                    //Check to see if the player will be able to have the space on the screen to move or not.
                    if (_position - 2 > 0)
                    {
                        //If so, move the player.
                        double location = Canvas.GetLeft(_uiPlayer);
                        location -= 2;
                        Canvas.SetLeft(_uiPlayer, location);
                        _position -= 2;
                    }
                    else
                    {
                        Canvas.SetLeft(_uiPlayer, 0);
                        _position = 0;
                    }
                }
                else
                {
                    //Check to see if the player will be able to have the space on the screen to move or not.
                    if (_position + 2 < 720)
                    {
                        //If so, move the player.
                        double location = Canvas.GetLeft(_uiPlayer);
                        location += 2;
                        Canvas.SetLeft(_uiPlayer, location);
                        _position += 2;
                    }
                    else
                    {
                        //TODO: USE ACTUALWIDTH INSTEAD.
                        Canvas.SetLeft(_uiPlayer, 720);
                        _position = 720;
                    }
                }
            }

            // Else if space the player is going to move is not enough but has some space remaining between it and the edge.
            // Move the player to the end of the screen.
            // Otherwise, if the player has hit the end of the screen.
            // Do not move the player at all.
        }

        public void OnShoot()
        {
            //Cause the bullet to be visible.

        }

        public void OnDeath()
        {
            _uiPlayer.Visibility = Visibility.Collapsed;
            //TODO: ADD MORE FUNCTIONALITY.
        }

        public void Respawn()
        {
            _uiPlayer.Visibility = Visibility.Visible;
            _lives -= 1;
            //TODO: ADD MORE FUNCTIONALITY.
        }

        public void SetImage(Color color, byte type)
        {
            _color = color;
            _type = type;
            List<List<Image>> imageCombo = new List<List<Image>>();
            //TODO: Add the images into the list, each row representing a color.
            //set the image of the player to the image in the list.
            //_uiPlayer = List<List<Image>>[(int)color][type];

        }

        public void Reset()
        {
            Canvas.SetLeft(_uiPlayer, 0);
            _position = 0;
        }
    }
}
