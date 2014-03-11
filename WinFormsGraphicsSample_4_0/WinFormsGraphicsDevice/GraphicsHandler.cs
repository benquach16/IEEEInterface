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
        UIWindow weatherWindow;
        UIWindow infoWindow;
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
            this.currentSlide = 0;
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
            weatherWindow = uiManager.addWindow(new Vector2(10, 10), new Vector2(1000, 600));
            slides.Add(weatherWindow);

            infoWindow = uiManager.addWindow(new Vector2(1010, 10), new Vector2(1000, 600));
            slides.Add(infoWindow);

            uiManager.addStaticText(new Vector2(0,0), new Vector2(200,200), "Weather", font, weatherWindow);
            uiManager.addImage(new Vector2(20, 20), t, weatherWindow);
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
                doLeftSwipe();

        }
        //shift all slides over
        protected void decrementSlide()
        {
            if (currentSlide > 0)
                currentSlide--;
            
        }
        protected void incrementSlide()
        {
            if (currentSlide < slides.Count-1)
                currentSlide++;
        }

        public bool getLeftSwipe()
        {
            //this gun be a slight pain
            //record for 1 second
            return sweptLeft;
        }
        public bool getRightSwipe()
        {
            return sweptRight;
        }
        protected void doRightSwipe()
        {
            if (getRightSwipe())
            {

            }
        }
        protected void doLeftSwipe()
        {
            if (getLeftSwipe())
            {
                timer.Start();
                if (timer.ElapsedMilliseconds < 500)
                {
                    //all slides need to transition
                    for(int i = 0; i < slides.Count; i++)
                        slides[i].transition();
                }
                else
                {
                    incrementSlide();
                    timer.Reset();
                    sweptLeft = false;
                }
            }
            else
            {
                //find right to left movement
                timer.Start();
                if (timer.ElapsedMilliseconds < 500)
                {
                    if (!this.takingInput)
                    {
                        this.oldX = MainForm.mX;
                        this.takingInput = true;
                    }

                }
                else
                {
                    if ((this.oldX - MainForm.mX) > 50)
                    {
                        sweptLeft = true;
                    }
                    timer.Reset();
                    this.takingInput = false;
                    
                }

            }
        }
    }
}
