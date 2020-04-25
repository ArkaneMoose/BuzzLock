using BuzzLockGui.Backend;
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
    public partial class FormOptions : FormBuzzLock
    {
        private FormStart _formStart;
        private Stopwatch stopWatchOptionsStatus = new Stopwatch();

        public FormOptions(FormStart formStart)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.KeyPreview = true;

            _formStart = formStart;

            foreach (Control control in Controls)
            {
                control.MouseClick += OnAnyMouseClick;
            }
        }

        private void FormOptions_KeyPress(object sender, KeyPressEventArgs e)
        {
            RestartTimer();
        }

        public new void Show()
        {
            this.UpdateComponents();
            RestartTimer();
            base.Show();
        }

        public new void Hide()
        {
            StopTimer();
            base.Hide();
        }

        private void OnAnyMouseClick(object sender, EventArgs e)
        {
            RestartTimer();
        }

        private void StopTimer()
        {
            stopWatchOptionsStatus.Reset();
            timerOptionsTimeout.Enabled = false;
            timerOptionsStatus.Enabled = false;
        }

        private void StartTimer()
        {
            timerOptionsStatus_Tick(timerOptionsStatus, EventArgs.Empty);
            stopWatchOptionsStatus.Start();
            timerOptionsTimeout.Enabled = true;
            timerOptionsStatus.Enabled = true;
        }

        private void RestartTimer()
        {
            StopTimer();
            StartTimer();
        }

        private void timerOptionsTimeout_Tick(object sender, EventArgs e)
        {
            stopWatchOptionsStatus.Stop();
            stopWatchOptionsStatus.Reset();
            timerOptionsTimeout.Enabled = false;
            timerOptionsStatus.Enabled = false;
            _globalState = State.Idle;
            _formStart.UpdateComponents();
            _formStart.Show();
            this.Hide();
        }

        private void timerOptionsStatus_Tick(object sender, EventArgs e)
        {
            txtOptionsStatus.Text = Utility.Pluralize(30 - stopWatchOptionsStatus.Elapsed.Seconds, "second") + " until timeout.";
        }

        private void btnOptionsSave_Click(object sender, EventArgs e)
        {
            if (_globalState == State.UserOptions)
            {
                // ResetTimer(false);
                _globalState = State.Authenticated;
                _formStart.UpdateComponents();
                _formStart.Show();
                this.Hide();
            }
            else if (_globalState == State.UserOptions_EditProfile)
            {
                // saves new data to database
                
                _currentUser.Name = tbxNewName.Text;
                _currentUser.PhoneNumber = tbxNewPhone.Text;
                // ResetTimer();
                _globalState = State.UserOptions;
                this.UpdateComponents();
            }
            else if (_globalState == State.UserOptions_EditAuth)
            {
                //TODO: save new data to database
                // ResetTimer();
                _globalState = State.UserOptions;
                this.UpdateComponents();
            }
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            // this.TopMost = true;
            // this.FormBorderStyle = FormBorderStyle.None;
            // this.WindowState = FormWindowState.Maximized;
        }

        private void UpdateComponents()
        {
            //UserOptions
            btnEditAuth.Visible = _globalState == State.UserOptions;
            btnEditProfile.Visible = _globalState == State.UserOptions;
            btnRemoveUser.Visible = _globalState == State.UserOptions;
            txtEditAuth.Visible = _globalState == State.UserOptions;
            txtEditProfile.Visible = _globalState == State.UserOptions;
            txtRemoveUser.Visible = _globalState == State.UserOptions;


            //EditProfile
            txtCurrentName.Visible = _globalState == State.UserOptions_EditProfile;
            txtCurrentPhone.Visible = _globalState == State.UserOptions_EditProfile;
            tbxNewName.Visible = _globalState == State.UserOptions_EditProfile;
            tbxNewPhone.Visible = _globalState == State.UserOptions_EditProfile;
            //dataCurrentPicture.Visible = _globalState == State.UserOptions_EditProfile;
            //btnChangePictureOrTakePicture.Visible = _globalState == State.UserOptions_EditProfile;

            //EditAuth
            txtPrimAuth.Visible = _globalState == State.UserOptions_EditAuth;
            txtSecAuth.Visible = _globalState == State.UserOptions_EditAuth;
            cbxPrimAuth.Visible = _globalState == State.UserOptions_EditAuth;
            cbxSecAuth.Visible = _globalState == State.UserOptions_EditAuth;
            txtPrimChooseDev.Visible = _globalState == State.UserOptions_EditAuth;
            cbxBTSelect1.Visible = _globalState == State.UserOptions_EditAuth;
            txtSecChooseDevOrPin.Visible = _globalState == State.UserOptions_EditAuth;
            cbxBTSelect2.Visible = _globalState == State.UserOptions_EditAuth;

            //Set the welcome textbox
            tbxStatus.Text = $"Welcome, {_currentUser.Name}";
            //Set the permission level textbox
            txtUserPermission.Text = $"Permission Level: {_currentUser.PermissionLevel}";

            switch (_globalState)
            {
                case State.UserOptions:
                    txtOptionsTitle.Text = "BuzzLock Options Menu";
                    break;
                case State.UserOptions_EditProfile:
                    txtOptionsTitle.Text = "Edit your profile:";
                    // Query database for current name and phone number and picture
                    tbxNewName.Text = _currentUser.Name; // Populate textbox with user's current name
                    tbxNewPhone.Text = _currentUser.PhoneNumber; // Populate textbox with user's current phone number
                    ValidateTextBox(tbxNewName, EventArgs.Empty);
                    ValidatePhoneBox(tbxNewPhone, EventArgs.Empty);
                    btnOptionsSave.Enabled = noErrors;
                    break;
                case State.UserOptions_EditAuth:
                    txtOptionsTitle.Text = "Edit your authentication methods:";
                    break;
                default:
                    break;
            }
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
                // Remove the current user from the database.
                _currentUser.Delete();

                // Close options and stop both timers. Go back to IDLE
                StopTimer();

                // Query for number of users. If number of users is zero, go back to INITIALIZING
                _globalState = User.GetAll().Count != 0 ? State.Idle : State.Uninitialized;
                _formStart.UpdateComponents();
                _formStart.Show();
                this.Hide();
            }
        }

        private void FormOptions_MouseClick(object sender, EventArgs e)
        {
            // Reset active control to default
            this.ActiveControl = tbxStatus;
            RestartTimer();
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            _globalState = State.UserOptions_EditProfile;
            this.UpdateComponents();
        }

        private void btnEditAuth_Click(object sender, EventArgs e)
        {
            _globalState = State.UserOptions_EditAuth;
            this.UpdateComponents();
        }
    }

}
