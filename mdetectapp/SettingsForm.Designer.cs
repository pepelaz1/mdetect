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
            this.label2 = new System.Windows.Forms.Label();
            this.tbLogsPath = new System.Windows.Forms.TextBox();
            this.tbScreenshootsPath = new System.Windows.Forms.TextBox();
            this.btnBrowseLogPath = new System.Windows.Forms.Button();
            this.btnBrowseScreenshotsPath = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logs path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Screenshoots path:";
            // 
            // tbLogsPath
            // 
            this.tbLogsPath.Location = new System.Drawing.Point(119, 26);
            this.tbLogsPath.Name = "tbLogsPath";
            this.tbLogsPath.Size = new System.Drawing.Size(284, 20);
            this.tbLogsPath.TabIndex = 2;
            // 
            // tbScreenshootsPath
            // 
            this.tbScreenshootsPath.Location = new System.Drawing.Point(119, 56);
            this.tbScreenshootsPath.Name = "tbScreenshootsPath";
            this.tbScreenshootsPath.Size = new System.Drawing.Size(284, 20);
            this.tbScreenshootsPath.TabIndex = 3;
            // 
            // btnBrowseLogPath
            // 
            this.btnBrowseLogPath.Location = new System.Drawing.Point(402, 25);
            this.btnBrowseLogPath.Name = "btnBrowseLogPath";
            this.btnBrowseLogPath.Size = new System.Drawing.Size(29, 22);
            this.btnBrowseLogPath.TabIndex = 4;
            this.btnBrowseLogPath.Text = "...";
            this.btnBrowseLogPath.UseVisualStyleBackColor = true;
            this.btnBrowseLogPath.Click += new System.EventHandler(this.btnBrowseLogPath_Click);
            // 
            // btnBrowseScreenshotsPath
            // 
            this.btnBrowseScreenshotsPath.Location = new System.Drawing.Point(402, 55);
            this.btnBrowseScreenshotsPath.Name = "btnBrowseScreenshotsPath";
            this.btnBrowseScreenshotsPath.Size = new System.Drawing.Size(29, 22);
            this.btnBrowseScreenshotsPath.TabIndex = 5;
            this.btnBrowseScreenshotsPath.Text = "...";
            this.btnBrowseScreenshotsPath.UseVisualStyleBackColor = true;
            this.btnBrowseScreenshotsPath.Click += new System.EventHandler(this.btnBrowseScreenshotsPath_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(191, 117);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 156);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnBrowseScreenshotsPath);
            this.Controls.Add(this.btnBrowseLogPath);
            this.Controls.Add(this.tbScreenshootsPath);
            this.Controls.Add(this.tbLogsPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLogsPath;
        private System.Windows.Forms.TextBox tbScreenshootsPath;
        private System.Windows.Forms.Button btnBrowseLogPath;
        private System.Windows.Forms.Button btnBrowseScreenshotsPath;
        private System.Windows.Forms.Button btnOk;
    }
}