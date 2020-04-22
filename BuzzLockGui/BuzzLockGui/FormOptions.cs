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

            // Also close "remove user" message box if it is open
        }

        private void timerOptionsStatus_Tick(object sender, EventArgs e)
        {
            txtOptionsStatus.Text = (30 - stopWatchOptionsStatus.Elapsed.Seconds) + " seconds until timeout.";
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            // Initializes the variables to pass to the MessageBox.Show method.
            string message = "Are you sure you want to remove your user? This cannot be undone.";
            string caption = "Remove user";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Yes)
            {
                //TODO: Remove the current user from the database.
                
                // Close options and stop both timers. Go back to IDLE
                timerOptionsTimeout.Enabled = false;
                timerOptionsStatus.Enabled = false;
                _formStart._state = State.Idle;
                _formStart.UpdateComponents();
                _formStart.Show();
                this.Hide();
            }
        }

        private void FormOptions_Click(object sender, EventArgs e)
        {
            // Reset active control to default
            this.ActiveControl = tbxStatus;
        }
    }
    
}
