using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Aerritis.UI
{
    public class ButtonUI:Window
    {
        #region Declarations
        private Button[] buttons;
        private Vector2[] buttonLocations;
        private Vector2 Button1 = new Vector2(26, 41);
        private Vector2 Button2 = new Vector2(26, 78);
        private Vector2 Button3 = new Vector2(26, 115);
        private Vector2 Button4 = new Vector2(26, 152);
        private Vector2 Button5 = new Vector2(26, 189);
        private Vector2 Button6 = new Vector2(26, 225);
        private Vector2 Button7 = new Vector2(26, 261);
        private Vector2 Button8 = new Vector2(26, 297);
        private Vector2 Button9 = new Vector2(72, 170);
        private const int button_width = 32;
        private const int button_height = 32;
        #endregion

        #region Constructor
        public ButtonUI(GameHandler game, Texture2D texture) : base(game, texture)
        {
            position = new Vector2(3, (game.Camera.getViewPortHeight() / 2) - (texture.Height / 2));

            //set up buttons
            buttons = new Button[9];
            buttonLocations = new Vector2[9];
            buttonLocations[0] = Button1;
            buttonLocations[1] = Button2;
            buttonLocations[2] = Button3;
            buttonLocations[3] = Button4;
            buttonLocations[4] = Button5;
            buttonLocations[5] = Button6;
            buttonLocations[6] = Button7;
            buttonLocations[7] = Button8;
            buttonLocations[8] = Button9;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button(game, game.spriteManager.UIs["button_gui"]);
                buttons[i].Position = buttonLocations[i] + this.position;
                buttons[i].State = Window_States.Enabled;
            }
        }
        #endregion 

        private Rectangle getButtonRect(int button_index)
        {
            Vector2 pos = buttonLocations[button_index];
            return new Rectangle((int)pos.X, (int)pos.Y, button_width, button_height);
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Draw(spriteBatch);
            }
        }
    }
}