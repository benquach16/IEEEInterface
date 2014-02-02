namespace WinFormsGraphicsDevice
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spinningTriangleControl = new WinFormsGraphicsDevice.GraphicsHandler();
            this.SuspendLayout();
            // 
            // spinningTriangleControl
            // 
            this.spinningTriangleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinningTriangleControl.Location = new System.Drawing.Point(0, 0);
            this.spinningTriangleControl.Name = "spinningTriangleControl";
            this.spinningTriangleControl.Size = new System.Drawing.Size(792, 573);
            this.spinningTriangleControl.TabIndex = 0;
            this.spinningTriangleControl.Text = "spinningTriangleControl";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.spinningTriangleControl);
            this.Name = "MainForm";
            this.Text = "WinForms Graphics Device";
            this.ResumeLayout(false);

        }

        #endregion

        private GraphicsHandler spinningTriangleControl;
    }
}

