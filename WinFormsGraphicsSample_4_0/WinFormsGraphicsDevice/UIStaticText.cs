using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    class UIStaticText : UIElement
    {
        protected String text;
        protected SpriteFont font;
        //static text class
        public UIStaticText(Vector2 position, Vector2 size, String text, SpriteFont font, UIElement parent) : 
            base(position, size, parent, E_UI_TYPES.UI_STATIC_TEXT)
        {
            //just draw text on screen
        }

        public void draw(SpriteBatch batch)
        {
            if (visible)
            {
            }
            base.draw(batch);
        }
    }
}
