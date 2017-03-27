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
            this.startStateListBox = new System.Windows.Forms.ListBox();
            this.quickMenuLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.renderArea)).BeginInit();
            this.SuspendLayout();
            // 
            // renderArea
            // 
            this.renderArea.Location = new System.Drawing.Point(365, 12);
            this.renderArea.Name = "renderArea";
            this.renderArea.Size = new System.Drawing.Size(604, 604);
            this.renderArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.renderArea.TabIndex = 0;
            this.renderArea.TabStop = false;
            // 
            // iterateButton
            // 
            this.iterateButton.Location = new System.Drawing.Point(19, 13);
            this.iterateButton.Name = "iterateButton";
            this.iterateButton.Size = new System.Drawing.Size(137, 23);
            this.iterateButton.TabIndex = 1;
            this.iterateButton.Text = "Iterate one step";
            this.iterateButton.UseVisualStyleBackColor = true;
            this.iterateButton.Click += new System.EventHandler(this.iterate_Button_Click);
            // 
            // textBoxPerfromance
            // 
            this.textBoxPerfromance.Location = new System.Drawing.Point(886, 635);
            this.textBoxPerfromance.Name = "textBoxPerfromance";
            this.textBoxPerfromance.Size = new System.Drawing.Size(83, 20);
            this.textBoxPerfromance.TabIndex = 2;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(162, 13);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(137, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartToggleClicked);
            // 
            // iterationCounterTextBox
            // 
            this.iterationCounterTextBox.Location = new System.Drawing.Point(788, 635);
            this.iterationCounterTextBox.Name = "iterationCounterTextBox";
            this.iterationCounterTextBox.Size = new System.Drawing.Size(92, 20);
            this.iterationCounterTextBox.TabIndex = 4;
            // 
            // startStateListBox
            // 
            this.startStateListBox.FormattingEnabled = true;
            this.startStateListBox.Items.AddRange(new object[] {
            "Fill 6",
            "Fill 7",
            "Mid 7",
            "TopLeftBack 7",
            "BottomRightFront 7"});
            this.startStateListBox.Location = new System.Drawing.Point(19, 69);
            this.startStateListBox.Name = "startStateListBox";
            this.startStateListBox.Size = new System.Drawing.Size(137, 225);
            this.startStateListBox.TabIndex = 5;
            this.startStateListBox.SelectedIndexChanged += new System.EventHandler(this.StartStateListBox_SelectedIndexChanged);
            // 
            // quickMenuLabel
            // 
            this.quickMenuLabel.AutoSize = true;
            this.quickMenuLabel.Location = new System.Drawing.Point(16, 53);
            this.quickMenuLabel.Name = "quickMenuLabel";
            this.quickMenuLabel.Size = new System.Drawing.Size(65, 13);
            this.quickMenuLabel.TabIndex = 7;
            this.quickMenuLabel.Text = "Quick Menu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(785, 619);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Iterations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(883, 619);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Iteration duration";
            // 
            // Sandpiles3DRender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 713);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.quickMenuLabel);
            this.Controls.Add(this.startStateListBox);
            this.Controls.Add(this.iterationCounterTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.textBoxPerfromance);
            this.Controls.Add(this.iterateButton);
            this.Controls.Add(this.renderArea);
            this.Name = "Sandpiles3DRender";
            this.Text = "Sandpiles3D";
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
        private System.Windows.Forms.ListBox startStateListBox;
        private System.Windows.Forms.Label quickMenuLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

