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
        bool transitioning;
        bool takingInput;
        bool pongPressed;
        bool froggerPressed;
        int oldX;
        
        BasicEffect effect;
        Stopwatch timer;
        Stopwatch timer2;
        SpriteBatch spriteBatch;
        Texture2D bkg;
        UIManager uiManager;
        ContentManager Content;
        UIWindow sidebar;
        UIWindow weatherWindow;
        UIWindow infoWindow;
        UIWindow humidityWindow;
        UIWindow windWindow;
        UIButton pongButton;
        UIButton froggerButton;
        UIGraph weatherGraph;
        UIImage weatherImage;
        Background backGrnd;

        //for weather

        enum E_WEATHER_STATES
        {
            WEATHER_CLOUDY,
            WEATHER_SUNNY,
            WEATHER_DRIZZLE,
            WEATHER_THUNDERSTORMS,
            WEATHER_RAIN
        };
        E_WEATHER_STATES weatherState;
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
            this.transitioning = false;
            this.froggerPressed = false;
            this.pongPressed = false;
            this.oldX = 0;
            weatherState = E_WEATHER_STATES.WEATHER_RAIN;
            effect = new BasicEffect(GraphicsDevice);
            slides = new List<UIElement>();
            effect.VertexColorEnabled = true;

            // Start the animation timer.
            timer = Stopwatch.StartNew();
            timer2 = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };

            MouseMove += new MouseEventHandler(GH_MouseMove);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            ServiceContainer services = new ServiceContainer();
            Content = new ContentManager(Services, "Content");
            bkg = Content.Load<Texture2D>("bkg");
            Texture2D over = Content.Load<Texture2D>("overlay");
            backGrnd = new Background(GraphicsDevice, bkg, over);
            Texture2D t = Content.Load<Texture2D>("test");
            
            //TODO: REPLACE THIS WITH NON SHITTY FONT
            SpriteFont font = Content.Load<SpriteFont>("defaultFont");
            SpriteFont small = Content.Load<SpriteFont>("smallFont");

            uiManager = new UIManager(GraphicsDevice);

            infoWindow = uiManager.addWindow(new Vector2(0, 0), new Vector2(1366, 768));
            //use windowdiff as offset
            weatherWindow = uiManager.addWindow(new Vector2(WINDOWX+WINDOWDIFF, 0), new Vector2(WINDOWX, WINDOWY));
            humidityWindow = uiManager.addWindow(new Vector2((WINDOWX * 2) + WINDOWDIFF*2, 0), new Vector2(WINDOWX, WINDOWY));
            windWindow = uiManager.addWindow(new Vector2((WINDOWX * 3) + WINDOWDIFF * 3, 0), new Vector2(WINDOWX, WINDOWY));
            //SlideInfo tmp = uiManager.createInfoSlide(new Vector2(0, 0), new Vector2(1366, 768));
            slides.Add(windWindow);
            slides.Add(humidityWindow);
            slides.Add(weatherWindow);
            slides.Add(infoWindow);
            //slides.Add(tmp);
            sidebar = uiManager.addWindow(new Vector2(1066, 0), new Vector2(SIDEBARX, WINDOWY));
            froggerButton = uiManager.addButton(new Vector2(10, 300), new Vector2(280, 60), "Frogger", font, sidebar);
            pongButton = uiManager.addButton(new Vector2(10, 400), new Vector2(280, 60), "Pong", font, sidebar);
            
            uiManager.addStaticText(new Vector2(0, 0), new Vector2(200, 200), "UCR Information", font, infoWindow);
            uiManager.addStaticText(new Vector2(20, 60), new Vector2(400, 400), "Information about UCR goes here", small, infoWindow);
            uiManager.addStaticText(new Vector2(0, 0), new Vector2(200,200), "Weather - Tempurature", font, weatherWindow);
            weatherImage = uiManager.addImage(new Vector2(100, 20), Content.Load<Texture2D>("Cloudy"), weatherWindow);
            uiManager.addStaticText(new Vector2(10, 90), new Vector2(200, 200), "666 C", font, weatherWindow);
            uiManager.addStaticText(new Vector2(0, 0), new Vector2(200, 200), "Weather - Humidity", font, humidityWindow);
            uiManager.addStaticText(new Vector2(0, 0), new Vector2(200, 200), "Weather - Wind Speed", font, windWindow);
            uiManager.addStaticText(new Vector2(20, 50), new Vector2(200, 200), "420 MPH", small, windWindow);
            weatherGraph = uiManager.addGraph(new Vector2(20,100), new Vector2(700, 650),small, windWindow);

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
            setWeatherPictures();
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            backGrnd.run((int)slides[0].getPosition().X);
            Random r = new Random();
            
            weatherGraph.update(r.Next()%200);
            //draw cool background first
            //!!MAKE SURE THAT WE ASSIGN THE RECT TO 2560x2048!!
            spriteBatch.Draw(bkg, new Rectangle(0, 0, WINDOWX, WINDOWY), Color.White);
            //spriteBatch.Draw(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            backGrnd.draw(spriteBatch);
            uiManager.drawAll(spriteBatch);
            spriteBatch.End();
            //handle buttons
            if (froggerButton.getMouseOver())
            {
                //wait for a second
                if (!froggerPressed)
                {
                    timer2.Start();
                    froggerPressed = true;
                }
                else
                {
                    if (timer2.ElapsedMilliseconds > 1000)
                    {
                        //execute frogger
                        timer2.Reset();
                        froggerPressed = false;
                    }
                }
            }
            if (pongButton.getMouseOver())
            {
                //wait for a second
                if (!pongPressed)
                {
                    timer2.Start();
                    pongPressed = true;
                }
                else
                {
                    if (timer2.ElapsedMilliseconds > 1000)
                    {
                        timer2.Reset();
                        pongPressed = false;
                    }
                }
            }
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
            else
            {
                //play an effect to indicate that we hit the end of our slides
            }
            
        }
        protected void incrementSlide()
        {
            if (currentSlide < slides.Count)
                currentSlide++;
            else
            {
            }
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
            if (timer.ElapsedMilliseconds > 20 && !sweptLeft && !sweptRight && !transitioning)
            {
                //check for left or right 
                if ((oldX - MainForm.mX) > 500)
                {
                    //swept to the left
                    oldX = MainForm.mX;
                    if (currentSlide > 0)
                    {
                        currentSlide--;
                        sweptLeft = true;
                        transitioning = true;
                    }
                }
                else if ((oldX - MainForm.mX) < -500)
                {
                    //swept to the right
                    oldX = MainForm.mX;
                    if (currentSlide < slides.Count - 1)
                    {
                        currentSlide++;
                        sweptRight = true;
                        transitioning = true;
                    }
                    
                }
                timer.Reset();
                takingInput = false;
            }

            if (sweptLeft && transitioning)
            {

                for (int i = 0; i < slides.Count; i++)
                {
                    sweptRight = false;
                    if (!slides[i].stopTransition())
                        slides[i].transition();
                    else
                    {
                        //end everything here
                        slides[i].resetTransition();
                        sweptLeft = false;
                        transitioning = false;
                        oldX = MainForm.mX;
                        setSlidePositions();
                    }
                }
            }
            else if (sweptRight && transitioning)
            {

                for (int i = 0; i < slides.Count; i++)
                {
                    sweptLeft = false;
                    if (!slides[i].stopTransition())
                        slides[i].transition(false);
                    else
                    {
                        slides[i].resetTransition();
                        sweptRight = false;
                        transitioning = false;
                        oldX = MainForm.mX;
                        setSlidePositions();
                    }
                }
            }
        }

        //here we artificially set the slides to their correct positions in case of transition lag
        protected void setSlidePositions()
        {
            for (int i = 0; i < slides.Count; i++)
            {
                slides[i].setPosition(new Vector2((currentSlide - i) * 1366, 0));
            }
        }

        //handle the changing of icons for weataher here
        protected void setWeatherPictures()
        {
            switch(weatherState)
            {
                case E_WEATHER_STATES.WEATHER_CLOUDY:
                {
                    weatherImage.setImage(Content.Load<Texture2D>("Cloudy"));
                    break;
                }
                case E_WEATHER_STATES.WEATHER_DRIZZLE:
                {
                    weatherImage.setImage(Content.Load<Texture2D>("Slight Drizzle"));
                    break;
                }
                case E_WEATHER_STATES.WEATHER_RAIN:
                {
                    weatherImage.setImage(Content.Load<Texture2D>("Drizzle"));
                    break;
                }
                case E_WEATHER_STATES.WEATHER_SUNNY:
                {
                    weatherImage.setImage(Content.Load<Texture2D>("Sunny"));
                    break;
                }
                case E_WEATHER_STATES.WEATHER_THUNDERSTORMS:
                {
                    weatherImage.setImage(Content.Load<Texture2D>("Thunderstorms"));
                    break;
                }
            }

        }
    }
}
