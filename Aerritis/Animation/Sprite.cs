using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aerritis.Animation
{
    public class Sprite
    {
        #region Declarations
        protected Texture2D texture;
        protected Vector2 position;
        protected int currentFrame;
        protected float duration;
        protected bool loop;
        protected int totalFrames;
        private int frameWidth;
        private int frameHeight;
        private Aerritis.GameHandler game;
        private float elapsedTime;
        private bool expired;
        private int paddingX;
        private int paddingY;
        private bool animate;
        private float rotation;
        private int direction;
        private float scale;
        private float depth;
        #endregion

        #region Properties

        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
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
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
            }
        }
        public bool Expired
        {
            get
            {
                return expired;
            }
            set
            {
                expired = value;
            }
        }
        public bool Animate
        {
            get
            {
                return animate;
            }
            set
            {
                animate = value;
            }
        }
        public int PaddingX => paddingX;
        public int PaddingY => paddingY;
        public int FrameWidth
        {
            get
            {
                return frameWidth;
            }
        }
        public int FrameHeight
        {
            get
            {
                return frameHeight;
            }
        }
        public int TotalFrames
        {
            get
            {
                return totalFrames;
            }
        }
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        #endregion

        #region Constructor
        public Sprite(ref GameHandler game, Texture2D texture, int frameWidth, int frameHeight, bool loops, int totalFrames)
        {
            this.game = game;
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.totalFrames = totalFrames;
            currentFrame = 0;
            loop = loops;
            duration = 0.16f;
            elapsedTime = 0.0f;
            expired = false;
            paddingX = 0;
            paddingY = 0;
            animate = true;
            rotation = 0.0f;
            direction = 0;
            scale = 1.0f;
            depth = 1.0f;
        }
        public Sprite(Sprite sprite)
        {
            game = sprite.game;
            texture = sprite.texture;
            frameWidth = sprite.FrameWidth;
            frameHeight = sprite.FrameHeight;
            loop = sprite.loop;
            totalFrames = sprite.TotalFrames;
            currentFrame = 0;
            duration = 0.16f;
            elapsedTime = 0.0f;
            expired = false;
            paddingX = 0;
            paddingY = 0;
            animate = true;
            rotation = 0.0f;
            direction = 0;
            scale = 1.0f;
            depth = 1.0f;
        }
        #endregion

        #region Methods
        public Rectangle getFrame(int frame)
        {
            int srcX = frame * frameWidth;
            int srcY = direction * frameHeight;
            return new Rectangle(srcX, srcY, FrameWidth, frameHeight);
        }

        /// <summary>
        /// Sets the animation speed in seconds(ex 60fps = 1/60 or .16f)
        /// </summary>
        /// <param name="seconds">The number of seconds between each frame</param>
        public void setAnimationSpeed(float seconds)
        {
            duration = seconds;
        }

        public void RotateTo(Vector2 direction)
        {
            rotation = (float)(Math.Atan2(direction.Y, direction.X));
        }

        public Rectangle WorldRectangle()
        {
            return new Rectangle((int)position.X + paddingX, (int)position.Y + paddingY, frameWidth - paddingX, frameHeight - paddingY);
        }

        public Rectangle ScreenRectangle()
        {
            return game.Camera.WorldToScreen(WorldRectangle());
        }

        public bool isColliding(Rectangle other)
        {
            return WorldRectangle().Intersects(other);
        }

        private Vector2 RelativeCenter()
        {
            return new Vector2(frameWidth / 2.0f, frameHeight / 2.0f);
        }

        public Vector2 WorldCenter()
        {
            return (position + RelativeCenter());
        }

        public Vector2 ScreenCenter()
        {
            return game.Camera.WorldToScreen(WorldCenter());
        }

        public bool isOnScreen()
        {
            return game.Camera.ViewPort.Intersects(WorldRectangle());
        }

        public Rectangle dstRect()
        {
            Rectangle screenRect = game.Camera.WorldToScreen(WorldRectangle());
            int width = Convert.ToInt32(FrameWidth * scale);
            int height = Convert.ToInt32(FrameHeight * scale);
            int dstX = (int)ScreenCenter().X - (int)(width / 2.0f);
            int dstY = (int)ScreenCenter().Y - (int)(height/ 2.0f);
            return new Rectangle(dstX, dstY, width, height);
        }
        #endregion

        #region Update and Draw
        public void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //update animation
            if (elapsedTime > duration)
            {
                elapsedTime = 0.0f;
                if (animate && !expired)
                    currentFrame += 1;
                //check if animation needs to loop
                if (currentFrame > TotalFrames - 1)
                {
                    if (loop == true)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        expired = false;
                    }
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (expired || !isOnScreen())
                return;
            spriteBatch.Draw(texture, dstRect(), getFrame(currentFrame), Color.White, rotation, RelativeCenter(), SpriteEffects.None, depth);
        }

        public void DrawTile(SpriteBatch spriteBatch, int X, int Y, float depth)
        {
            spriteBatch.Draw(texture,
                game.Camera.WorldToScreen(new Rectangle(X, Y, game.tilemap.TileWidth, game.tilemap.TileHeight)),
                getFrame(currentFrame), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, depth);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (expired || !isOnScreen())
                return;
            spriteBatch.Draw(texture, dstRect(), getFrame(currentFrame), Color.White, rotation, RelativeCenter(), SpriteEffects.None, depth);
        }
        #endregion
    }
}