using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WinFormsGraphicsDevice
{
    //setting up more hierarchy
    class UIButton : WinFormsGraphicsDevice.UIElement
    {
        protected char text;
        public UIButton(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, char text, UIElement parent) :
            base(position, size, parent, E_UI_TYPES.UI_BUTTON)
        {
            this.text = text;
        }

        public override void draw(SpriteBatch batch)
        {
            //draw chillun
            base.draw(batch);
        }
        //acccessor function
        public bool getMouseOver()
        {
            //get the x and y coords for mouse and check
            return ((Mouse.GetState().X > position.X && Mouse.GetState().X + size.X < position.X + size.X)
                && (Mouse.GetState().Y > position.Y && Mouse.GetState().Y + size.Y < position.Y + size.Y));
        }


    }
}
