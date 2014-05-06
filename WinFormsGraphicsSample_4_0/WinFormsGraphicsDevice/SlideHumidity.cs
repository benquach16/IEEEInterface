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
        public SlideHumidity(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) :
            base(graphicsDevice, uiManager, Content, position, size)
        {
            uiManager.addStaticText(new Vector2(20, 190), new Vector2(200, 200), "The Relative Humidity is 77%", fontSize48, this);
            uiManager.addStaticText(new Vector2(20, 20), new Vector2(200, 200), "Weather - Humidity", font, this);
            uiManager.addStaticText(new Vector2(20, 290), new Vector2(200, 200), "The Dew Point is 35 C", fontSize32, this);
            uiManager.addStaticText(new Vector2(20, 360), new Vector2(200, 200), "The Atmospheric Pressure is 1000hPa", fontSize32, this);
        }
        public override void run()
        {
            //if we need to run anything here
            //we probably dont for this slide

        }
    }
}
