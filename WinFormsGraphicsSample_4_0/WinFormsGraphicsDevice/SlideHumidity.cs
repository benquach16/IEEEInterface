using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace WinFormsGraphicsDevice
{
    class SlideHumidity : Slide
    {
        UIStaticText dewText;
        UIStaticText atmoText;
        UIStaticText radText;
        
        public SlideHumidity(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) :
            base(graphicsDevice, uiManager, Content, position, size)
        {
            dewText = uiManager.addStaticText(new Vector2(20, 100), new Vector2(200, 200), "The Dew Point is 35 C", fontSize32, this);
            atmoText = uiManager.addStaticText(new Vector2(20, 250), new Vector2(200, 200), "The Atmospheric Pressure is 1000hPa", fontSize32, this);
            radText = uiManager.addStaticText(new Vector2(20, 400), new Vector2(200, 200), "The Solar Radiation level is 500 watts per square meter", small, this);
            
        }
        public void update(float newRad, float newDew, float wndSpd, float dir)
        {
            dewText.setText("The Dew Point is " + newDew.ToString() + " C");
            radText.setText("The Solar Radiation Level is " + newRad.ToString() + " watts per square meter");
            
        }
        public override void run()
        {
            //if we need to run anything here
            //we probably dont for this slide

        }
    }
}
