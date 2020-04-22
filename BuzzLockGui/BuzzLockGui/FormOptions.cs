using ModernMessageBoxLib;
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
            if (_formStart._state == State.UserOptions)
            {
                stopWatchOptionsStatus.Stop();
                stopWatchOptionsStatus.Reset();
                timerOptionsTimeout.Enabled = false;
                timerOptionsStatus.Enabled = false;
                _formStart._state = State.Authenticated;
                _formStart.UpdateComponents();
                _formStart.Show();
                this.Hide();
            }
            else if (_formStart._state == State.UserOptions_EditProfile
                     || _formStart._state == State.UserOptions_EditAuth)
            {
                //TODO: save new data to database
                _formStart._state = State.UserOptions;
                this.UpdateComponents();
            }
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            this.UpdateComponents();
        }
        private void UpdateComponents()
        {
            //UserOptions
            btnEditAuth.Visible = _formStart._state == State.UserOptions;
            btnEditProfile.Visible = _formStart._state == State.UserOptions;
            btnRemoveUser.Visible = _formStart._state == State.UserOptions;
            txtEditAuth.Visible = _formStart._state == State.UserOptions;
            txtEditProfile.Visible = _formStart._state == State.UserOptions;
            txtRemoveUser.Visible = _formStart._state == State.UserOptions;


            //EditProfile
            txtCurrentName.Visible = _formStart._state == State.UserOptions_EditProfile;
            txtCurrentPhone.Visible = _formStart._state == State.UserOptions_EditProfile;
            txtNewName.Visible = _formStart._state == State.UserOptions_EditProfile;
            txtNewPhone.Visible = _formStart._state == State.UserOptions_EditProfile;
            tbxCurrentName.Visible = _formStart._state == State.UserOptions_EditProfile;
            tbxCurrentPhone.Visible = _formStart._state == State.UserOptions_EditProfile;
            tbxNewName.Visible = _formStart._state == State.UserOptions_EditProfile;
            tbxNewPhone.Visible = _formStart._state == State.UserOptions_EditProfile;
            btnChangeName.Visible = _formStart._state == State.UserOptions_EditProfile;
            btnChangePhone.Visible = _formStart._state == State.UserOptions_EditProfile;
            //dataCurrentPicture.Visible = _formStart._state == State.UserOptions_EditProfile;
            //btnChangePictureOrTakePicture.Visible = _formStart._state == State.UserOptions_EditProfile;

            //EditAuth
            //txtPrimAuth.Visible = _formStart._state == State.UserOptions_EditAuth;
            //txtSecAuth.Visible = _formStart._state == State.UserOptions_EditAuth;
            //cbxPrimAuth.Visible = _formStart._state == State.UserOptions_EditAuth;
            //cbxSecAuth.Visible = _formStart._state == State.UserOptions_EditAuth;

            switch (_formStart._state)
            {
                case State.UserOptions:
                    txtOptionsTitle.Text = "BuzzLock Options Menu";
                    break;
                case State.UserOptions_EditProfile:
                    txtOptionsTitle.Text = "Edit your profile:";
                    //TODO: Query database for current name and phone number and picture
                    tbxCurrentName.Text = "<get from database>";
                    tbxCurrentPhone.Text = "<get from database>";
                    validateNewName(tbxNewName, EventArgs.Empty);
                    validateNewPhone(tbxNewPhone, EventArgs.Empty);
                    break;
                case State.UserOptions_EditAuth:
                    txtOptionsTitle.Text = "Edit your authentication methods:";
                    break;
                default:
                    break;
            }
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
            // Initializes and displays the AutoClosingMessageBox.
            var result = AutoClosingMessageBox.Show(
                text: "Are you sure you want to remove your user? This cannot be undone.",
                caption: "Remove User",
                timeout: 5000, //5 seconds
                buttons: MessageBoxButtons.YesNo,
                defaultResult: DialogResult.No);
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

        //TODO: reset options timer every time the user moves, 
        //      the mouse, clicks something or types something. 

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            _formStart._state = State.UserOptions_EditProfile;
            this.UpdateComponents();
        }

        private void btnEditAuth_Click(object sender, EventArgs e)
        {
            _formStart._state = State.UserOptions_EditAuth;
            this.UpdateComponents();
        }

        private void tbxCurrentName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCurrentName_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeName_Click(object sender, EventArgs e)
        {
            //TODO: Change user name in database

            // Update current name textbox and reset new name textbox
            tbxCurrentName.Text = tbxNewName.Text;
            tbxNewName.Text = "";
        }

        private void validateNewName(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            btnChangeName.Enabled = textBox.TextLength > 0;
        }

        private void validateNewPhone(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            //TODO: implement phone number validation
            btnChangePhone.Enabled = textBox.TextLength > 0;
        }

        private void btnChangePhone_Click(object sender, EventArgs e)
        {
            //TODO: Change user phone in database

            // Update currentPhone textbox and reset new phone textbox
            tbxCurrentPhone.Text = tbxNewPhone.Text;
            tbxNewPhone.Text = "";
        }
    }
    
}
