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
            uiManager.addStaticText(new Vector2(20, 80), new Vector2(200, 200), "Weather - Wind Speed", font, this);
            uiManager.addStaticText(new Vector2(20, 300), new Vector2(200, 200), "The current Wind Speed", fontSize32, this);
            instantWindSpeedText = uiManager.addStaticText(new Vector2(35, 375), new Vector2(200, 200), "is 420 M/S", fontSize32, this);
            uiManager.addStaticText(new Vector2(20, 550), new Vector2(200, 200), "The Wind Direction is", fontSize32, this);
            windDirectionText = uiManager.addStaticText(new Vector2(20, 600), new Vector2(200, 200), "90 degrees from NORTH", fontSize32, this);
            weatherGraph = uiManager.addGraph(new Vector2(725, 20), new Vector2(775, 800), small, this);
        }

        
        public override void run()
        {
            
            base.run();
        }
        
        public void update(float wndSpd, float dir)
        {
            weatherGraph.update(wndSpd);
            instantWindSpeedText.setText("is " + wndSpd.ToString() + " M/S");
            windDirectionText.setText( ((int)((dir * 180) / 3.141)).ToString() + " degrees from NORTH");
        }
    }
}
