﻿using System;
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
        protected SpriteFont font;
        //Long consturctor
        public UIButton(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, String text, SpriteFont font, UIElement parent) :
            base(position, size, parent, E_UI_TYPES.UI_BUTTON)
        {
            this.text = text;
            this.font = font;
            this.butTex = new Texture2D(graphicsDevice, (int)size.X, (int)size.Y);
            Color[] colorData = new Color[(int)size.X * (int)size.Y];
            for (int i = 0; i < size.X * size.Y; i++)
            {
                if (i % size.X == 0 || i % size.X == size.X - 1)
                {
                    colorData[i] = new Color(200, 200, 200, 255);
                }
                else if (i < size.X || (i < size.X * size.Y) && i > size.X * (size.Y - 1))
                {
                    colorData[i] = new Color(200, 200, 200, 255);
                }
                else
                {
                    int texColor = 64;
                    texColor -= i / 250;
                    colorData[i] = new Color(texColor, texColor, texColor, 0);
                }
            }
            butTex.SetData<Color>(colorData);
            alpha = 0.3f;
        }

        public override void draw(SpriteBatch batch)
        {
            //draw chillun
            if (visible)
            {
                //draw this
                batch.Draw(butTex, absolutePosition, Color.White * alpha);
                //draw two times, one for shadow
                //make sure we can center the text
                batch.DrawString(font, text, absolutePosition + new Vector2(size.X/2 - (font.MeasureString(text).X/2),
                    size.Y/2-(font.MeasureString(text).Y/2)), Color.Black);
                batch.DrawString(font, text, absolutePosition + new Vector2(size.X/2 +2 - (font.MeasureString(text).X/2), 
                    size.Y / 2 -2-(font.MeasureString(text).Y/2)), Color.White);
            }
            base.draw(batch);
        }

        //acccessor function
        public bool getMouseOver()
        {
            //get the x and y coords for mouse and check
            return ((Mouse.GetState().X > absolutePosition.X && Mouse.GetState().X < absolutePosition.X + size.X)
                && (Mouse.GetState().Y > absolutePosition.Y && Mouse.GetState().Y < absolutePosition.Y + size.Y));
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
