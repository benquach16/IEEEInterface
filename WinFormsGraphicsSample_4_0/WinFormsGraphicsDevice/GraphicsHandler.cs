#region File Description
//-----------------------------------------------------------------------------
// SpinningTriangleControl.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        UIWindow t;
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
            t = new UIWindow(GraphicsDevice, new Vector2(40, 40), new Vector2(500, 500), null);
 
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            float aspect = GraphicsDevice.Viewport.AspectRatio;
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 10);
            // Set renderstates.
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Draw the triangle.
            spriteBatch.Begin();
            //draw cool background first
            t.draw(spriteBatch);
            spriteBatch.End();
            
        }
    }
}