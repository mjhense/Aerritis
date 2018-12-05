using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Aerritis.Animation;
using Aerritis.Items;
using Aerritis.UI;

namespace Aerritis.Character
{
    public class Character
    {
        protected Texture2D texture;
        protected Sprite sprite;
        protected Vector2 position;
        protected Vector2 oldPosition;
        protected int Strength;
        protected int Defense;
        protected int Dexterity;
        protected Inventory inventory;

        protected GameHandler game;

        public int STR => Strength;
        public int DEF => Defense;
        public int DEX => Dexterity;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public float Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public int Direction
        {
            get { return sprite.Direction; }
            set { sprite.Direction = value; }
        }

        public Character(GameHandler game)
        {
            this.game = game;
        }

        public Vector2 WorldCenter
        {
            get
            {
                sprite.Position = position;
                return sprite.WorldCenter();
            }
        }

        public Vector2 ScreenCenter
        {
            get
            {
                sprite.Position = position;
                return sprite.ScreenCenter();
            }
        }

        public Rectangle feetPosition()
        {
            int y = (int)(sprite.FrameHeight / 4);
            int x = (int)(sprite.FrameWidth * 0.68f);
            Vector2 pos = sprite.WorldCenter();
            return new Rectangle((int)sprite.Position.X - (int)(x / 2), (int)pos.Y - y, x, y);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (oldPosition!= position)
            {
                oldPosition = position;
                sprite.Animate = true;
            }
            else
            {
                sprite.Animate = false;
            }
            sprite.Position = position;
            sprite.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}