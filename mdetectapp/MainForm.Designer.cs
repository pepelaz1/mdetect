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
            this.btnProcess = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.timerProcess = new System.Windows.Forms.Timer(this.components);
            this.numericUpDownContrast = new System.Windows.Forms.NumericUpDown();
            this.trackBarContrast = new System.Windows.Forms.TrackBar();
            this.lblContrast = new System.Windows.Forms.Label();
            this.numericUpDownBrightness = new System.Windows.Forms.NumericUpDown();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
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
            this.btnStop = new System.Windows.Forms.Button();
            this.lbViewMotion = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripVector = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialogLog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogMotion = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogMotion = new System.Windows.Forms.SaveFileDialog();
            this.labelCurrentPage = new System.Windows.Forms.Label();
            this.labelTotalPages = new System.Windows.Forms.Label();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.numericUpDownSkipFrames = new System.Windows.Forms.NumericUpDown();
            this.lblThreshold = new System.Windows.Forms.Label();
            this.trackBarThreshold = new System.Windows.Forms.TrackBar();
            this.numericUpDownThreshold = new System.Windows.Forms.NumericUpDown();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.btnRemoveExcZone = new System.Windows.Forms.Button();
            this.btnAddExcZone = new System.Windows.Forms.Button();
            this.gbExcZones = new System.Windows.Forms.GroupBox();
            this.btnClearExcZones = new System.Windows.Forms.Button();
            this.lbExcZones = new System.Windows.Forms.ListBox();
            this.openFileDialogLog = new System.Windows.Forms.OpenFileDialog();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblLogFile = new System.Windows.Forms.Label();
            this.lblLogFilename = new System.Windows.Forms.Label();
            this.gbTimeFilter = new System.Windows.Forms.GroupBox();
            this.btnDoTimeFilter = new System.Windows.Forms.Button();
            this.btnResetTimeFilter = new System.Windows.Forms.Button();
            this.udTimeFilter = new System.Windows.Forms.NumericUpDown();
            this.lblDistance = new System.Windows.Forms.Label();
            this.btnDefault = new System.Windows.Forms.Button();
            this.lblCriteria = new System.Windows.Forms.Label();
            this.cmbCriteria = new System.Windows.Forms.ComboBox();
            this.panelPreview = new MotionDetector.DbPanel();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProgress)).BeginInit();
            this.contextMenuStripVector.SuspendLayout();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkipFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            this.gbExcZones.SuspendLayout();
            this.gbTimeFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTimeFilter)).BeginInit();
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
            this.progressBarProcess.Enabled = false;
            this.progressBarProcess.Location = new System.Drawing.Point(17, 576);
            this.progressBarProcess.Name = "progressBarProcess";
            this.progressBarProcess.Size = new System.Drawing.Size(642, 21);
            this.progressBarProcess.TabIndex = 2;
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProcess.Enabled = false;
            this.btnProcess.Location = new System.Drawing.Point(17, 541);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(103, 31);
            this.btnProcess.TabIndex = 9;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.motionToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(676, 24);
            this.mainMenu.TabIndex = 10;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openLogStripMenuItem,
            this.saveLogStripMenuItem,
            this.batchProcessToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openLogStripMenuItem
            // 
            this.openLogStripMenuItem.Name = "openLogStripMenuItem";
            this.openLogStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.openLogStripMenuItem.Text = "Open motion log";
            this.openLogStripMenuItem.Click += new System.EventHandler(this.openLogStripMenuItem_Click);
            // 
            // saveLogStripMenuItem
            // 
            this.saveLogStripMenuItem.Enabled = false;
            this.saveLogStripMenuItem.Name = "saveLogStripMenuItem";
            this.saveLogStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.saveLogStripMenuItem.Text = "Save motion log";
            this.saveLogStripMenuItem.Click += new System.EventHandler(this.saveLogStripMenuItem_Click);
            // 
            // batchProcessToolStripMenuItem
            // 
            this.batchProcessToolStripMenuItem.Name = "batchProcessToolStripMenuItem";
            this.batchProcessToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.batchProcessToolStripMenuItem.Text = "Batch Process";
            this.batchProcessToolStripMenuItem.Click += new System.EventHandler(this.batchProcessToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem1.Text = "Settings";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(162, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
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
            this.motionToolStripMenuItem.Enabled = false;
            this.motionToolStripMenuItem.Name = "motionToolStripMenuItem";
            this.motionToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.motionToolStripMenuItem.Text = "Motion";
            this.motionToolStripMenuItem.Visible = false;
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Enabled = false;
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveFileToolStripMenuItem.Text = "Save Log File";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // importMotionDataToolStripMenuItem
            // 
            this.importMotionDataToolStripMenuItem.Name = "importMotionDataToolStripMenuItem";
            this.importMotionDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importMotionDataToolStripMenuItem.Text = "Import motion data";
            // 
            // exportMotionDataToolStripMenuItem
            // 
            this.exportMotionDataToolStripMenuItem.Enabled = false;
            this.exportMotionDataToolStripMenuItem.Name = "exportMotionDataToolStripMenuItem";
            this.exportMotionDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportMotionDataToolStripMenuItem.Text = "Export motion data";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // undoDeleteToolStripMenuItem
            // 
            this.undoDeleteToolStripMenuItem.Enabled = false;
            this.undoDeleteToolStripMenuItem.Name = "undoDeleteToolStripMenuItem";
            this.undoDeleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoDeleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.undoDeleteToolStripMenuItem.Text = "Undo Delete";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // timerProcess
            // 
            this.timerProcess.Tick += new System.EventHandler(this.timerProcess_Tick);
            // 
            // numericUpDownContrast
            // 
            this.numericUpDownContrast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownContrast.Enabled = false;
            this.numericUpDownContrast.Location = new System.Drawing.Point(399, 451);
            this.numericUpDownContrast.Maximum = new decimal(new int[] {
            255,
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
            this.trackBarContrast.Enabled = false;
            this.trackBarContrast.Location = new System.Drawing.Point(76, 451);
            this.trackBarContrast.Maximum = 255;
            this.trackBarContrast.Name = "trackBarContrast";
            this.trackBarContrast.Size = new System.Drawing.Size(317, 26);
            this.trackBarContrast.TabIndex = 16;
            this.trackBarContrast.TickFrequency = 0;
            this.trackBarContrast.Value = 128;
            this.trackBarContrast.Scroll += new System.EventHandler(this.trackBarContrast_Scroll);
            // 
            // lblContrast
            // 
            this.lblContrast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblContrast.AutoSize = true;
            this.lblContrast.Enabled = false;
            this.lblContrast.Location = new System.Drawing.Point(19, 455);
            this.lblContrast.Name = "lblContrast";
            this.lblContrast.Size = new System.Drawing.Size(46, 13);
            this.lblContrast.TabIndex = 18;
            this.lblContrast.Text = "Contrast";
            // 
            // numericUpDownBrightness
            // 
            this.numericUpDownBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownBrightness.Enabled = false;
            this.numericUpDownBrightness.Location = new System.Drawing.Point(399, 481);
            this.numericUpDownBrightness.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
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
            this.trackBarBrightness.Enabled = false;
            this.trackBarBrightness.Location = new System.Drawing.Point(76, 481);
            this.trackBarBrightness.Maximum = 255;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(317, 27);
            this.trackBarBrightness.TabIndex = 19;
            this.trackBarBrightness.TickFrequency = 0;
            this.trackBarBrightness.Value = 128;
            this.trackBarBrightness.Scroll += new System.EventHandler(this.trackBarBrightness_Scroll);
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
            this.toolBarControls.Enabled = false;
            this.toolBarControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolBarControls.ImageList = this.imageListControls;
            this.toolBarControls.Location = new System.Drawing.Point(17, 395);
            this.toolBarControls.Name = "toolBarControls";
            this.toolBarControls.ShowToolTips = true;
            this.toolBarControls.Size = new System.Drawing.Size(130, 21);
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
            this.trackBarProgress.Location = new System.Drawing.Point(12, 342);
            this.trackBarProgress.Name = "trackBarProgress";
            this.trackBarProgress.Size = new System.Drawing.Size(437, 38);
            this.trackBarProgress.SmallChange = 0;
            this.trackBarProgress.TabIndex = 23;
            this.trackBarProgress.TickFrequency = 0;
            this.trackBarProgress.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarProgress.Scroll += new System.EventHandler(this.trackBarProgress_Scroll);
            this.trackBarProgress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trackBarProgress_KeyDown);
            this.trackBarProgress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBarProgress_MouseDown);
            this.trackBarProgress.MouseMove += new System.Windows.Forms.MouseEventHandler(this.trackBarProgress_MouseMove);
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.Location = new System.Drawing.Point(313, 373);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(118, 21);
            this.labelTime.TabIndex = 24;
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timerPlay
            // 
            this.timerPlay.Tick += new System.EventHandler(this.timerPlay_Tick);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(126, 541);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(103, 31);
            this.btnStop.TabIndex = 25;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.buttonStopProcess_Click);
            // 
            // lbViewMotion
            // 
            this.lbViewMotion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbViewMotion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.lbViewMotion.ContextMenuStrip = this.contextMenuStripVector;
            this.lbViewMotion.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbViewMotion.Enabled = false;
            this.lbViewMotion.FullRowSelect = true;
            this.lbViewMotion.HideSelection = false;
            this.lbViewMotion.Location = new System.Drawing.Point(461, 36);
            this.lbViewMotion.MultiSelect = false;
            this.lbViewMotion.Name = "lbViewMotion";
            this.lbViewMotion.Size = new System.Drawing.Size(198, 306);
            this.lbViewMotion.TabIndex = 37;
            this.lbViewMotion.UseCompatibleStateImageBehavior = false;
            this.lbViewMotion.View = System.Windows.Forms.View.Details;
            this.lbViewMotion.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lbViewMotion_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Frame";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Time";
            this.columnHeader3.Width = 110;
            // 
            // contextMenuStripVector
            // 
            this.contextMenuStripVector.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStripVector.Name = "contextMenuStripVector";
            this.contextMenuStripVector.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // saveFileDialogLog
            // 
            this.saveFileDialogLog.Filter = "Log files|*.log";
            // 
            // openFileDialogMotion
            // 
            this.openFileDialogMotion.Filter = "Motion files|*.motion|All files|*.*";
            // 
            // saveFileDialogMotion
            // 
            this.saveFileDialogMotion.Filter = "Motion files|*.motion";
            // 
            // labelCurrentPage
            // 
            this.labelCurrentPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentPage.Location = new System.Drawing.Point(505, 396);
            this.labelCurrentPage.Name = "labelCurrentPage";
            this.labelCurrentPage.Size = new System.Drawing.Size(50, 8);
            this.labelCurrentPage.TabIndex = 44;
            this.labelCurrentPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCurrentPage.Visible = false;
            // 
            // labelTotalPages
            // 
            this.labelTotalPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTotalPages.Location = new System.Drawing.Point(583, 396);
            this.labelTotalPages.Name = "labelTotalPages";
            this.labelTotalPages.Size = new System.Drawing.Size(50, 8);
            this.labelTotalPages.TabIndex = 45;
            this.labelTotalPages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusBar.Location = new System.Drawing.Point(0, 601);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(676, 20);
            this.statusBar.TabIndex = 50;
            this.statusBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 0);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 15);
            this.toolStripStatusLabel2.Text = " ";
            // 
            // numericUpDownSkipFrames
            // 
            this.numericUpDownSkipFrames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownSkipFrames.Enabled = false;
            this.numericUpDownSkipFrames.Location = new System.Drawing.Point(150, 396);
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
            // lblThreshold
            // 
            this.lblThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblThreshold.AutoSize = true;
            this.lblThreshold.Enabled = false;
            this.lblThreshold.Location = new System.Drawing.Point(14, 427);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(54, 13);
            this.lblThreshold.TabIndex = 55;
            this.lblThreshold.Text = "Threshold";
            // 
            // trackBarThreshold
            // 
            this.trackBarThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarThreshold.AutoSize = false;
            this.trackBarThreshold.Enabled = false;
            this.trackBarThreshold.Location = new System.Drawing.Point(76, 424);
            this.trackBarThreshold.Maximum = 255;
            this.trackBarThreshold.Name = "trackBarThreshold";
            this.trackBarThreshold.Size = new System.Drawing.Size(317, 28);
            this.trackBarThreshold.TabIndex = 56;
            this.trackBarThreshold.TickFrequency = 0;
            this.trackBarThreshold.Scroll += new System.EventHandler(this.trackBarThreshold_Scroll);
            // 
            // numericUpDownThreshold
            // 
            this.numericUpDownThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownThreshold.Enabled = false;
            this.numericUpDownThreshold.Location = new System.Drawing.Point(399, 423);
            this.numericUpDownThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownThreshold.Name = "numericUpDownThreshold";
            this.numericUpDownThreshold.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownThreshold.TabIndex = 57;
            this.numericUpDownThreshold.ValueChanged += new System.EventHandler(this.numericUpDownTheshold_ValueChanged);
            // 
            // lblBrightness
            // 
            this.lblBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBrightness.AutoSize = true;
            this.lblBrightness.Enabled = false;
            this.lblBrightness.Location = new System.Drawing.Point(14, 484);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(56, 13);
            this.lblBrightness.TabIndex = 21;
            this.lblBrightness.Text = "Brightness";
            // 
            // btnRemoveExcZone
            // 
            this.btnRemoveExcZone.Enabled = false;
            this.btnRemoveExcZone.Location = new System.Drawing.Point(64, 107);
            this.btnRemoveExcZone.Name = "btnRemoveExcZone";
            this.btnRemoveExcZone.Size = new System.Drawing.Size(60, 21);
            this.btnRemoveExcZone.TabIndex = 33;
            this.btnRemoveExcZone.Text = "Remove";
            this.btnRemoveExcZone.UseVisualStyleBackColor = true;
            this.btnRemoveExcZone.Click += new System.EventHandler(this.buttonResetExclusionZones_Click);
            // 
            // btnAddExcZone
            // 
            this.btnAddExcZone.Enabled = false;
            this.btnAddExcZone.Location = new System.Drawing.Point(7, 106);
            this.btnAddExcZone.Name = "btnAddExcZone";
            this.btnAddExcZone.Size = new System.Drawing.Size(49, 21);
            this.btnAddExcZone.TabIndex = 34;
            this.btnAddExcZone.Text = "Add";
            this.btnAddExcZone.UseVisualStyleBackColor = true;
            this.btnAddExcZone.Click += new System.EventHandler(this.buttonAddExclusionZones_Click);
            // 
            // gbExcZones
            // 
            this.gbExcZones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExcZones.Controls.Add(this.btnClearExcZones);
            this.gbExcZones.Controls.Add(this.lbExcZones);
            this.gbExcZones.Controls.Add(this.btnAddExcZone);
            this.gbExcZones.Controls.Add(this.btnRemoveExcZone);
            this.gbExcZones.Enabled = false;
            this.gbExcZones.Location = new System.Drawing.Point(461, 407);
            this.gbExcZones.Name = "gbExcZones";
            this.gbExcZones.Size = new System.Drawing.Size(198, 135);
            this.gbExcZones.TabIndex = 38;
            this.gbExcZones.TabStop = false;
            this.gbExcZones.Text = "Exclusion Zones";
            // 
            // btnClearExcZones
            // 
            this.btnClearExcZones.Enabled = false;
            this.btnClearExcZones.Location = new System.Drawing.Point(132, 107);
            this.btnClearExcZones.Name = "btnClearExcZones";
            this.btnClearExcZones.Size = new System.Drawing.Size(60, 21);
            this.btnClearExcZones.TabIndex = 36;
            this.btnClearExcZones.Text = "Clear";
            this.btnClearExcZones.UseVisualStyleBackColor = true;
            this.btnClearExcZones.Click += new System.EventHandler(this.btnClearExcZones_Click);
            // 
            // lbExcZones
            // 
            this.lbExcZones.FormattingEnabled = true;
            this.lbExcZones.Location = new System.Drawing.Point(6, 21);
            this.lbExcZones.Name = "lbExcZones";
            this.lbExcZones.Size = new System.Drawing.Size(186, 82);
            this.lbExcZones.TabIndex = 35;
            this.lbExcZones.SelectedIndexChanged += new System.EventHandler(this.lbExcZones_SelectedIndexChanged);
            // 
            // openFileDialogLog
            // 
            this.openFileDialogLog.Filter = "Motion log file|*.log";
            // 
            // cmbMode
            // 
            this.cmbMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.Enabled = false;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            "Detection preview",
            "Display motion log",
            "Simple playback"});
            this.cmbMode.Location = new System.Drawing.Point(253, 396);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(135, 21);
            this.cmbMode.TabIndex = 58;
            this.cmbMode.SelectedIndexChanged += new System.EventHandler(this.cmbMode_SelectedIndexChanged);
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMode.AutoSize = true;
            this.lblMode.Enabled = false;
            this.lblMode.Location = new System.Drawing.Point(210, 400);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(37, 13);
            this.lblMode.TabIndex = 59;
            this.lblMode.Text = "Mode:";
            // 
            // lblLogFile
            // 
            this.lblLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLogFile.AutoSize = true;
            this.lblLogFile.Location = new System.Drawing.Point(248, 550);
            this.lblLogFile.Name = "lblLogFile";
            this.lblLogFile.Size = new System.Drawing.Size(44, 13);
            this.lblLogFile.TabIndex = 60;
            this.lblLogFile.Text = "Log file:";
            this.lblLogFile.Visible = false;
            // 
            // lblLogFilename
            // 
            this.lblLogFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLogFilename.AutoSize = true;
            this.lblLogFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLogFilename.Location = new System.Drawing.Point(293, 550);
            this.lblLogFilename.Name = "lblLogFilename";
            this.lblLogFilename.Size = new System.Drawing.Size(0, 13);
            this.lblLogFilename.TabIndex = 61;
            // 
            // gbTimeFilter
            // 
            this.gbTimeFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTimeFilter.Controls.Add(this.btnDoTimeFilter);
            this.gbTimeFilter.Controls.Add(this.btnResetTimeFilter);
            this.gbTimeFilter.Controls.Add(this.udTimeFilter);
            this.gbTimeFilter.Controls.Add(this.lblDistance);
            this.gbTimeFilter.Enabled = false;
            this.gbTimeFilter.Location = new System.Drawing.Point(461, 350);
            this.gbTimeFilter.Name = "gbTimeFilter";
            this.gbTimeFilter.Size = new System.Drawing.Size(198, 47);
            this.gbTimeFilter.TabIndex = 62;
            this.gbTimeFilter.TabStop = false;
            this.gbTimeFilter.Text = "Log time filter";
            // 
            // btnDoTimeFilter
            // 
            this.btnDoTimeFilter.Location = new System.Drawing.Point(106, 19);
            this.btnDoTimeFilter.Name = "btnDoTimeFilter";
            this.btnDoTimeFilter.Size = new System.Drawing.Size(39, 23);
            this.btnDoTimeFilter.TabIndex = 3;
            this.btnDoTimeFilter.Text = "Filter";
            this.btnDoTimeFilter.UseVisualStyleBackColor = true;
            this.btnDoTimeFilter.Click += new System.EventHandler(this.btnDoTimeFilter_Click);
            // 
            // btnResetTimeFilter
            // 
            this.btnResetTimeFilter.Location = new System.Drawing.Point(150, 19);
            this.btnResetTimeFilter.Name = "btnResetTimeFilter";
            this.btnResetTimeFilter.Size = new System.Drawing.Size(43, 23);
            this.btnResetTimeFilter.TabIndex = 2;
            this.btnResetTimeFilter.Text = "Reset";
            this.btnResetTimeFilter.UseVisualStyleBackColor = true;
            this.btnResetTimeFilter.Click += new System.EventHandler(this.btnResetTimeFilter_Click);
            // 
            // udTimeFilter
            // 
            this.udTimeFilter.DecimalPlaces = 2;
            this.udTimeFilter.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.udTimeFilter.Location = new System.Drawing.Point(55, 21);
            this.udTimeFilter.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udTimeFilter.Name = "udTimeFilter";
            this.udTimeFilter.Size = new System.Drawing.Size(47, 20);
            this.udTimeFilter.TabIndex = 1;
            this.udTimeFilter.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Location = new System.Drawing.Point(4, 23);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(55, 13);
            this.lblDistance.TabIndex = 0;
            this.lblDistance.Text = "Distance: ";
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.Enabled = false;
            this.btnDefault.Location = new System.Drawing.Point(397, 513);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(52, 21);
            this.btnDefault.TabIndex = 63;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // lblCriteria
            // 
            this.lblCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCriteria.AutoSize = true;
            this.lblCriteria.Enabled = false;
            this.lblCriteria.Location = new System.Drawing.Point(14, 515);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(85, 13);
            this.lblCriteria.TabIndex = 64;
            this.lblCriteria.Text = "Decision criteria:";
            // 
            // cmbCriteria
            // 
            this.cmbCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriteria.Enabled = false;
            this.cmbCriteria.FormattingEnabled = true;
            this.cmbCriteria.Items.AddRange(new object[] {
            "Min-max",
            "Average value"});
            this.cmbCriteria.Location = new System.Drawing.Point(107, 511);
            this.cmbCriteria.Name = "cmbCriteria";
            this.cmbCriteria.Size = new System.Drawing.Size(121, 21);
            this.cmbCriteria.TabIndex = 65;
            this.cmbCriteria.SelectedIndexChanged += new System.EventHandler(this.cmbCriteria_SelectedIndexChanged);
            // 
            // panelPreview
            // 
            this.panelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreview.BackColor = System.Drawing.Color.Black;
            this.panelPreview.Location = new System.Drawing.Point(12, 36);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(437, 306);
            this.panelPreview.TabIndex = 11;
            this.panelPreview.SizeChanged += new System.EventHandler(this.panelPreview_SizeChanged);
            this.panelPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.panelPreview_Paint);
            this.panelPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelPreview_MouseDown);
            this.panelPreview.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelPreview_MouseMove);
            this.panelPreview.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelPreview_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 621);
            this.Controls.Add(this.cmbCriteria);
            this.Controls.Add(this.lblCriteria);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.gbTimeFilter);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.lblLogFilename);
            this.Controls.Add(this.lblLogFile);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.numericUpDownThreshold);
            this.Controls.Add(this.trackBarThreshold);
            this.Controls.Add(this.lblThreshold);
            this.Controls.Add(this.numericUpDownSkipFrames);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.labelTotalPages);
            this.Controls.Add(this.gbExcZones);
            this.Controls.Add(this.lbViewMotion);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.trackBarProgress);
            this.Controls.Add(this.toolBarControls);
            this.Controls.Add(this.lblBrightness);
            this.Controls.Add(this.numericUpDownBrightness);
            this.Controls.Add(this.trackBarBrightness);
            this.Controls.Add(this.lblContrast);
            this.Controls.Add(this.numericUpDownContrast);
            this.Controls.Add(this.trackBarContrast);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.progressBarProcess);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.labelCurrentPage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Motion Detector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProgress)).EndInit();
            this.contextMenuStripVector.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkipFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).EndInit();
            this.gbExcZones.ResumeLayout(false);
            this.gbTimeFilter.ResumeLayout(false);
            this.gbTimeFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udTimeFilter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogMov;
        private System.Windows.Forms.ProgressBar progressBarProcess;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.Timer timerProcess;
        private System.Windows.Forms.NumericUpDown numericUpDownContrast;
        private System.Windows.Forms.TrackBar trackBarContrast;
        private System.Windows.Forms.Label lblContrast;
        private System.Windows.Forms.NumericUpDown numericUpDownBrightness;
        private System.Windows.Forms.TrackBar trackBarBrightness;
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
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ListView lbViewMotion;
        private System.Windows.Forms.ColumnHeader columnHeader1;
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
        private System.Windows.Forms.Label labelCurrentPage;
        private System.Windows.Forms.Label labelTotalPages;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripVector;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoDeleteToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDownSkipFrames;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.TrackBar trackBarThreshold;
        private System.Windows.Forms.NumericUpDown numericUpDownThreshold;
        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.Button btnRemoveExcZone;
        private System.Windows.Forms.Button btnAddExcZone;
        private System.Windows.Forms.GroupBox gbExcZones;
        private System.Windows.Forms.OpenFileDialog openFileDialogLog;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Label lblLogFile;
        private System.Windows.Forms.Label lblLogFilename;
        private System.Windows.Forms.ListBox lbExcZones;
        private System.Windows.Forms.Button btnClearExcZones;
        private DbPanel panelPreview;
        private System.Windows.Forms.GroupBox gbTimeFilter;
        private System.Windows.Forms.Button btnDoTimeFilter;
        private System.Windows.Forms.Button btnResetTimeFilter;
        private System.Windows.Forms.NumericUpDown udTimeFilter;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLogStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLogStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Label lblCriteria;
        private System.Windows.Forms.ComboBox cmbCriteria;
    }
}

