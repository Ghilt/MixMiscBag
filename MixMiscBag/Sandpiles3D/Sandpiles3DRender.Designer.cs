﻿namespace Sandpiles3D
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sandpiles3DRender));
            this.renderArea = new System.Windows.Forms.PictureBox();
            this.iterateButton = new System.Windows.Forms.Button();
            this.textBoxPerfromance = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.iterationCounterTextBox = new System.Windows.Forms.TextBox();
            this.startStateListBox = new System.Windows.Forms.ListBox();
            this.quickMenuLabel = new System.Windows.Forms.Label();
            this.iterationsCountLabel = new System.Windows.Forms.Label();
            this.iterationDurationLabel = new System.Windows.Forms.Label();
            this.advancedSetupLabel = new System.Windows.Forms.Label();
            this.setXTrackBar = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.xSizeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.setYDimensionCheckbox = new System.Windows.Forms.CheckBox();
            this.setZDimensionCheckbox = new System.Windows.Forms.CheckBox();
            this.setXDimensionCheckbox = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.setValueButton = new System.Windows.Forms.Button();
            this.setValueGroup = new System.Windows.Forms.GroupBox();
            this.sizeGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.renderArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setXTrackBar)).BeginInit();
            this.setValueGroup.SuspendLayout();
            this.sizeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // renderArea
            // 
            this.renderArea.Location = new System.Drawing.Point(419, 13);
            this.renderArea.Name = "renderArea";
            this.renderArea.Size = new System.Drawing.Size(604, 604);
            this.renderArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.renderArea.TabIndex = 0;
            this.renderArea.TabStop = false;
            // 
            // iterateButton
            // 
            this.iterateButton.Location = new System.Drawing.Point(22, 509);
            this.iterateButton.Name = "iterateButton";
            this.iterateButton.Size = new System.Drawing.Size(137, 23);
            this.iterateButton.TabIndex = 1;
            this.iterateButton.Text = "Iterate one step";
            this.iterateButton.UseVisualStyleBackColor = true;
            this.iterateButton.Click += new System.EventHandler(this.iterate_Button_Click);
            // 
            // textBoxPerfromance
            // 
            this.textBoxPerfromance.Location = new System.Drawing.Point(940, 636);
            this.textBoxPerfromance.Name = "textBoxPerfromance";
            this.textBoxPerfromance.Size = new System.Drawing.Size(83, 20);
            this.textBoxPerfromance.TabIndex = 2;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(165, 509);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(137, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartToggleClicked);
            // 
            // iterationCounterTextBox
            // 
            this.iterationCounterTextBox.Location = new System.Drawing.Point(842, 636);
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
            this.startStateListBox.Location = new System.Drawing.Point(19, 159);
            this.startStateListBox.Name = "startStateListBox";
            this.startStateListBox.Size = new System.Drawing.Size(137, 134);
            this.startStateListBox.TabIndex = 5;
            this.startStateListBox.SelectedIndexChanged += new System.EventHandler(this.StartStateListBox_SelectedIndexChanged);
            // 
            // quickMenuLabel
            // 
            this.quickMenuLabel.AutoSize = true;
            this.quickMenuLabel.Location = new System.Drawing.Point(12, 143);
            this.quickMenuLabel.Name = "quickMenuLabel";
            this.quickMenuLabel.Size = new System.Drawing.Size(65, 13);
            this.quickMenuLabel.TabIndex = 7;
            this.quickMenuLabel.Text = "Quick Menu";
            // 
            // iterationsCountLabel
            // 
            this.iterationsCountLabel.AutoSize = true;
            this.iterationsCountLabel.Location = new System.Drawing.Point(839, 620);
            this.iterationsCountLabel.Name = "iterationsCountLabel";
            this.iterationsCountLabel.Size = new System.Drawing.Size(50, 13);
            this.iterationsCountLabel.TabIndex = 8;
            this.iterationsCountLabel.Text = "Iterations";
            // 
            // iterationDurationLabel
            // 
            this.iterationDurationLabel.AutoSize = true;
            this.iterationDurationLabel.Location = new System.Drawing.Point(937, 620);
            this.iterationDurationLabel.Name = "iterationDurationLabel";
            this.iterationDurationLabel.Size = new System.Drawing.Size(86, 13);
            this.iterationDurationLabel.TabIndex = 9;
            this.iterationDurationLabel.Text = "Iteration duration";
            // 
            // advancedSetupLabel
            // 
            this.advancedSetupLabel.AutoSize = true;
            this.advancedSetupLabel.Location = new System.Drawing.Point(12, 314);
            this.advancedSetupLabel.Name = "advancedSetupLabel";
            this.advancedSetupLabel.Size = new System.Drawing.Size(112, 13);
            this.advancedSetupLabel.TabIndex = 10;
            this.advancedSetupLabel.Text = "Advanced setup(todo)";
            // 
            // setXTrackBar
            // 
            this.setXTrackBar.Location = new System.Drawing.Point(45, 29);
            this.setXTrackBar.Name = "setXTrackBar";
            this.setXTrackBar.Size = new System.Drawing.Size(235, 45);
            this.setXTrackBar.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(286, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(67, 20);
            this.textBox1.TabIndex = 12;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(29, 16);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(67, 20);
            this.textBox4.TabIndex = 21;
            // 
            // xSizeLabel
            // 
            this.xSizeLabel.AutoSize = true;
            this.xSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.xSizeLabel.Location = new System.Drawing.Point(6, 16);
            this.xSizeLabel.Name = "xSizeLabel";
            this.xSizeLabel.Size = new System.Drawing.Size(17, 17);
            this.xSizeLabel.TabIndex = 22;
            this.xSizeLabel.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 24;
            this.label2.Text = "Y";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(29, 42);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(67, 20);
            this.textBox5.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "Z";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(29, 68);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(67, 20);
            this.textBox6.TabIndex = 25;
            // 
            // setYDimensionCheckbox
            // 
            this.setYDimensionCheckbox.AutoSize = true;
            this.setYDimensionCheckbox.Location = new System.Drawing.Point(6, 42);
            this.setYDimensionCheckbox.Name = "setYDimensionCheckbox";
            this.setYDimensionCheckbox.Size = new System.Drawing.Size(33, 17);
            this.setYDimensionCheckbox.TabIndex = 27;
            this.setYDimensionCheckbox.Text = "Y";
            this.setYDimensionCheckbox.UseVisualStyleBackColor = true;
            // 
            // setZDimensionCheckbox
            // 
            this.setZDimensionCheckbox.AutoSize = true;
            this.setZDimensionCheckbox.Location = new System.Drawing.Point(6, 65);
            this.setZDimensionCheckbox.Name = "setZDimensionCheckbox";
            this.setZDimensionCheckbox.Size = new System.Drawing.Size(33, 17);
            this.setZDimensionCheckbox.TabIndex = 28;
            this.setZDimensionCheckbox.Text = "Z";
            this.setZDimensionCheckbox.UseVisualStyleBackColor = true;
            // 
            // setXDimensionCheckbox
            // 
            this.setXDimensionCheckbox.AutoSize = true;
            this.setXDimensionCheckbox.Location = new System.Drawing.Point(6, 19);
            this.setXDimensionCheckbox.Name = "setXDimensionCheckbox";
            this.setXDimensionCheckbox.Size = new System.Drawing.Size(33, 17);
            this.setXDimensionCheckbox.TabIndex = 29;
            this.setXDimensionCheckbox.Text = "X";
            this.setXDimensionCheckbox.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(143, 80);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(67, 20);
            this.textBox2.TabIndex = 30;
            // 
            // setValueButton
            // 
            this.setValueButton.Location = new System.Drawing.Point(143, 106);
            this.setValueButton.Name = "setValueButton";
            this.setValueButton.Size = new System.Drawing.Size(67, 20);
            this.setValueButton.TabIndex = 31;
            this.setValueButton.Text = "Set";
            this.setValueButton.UseVisualStyleBackColor = true;
            // 
            // setValueGroup
            // 
            this.setValueGroup.Controls.Add(this.setXDimensionCheckbox);
            this.setValueGroup.Controls.Add(this.setValueButton);
            this.setValueGroup.Controls.Add(this.setXTrackBar);
            this.setValueGroup.Controls.Add(this.textBox2);
            this.setValueGroup.Controls.Add(this.textBox1);
            this.setValueGroup.Controls.Add(this.setYDimensionCheckbox);
            this.setValueGroup.Controls.Add(this.setZDimensionCheckbox);
            this.setValueGroup.Location = new System.Drawing.Point(12, 343);
            this.setValueGroup.Name = "setValueGroup";
            this.setValueGroup.Size = new System.Drawing.Size(366, 134);
            this.setValueGroup.TabIndex = 32;
            this.setValueGroup.TabStop = false;
            this.setValueGroup.Text = "Set value";
            // 
            // sizeGroupBox
            // 
            this.sizeGroupBox.Controls.Add(this.xSizeLabel);
            this.sizeGroupBox.Controls.Add(this.textBox4);
            this.sizeGroupBox.Controls.Add(this.label3);
            this.sizeGroupBox.Controls.Add(this.textBox5);
            this.sizeGroupBox.Controls.Add(this.textBox6);
            this.sizeGroupBox.Controls.Add(this.label2);
            this.sizeGroupBox.Location = new System.Drawing.Point(12, 30);
            this.sizeGroupBox.Name = "sizeGroupBox";
            this.sizeGroupBox.Size = new System.Drawing.Size(200, 100);
            this.sizeGroupBox.TabIndex = 33;
            this.sizeGroupBox.TabStop = false;
            this.sizeGroupBox.Text = "Size";
            // 
            // Sandpiles3DRender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 713);
            this.Controls.Add(this.sizeGroupBox);
            this.Controls.Add(this.setValueGroup);
            this.Controls.Add(this.advancedSetupLabel);
            this.Controls.Add(this.iterationDurationLabel);
            this.Controls.Add(this.iterationsCountLabel);
            this.Controls.Add(this.quickMenuLabel);
            this.Controls.Add(this.startStateListBox);
            this.Controls.Add(this.iterationCounterTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.textBoxPerfromance);
            this.Controls.Add(this.iterateButton);
            this.Controls.Add(this.renderArea);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sandpiles3DRender";
            this.Text = "Sandpiles3D";
            ((System.ComponentModel.ISupportInitialize)(this.renderArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setXTrackBar)).EndInit();
            this.setValueGroup.ResumeLayout(false);
            this.setValueGroup.PerformLayout();
            this.sizeGroupBox.ResumeLayout(false);
            this.sizeGroupBox.PerformLayout();
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
        private System.Windows.Forms.Label iterationsCountLabel;
        private System.Windows.Forms.Label iterationDurationLabel;
        private System.Windows.Forms.Label advancedSetupLabel;
        private System.Windows.Forms.TrackBar setXTrackBar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label xSizeLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.CheckBox setYDimensionCheckbox;
        private System.Windows.Forms.CheckBox setZDimensionCheckbox;
        private System.Windows.Forms.CheckBox setXDimensionCheckbox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button setValueButton;
        private System.Windows.Forms.GroupBox setValueGroup;
        private System.Windows.Forms.GroupBox sizeGroupBox;
    }
}

