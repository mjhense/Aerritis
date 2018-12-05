using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Aerritis.Generic
{
    public static class Tools
    {
        public static Vector2 getMousePosition()
        {
            Point pos = Mouse.GetState().Position;
            return new Vector2(pos.X, pos.Y);
        }

        public static Rectangle getMouseBounds()
        {
            Vector2 pos = getMousePosition();
            return new Rectangle((int)pos.X, (int)pos.Y, 1, 1);
        }

        public static ButtonState getMouseButtonsLeft()
        {
            return Mouse.GetState().LeftButton;
        }
        public static ButtonState getMouseButtonsRight()
        {
            return Mouse.GetState().RightButton;
        }

        public static float Distance(Vector2 pos1, Vector2 pos2)
        {
            float x = Math.Abs(pos1.X - pos2.X);
            float y = Math.Abs(pos1.Y - pos2.Y);
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
    }
}
