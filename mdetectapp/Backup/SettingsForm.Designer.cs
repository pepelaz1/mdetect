namespace MotionDetector
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDetectedMotion = new System.Windows.Forms.Button();
            this.colorDialogDetectedMotion = new System.Windows.Forms.ColorDialog();
            this.colorDialogHighlight = new System.Windows.Forms.ColorDialog();
            this.colorDialogSelected = new System.Windows.Forms.ColorDialog();
            this.buttonHighlight = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSelected = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxVectorColors = new System.Windows.Forms.GroupBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxVectorColors.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Detected motion";
            // 
            // buttonDetectedMotion
            // 
            this.buttonDetectedMotion.BackColor = System.Drawing.Color.Red;
            this.buttonDetectedMotion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDetectedMotion.Location = new System.Drawing.Point(110, 25);
            this.buttonDetectedMotion.Name = "buttonDetectedMotion";
            this.buttonDetectedMotion.Size = new System.Drawing.Size(44, 23);
            this.buttonDetectedMotion.TabIndex = 1;
            this.buttonDetectedMotion.UseVisualStyleBackColor = false;
            this.buttonDetectedMotion.Click += new System.EventHandler(this.buttonDetectedMotion_Click);
            // 
            // buttonHighlight
            // 
            this.buttonHighlight.BackColor = System.Drawing.Color.Yellow;
            this.buttonHighlight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHighlight.Location = new System.Drawing.Point(110, 54);
            this.buttonHighlight.Name = "buttonHighlight";
            this.buttonHighlight.Size = new System.Drawing.Size(44, 23);
            this.buttonHighlight.TabIndex = 3;
            this.buttonHighlight.UseVisualStyleBackColor = false;
            this.buttonHighlight.Click += new System.EventHandler(this.buttonHighlight_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Highlight motion";
            // 
            // buttonSelected
            // 
            this.buttonSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelected.Location = new System.Drawing.Point(110, 83);
            this.buttonSelected.Name = "buttonSelected";
            this.buttonSelected.Size = new System.Drawing.Size(44, 23);
            this.buttonSelected.TabIndex = 5;
            this.buttonSelected.UseVisualStyleBackColor = false;
            this.buttonSelected.Click += new System.EventHandler(this.buttonSelected_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Selected Vector";
            // 
            // groupBoxVectorColors
            // 
            this.groupBoxVectorColors.Controls.Add(this.label1);
            this.groupBoxVectorColors.Controls.Add(this.buttonSelected);
            this.groupBoxVectorColors.Controls.Add(this.buttonDetectedMotion);
            this.groupBoxVectorColors.Controls.Add(this.label3);
            this.groupBoxVectorColors.Controls.Add(this.label2);
            this.groupBoxVectorColors.Controls.Add(this.buttonHighlight);
            this.groupBoxVectorColors.Location = new System.Drawing.Point(101, 12);
            this.groupBoxVectorColors.Name = "groupBoxVectorColors";
            this.groupBoxVectorColors.Size = new System.Drawing.Size(173, 121);
            this.groupBoxVectorColors.TabIndex = 6;
            this.groupBoxVectorColors.TabStop = false;
            this.groupBoxVectorColors.Text = "Vector Colors";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(168, 206);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 241);
            this.ControlBox = false;
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxVectorColors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.groupBoxVectorColors.ResumeLayout(false);
            this.groupBoxVectorColors.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDetectedMotion;
        private System.Windows.Forms.ColorDialog colorDialogDetectedMotion;
        private System.Windows.Forms.ColorDialog colorDialogHighlight;
        private System.Windows.Forms.ColorDialog colorDialogSelected;
        private System.Windows.Forms.Button buttonHighlight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSelected;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxVectorColors;
        private System.Windows.Forms.Button buttonOK;
    }
}