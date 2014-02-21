using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    //graph class for getting input from arduino chip
    class UIGraph : UIElement
    {
        protected Texture2D bkg;
        protected int x, y;
        public UIGraph(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, UIElement parent = null)
            : base(position, size, parent, E_UI_TYPES.UI_GRAPH)
        {
            this.bkg = new Texture2D(graphicsDevice, (int)size.X, (int)size.Y, false, SurfaceFormat.Color);
            Color[] colorData = new Color[(int)size.X * (int)size.Y];
            for (int i = 0; i < (int)size.X * (int)size.Y; i++)
            {
                //fill with temp colors for now

            }

        }

        public override void draw(SpriteBatch batch)
        {
            //for now, just draw like a box
            if (visible)
            {
                batch.Draw(bkg, position, Color.White);
            }
            base.draw(batch);
        }

        public void update(float input)
        {
            //use this function to update the graph

        }

    }
}
