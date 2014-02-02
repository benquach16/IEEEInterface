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
        UIWindow(Vector2 position, Vector2 size, UIElement parent) : 
            base(position, size, parent, E_UI_TYPES.UI_WINDOW)
        {
            //initialize the window texture
            //remporary thing here
            
        }
        public void loadContent(GraphicsDevice graphicsDevice)
        {

        }
        //override pure virtual functions
        public override void draw(SpriteBatch batch)
        {
            //draw a rectangle that is the of the position and size specificed
            batch.Draw(winTex, position, Color.White);
            base.draw(batch);
        }
        public override UIElement.E_UI_TYPES getUIType()
        {
            return UIElement.E_UI_TYPES.UI_WINDOW;
        }
    }
}
