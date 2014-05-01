using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WinFormsGraphicsDevice
{
    //class to handle each slide invididually
    class SlideInfo : Slide
    {
        public SlideInfo(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) : 
            base(graphicsDevice, uiManager, Content, position, size)
        {
            uiManager.addStaticText(new Vector2(20, 20), new Vector2(200, 200), "UCR Information", font, this);
            uiManager.addStaticText(new Vector2(20, 150), new Vector2(200, 200), "It is 14 April, 2077", fontSize48, this);
            uiManager.addStaticText(new Vector2(20, 300), new Vector2(400, 400), "Information about UCR goes here", small, this);
        }

        ~SlideInfo()
        {
        }

        public override void run()
        {
            //if we need to run anything here
            //we probably dont for this slide
            
        }
    }
}
