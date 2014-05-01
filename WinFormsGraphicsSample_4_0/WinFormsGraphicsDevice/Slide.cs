using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WinFormsGraphicsDevice
{
    class Slide : UIWindow
    {
        protected GraphicsDevice graphicsDevice;
        protected UIManager uiManager;
        protected ContentManager Content;
        protected SpriteFont font;
        protected SpriteFont small;
        protected SpriteFont fontSize32;
        protected SpriteFont fontSize48;
        public Slide(GraphicsDevice graphicsDevice, UIManager uiManager, ContentManager Content, Vector2 position, Vector2 size) :
            base(graphicsDevice, position, size, null)
        {
            this.graphicsDevice = graphicsDevice;
            this.uiManager = uiManager;
            this.Content = Content;
            font = Content.Load<SpriteFont>("defaultFont");
            small = Content.Load<SpriteFont>("smallFont");
            fontSize32 = Content.Load<SpriteFont>("Size32");
            fontSize48 = Content.Load<SpriteFont>("fontSize48");
        }
    }
}
