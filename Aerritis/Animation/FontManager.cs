using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerritis.Animation
{
    public class FontManager
    {
        private GameHandler game;
        public Dictionary<string, SpriteFont> fonts;

        public FontManager(GameHandler game, ContentManager content)
        {
            this.game = game;
            fonts = new Dictionary<string, SpriteFont>
            {
                { "dialogFont", content.Load<SpriteFont>("Fonts/mapFont") }
            };
        }
    }
}