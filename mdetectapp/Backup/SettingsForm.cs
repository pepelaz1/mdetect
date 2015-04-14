using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MotionDetector
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void buttonDetectedMotion_Click(object sender, EventArgs e)
        {
            if (colorDialogDetectedMotion.ShowDialog() == DialogResult.OK)
            {
                buttonDetectedMotion.BackColor = colorDialogDetectedMotion.Color;
            }
        }

        private void buttonHighlight_Click(object sender, EventArgs e)
        {
            if (colorDialogHighlight.ShowDialog() == DialogResult.OK)
            {
                buttonHighlight.BackColor = colorDialogHighlight.Color;
            }
        }

        private void buttonSelected_Click(object sender, EventArgs e)
        {
            if (colorDialogSelected.ShowDialog() == DialogResult.OK)
            {
                buttonSelected.BackColor = colorDialogSelected.Color;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }


        public Color DetectedMotionColor
        {
            get
            {
                return buttonDetectedMotion.BackColor;
            }
            set
            {
                buttonDetectedMotion.BackColor = value;
            }
        }


        public Color HighlightMotionColor
        {
            get
            {
                return buttonHighlight.BackColor;
            }
            set
            {
                buttonHighlight.BackColor = value;
            }
        }


        public Color SelectedVectorColor
        {
            get
            {
                return buttonSelected.BackColor;
            }
            set
            {
                buttonSelected.BackColor = value;
            }
        }


    }
}