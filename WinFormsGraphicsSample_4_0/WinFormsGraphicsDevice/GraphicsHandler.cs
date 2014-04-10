#region File Description
//-----------------------------------------------------------------------------
// SpinningTriangleControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace WinFormsGraphicsDevice
{
    /// <summary>
    /// Example control inherits from GraphicsDeviceControl, which allows it to
    /// render using a GraphicsDevice. This control shows how to draw animating
    /// 3D graphics inside a WinForms application. It hooks the Application.Idle
    /// event, using this to invalidate the control, which will cause the animation
    /// to constantly redraw.
    /// </summary>
    class GraphicsHandler : GraphicsDeviceControl
    {
        //global variables
        const int WINDOWX = 1366;
        const int WINDOWY = 768;
        const int SIDEBARX = 300;
        const int WINDOWDIFF = 4;

        //initialize some varaibles here
        bool sweptRight;
        bool sweptLeft;
        bool takingInput;
        int oldX;
        
        BasicEffect effect;
        Stopwatch timer;
        SpriteBatch spriteBatch;
        Texture2D bkg;
        UIManager uiManager;
        ContentManager Content;
        UIWindow sidebar;
        UIWindow weatherWindow;
        UIWindow infoWindow;
        UIWindow humidityWindow;
        UIWindow windWindow;
        UIGraph weatherGraph;
        

        int currentSlide;
        List<UIElement> slides;
        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            // Create our effect.
            this.sweptLeft = false;
            this.sweptRight = false;
            this.takingInput = false;
            this.oldX = 0;

            effect = new BasicEffect(GraphicsDevice);
            slides = new List<UIElement>();
            effect.VertexColorEnabled = true;

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };

            MouseMove += new MouseEventHandler(GH_MouseMove);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            ServiceContainer services = new ServiceContainer();
            Content = new ContentManager(Services, "Content");
            bkg = Content.Load<Texture2D>("bkg");
            Texture2D t = Content.Load<Texture2D>("test");
            
            //TODO: REPLACE THIS WITH NON SHITTY FONT
            SpriteFont font = Content.Load<SpriteFont>("defaultFont");

            uiManager = new UIManager(GraphicsDevice);

            infoWindow = uiManager.addWindow(new Vector2(0, 0), new Vector2(1366, 768));
            //use windowdiff as offset
            weatherWindow = uiManager.addWindow(new Vector2(WINDOWX+WINDOWDIFF, 0), new Vector2(WINDOWX, WINDOWY));
            humidityWindow = uiManager.addWindow(new Vector2((WINDOWX * 2) + WINDOWDIFF*2, 0), new Vector2(WINDOWX, WINDOWY));
            windWindow = uiManager.addWindow(new Vector2((WINDOWX * 3) + WINDOWDIFF * 3, 0), new Vector2(WINDOWX, WINDOWY));

            slides.Add(windWindow);
            slides.Add(humidityWindow);
            slides.Add(weatherWindow);
            slides.Add(infoWindow);
            sidebar = uiManager.addWindow(new Vector2(1066, 0), new Vector2(SIDEBARX, WINDOWY));
            uiManager.addButton(new Vector2(10, 300), new Vector2(280, 60), "Frogger", font, sidebar);
            uiManager.addButton(new Vector2(10, 400), new Vector2(280, 60), "Pong", font, sidebar);
            
            uiManager.addStaticText(new Vector2(0, 0), new Vector2(200, 200), "UCR Information", font, infoWindow);
            uiManager.addStaticText(new Vector2(20, 60), new Vector2(400, 400), "Information about UCR goes here", font, infoWindow);
            uiManager.addStaticText(new Vector2(0, 0), new Vector2(200,200), "Weather - Tempurature", font, weatherWindow);
            uiManager.addStaticText(new Vector2(10, 90), new Vector2(200, 200), "666 C", font, weatherWindow);
            uiManager.addStaticText(new Vector2(0, 0), new Vector2(200, 200), "Weather - Humidity", font, humidityWindow);
            uiManager.addStaticText(new Vector2(0, 0), new Vector2(200, 200), "Weather - Wind Speed", font, windWindow);
            uiManager.addGraph(new Vector2(20, 50), new Vector2(700, 700), windWindow);

            this.currentSlide = slides.Count - 1 ;
        }

        private void GH_MouseMove(object sender, MouseEventArgs e)
        {
            MainForm.mX = e.X;
            MainForm.mY = e.Y;
        }
        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Black);
            //
            //TODO:HANDLE SWIPING GESTURES!!!!!
            //
            float aspect = GraphicsDevice.Viewport.AspectRatio;
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 10);
            // Set renderstates.
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            updateSwipes();
            spriteBatch.Begin();
            
            //draw cool background first
            //!!MAKE SURE THAT WE ASSIGN THE RECT TO 2560x2048!!
            spriteBatch.Draw(bkg, new Rectangle(0, 0, 1366, 768), Color.White);
            uiManager.drawAll(spriteBatch);
            spriteBatch.End();

        }
        protected void updateSwipes()
        {
            //update the bools for swipes ehre
            int tempX, tempY;
            tempX = MainForm.mX;
            tempY = MainForm.mY;

            //do this over a period of time
            doSwipes();
            //setSlidePositions();

        }
        //shift all slides over
        protected void decrementSlide()
        {
            if (currentSlide > 0)
                currentSlide--;
            
        }
        protected void incrementSlide()
        {
            if (currentSlide < slides.Count)
                currentSlide++;
        }

        protected void doSwipes()
        {
            //set a timer here
            timer.Start();
            if (!takingInput)
            {
                oldX = MainForm.mX;
                takingInput = true;
            }
            if (timer.ElapsedMilliseconds > 100)
            {
                //check for left or right 
                if ((oldX - MainForm.mX) > 500)
                {
                    //swept to the left

                    if (currentSlide > 0)
                    {
                        currentSlide--;
                        sweptLeft = true;
                    }
                }
                else if ((oldX - MainForm.mX) < -500)
                {
                    //swept to the right

                    if (currentSlide < slides.Count - 1)
                    {
                        currentSlide++;
                        sweptRight = true;
                    }
                    
                }
                timer.Reset();
                takingInput = false;
            }

            if (sweptLeft)
            {
                for (int i = 0; i < slides.Count; i++)
                {
                    if (!slides[i].stopTransition())
                        slides[i].transition();
                    else
                    {
                        //end everything here
                        slides[i].resetTransition();
                        sweptLeft = false;
                        setSlidePositions();
                    }
                }
            }
            else if (sweptRight)
            {
                for (int i = 0; i < slides.Count; i++)
                {
                    if (!slides[i].stopTransition())
                        slides[i].transition(false);
                    else
                    {
                        slides[i].resetTransition();
                        sweptRight = false;
                        setSlidePositions();
                    }
                }
            }
        }

        //here we artificially set the slides to their correct positions in case of transition lag
        protected void setSlidePositions()
        {
            /*
            slides[currentSlide].setPosition(new Vector2(0, 0));
            //all slides after
            for (int i = currentSlide-1; i >= 0; i--)
            {
                //take into account if i is 0
                slides[i].setPosition(new Vector2((currentSlide-i)* 1366, 0));
            }
            for (int i = currentSlide+1; i < slides.Count; i++)
            {
                slides[i].setPosition(new Vector2((currentSlide-i)* 1366, 0));
            }*/
            for (int i = 0; i < slides.Count; i++)
            {
                slides[i].setPosition(new Vector2((currentSlide - i) * 1366, 0));
            }
        }
    }
}
