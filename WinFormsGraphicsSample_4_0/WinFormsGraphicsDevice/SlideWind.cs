using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WinFormsGraphicsDevice
{
    class SlideWind : Slide
    {
        UIGraph weatherGraph;
        UIStaticText instantWindSpeedText;
        UIStaticText windDirectionText;
        public SlideWind(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) :
            base(graphicsDevice, uiManager, Content, position, size)
        {
            uiManager.addStaticText(new Vector2(20, 20), new Vector2(200, 200), "Weather - Wind Speed", font, this);
            instantWindSpeedText = uiManager.addStaticText(new Vector2(20, 160), new Vector2(200, 200), "The current Wind Speed is 420 M/S", fontSize48, this);
            windDirectionText = uiManager.addStaticText(new Vector2(20, 250), new Vector2(200, 200), "The Wind Direction is 90 degrees from NORTH", small, this);
            weatherGraph = uiManager.addGraph(new Vector2(60, 1000), new Vector2(1000, 1000), small, this);
        }

        
        public override void run()
        {
            
            base.run();
        }
        
        public void update(float wndSpd, float dir)
        {
            weatherGraph.update(wndSpd);
            instantWindSpeedText.setText("The current Wind Speed is " + wndSpd.ToString() + " M/S");
            windDirectionText.setText("The Wind Direction is " + ((int)((dir*180)/3.141)).ToString() + " degrees from NORTH");
        }
    }
}
