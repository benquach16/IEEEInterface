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
        protected String text;
        protected Texture2D butTex;
        public UIButton(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, String text, UIElement parent) :
            base(position, size, parent, E_UI_TYPES.UI_BUTTON)
        {
            this.text = text;
            this.butTex = new Texture2D(graphicsDevice, (int)size.X, (int)size.Y);
            Color[] colorData = new Color[(int)size.X * (int)size.Y];
            for (int i = 0; i < size.X * size.Y; i++)
            {
                colorData[i] = Color.Gainsboro;
            }
            butTex.SetData<Color>(colorData);
        }

        public override void draw(SpriteBatch batch)
        {
            //draw chillun
            if (visible)
            {
                //draw this
                batch.Draw(butTex, position, Color.White);
            }
            base.draw(batch);
        }
        //acccessor function
        public bool getMouseOver()
        {
            //get the x and y coords for mouse and check
            return ((Mouse.GetState().X > position.X && Mouse.GetState().X + size.X < position.X + size.X)
                && (Mouse.GetState().Y > position.Y && Mouse.GetState().Y + size.Y < position.Y + size.Y));
        }
        public String getText()
        {
            return text;
        }

    }
}
