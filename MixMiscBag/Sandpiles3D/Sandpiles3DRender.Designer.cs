namespace Sandpiles3D
{
    partial class Sandpiles3DRender
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
            this.renderArea = new System.Windows.Forms.PictureBox();
            this.iterateButton = new System.Windows.Forms.Button();
            this.textBoxPerfromance = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.iterationCounterTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.renderArea)).BeginInit();
            this.SuspendLayout();
            // 
            // renderArea
            // 
            this.renderArea.Location = new System.Drawing.Point(226, 12);
            this.renderArea.Name = "renderArea";
            this.renderArea.Size = new System.Drawing.Size(743, 604);
            this.renderArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.renderArea.TabIndex = 0;
            this.renderArea.TabStop = false;
            // 
            // iterateButton
            // 
            this.iterateButton.Location = new System.Drawing.Point(70, 12);
            this.iterateButton.Name = "iterateButton";
            this.iterateButton.Size = new System.Drawing.Size(137, 23);
            this.iterateButton.TabIndex = 1;
            this.iterateButton.Text = "Iterate one step";
            this.iterateButton.UseVisualStyleBackColor = true;
            this.iterateButton.Click += new System.EventHandler(this.iterate_Button_Click);
            // 
            // textBoxPerfromance
            // 
            this.textBoxPerfromance.Location = new System.Drawing.Point(869, 622);
            this.textBoxPerfromance.Name = "textBoxPerfromance";
            this.textBoxPerfromance.Size = new System.Drawing.Size(100, 20);
            this.textBoxPerfromance.TabIndex = 2;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(70, 42);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(137, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartToggleClicked);
            // 
            // iterationCounterTextBox
            // 
            this.iterationCounterTextBox.Location = new System.Drawing.Point(741, 622);
            this.iterationCounterTextBox.Name = "iterationCounterTextBox";
            this.iterationCounterTextBox.Size = new System.Drawing.Size(100, 20);
            this.iterationCounterTextBox.TabIndex = 4;
            // 
            // Sandpiles3DRender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 713);
            this.Controls.Add(this.iterationCounterTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.textBoxPerfromance);
            this.Controls.Add(this.iterateButton);
            this.Controls.Add(this.renderArea);
            this.Name = "Sandpiles3DRender";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.renderArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox renderArea;
        private System.Windows.Forms.Button iterateButton;
        private System.Windows.Forms.TextBox textBoxPerfromance;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox iterationCounterTextBox;
    }
}

