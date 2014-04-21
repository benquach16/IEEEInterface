using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    //ui image class
    class UIImage : UIElement
    {
        protected Texture2D image;
        protected Vector2 scale;
        //constructor
        public UIImage(GraphicsDevice graphicsDevice, Vector2 position, Texture2D image, UIElement parent = null) : 
            base(position, new Vector2(image.Bounds.Width,image.Bounds.Height), parent, E_UI_TYPES.UI_IMAGE)
        {
            this.image = image;
            this.scale = new Vector2(1, 1);
           
        }


        public override void draw(SpriteBatch batch)
        {
            //draw the image with the spritebatch
            if(visible)
                batch.Draw(image, absolutePosition, Color.White);
            base.draw(batch);
        }

        //mutators and accesssors
        public Vector2 getScale()
        {
            return scale;
        }
        //doesnt do anything atm
        public void setScale(Vector2 newScale)
        {
            scale = newScale;
        }
        public void setImage(Texture2D image)
        {
            this.image = image;
        }
    }
}
