using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Aerritis.Generic;
using Microsoft.Xna.Framework.Input;

namespace Aerritis.UI
{
    public class Button:Window
    {
        public enum Button_States
        {
            Pressed,
            Released,
            Idle,
            Mouse_Over
        }

        private Button_States buttonState;
        private Texture2D buttonTexture;
        private float tintScale;
        private Vector2 position;
        private float depth;
        private Window_States state = Window_States.Enabled;
        private float elapsed = 0.0f;
        private ButtonState lastButton;

        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }

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

        public float TintScale
        {
            get { return tintScale; }
            set { tintScale = value; }
        }

        public Button(GameHandler game, Texture2D texture): base(game, texture)
        {
            buttonState = Button_States.Idle;
            buttonTexture = texture;
            tintScale = 0.2f;
            depth = 0.99f;
            elapsed = 0.0f;
            lastButton = ButtonState.Released;
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)X, (int)Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Tools.getMouseBounds().Intersects(getBounds()))
            {
                buttonState = Button_States.Mouse_Over;
                state = Window_States.Enabled;
                tintScale = 0.2f;
            }
            else
            {
                state = Window_States.Disabled;
                buttonState = Button_States.Idle;
                tintScale = 1.0f;
            }
            if (buttonState == Button_States.Mouse_Over)
            {

                if (Tools.getMouseButtonsLeft() == ButtonState.Pressed
                    && Tools.getMouseButtonsLeft() != lastButton)
                {
                    
                    lastButton = Tools.getMouseButtonsLeft();
                }
            }
        }

        static float rotation = 0f;
        public new void Draw(SpriteBatch spriteBatch)
        {
            if (state != Window_States.Enabled || texture == null)
                return;
            spriteBatch.Draw(texture, getBounds(), new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White * tintScale, rotation, Vector2.Zero, SpriteEffects.None, depth);
        }
    }
}