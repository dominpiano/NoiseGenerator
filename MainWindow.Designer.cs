namespace NoiseGenerator {
    partial class MainWindow {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            stopButton = new Button();
            playButton = new Button();
            formsPlot1 = new ScottPlot.FormsPlot();
            hScrollBar1 = new HScrollBar();
            openFileDialog1 = new OpenFileDialog();
            SuspendLayout();
            // 
            // stopButton
            // 
            stopButton.Font = new Font("Simplex_IV25", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            stopButton.Location = new Point(230, 50);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(120, 80);
            stopButton.TabIndex = 3;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += stopButton_Click;
            // 
            // playButton
            // 
            playButton.Font = new Font("Simplex_IV25", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            playButton.Location = new Point(50, 50);
            playButton.Name = "playButton";
            playButton.Size = new Size(120, 80);
            playButton.TabIndex = 2;
            playButton.Text = "Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += playButton_Click;
            // 
            // formsPlot1
            // 
            formsPlot1.BackColor = Color.White;
            formsPlot1.Font = new Font("Simplex_IV25", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            formsPlot1.ForeColor = Color.White;
            formsPlot1.Location = new Point(0, 266);
            formsPlot1.Margin = new Padding(5, 4, 5, 4);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1265, 415);
            formsPlot1.TabIndex = 4;
            // 
            // hScrollBar1
            // 
            hScrollBar1.Location = new Point(50, 196);
            hScrollBar1.Maximum = 1000;
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(506, 30);
            hScrollBar1.SmallChange = 2;
            hScrollBar1.TabIndex = 5;
            hScrollBar1.ValueChanged += hScrollBar1_ValueChanged;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(  40,   41,   35);
            ClientSize = new Size(1264, 681);
            Controls.Add(hScrollBar1);
            Controls.Add(formsPlot1);
            Controls.Add(stopButton);
            Controls.Add(playButton);
            Name = "MainWindow";
            Text = "Noise Generator";
            FormClosed += MainWindow_FormClosed;
            ResumeLayout(false);
        }

        #endregion

        private Button stopButton;
        private Button playButton;
        private ScottPlot.FormsPlot formsPlot1;
        private HScrollBar hScrollBar1;
        private OpenFileDialog openFileDialog1;
    }
}