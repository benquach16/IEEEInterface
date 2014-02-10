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
            this.font = font;
            this.text = text;
        }

        public override void draw(SpriteBatch batch)
        {
            //draw chillun
            if (visible)
            {
                //draw two times, one for shadow
                batch.DrawString(font, text, absolutePosition + new Vector2(-2,2), Color.Black);
                batch.DrawString(font, text, absolutePosition, Color.White);
            }
            base.draw(batch);
        }

        public String getText()
        {
            return text;
        }
        public void setText(String newText)
        {
            text = newText;
        }
    }
}
