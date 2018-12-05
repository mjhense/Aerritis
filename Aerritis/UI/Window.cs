using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aerritis.UI
{
    public class Window
    {

        public enum Window_States
        {
            Enabled,
            Disabled
        }

        protected Vector2 position;
        protected Texture2D texture;
        protected GameHandler game;
        protected Window_States state;
        protected float depth;

        public Window_States State
        {
            get { return state; }
            set { state = value; }
        }

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

        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public Window(GameHandler game, Texture2D texture)
        {
            this.game = game;
            this.texture = texture;
            position = Vector2.Zero;
            state = Window_States.Enabled;
            depth = 0.98f;
        }

        public Rectangle getScreenRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (state != Window_States.Enabled)
                return;
            spriteBatch.Draw(texture, getScreenRectangle(), new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, depth);
        }
    }
}