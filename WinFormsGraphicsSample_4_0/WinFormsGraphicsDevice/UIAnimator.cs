using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    class UIAnimator : UIElement
    {
        //image swapping class or gif playing class designed for ui animations
        //GIFS REQUIRE A LIBRARY SO WE GO HARD
        //images should be loaded into a list
        protected int currentImage;
        Stopwatch timer;
        
        List<Texture2D> textureList;
        GraphicsDevice device;
        public UIAnimator(GraphicsDevice device, Vector2 position, List<Texture2D> textureList, UIElement parent = null) :
            base(position, new Vector2(0, 0), parent, E_UI_TYPES.UI_ANIMATION)
        {
            this.timer = new Stopwatch();
            timer.Start();
            this.textureList = textureList;
            currentImage = 0;
            this.device = device;
            this.size = new Vector2(textureList[0].Bounds.Width, textureList[0].Bounds.Height);
        }
        ~UIAnimator()
        {
        }

        public override void  draw(SpriteBatch batch)
        {
            //lets do the image flipping here
            //change based on a milisecond timer
            batch.Draw(textureList[currentImage], absolutePosition, Color.White);
            //get the next image and set it
            //probably need to set like a timer
            if (timer.ElapsedMilliseconds > 100)
            {
                timer.Reset();
                if (currentImage < textureList.Count)
                    currentImage++;
                else
                    currentImage = 0;
                timer.Start();
            }
            //reload the image
 	        base.draw(batch);
        }

        //just stops the timer
        public void stop()
        {
            timer.Stop();
        }

        public void start()
        {
            timer.Start();
        }


    }
}
