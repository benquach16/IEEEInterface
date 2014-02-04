using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WinFormsGraphicsDevice
{
    class UIManager
    {
        //UIElement root;
        List<UIElement> unparentedNodes;
        //reference to the current graphics device
        GraphicsDevice graphicsDevice;
        public UIManager(GraphicsDevice graphicsDevice)
        {
            //setup the root ui element at 0,0
            this.graphicsDevice = graphicsDevice;
            this.unparentedNodes = new List<UIElement>();
        }
        public void drawAll(SpriteBatch batch)
        {
            //draw all ui elements here
            for (int i = 0; i < unparentedNodes.Count; i++)
            {
                unparentedNodes[i].draw(batch);
            }
        }
        //add factory functions
        /*
         * create window with params
         */
        public UIWindow addWindow(Vector2 position, Vector2 size, UIElement parent = null)
        {
            UIWindow ret = new UIWindow(graphicsDevice, position, size, parent);
            if (parent == null)
            {
                unparentedNodes.Add(ret);
            }
            return ret;
        }
        public UIButton addButton(Vector2 position, Vector2 size, String text, UIElement parent = null)
        {
            UIButton ret = new UIButton(graphicsDevice, position, size, text, parent);
            if (parent == null)
            {
                unparentedNodes.Add(ret);
            }
            return ret;
        }

    }
}
