#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.Windows.Forms;
#endregion

namespace WinFormsGraphicsDevice
{
    // System.Drawing and the XNA Framework both define Color types.
    // To avoid conflicts, we define shortcut names for them both.
    using GdiColor = System.Drawing.Color;
    using XnaColor = Microsoft.Xna.Framework.Color;
    using System.Diagnostics;
    using System;
    using System.Timers;

    
    /// <summary>
    /// Custom form provides the main user interface for the program.
    /// In this sample we used the designer to add a splitter pane to the form,
    /// which contains a SpriteFontControl and a SpinningTriangleControl.
    /// </summary>
    public partial class MainForm : Form
    {
        public static Boolean onDisplay = false;
        public static int mX = 0, mY = 0;
        public int lastX = 0, lastY = 0;
        public int hoverX = 0, hoverY = 0;
        int numNoMoves = 0;
        int hoverTime = 0;
        System.Timers.Timer mouseWatcher;
        // This is in 1/10th seconds (the current Timer interval). 
        // It's easier that way.
        public const int hoverInterval = 10;
        // Radius in whcih the hand has to stay in (in pixels)
        // for it to count as a click.
        public const int hoverRadius = 100;

        public MainForm()
        {
            InitializeComponent();
            /*
            if (true)
            {
                //Opens up the KinectMouse.
                //Process.Start(
                  //  @"C:\Users\Nine\Desktop\Kinect Magic Cursor (1.7)\Kinect Magic Cursor\magiccursor.exe"
                //);
                ProcessStartInfo info = new ProcessStartInfo(
               @"WinFormsGraphicsDevice");
                Process.Start(info);
           
            }
            */
            if (!onDisplay)
            {
                //initialise the mouse
                onDisplay = true;
                ProcessStartInfo info = new ProcessStartInfo(
               @"C:/KinectMouse.exe");
                
                Process.Start(info);
                this.Activate();
                this.Show();
            }

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;

            mouseWatcher = new System.Timers.Timer(100);
            mouseWatcher.Elapsed += new ElapsedEventHandler(mouseWatcher_Elapsed);
            mouseWatcher.Start();
        }

        void killExec(string exec)
        {
            Process[] GetPArry = Process.GetProcesses();
            foreach (Process testProcess in GetPArry)
            {
                string ProcessName = testProcess.ProcessName;
                ProcessName = ProcessName.ToLower();
                if (ProcessName.CompareTo(exec) == 0)
                    testProcess.Kill();
            }
        }

        void clickAt(int clickX, int clickY)
        {
            Console.Write("Clicked at " + clickX + ", " + clickY + ".");
            if (clickX < 100 && clickY < 100)
            {
                if (onDisplay)
                {
                    killExec("magiccursor");
                    System.Diagnostics.Process.Start(
                        @"C:\Windows\system32\cmd.exe"
                    );
                }
            }
        }
        void mouseWatcher_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (hoverTime >= hoverInterval && hoverTime != -1)
            {
                if (numNoMoves < hoverInterval / 2)
                {
                    //This will "click" on the area. More specifically hoverX, hoverY.
                    //Currently just changes the screen color due to having nothing to click.
                    numNoMoves = 0;
                    //Hover time is set to -1 so the program will not repeatedly click on the
                    //same area. hoverTime gets reset to 0 after the user has moved their hand
                    //elsewhere.
                    hoverTime = -1;
                    clickAt(hoverX, hoverY);
                }
            }

            if ((mX - lastX == 0) && (mY - lastY == 0))
            {
                //This will catch the number of times the Kinect Mouse is moving during the
                //interval. We have this here because the Kinect will often see the hand 
                //jittering in place. If The mouse is not moving around it is more than likely
                //because nobody is there to move the mouse around.
                numNoMoves++;
            }
            else if (Math.Sqrt(Math.Pow(mX - hoverX, 2) + Math.Pow(mY - hoverY, 2)) < 100)
            {
                if (hoverTime != -1)
                {
                    hoverTime++;
                }
            }
            else
            {
                hoverX = mX;
                hoverY = mY;
                hoverTime = 0;
                numNoMoves = 0;
            }

            lastX = mX;
            lastY = mY;
        }

    }
}
