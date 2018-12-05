using Aerritis.Animation;
using Aerritis.Character;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Aerritis.Items;
using LevelEditor.Tilemap_Engine;
using Aerritis.Level;
using Aerritis.UI;

namespace Aerritis
{
    public class GameHandler
    {
        private Camera camera;
        private GraphicsDeviceManager graphics;
        public SpriteManager spriteManager;
        public Player player;
        public Aerritis.Level.Tilemap tilemap;
        public FontManager fontManager;
        public ButtonUI buttonUI;
        public Texture2D tileAnimation;
        public Texture2D tileDecorations;

        private Random rand;

        public Camera Camera => camera;
        public GameHandler(ref GraphicsDeviceManager graphics, ContentManager content)
        {
            camera = new Camera(ref graphics);
            camera.setViewPortHeight(800);
            camera.setViewPortHeight(600);
            this.graphics = graphics;
            rand = new Random();
            rand = new Random(rand.Next());
            spriteManager = new SpriteManager(this, content);
            tileAnimation = content.Load<Texture2D>("Levels/Animations/animations");
            tileDecorations = content.Load<Texture2D>("Levels/Animations/decorations");
            tilemap = new Aerritis.Level.Tilemap(spriteManager.palettes["wood_tileset"],
                content.Load<SpriteFont>("Fonts/mapFont"), this);
            tilemap.LoadTileMap("default.MAP");
            player = new Player(this);
            fontManager = new FontManager(this, content);
            buttonUI = new UI.ButtonUI(this, spriteManager.UIs["button_ui"]);
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            tilemap.Update(gameTime);
            buttonUI.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tilemap.Draw(spriteBatch);
            player.Draw(spriteBatch);
            buttonUI.Draw(spriteBatch);
        }

        public void ToggleVSycnc()
        {
            graphics.SynchronizeWithVerticalRetrace = !graphics.SynchronizeWithVerticalRetrace;
            graphics.ApplyChanges();
        }
        public void SetVSync(bool value)
        {
            graphics.SynchronizeWithVerticalRetrace = value;
            graphics.ApplyChanges();
        }

        #region Random Generation
        /// <summary>
        /// gets a random number between 2 values
        /// </summary>
        /// <param name="minValue">the minimum number to generate</param>
        /// <param name="maxValue">the maximum number to generate</param>
        /// <returns>the random result</returns>
        public int Random(int minValue, int maxValue)
        {
            return rand.Next(minValue, maxValue + 1);
        }
        /// <summary>
        /// gets a random number between 0 and max value
        /// </summary>
        /// <param name="maxValue">the maximum number to generate</param>
        /// <returns>the random result</returns>
        public int Random(int maxValue)
        {
            return Random(0, maxValue);
        }
        /// <summary>
        /// gets a random float between 0.0 and 1.0
        /// </summary>
        /// <returns>the random result</returns>
        public float RandomFloat()
        {
            return (float)rand.NextDouble();
        }
        #endregion
    }
}