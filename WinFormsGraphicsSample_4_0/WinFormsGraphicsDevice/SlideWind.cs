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
            
            weatherGraph = uiManager.addGraph(new Vector2(200, 10), new Vector2(800, 800), small, this);
        }

        
        public override void run()
        {
            
            base.run();
        }
        
        public void update(float wndSpd, float dir)
        {
            weatherGraph.update(wndSpd);
           
        }
    }
}
