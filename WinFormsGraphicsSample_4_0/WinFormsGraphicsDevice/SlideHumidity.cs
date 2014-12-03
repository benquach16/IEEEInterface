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
        UIStaticText instantWindSpeedText;
        UIStaticText windDirectionText;
        public SlideHumidity(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) :
            base(graphicsDevice, uiManager, Content, position, size)
        {
            dewText = uiManager.addStaticText(new Vector2(20, 290), new Vector2(200, 200), "The Dew Point is 35 C", fontSize32, this);
            atmoText = uiManager.addStaticText(new Vector2(20, 360), new Vector2(200, 200), "The Atmospheric Pressure is 1000hPa", fontSize32, this);
            radText = uiManager.addStaticText(new Vector2(20, 4205), new Vector2(200, 200), "The Solar Radiation level is 500 watts per square meter", small, this);
            instantWindSpeedText = uiManager.addStaticText(new Vector2(20, 160), new Vector2(200, 200), "The current Wind Speed is 420 M/S", fontSize48, this);
            windDirectionText = uiManager.addStaticText(new Vector2(20, 250), new Vector2(200, 200), "The Wind Direction is 90 degrees from NORTH", small, this);
        }
        public void update(float newRad, float newDew, float wndSpd, float dir)  
        {
            dewText.setText("The Dew Point is " + newDew.ToString() + " C");
            radText.setText("The Solar Radiation Level is " + newRad.ToString() + " watts per square meter");
            instantWindSpeedText.setText("The current Wind Speed is " + wndSpd.ToString() + " M/S");
            windDirectionText.setText("The Wind Direction is " + ((int)((dir * 180) / 3.141)).ToString() + " degrees from NORTH");
        }
        public override void run()
        {
            //if we need to run anything here
            //we probably dont for this slide

        }
    }
}
