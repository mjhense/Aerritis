using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Aerritis.Level;
using Aerritis.Animation;
using Aerritis.Character;

namespace Aerritis
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameHandler game;
        int _total_frames = 0;
        float _elapsed_time = 0.0f;
        int _fps = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            SetFrameRate(1.0f / 144.0f);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //initialize the game object
            game = new GameHandler(ref graphics, Content);
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            _elapsed_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();
            }

            game.Update(gameTime);

            // 1 Second has passed
            if (_elapsed_time> 1000.0f)
            {
                CountFrameRate();
            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            //refresh the display
            GraphicsDevice.Clear(Color.Black);
            //framerate counter
            _total_frames++;

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            game.Draw(spriteBatch);
            DrawFrameRate();
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Custom Methods
        public void SetFrameRate(float value)
        {
            this.TargetElapsedTime = System.TimeSpan.FromSeconds(value);
        }
        public void CountFrameRate()
        {
            _fps = _total_frames;
            _total_frames = 0;
            _elapsed_time = 0;
        }
        public void DrawFrameRate()
        {
            spriteBatch.DrawString(game.fontManager.fonts["dialogFont"], string.Format("FPS: {0}", _fps),
                new Vector2(10.0f, 20.0f), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
        }
        #endregion
    }
}