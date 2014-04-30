using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    class Background
    {
        Texture2D bkg;
        Texture2D overlay;
        Texture2D bkOverlay;
        Texture2D clouds;
        GraphicsDevice device;
        int offset;
        //for parallax background
        public Background(GraphicsDevice device, Texture2D bkg, Texture2D overlay, Texture2D clouds)
        {
            //we have hardcoded values ofc
            this.device = device;
            this.offset = 0;
            this.bkg = bkg;
            this.overlay = overlay;
            this.bkOverlay = overlay;
            this.clouds = clouds;
        }

        ~Background()
        {
        }

        public void run(int i)
        {
            //move while we are doin stuff-
            offset = i;
        }

        public void draw(SpriteBatch batch)
        {
            batch.Draw(bkg, new Vector2(0, 0), Color.White);
            batch.Draw(clouds, new Vector2(0, 0), new Rectangle(-offset / 20, 0, 2560, 2048), Color.Gray);
            //batch.Draw(overlay, new Rectangle(offset*10, 0, 1366, 768), Color.White);
            batch.Draw(bkOverlay, new Vector2(0, 0), new Rectangle(-offset / 15, 0, 2560, 2048), Color.Black);
            batch.Draw(overlay, new Vector2(0, 0), new Rectangle(-offset/10, 0, 2560, 2048), Color.White);
        }
    }
}
