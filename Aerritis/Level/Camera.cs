using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aerritis.Level
{
    public class Camera
    {
        private Vector2 position = Vector2.Zero;
        private Vector2 viewPortSize = Vector2.Zero;
        private Rectangle worldRectangle = new Rectangle(0, 0, 0, 0);
        GraphicsDeviceManager graphics;

        public Camera(ref GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            setViewPortWidth(graphics.PreferredBackBufferWidth);
            setViewPortHeight(graphics.PreferredBackBufferWidth);
        }

        private Rectangle WorldRectangle => worldRectangle;
        public int getViewPortWidth()
        {
            return (int)viewPortSize.X;
        }
        public int getViewPortHeight()
        {
            return (int)viewPortSize.Y;
        }
        public void setViewPortWidth(float value)
        {
            viewPortSize.X = value;
            graphics.PreferredBackBufferWidth = (int)value;
            graphics.ApplyChanges();
        }
        public void setViewPortHeight(float value)
        {
            viewPortSize.Y = value;
            graphics.PreferredBackBufferHeight = (int)value;
            graphics.ApplyChanges();
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

        public Rectangle ViewPort
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, getViewPortWidth(), getViewPortHeight());
            }
        }

        public void Move(Vector2 offset)
        {
            position += offset;
        }

        public Boolean isObjectVisible(Rectangle bounds)
        {
            return ViewPort.Intersects(bounds);
        }

        public Vector2 WorldToScreen(Vector2 worldLocation)
        {
            return worldLocation - position;
        }

        public Rectangle WorldToScreen(Rectangle worldRect)
        {
            return new Rectangle(
            worldRect.Left - (int)position.X,
            worldRect.Top - (int)position.Y,
            worldRect.Width,
            worldRect.Height);
        }

        public Vector2 ScreenToWorld(Vector2 screenLocation)
        {
            return screenLocation += position;
        }

        public Rectangle ScreenToWorld(Rectangle screenRect)
        {
            return new Rectangle(
                screenRect.Left - (int)position.X,
                screenRect.Top - (int)position.Y,
                screenRect.Width,
                screenRect.Height);
        }
    }
}