using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuzzLockGui
{
    public partial class FormOptions : Form
    {
        private FormStart _formStart;
        private FormOptions _formOptions;
        public Stopwatch stopWatchOptionsStatus = new Stopwatch();

        public FormOptions(FormStart formStart)
        {
            InitializeComponent();
            
            this.StartPosition = FormStartPosition.Manual;
            this.Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            _formStart = formStart;
            _formOptions = this;
        }

        private void btnOptionsSave_Click(object sender, EventArgs e)
        {
            txtOptionsStatus.Text = "30 seconds until timeout.";
            stopWatchOptionsStatus.Stop();
            stopWatchOptionsStatus.Reset();
            timerOptionsTimeout.Enabled = false;
            timerOptionsStatus.Enabled = false;
            _formStart._state = State.Authenticated;
            _formStart.UpdateComponents();
            _formStart.Show();
            this.Hide();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {

        }

        private void timerOptionsTimeout_Tick(object sender, EventArgs e)
        {
            stopWatchOptionsStatus.Stop();
            stopWatchOptionsStatus.Reset();
            timerOptionsTimeout.Enabled = false;
            timerOptionsStatus.Enabled = false;
            _formStart._state = State.Idle;
            _formStart.UpdateComponents();
            _formStart.Show();
            this.Hide();
        }

        private void timerOptionsStatus_Tick(object sender, EventArgs e)
        {
            txtOptionsStatus.Text = (30 - stopWatchOptionsStatus.Elapsed.Seconds) + " seconds until timeout.";
        }
    }
}
