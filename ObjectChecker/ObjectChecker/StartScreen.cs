using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ObjectChecker
{
    public enum Mode
    {
        Checker,
        Recognition,
        Developer
    }
    public partial class StartScreen : Form
    {
        public Mode currentMode = Mode.Checker;

        public StartScreen()
        {
            InitializeComponent();
            startScreenComboBox.SelectedIndex = 0;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            ActiveForm.Hide();

            ObjectChecker objectChecker;
            DeveloperModeForm developerMode;


            if(startScreenComboBox.SelectedIndex == 3)
            {
                developerMode = new DeveloperModeForm();
                developerMode.Show();
            }
            else
            {
                objectChecker = new ObjectChecker(currentMode);
                objectChecker.Show();
                startBtn.Enabled = true;
            }
        }

        private void StartScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void startScreenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (startScreenComboBox.SelectedIndex)
            {
                case 0:
                    startBtn.Enabled = false;
                    break;
                case 1:
                    currentMode = Mode.Checker;
                    startBtn.Enabled = true;
                    break;
                case 2:
                    currentMode = Mode.Recognition;
                    startBtn.Enabled = true;
                    break;
                case 3:
                    currentMode = Mode.Developer;
                    startBtn.Enabled = true;
                    break;
            }
        }
    }
}
