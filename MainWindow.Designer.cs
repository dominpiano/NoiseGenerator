namespace NoiseGenerator
{
    partial class MainWindow
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
            components = new System.ComponentModel.Container();
            stopButton = new Button();
            playButton = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // stopButton
            // 
            stopButton.Font = new Font("Simplex_IV25", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            stopButton.Location = new Point(518, 78);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(117, 87);
            stopButton.TabIndex = 3;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // playButton
            // 
            playButton.Font = new Font("Simplex_IV25", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            playButton.Location = new Point(188, 78);
            playButton.Name = "playButton";
            playButton.Size = new Size(117, 87);
            playButton.TabIndex = 2;
            playButton.Text = "Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += playButton_Click;
            // 
            // timer1
            // 
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 41, 35);
            ClientSize = new Size(800, 450);
            Controls.Add(stopButton);
            Controls.Add(playButton);
            Name = "MainWindow";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button stopButton;
        private Button playButton;
        private System.Windows.Forms.Timer timer1;
    }
}