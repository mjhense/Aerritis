using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerritis.Animation
{
    public class SpriteManager
    {
        private GameHandler game;
        public Dictionary<string, Sprite> characterSprites;
        public Dictionary<string, Texture2D> palettes;
        public Dictionary<string, Texture2D> UIs;

        public SpriteManager(GameHandler game, ContentManager content)
        {
            this.game = game;
            characterSprites = new Dictionary<string, Sprite>
            {
                { "skeleton", new Sprite(ref game, content.Load<Texture2D>("Sprites/Characters/skeleton"), 32, 64, true, 3) }
            };
            palettes = new Dictionary<string, Texture2D>
            {
                { "wood_tileset", content.Load<Texture2D>("Levels/wood_tileset") }
            };
            UIs = new Dictionary<string, Texture2D>
            {
                {"button_ui", content.Load<Texture2D>("UI/GUI") },
                {"expanded_ui", content.Load<Texture2D>("UI/gui_expanded") },
                {"button_gui", content.Load<Texture2D>("UI/button_gui") }
            };
        }
    }
}