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
            //Content.Load<Texture2D>("bkg.jpg");
            //TODO: REPLACE THIS WITH NON SHITTY FONT
            SpriteFont font = Content.Load<SpriteFont>("defaultFont");

            uiManager = new UIManager(GraphicsDevice);
            UIWindow window = uiManager.addWindow(new Vector2(400, 20), new Vector2(600, 400));
            String w = "Weather";
            uiManager.addButton(new Vector2(20, 20), new Vector2(200, 40), w, font);
            uiManager.addStaticText(new Vector2(20, 20), new Vector2(200, 200), w, font, window);
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
            uiManager.drawAll(spriteBatch);
            spriteBatch.End();
            
        }
    }
}
