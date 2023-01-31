namespace DrawTest2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.window1 = new DrawTest2.Window();
            ((System.ComponentModel.ISupportInitialize)(this.window1)).BeginInit();
            this.SuspendLayout();
            // 
            // window1
            // 
            this.window1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.window1.Location = new System.Drawing.Point(79, 57);
            this.window1.Name = "window1";
            this.window1.Size = new System.Drawing.Size(551, 292);
            this.window1.TabIndex = 0;
            this.window1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.window1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.window1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Window window1;
    }
}