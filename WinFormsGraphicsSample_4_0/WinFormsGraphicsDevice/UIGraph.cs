﻿using System;
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
        public UIGraph(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, UIElement parent)
            : base(position, size, parent, E_UI_TYPES.UI_GRAPH)
        {
            this.bkg = new Texture2D(graphicsDevice, (int)size.X, (int)size.Y, false, SurfaceFormat.Color);

        }

        public override void draw(SpriteBatch batch)
        {
            //for now, just draw like a box
            base.draw(batch);
        }

    }
}