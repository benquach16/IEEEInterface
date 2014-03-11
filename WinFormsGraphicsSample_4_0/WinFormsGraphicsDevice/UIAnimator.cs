using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    class UIAnimator : UIElement
    {
        //image swapping class or gif playing class designed for ui animations
        //GIFS REQUIRE A LIBRARY SO WE GO HARD
        //images should be named a certain way
        //like image[i]
        protected int numImages;
        protected int animLength;
        protected int currentImage;
        Texture2D currentTexture;
        GraphicsDevice device;
        public UIAnimator(GraphicsDevice device, Vector2 position, Texture2D image, int numImages, UIElement parent = null) :
            base(position, new Vector2(image.Bounds.Width, image.Bounds.Height), parent, E_UI_TYPES.UI_ANIMATION)
        {
            currentTexture = image;
            currentImage = 0;
            this.device = device;
        }
        ~UIAnimator()
        {
        }

        public override void  draw(SpriteBatch batch)
        {
            //lets do the image flipping here
            batch.Draw(currentTexture, position, Color.White);
            currentImage++;
            //get the next image and set it
            
            //reload the image
 	        base.draw(batch);
        }


    }
}
