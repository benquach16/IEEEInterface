﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    //class for windows
    class UIWindow : UIElement
    {
        protected Texture2D winTex;
        protected Texture2D glass;
        protected Color color;
        protected float alpha;
        //inheritance
        public UIWindow(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, UIElement parent = null) : 
            base(position, size, parent, E_UI_TYPES.UI_WINDOW)
        {
            //initialize the window texture
            //remporary thing here
            winTex = new Texture2D(graphicsDevice, (int)size.X, (int)size.Y, false, SurfaceFormat.Color);
            Color[] colorData = new Color[(int)size.X * (int)size.Y];
            for (int i = 0; i < size.X * size.Y; i++)
            {
                //colorData[i] = Color.Gainsboro;
                if (i % size.X == 0 || i % size.X == size.X-1)
                {
                    colorData[i] = new Color(255, 255, 255, 255);
                }
                else
                {
                    int texColour = (i / (768 * 5));
                    colorData[i] = new Color(texColour, texColour, texColour, 0);
                }
            }
            winTex.SetData<Color>(colorData);

            this.alpha = 0.5f;
        }
        //this is probably deprecated
        public void loadContent(GraphicsDevice graphicsDevice)
        {

        }
        //override pure virtual functions
        public override void draw(SpriteBatch batch)
        {
            //draw a rectangle that is the of the position and size specificed
            if(visible)
                batch.Draw(winTex, position, Color.White*alpha);
            base.draw(batch);
        }
        public override UIElement.E_UI_TYPES getUIType()
        {
            return UIElement.E_UI_TYPES.UI_WINDOW;
        }
    }
}
