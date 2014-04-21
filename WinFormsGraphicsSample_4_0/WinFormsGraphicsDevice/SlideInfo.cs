using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    //class to handle each slide invididually
    class SlideInfo : UIWindow
    {
        protected UIManager uiManager;
        public SlideInfo(GraphicsDevice graphicsDevice, UIManager uiManager, Vector2 position, Vector2 size) : 
            base(graphicsDevice, position, size, null)
        {
            this.uiManager = uiManager;
            //uiManager.addStaticText(new Vector2(0, 0), new Vector2(200, 200), "UCR Information", font, this);
            //uiManager.addStaticText(new Vector2(20, 60), new Vector2(400, 400), "Information about UCR goes here", small, this);
        }

        ~SlideInfo()
        {
        }

        public void run()
        {
            //if we need to run anything here
        }
    }
}
