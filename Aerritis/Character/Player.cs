using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Aerritis.Animation;
using Microsoft.Xna.Framework.Input;
using Aerritis.UI;

namespace Aerritis.Character
{
    public class Player:Character
    {
        private float elapsed = 0.00f;
        private float speed;
        private PlayerInventory inven;
        private float duration;

        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public Sprite Sprite => sprite;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Player(GameHandler game) : base(game)
        {
            base.sprite = new Sprite(game.spriteManager.characterSprites["skeleton"]);
            duration = (1.0f / 144.0f);
            sprite.Duration = (.08f);
            Direction = 0;
            position = new Vector2(100, 100);
            speed = 1.0f;
            sprite.Depth = 0.20f;
        }

        public override void Update(GameTime gameTime)
        {
            elapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsed > Duration)
            {
                elapsed = 0.00f;

                DoInput();
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //testing player foot box collider
           // spriteBatch.Draw(game.spriteManager.palettes["wood_tileset"],
           //     game.Camera.WorldToScreen(feetPosition()),
           //     game.tilemap.srcRect(754), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }

        public void DoInput()
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 offset = Vector2.Zero;
            if (state.IsKeyDown(Keys.S))
            {
                Direction = 0;
                offset += new Vector2(0, 1);
            }
            if (state.IsKeyDown(Keys.W))
            {
                Direction = 3;
                offset += new Vector2(0, -1);
            }
            if (state.IsKeyDown(Keys.A))
            {
                Direction = 1;
                offset += new Vector2(-1, 0);
            }
            if (state.IsKeyDown(Keys.D))
            {
                Direction = 2;
                offset += new Vector2(1, 0);
            }
            offset *= speed;
            Vector2 oldPos = position;
            position += offset;
            Rectangle feetPos = feetPosition();
            Vector2 tilePos = game.tilemap.GetTileByPixel(new Vector2(feetPos.X, feetPos.Y));
            Rectangle tileRect = game.tilemap.GetTileWorldRectangle((int)tilePos.X, (int)tilePos.Y);
            if (feetPos.Intersects(tileRect) && game.tilemap.IsTileCollidable(tilePos))
            {
                position -= offset;
                oldPos *= -1;
                //position += offset;
            }

            //confine player to world
            if (position.X - (float)(sprite.WorldRectangle().Width / 2.0f) < 0)
                position.X = (float)(sprite.WorldRectangle().Width / 2.0f);
            if (position.Y - (float)(sprite.WorldRectangle().Height / 2.0f) < 0)
                position.Y = (float)(sprite.WorldRectangle().Height / 2.0f);
            if ((position.X + (float)sprite.WorldRectangle().Width / 2.0f) >
                    ((game.tilemap.MapWidth - 1) * game.tilemap.TileWidth))
                position.X = ((game.tilemap.MapWidth - 1) * game.tilemap.TileWidth) -
                (sprite.WorldRectangle().Width / 2.0f);
            if ((position.Y + (float)sprite.WorldRectangle().Height / 2.0f) >
                    ((game.tilemap.MapHeight - 1) * game.tilemap.TileHeight))
                position.Y = ((game.tilemap.MapHeight - 1) * game.tilemap.TileHeight) -
                (sprite.WorldRectangle().Height / 2.0f);
        }
    }
}