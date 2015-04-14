namespace MotionDetector
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFileDialogMov = new System.Windows.Forms.OpenFileDialog();
            this.progressBarProcess = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownThreshold = new System.Windows.Forms.NumericUpDown();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importMotionDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMotionDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.undoDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.timerProcess = new System.Windows.Forms.Timer(this.components);
            this.numericUpDownContrast = new System.Windows.Forms.NumericUpDown();
            this.trackBarContrast = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownBrightness = new System.Windows.Forms.NumericUpDown();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.imageListControls = new System.Windows.Forms.ImageList(this.components);
            this.toolBarControls = new System.Windows.Forms.ToolBar();
            this.toolBarButtonPlay = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPause = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonStop = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSeparador1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonDec10Sec = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonInc10Sec = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonStepBackward = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonStepForward = new System.Windows.Forms.ToolBarButton();
            this.trackBarProgress = new System.Windows.Forms.TrackBar();
            this.labelTime = new System.Windows.Forms.Label();
            this.timerPlay = new System.Windows.Forms.Timer(this.components);
            this.buttonStopProcess = new System.Windows.Forms.Button();
            this.buttonUpdateThreshold = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownFrames = new System.Windows.Forms.NumericUpDown();
            this.checkBoxShowMotion = new System.Windows.Forms.CheckBox();
            this.buttonResetExclusionZones = new System.Windows.Forms.Button();
            this.buttonAddExclusionZones = new System.Windows.Forms.Button();
            this.checkBoxFilterNoise = new System.Windows.Forms.CheckBox();
            this.listViewMotion = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStripVector = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownThresholdHigh = new System.Windows.Forms.NumericUpDown();
            this.saveFileDialogLog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogMotion = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogMotion = new System.Windows.Forms.SaveFileDialog();
            this.buttonPrevPage = new System.Windows.Forms.Button();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelCurrentPage = new System.Windows.Forms.Label();
            this.labelTotalPages = new System.Windows.Forms.Label();
            this.buttonPrevPage10 = new System.Windows.Forms.Button();
            this.buttonNextPage10 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownFrameDivider = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSkipFrames = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrames)).BeginInit();
            this.contextMenuStripVector.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdHigh)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrameDivider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkipFrames)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialogMov
            // 
            this.openFileDialogMov.Filter = "Quicktime Files (*.mov)|*.mov|All Files|*.*";
            // 
            // progressBarProcess
            // 
            this.progressBarProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarProcess.Location = new System.Drawing.Point(17, 559);
            this.progressBarProcess.Name = "progressBarProcess";
            this.progressBarProcess.Size = new System.Drawing.Size(638, 21);
            this.progressBarProcess.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sensitivity";
            // 
            // numericUpDownThreshold
            // 
            this.numericUpDownThreshold.DecimalPlaces = 1;
            this.numericUpDownThreshold.Location = new System.Drawing.Point(66, 45);
            this.numericUpDownThreshold.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownThreshold.Name = "numericUpDownThreshold";
            this.numericUpDownThreshold.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownThreshold.TabIndex = 8;
            this.numericUpDownThreshold.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownThreshold.ValueChanged += new System.EventHandler(this.numericUpDownThreshold_ValueChanged);
            // 
            // buttonProcess
            // 
            this.buttonProcess.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonProcess.Enabled = false;
            this.buttonProcess.Location = new System.Drawing.Point(112, 519);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(103, 31);
            this.buttonProcess.TabIndex = 9;
            this.buttonProcess.Text = "Process";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.motionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(672, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.batchProcessToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // batchProcessToolStripMenuItem
            // 
            this.batchProcessToolStripMenuItem.Name = "batchProcessToolStripMenuItem";
            this.batchProcessToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.batchProcessToolStripMenuItem.Text = "Batch Process";
            this.batchProcessToolStripMenuItem.Click += new System.EventHandler(this.batchProcessToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // motionToolStripMenuItem
            // 
            this.motionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.importMotionDataToolStripMenuItem,
            this.exportMotionDataToolStripMenuItem,
            this.toolStripSeparator3,
            this.undoDeleteToolStripMenuItem,
            this.toolStripSeparator4,
            this.settingsToolStripMenuItem});
            this.motionToolStripMenuItem.Name = "motionToolStripMenuItem";
            this.motionToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.motionToolStripMenuItem.Text = "Motion";
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Enabled = false;
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.saveFileToolStripMenuItem.Text = "Save Log File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // importMotionDataToolStripMenuItem
            // 
            this.importMotionDataToolStripMenuItem.Name = "importMotionDataToolStripMenuItem";
            this.importMotionDataToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.importMotionDataToolStripMenuItem.Text = "Import motion data";
            this.importMotionDataToolStripMenuItem.Click += new System.EventHandler(this.importMotionDataToolStripMenuItem_Click);
            // 
            // exportMotionDataToolStripMenuItem
            // 
            this.exportMotionDataToolStripMenuItem.Enabled = false;
            this.exportMotionDataToolStripMenuItem.Name = "exportMotionDataToolStripMenuItem";
            this.exportMotionDataToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.exportMotionDataToolStripMenuItem.Text = "Export motion data";
            this.exportMotionDataToolStripMenuItem.Click += new System.EventHandler(this.exportMotionDataToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(179, 6);
            // 
            // undoDeleteToolStripMenuItem
            // 
            this.undoDeleteToolStripMenuItem.Enabled = false;
            this.undoDeleteToolStripMenuItem.Name = "undoDeleteToolStripMenuItem";
            this.undoDeleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoDeleteToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.undoDeleteToolStripMenuItem.Text = "Undo Delete";
            this.undoDeleteToolStripMenuItem.Click += new System.EventHandler(this.undoDeleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(179, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // panelPreview
            // 
            this.panelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreview.BackColor = System.Drawing.Color.Black;
            this.panelPreview.Location = new System.Drawing.Point(12, 36);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(433, 300);
            this.panelPreview.TabIndex = 11;
            this.panelPreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelPreview_MouseMove);
            this.panelPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelPreview_MouseDown);
            this.panelPreview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelPreview_MouseUp);
            // 
            // timerProcess
            // 
            this.timerProcess.Tick += new System.EventHandler(this.timerProcess_Tick);
            // 
            // numericUpDownContrast
            // 
            this.numericUpDownContrast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownContrast.Location = new System.Drawing.Point(395, 421);
            this.numericUpDownContrast.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownContrast.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownContrast.Name = "numericUpDownContrast";
            this.numericUpDownContrast.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownContrast.TabIndex = 17;
            this.numericUpDownContrast.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownContrast.ValueChanged += new System.EventHandler(this.numericUpDownContrast_ValueChanged);
            // 
            // trackBarContrast
            // 
            this.trackBarContrast.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarContrast.AutoSize = false;
            this.trackBarContrast.Location = new System.Drawing.Point(76, 421);
            this.trackBarContrast.Maximum = 200;
            this.trackBarContrast.Minimum = 1;
            this.trackBarContrast.Name = "trackBarContrast";
            this.trackBarContrast.Size = new System.Drawing.Size(313, 20);
            this.trackBarContrast.TabIndex = 16;
            this.trackBarContrast.TickFrequency = 0;
            this.trackBarContrast.Value = 100;
            this.trackBarContrast.Scroll += new System.EventHandler(this.trackBarContrast_Scroll);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 423);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Contrast";
            // 
            // numericUpDownBrightness
            // 
            this.numericUpDownBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownBrightness.Location = new System.Drawing.Point(395, 447);
            this.numericUpDownBrightness.Minimum = new decimal(new int[] {
            35,
            0,
            0,
            -2147483648});
            this.numericUpDownBrightness.Name = "numericUpDownBrightness";
            this.numericUpDownBrightness.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownBrightness.TabIndex = 20;
            this.numericUpDownBrightness.ValueChanged += new System.EventHandler(this.numericUpDownBrightness_ValueChanged);
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarBrightness.AutoSize = false;
            this.trackBarBrightness.Location = new System.Drawing.Point(76, 447);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Minimum = -35;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(313, 20);
            this.trackBarBrightness.TabIndex = 19;
            this.trackBarBrightness.TickFrequency = 0;
            this.trackBarBrightness.Value = 10;
            this.trackBarBrightness.Scroll += new System.EventHandler(this.trackBarBrightness_Scroll);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 447);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Brightness";
            // 
            // imageListControls
            // 
            this.imageListControls.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListControls.ImageStream")));
            this.imageListControls.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageListControls.Images.SetKeyName(0, "");
            this.imageListControls.Images.SetKeyName(1, "");
            this.imageListControls.Images.SetKeyName(2, "");
            this.imageListControls.Images.SetKeyName(3, "");
            this.imageListControls.Images.SetKeyName(4, "");
            this.imageListControls.Images.SetKeyName(5, "");
            this.imageListControls.Images.SetKeyName(6, "");
            // 
            // toolBarControls
            // 
            this.toolBarControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toolBarControls.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBarControls.AutoSize = false;
            this.toolBarControls.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonPlay,
            this.toolBarButtonPause,
            this.toolBarButtonStop,
            this.toolBarButtonSeparador1,
            this.toolBarButtonDec10Sec,
            this.toolBarButtonInc10Sec,
            this.toolBarButtonStepBackward,
            this.toolBarButtonStepForward});
            this.toolBarControls.ButtonSize = new System.Drawing.Size(16, 16);
            this.toolBarControls.Divider = false;
            this.toolBarControls.Dock = System.Windows.Forms.DockStyle.None;
            this.toolBarControls.DropDownArrows = true;
            this.toolBarControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolBarControls.ImageList = this.imageListControls;
            this.toolBarControls.Location = new System.Drawing.Point(17, 386);
            this.toolBarControls.Name = "toolBarControls";
            this.toolBarControls.ShowToolTips = true;
            this.toolBarControls.Size = new System.Drawing.Size(130, 26);
            this.toolBarControls.TabIndex = 22;
            this.toolBarControls.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarControls_ButtonClick);
            // 
            // toolBarButtonPlay
            // 
            this.toolBarButtonPlay.ImageIndex = 0;
            this.toolBarButtonPlay.Name = "toolBarButtonPlay";
            this.toolBarButtonPlay.ToolTipText = "Play";
            // 
            // toolBarButtonPause
            // 
            this.toolBarButtonPause.ImageIndex = 1;
            this.toolBarButtonPause.Name = "toolBarButtonPause";
            this.toolBarButtonPause.ToolTipText = "Pause";
            // 
            // toolBarButtonStop
            // 
            this.toolBarButtonStop.ImageIndex = 2;
            this.toolBarButtonStop.Name = "toolBarButtonStop";
            this.toolBarButtonStop.ToolTipText = "Stop";
            // 
            // toolBarButtonSeparador1
            // 
            this.toolBarButtonSeparador1.Name = "toolBarButtonSeparador1";
            this.toolBarButtonSeparador1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonDec10Sec
            // 
            this.toolBarButtonDec10Sec.ImageIndex = 4;
            this.toolBarButtonDec10Sec.Name = "toolBarButtonDec10Sec";
            this.toolBarButtonDec10Sec.ToolTipText = "Backward 5 sec";
            this.toolBarButtonDec10Sec.Visible = false;
            // 
            // toolBarButtonInc10Sec
            // 
            this.toolBarButtonInc10Sec.ImageIndex = 5;
            this.toolBarButtonInc10Sec.Name = "toolBarButtonInc10Sec";
            this.toolBarButtonInc10Sec.ToolTipText = "Forward 5 sec";
            this.toolBarButtonInc10Sec.Visible = false;
            // 
            // toolBarButtonStepBackward
            // 
            this.toolBarButtonStepBackward.ImageIndex = 3;
            this.toolBarButtonStepBackward.Name = "toolBarButtonStepBackward";
            this.toolBarButtonStepBackward.ToolTipText = "Step Backward";
            // 
            // toolBarButtonStepForward
            // 
            this.toolBarButtonStepForward.ImageIndex = 6;
            this.toolBarButtonStepForward.Name = "toolBarButtonStepForward";
            this.toolBarButtonStepForward.ToolTipText = "Step Forward";
            // 
            // trackBarProgress
            // 
            this.trackBarProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarProgress.AutoSize = false;
            this.trackBarProgress.Enabled = false;
            this.trackBarProgress.LargeChange = 0;
            this.trackBarProgress.Location = new System.Drawing.Point(12, 341);
            this.trackBarProgress.Name = "trackBarProgress";
            this.trackBarProgress.Size = new System.Drawing.Size(433, 43);
            this.trackBarProgress.SmallChange = 0;
            this.trackBarProgress.TabIndex = 23;
            this.trackBarProgress.TickFrequency = 0;
            this.trackBarProgress.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarProgress.Scroll += new System.EventHandler(this.trackBarProgress_Scroll);
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.Location = new System.Drawing.Point(325, 373);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(113, 26);
            this.labelTime.TabIndex = 24;
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // timerPlay
            // 
            this.timerPlay.Tick += new System.EventHandler(this.timerPlay_Tick);
            // 
            // buttonStopProcess
            // 
            this.buttonStopProcess.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonStopProcess.Enabled = false;
            this.buttonStopProcess.Location = new System.Drawing.Point(231, 519);
            this.buttonStopProcess.Name = "buttonStopProcess";
            this.buttonStopProcess.Size = new System.Drawing.Size(103, 31);
            this.buttonStopProcess.TabIndex = 25;
            this.buttonStopProcess.Text = "Stop";
            this.buttonStopProcess.UseVisualStyleBackColor = true;
            this.buttonStopProcess.Click += new System.EventHandler(this.buttonStopProcess_Click);
            // 
            // buttonUpdateThreshold
            // 
            this.buttonUpdateThreshold.Enabled = false;
            this.buttonUpdateThreshold.Location = new System.Drawing.Point(120, 71);
            this.buttonUpdateThreshold.Name = "buttonUpdateThreshold";
            this.buttonUpdateThreshold.Size = new System.Drawing.Size(60, 25);
            this.buttonUpdateThreshold.TabIndex = 27;
            this.buttonUpdateThreshold.Text = "Apply";
            this.buttonUpdateThreshold.UseVisualStyleBackColor = true;
            this.buttonUpdateThreshold.Click += new System.EventHandler(this.buttonUpdateThreshold_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Interval (frames)";
            // 
            // numericUpDownFrames
            // 
            this.numericUpDownFrames.Location = new System.Drawing.Point(120, 19);
            this.numericUpDownFrames.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownFrames.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFrames.Name = "numericUpDownFrames";
            this.numericUpDownFrames.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownFrames.TabIndex = 29;
            this.numericUpDownFrames.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownFrames.ValueChanged += new System.EventHandler(this.numericUpDownFrames_ValueChanged);
            // 
            // checkBoxShowMotion
            // 
            this.checkBoxShowMotion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowMotion.AutoSize = true;
            this.checkBoxShowMotion.Location = new System.Drawing.Point(231, 387);
            this.checkBoxShowMotion.Name = "checkBoxShowMotion";
            this.checkBoxShowMotion.Size = new System.Drawing.Size(88, 17);
            this.checkBoxShowMotion.TabIndex = 30;
            this.checkBoxShowMotion.Text = "Show Motion";
            this.checkBoxShowMotion.UseVisualStyleBackColor = true;
            this.checkBoxShowMotion.CheckedChanged += new System.EventHandler(this.checkBoxShowMotion_CheckedChanged);
            // 
            // buttonResetExclusionZones
            // 
            this.buttonResetExclusionZones.Enabled = false;
            this.buttonResetExclusionZones.Location = new System.Drawing.Point(108, 24);
            this.buttonResetExclusionZones.Name = "buttonResetExclusionZones";
            this.buttonResetExclusionZones.Size = new System.Drawing.Size(60, 25);
            this.buttonResetExclusionZones.TabIndex = 33;
            this.buttonResetExclusionZones.Text = "Reset";
            this.buttonResetExclusionZones.UseVisualStyleBackColor = true;
            this.buttonResetExclusionZones.Click += new System.EventHandler(this.buttonResetExclusionZones_Click);
            // 
            // buttonAddExclusionZones
            // 
            this.buttonAddExclusionZones.Enabled = false;
            this.buttonAddExclusionZones.Location = new System.Drawing.Point(31, 24);
            this.buttonAddExclusionZones.Name = "buttonAddExclusionZones";
            this.buttonAddExclusionZones.Size = new System.Drawing.Size(60, 25);
            this.buttonAddExclusionZones.TabIndex = 34;
            this.buttonAddExclusionZones.Text = "Add";
            this.buttonAddExclusionZones.UseVisualStyleBackColor = true;
            this.buttonAddExclusionZones.Click += new System.EventHandler(this.buttonAddExclusionZones_Click);
            // 
            // checkBoxFilterNoise
            // 
            this.checkBoxFilterNoise.AutoSize = true;
            this.checkBoxFilterNoise.Checked = true;
            this.checkBoxFilterNoise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFilterNoise.Location = new System.Drawing.Point(13, 82);
            this.checkBoxFilterNoise.Name = "checkBoxFilterNoise";
            this.checkBoxFilterNoise.Size = new System.Drawing.Size(78, 17);
            this.checkBoxFilterNoise.TabIndex = 35;
            this.checkBoxFilterNoise.Text = "Filter Noise";
            this.checkBoxFilterNoise.UseVisualStyleBackColor = true;
            this.checkBoxFilterNoise.CheckedChanged += new System.EventHandler(this.checkBoxFilterNoise_CheckedChanged);
            // 
            // listViewMotion
            // 
            this.listViewMotion.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewMotion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMotion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.listViewMotion.ContextMenuStrip = this.contextMenuStripVector;
            this.listViewMotion.FullRowSelect = true;
            this.listViewMotion.HideSelection = false;
            this.listViewMotion.Location = new System.Drawing.Point(457, 36);
            this.listViewMotion.Name = "listViewMotion";
            this.listViewMotion.Size = new System.Drawing.Size(198, 270);
            this.listViewMotion.TabIndex = 37;
            this.listViewMotion.UseCompatibleStateImageBehavior = false;
            this.listViewMotion.View = System.Windows.Forms.View.Details;
            this.listViewMotion.SelectedIndexChanged += new System.EventHandler(this.listViewMotion_SelectedIndexChanged);
            this.listViewMotion.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewMotion_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            this.columnHeader3.Width = 80;
            // 
            // contextMenuStripVector
            // 
            this.contextMenuStripVector.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStripVector.Name = "contextMenuStripVector";
            this.contextMenuStripVector.Size = new System.Drawing.Size(117, 26);
            this.contextMenuStripVector.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripVector_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonAddExclusionZones);
            this.groupBox1.Controls.Add(this.buttonResetExclusionZones);
            this.groupBox1.Location = new System.Drawing.Point(457, 379);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 62);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exclusion Zones";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numericUpDownThresholdHigh);
            this.groupBox2.Controls.Add(this.numericUpDownFrames);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericUpDownThreshold);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonUpdateThreshold);
            this.groupBox2.Controls.Add(this.checkBoxFilterNoise);
            this.groupBox2.Location = new System.Drawing.Point(457, 447);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 105);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Motion";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(116, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "to";
            // 
            // numericUpDownThresholdHigh
            // 
            this.numericUpDownThresholdHigh.DecimalPlaces = 1;
            this.numericUpDownThresholdHigh.Location = new System.Drawing.Point(135, 45);
            this.numericUpDownThresholdHigh.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownThresholdHigh.Name = "numericUpDownThresholdHigh";
            this.numericUpDownThresholdHigh.Size = new System.Drawing.Size(60, 20);
            this.numericUpDownThresholdHigh.TabIndex = 52;
            this.numericUpDownThresholdHigh.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownThresholdHigh.ValueChanged += new System.EventHandler(this.numericUpDownThresholdHigh_ValueChanged);
            // 
            // saveFileDialogLog
            // 
            this.saveFileDialogLog.Filter = "Text files|*.txt";
            // 
            // openFileDialogMotion
            // 
            this.openFileDialogMotion.Filter = "Motion files|*.motion|All files|*.*";
            // 
            // saveFileDialogMotion
            // 
            this.saveFileDialogMotion.Filter = "Motion files|*.motion";
            // 
            // buttonPrevPage
            // 
            this.buttonPrevPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevPage.Enabled = false;
            this.buttonPrevPage.Location = new System.Drawing.Point(523, 311);
            this.buttonPrevPage.Name = "buttonPrevPage";
            this.buttonPrevPage.Size = new System.Drawing.Size(30, 25);
            this.buttonPrevPage.TabIndex = 40;
            this.buttonPrevPage.Text = "<";
            this.buttonPrevPage.UseVisualStyleBackColor = true;
            this.buttonPrevPage.Click += new System.EventHandler(this.buttonPrevPage_Click);
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNextPage.Enabled = false;
            this.buttonNextPage.Location = new System.Drawing.Point(559, 311);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(30, 25);
            this.buttonNextPage.TabIndex = 41;
            this.buttonNextPage.Text = ">";
            this.buttonNextPage.UseVisualStyleBackColor = true;
            this.buttonNextPage.Click += new System.EventHandler(this.buttonNextPage_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(475, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Page";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(563, 341);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "of";
            // 
            // labelCurrentPage
            // 
            this.labelCurrentPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentPage.Location = new System.Drawing.Point(507, 341);
            this.labelCurrentPage.Name = "labelCurrentPage";
            this.labelCurrentPage.Size = new System.Drawing.Size(50, 13);
            this.labelCurrentPage.TabIndex = 44;
            this.labelCurrentPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTotalPages
            // 
            this.labelTotalPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalPages.Location = new System.Drawing.Point(585, 341);
            this.labelTotalPages.Name = "labelTotalPages";
            this.labelTotalPages.Size = new System.Drawing.Size(50, 13);
            this.labelTotalPages.TabIndex = 45;
            this.labelTotalPages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonPrevPage10
            // 
            this.buttonPrevPage10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevPage10.Enabled = false;
            this.buttonPrevPage10.Location = new System.Drawing.Point(487, 311);
            this.buttonPrevPage10.Name = "buttonPrevPage10";
            this.buttonPrevPage10.Size = new System.Drawing.Size(30, 25);
            this.buttonPrevPage10.TabIndex = 46;
            this.buttonPrevPage10.Text = "<<";
            this.buttonPrevPage10.UseVisualStyleBackColor = true;
            this.buttonPrevPage10.Click += new System.EventHandler(this.buttonPrevPage10_Click);
            // 
            // buttonNextPage10
            // 
            this.buttonNextPage10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNextPage10.Enabled = false;
            this.buttonNextPage10.Location = new System.Drawing.Point(595, 311);
            this.buttonNextPage10.Name = "buttonNextPage10";
            this.buttonNextPage10.Size = new System.Drawing.Size(30, 25);
            this.buttonNextPage10.TabIndex = 47;
            this.buttonNextPage10.Text = ">>";
            this.buttonNextPage10.UseVisualStyleBackColor = true;
            this.buttonNextPage10.Click += new System.EventHandler(this.buttonNexPage10_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 588);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(672, 18);
            this.statusStrip1.TabIndex = 50;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 0);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 13);
            this.toolStripStatusLabel2.Text = " ";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(142, 481);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Image Reduction Factor";
            // 
            // numericUpDownFrameDivider
            // 
            this.numericUpDownFrameDivider.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numericUpDownFrameDivider.DecimalPlaces = 1;
            this.numericUpDownFrameDivider.Location = new System.Drawing.Point(269, 479);
            this.numericUpDownFrameDivider.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownFrameDivider.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFrameDivider.Name = "numericUpDownFrameDivider";
            this.numericUpDownFrameDivider.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownFrameDivider.TabIndex = 52;
            this.numericUpDownFrameDivider.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownSkipFrames
            // 
            this.numericUpDownSkipFrames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownSkipFrames.Location = new System.Drawing.Point(150, 386);
            this.numericUpDownSkipFrames.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSkipFrames.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSkipFrames.Name = "numericUpDownSkipFrames";
            this.numericUpDownSkipFrames.Size = new System.Drawing.Size(47, 20);
            this.numericUpDownSkipFrames.TabIndex = 54;
            this.numericUpDownSkipFrames.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 606);
            this.Controls.Add(this.numericUpDownSkipFrames);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDownFrameDivider);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonNextPage10);
            this.Controls.Add(this.buttonPrevPage10);
            this.Controls.Add(this.labelTotalPages);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonNextPage);
            this.Controls.Add(this.buttonPrevPage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listViewMotion);
            this.Controls.Add(this.buttonStopProcess);
            this.Controls.Add(this.checkBoxShowMotion);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.trackBarProgress);
            this.Controls.Add(this.toolBarControls);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownBrightness);
            this.Controls.Add(this.trackBarBrightness);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownContrast);
            this.Controls.Add(this.trackBarContrast);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.buttonProcess);
            this.Controls.Add(this.progressBarProcess);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.labelCurrentPage);
            this.Controls.Add(this.label4);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Motion Detector";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrames)).EndInit();
            this.contextMenuStripVector.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdHigh)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrameDivider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkipFrames)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogMov;
        private System.Windows.Forms.ProgressBar progressBarProcess;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownThreshold;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Timer timerProcess;
        private System.Windows.Forms.NumericUpDown numericUpDownContrast;
        private System.Windows.Forms.TrackBar trackBarContrast;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownBrightness;
        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageListControls;
        private System.Windows.Forms.ToolBar toolBarControls;
        private System.Windows.Forms.ToolBarButton toolBarButtonPlay;
        private System.Windows.Forms.ToolBarButton toolBarButtonPause;
        private System.Windows.Forms.ToolBarButton toolBarButtonStop;
        private System.Windows.Forms.ToolBarButton toolBarButtonSeparador1;
        private System.Windows.Forms.ToolBarButton toolBarButtonDec10Sec;
        private System.Windows.Forms.ToolBarButton toolBarButtonInc10Sec;
        private System.Windows.Forms.TrackBar trackBarProgress;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timerPlay;
        private System.Windows.Forms.Button buttonStopProcess;
        private System.Windows.Forms.Button buttonUpdateThreshold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownFrames;
        private System.Windows.Forms.CheckBox checkBoxShowMotion;
        private System.Windows.Forms.Button buttonResetExclusionZones;
        private System.Windows.Forms.Button buttonAddExclusionZones;
        private System.Windows.Forms.CheckBox checkBoxFilterNoise;
        private System.Windows.Forms.ListView listViewMotion;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem motionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogLog;
        private System.Windows.Forms.ToolStripMenuItem exportMotionDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMotionDataToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogMotion;
        private System.Windows.Forms.SaveFileDialog saveFileDialogMotion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolBarButton toolBarButtonStepBackward;
        private System.Windows.Forms.ToolBarButton toolBarButtonStepForward;
        private System.Windows.Forms.Button buttonPrevPage;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelCurrentPage;
        private System.Windows.Forms.Label labelTotalPages;
        private System.Windows.Forms.Button buttonPrevPage10;
        private System.Windows.Forms.Button buttonNextPage10;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripVector;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoDeleteToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem batchProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownThresholdHigh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownFrameDivider;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDownSkipFrames;
    }
}

