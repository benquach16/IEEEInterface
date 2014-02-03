using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    class UIWindow : UIElement
    {
        protected Texture2D winTex;
        //inheritance
        public UIWindow(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, UIElement parent = null) : 
            base(position, size, parent, E_UI_TYPES.UI_WINDOW)
        {
            //initialize the window texture
            //remporary thing here
            winTex = new Texture2D(graphicsDevice, (int)size.X, (int)size.Y, false, SurfaceFormat.Color);
            Color[] colorData = new Color[(int)size.X * (int)size.Y];
            for (int i = 0; i < size.X * size.Y; i++)
                colorData[i] = Color.Aqua;
            winTex.SetData<Color>(colorData);
        }
        public void loadContent(GraphicsDevice graphicsDevice)
        {

        }
        //override pure virtual functions
        public override void draw(SpriteBatch batch)
        {
            //draw a rectangle that is the of the position and size specificed
            batch.Draw(winTex, position, Color.White);
            //base.draw(batch);
        }
        public override UIElement.E_UI_TYPES getUIType()
        {
            return UIElement.E_UI_TYPES.UI_WINDOW;
        }
    }
}
