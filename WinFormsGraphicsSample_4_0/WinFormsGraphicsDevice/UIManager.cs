﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

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
        //destructor
        ~UIManager()
        {
            unparentedNodes.Clear();
            GC.Collect();
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
        //adds a button to the scene
        public UIButton addButton(Vector2 position, Vector2 size, string text, SpriteFont font, UIElement parent = null)
        {
            UIButton ret = new UIButton(graphicsDevice, position, size, text, font, parent);
            if (parent == null)
            {
                unparentedNodes.Add(ret);
            }
            return ret;
        }
        public UIStaticText addStaticText(Vector2 position, Vector2 size, String text, SpriteFont font, UIElement parent = null)
        {
            UIStaticText ret = new UIStaticText(position, size, text, font, parent);
            if (parent == null)
            {
                unparentedNodes.Add(ret);

            }
            return ret;
        }
        public UIImage addImage(Vector2 position, Texture2D image, UIElement parent = null)
        {
            UIImage ret = new UIImage(graphicsDevice, position, image, parent);
            if (parent == null)
            {
                unparentedNodes.Add(ret);
            }
            return ret;
        }
        public UIGraph addGraph(Vector2 position, Vector2 size, SpriteFont font, UIElement parent = null)
        {
            UIGraph ret = new UIGraph(graphicsDevice, position, size, font, parent);
            if (parent == null)
                unparentedNodes.Add(ret);
            return ret;
        }

        //for individual slides
        //create information slide
        public SlideInfo createInfoSlide(ContentManager Content, Vector2 position, Vector2 size)
        {
            SlideInfo ret = new SlideInfo(graphicsDevice,this,Content, position, size);
            unparentedNodes.Add(ret);
            return ret;
        }
        //create a tempurature reading slide here
        public SlideTemp createTempuratureSlide(ContentManager Content, Vector2 position, Vector2 size)
        {
            SlideTemp ret = new SlideTemp(graphicsDevice, this, Content, position, size);
            unparentedNodes.Add(ret);
            return ret;
        }
        public SlideHumidity createHumiditySlide(ContentManager Content, Vector2 position, Vector2 size)
        {
            SlideHumidity ret = new SlideHumidity(graphicsDevice, this, Content, position, size);
            unparentedNodes.Add(ret);
            return ret;
        }
        public SlideWind createWindSlide(ContentManager Content, Vector2 position, Vector2 size)
        {
            SlideWind ret = new SlideWind(graphicsDevice, this, Content, position, size);
            unparentedNodes.Add(ret);
            return ret;
        }
        
    }
}
