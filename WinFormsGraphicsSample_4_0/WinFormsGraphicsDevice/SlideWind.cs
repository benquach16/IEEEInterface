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
        public SlideWind(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) :
            base(graphicsDevice, uiManager, Content, position, size)
        {
            uiManager.addStaticText(new Vector2(20, 20), new Vector2(200, 200), "Weather - Wind Speed", font, this);
            uiManager.addStaticText(new Vector2(20, 150), new Vector2(200, 200), "The current Wind Speed is 420 M/S", small, this);
            uiManager.addStaticText(new Vector2(20, 200), new Vector2(200, 200), "The Wind Direction is 90 degrees from North", small, this);
            weatherGraph = uiManager.addGraph(new Vector2(60, 1000), new Vector2(1000, 1000), small, this);
        }

        public override void run()
        {
            
            base.run();
        }
        public void update(float input)
        {
            weatherGraph.update(input);
        }
    }
}
