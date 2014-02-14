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
        BasicEffect effect;
        Stopwatch timer;
        SpriteBatch spriteBatch;
        Texture2D bkg;
        UIManager uiManager;
        ContentManager Content;
        UIWindow weatherWindow;
        UIButton weatherButton;
        UIButton infoButton;
        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            // Create our effect.
            effect = new BasicEffect(GraphicsDevice);

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
            weatherWindow = uiManager.addWindow(new Vector2(600, 20), new Vector2(600, 400));
            weatherWindow.setVisible(false);

            weatherButton = uiManager.addButton(new Vector2(20, 20), new Vector2(400, 80), "Weather", font);
            infoButton = uiManager.addButton(new Vector2(20, 160), new Vector2(400, 80), "Information", font);
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

            float aspect = GraphicsDevice.Viewport.AspectRatio;
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 10);
            // Set renderstates.
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Draw the triangle.
            spriteBatch.Begin();
            //draw cool background first
            //!!MAKE SURE THAT WE ASSIGN THE RECT TO 2560x2048!!
            spriteBatch.Draw(bkg, new Rectangle(0, 0, 1366, 768), Color.White);
            uiManager.drawAll(spriteBatch);
            spriteBatch.End();

            if (weatherButton.getMouseOver())
            {
                weatherWindow.setVisible(true);
            }
            else
                weatherWindow.setVisible(false);
            
        }
    }
}
