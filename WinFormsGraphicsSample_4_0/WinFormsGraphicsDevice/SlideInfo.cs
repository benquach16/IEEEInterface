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
        UIStaticText dateText;
        UIStaticText timeText;
        
        public SlideInfo(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) : 
            base(graphicsDevice, uiManager, Content, position, size)
        {
            uiManager.addStaticText(new Vector2(20, 20), new Vector2(200, 200), "UCR Information", font, this);
            dateText = uiManager.addStaticText(new Vector2(20, 160), new Vector2(200, 200), "It is 14 April, 2077", fontSize48, this);
            timeText = uiManager.addStaticText(new Vector2(20, 250), new Vector2(200, 200), "It is now 5 pm", fontSize48, this);
            uiManager.addStaticText(new Vector2(20, 370), new Vector2(400, 400), "Information about UCR goes here", small, this);
            //uiManager.addStaticText(new Vector2(20, 500), new Vector2(400, 400), "BCOE has super special awesome stuff", small, this);
        }

        ~SlideInfo()
        {
        }

        public void update(string date, string time)
        {
            dateText.setText("It is " + date);
            timeText.setText("The current time is " + time);
        }

        public override void run()
        {
            //if we need to run anything here
            //we probably dont for this slide
            
        }
    }
}
