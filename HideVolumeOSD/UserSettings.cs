﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace HideVolumeOSD
{
    public partial class UserSettings : Form
    {
        public UserSettings()
        {
            InitializeComponent();

            checkBoxSystemTrayVolume.Checked = Settings.Default.VolumeInSystemTray;
            trackBarDelay.Value = Settings.Default.VolumeHideDelay;
            SetChecked(Settings.Default.VolumeDisplaySize);
            radioButtonLight.Checked = Settings.Default.VolumeDisplayLight;
            radioButtonDark.Checked = !Settings.Default.VolumeDisplayLight;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible)
            {
                Rectangle rect = Screen.FromHandle(this.Handle).Bounds;
                Location = new Point(rect.Width - this.Width - 64, rect.Height - this.Height - 96);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Settings.Default.Save();
        }

        private void trackBarDelay_Scroll(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            double indexDbl = (value * 1.0) / trackBarDelay.TickFrequency;
            int index = Convert.ToInt32(Math.Round(indexDbl));

            trackBarDelay.Value = trackBarDelay.TickFrequency * index;

            labelDelay.Text = trackBarDelay.Value.ToString() + " ms";

            Settings.Default.VolumeHideDelay = trackBarDelay.Value;
        }

        private void checkBoxSystemTrayVolume_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.VolumeInSystemTray = checkBoxSystemTrayVolume.Checked;
        }
    
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int GetCheckedIndex()
        {
            if (radioButtonSmall.Checked)
            {
                return 0;
            }
            else
                if (radioButtonMedium.Checked)
                {
                    return 1;
                }
                else
                    if (radioButtonBig.Checked)
                    {
                        return 2;
                    }

            return 0;
        }

        private void SetChecked(int value)
        {
            switch (value)
            {
                case 0:

                    radioButtonSmall.Checked = true;
                    break;

                case 1:

                    radioButtonMedium.Checked = true;
                    break;

                  
                case 2:
                    radioButtonBig.Checked = true;
                    break;
            }

            Settings.Default.VolumeDisplaySize = value;
        }

        private void radioButtonSmall_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSmall.Checked)
            {
                SetChecked(0);
            }
        }

        private void radioButtonMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMedium.Checked)
            {
                SetChecked(1);
            }
        }

        private void radioButtonBig_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBig.Checked)
            {
                SetChecked(2);
            }
        }

        private void radioButtonLight_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.VolumeDisplayLight = radioButtonLight.Checked;
        }

        private void radioButtonDark_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.VolumeDisplayLight = radioButtonLight.Checked;
        }
    }
}
