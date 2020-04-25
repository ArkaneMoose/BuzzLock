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
            txtPrimChooseDev.Visible = false;
            txtCard.Visible = false;
            tbxCard.Visible = false;
            cbxBTSelect1.Visible = false;
            txtSecChooseDevOrPin.Visible = false;
            cbxBTSelect2.Visible = false;
            tbxPin.Visible = false;

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

                    // Ensure user properly fills the Name and Phone text boxes before submitting to database
                    tbxNewName.Tag = "Please enter a your full name.";
                    ValidateTextBox(tbxNewName, EventArgs.Empty);
                    ValidatePhoneBox(tbxNewPhone, EventArgs.Empty);
                    OnValidate();
                    break;
                case State.UserOptions_EditAuth:
                    txtOptionsTitle.Text = "Edit your authentication methods:";
                    // Query database for current primary and secondary authentication method.
                    AuthenticationMethod primary = _currentUser.AuthenticationMethods.Primary;
                    switch(primary.GetType().Name)
                    {
                        case "Card":
                            cbxPrimAuth.Items.Insert(0, "Card");
                            cbxPrimAuth.Items.Insert(1, "Bluetooth");
                            txtCard.Visible = true;
                            tbxCard.Visible = true;
                            Card card = (Card)primary;
                            tbxCard.Text = card.Id;
                            break;
                        case "BluetoothDevice":
                            cbxPrimAuth.Items.Insert(0, "Bluetooth");
                            cbxPrimAuth.Items.Insert(1, "Card");
                            txtPrimChooseDev.Visible = true;
                            cbxBTSelect1.Visible = true;
                            BluetoothDevice btDevice = (BluetoothDevice)primary;
                            cbxBTSelect1.Items.Insert(0, btDevice.Address);
                            break;
                    }
                    cbxPrimAuth.SelectedIndex = 0;

                    AuthenticationMethod secondary = _currentUser.AuthenticationMethods.Secondary;
                    switch(secondary.GetType().Name)
                    {
                        case "BluetoothDevice":
                            cbxSecAuth.Items.Insert(0, "Bluetooth");
                            cbxSecAuth.Items.Insert(1, "PIN");
                            txtSecChooseDevOrPin.Visible = true;
                            txtSecChooseDevOrPin.Text = "Choose device:";
                            BluetoothDevice btDevice = (BluetoothDevice)secondary;
                            cbxBTSelect2.Items.Insert(0, btDevice.Address);
                            break;
                        case "Pin":
                            cbxSecAuth.Items.Insert(0, "PIN");
                            cbxSecAuth.Items.Insert(1, "Bluetooth"");
                            txtSecChooseDevOrPin.Visible = true;
                            txtSecChooseDevOrPin.Text = "Insert PIN:";
                            tbxPin.Visible = true;
                            Pin pin = (Pin)secondary;
                            tbxPin.Text = pin.PinValue;
                            break;
                    }
                    cbxSecAuth.SelectedIndex = 0;
                    //ModifyPrimaryAuthConfiguration(cbxPrimAuth, EventArgs.Empty);
                    //ModifySecondaryAuthConfiguration(cbxSecAuth, EventArgs.Empty);
                    // TODO: the Pin AuthenticationMethod returns a PinValue ToString() as "PIN: ######". Do we have to extract ###### manually?
                    //cbxPrimAuth.SelectedItem = _currentUser.AuthenticationMethods.Primary.ToString();
                    //cbxSecAuth.SelectedItem = _currentUser.AuthenticationMethods.Secondary.ToString();
                    break;
                default:
                    break;
            }
        }

        private void ModifyPrimaryAuthConfiguration(object sender, EventArgs e)
        {
            if (sender.GetType().Name == "ComboBox")
            {
                ComboBox comboBox = (ComboBox)sender;
                ValidateComboBox(comboBox, e);
                if (comboBox.SelectedItem.ToString() == "Bluetooth")
                {
                    txtPrimChooseDev.Visible = true;
                    cbxBTSelect1.Visible = true;
                    ValidateComboBox(cbxBTSelect1, EventArgs.Empty);
                    //TODO: get bluetooth devices and place them in the combo box with their names

                    // Add Card to Secondary Auth if it has been removed
                    if (!cbxSecAuth.Items.Contains("Card"))
                    {
                        cbxSecAuth.Items.Insert(1, "Card");
                    }

                    // Swap primary and secondary auth if needed
                    if (cbxSecAuth.SelectedItem != null && cbxSecAuth.SelectedItem.ToString() == "Bluetooth")
                    {
                        cbxSecAuth.SelectedItem = "Card";
                        cbxSecAuth.Text = cbxSecAuth.SelectedItem.ToString();
                        cbxSecAuth.Update();
                    }

                    // Remove Bluetooth from Secondary Auth
                    if (cbxSecAuth.Items.Contains("Bluetooth"))
                    {
                        cbxSecAuth.Items.Remove("Bluetooth");
                    }
                }
                else if (comboBox.SelectedItem.ToString() == "Card")
                {
                    txtPrimChooseDev.Visible = false;
                    cbxBTSelect1.Visible = false;
                    errorControls.Remove(cbxBTSelect1);

                    // Add Blutetooth to Secondary Auth if it has been removed
                    if (!cbxSecAuth.Items.Contains("Bluetooth"))
                    {
                        cbxSecAuth.Items.Insert(1, "Bluetooth");
                    }

                    // Swap primary and secondary auth if needed
                    if (cbxSecAuth.SelectedItem != null && cbxSecAuth.SelectedItem.ToString() == "Card")
                    {
                        cbxSecAuth.SelectedItem = "Bluetooth";
                        cbxSecAuth.Update();
                    }

                    // Remove Card from Secondary Auth
                    if (cbxSecAuth.Items.Contains("Card"))
                    {
                        cbxSecAuth.Items.Remove("Card");
                    }
                }
            }
            OnValidate();
        }

        private void ModifySecondaryAuthConfiguration(object sender, EventArgs e)
        {
            if (sender.GetType().Name == "ComboBox")
            {
                ComboBox comboBox = (ComboBox)sender;
                ValidateComboBox(comboBox, e);
                if (comboBox.SelectedItem.ToString() == "Bluetooth")
                {
                    txtSecChooseDevOrPin.Text = "Choose device:";
                    txtSecChooseDevOrPin.Visible = true;
                    cbxBTSelect2.Visible = true;
                    tbxPin.Visible = false;
                    errorControls.Remove(tbxPin);
                    ValidateComboBox(cbxBTSelect2, EventArgs.Empty);
                }
                else if (comboBox.SelectedItem.ToString() == "PIN")
                {
                    txtSecChooseDevOrPin.Text = "Insert PIN:";
                    txtSecChooseDevOrPin.Visible = true;
                    cbxBTSelect2.Visible = false;
                    errorControls.Remove(cbxBTSelect2);
                    tbxPin.Visible = true;
                    ValidatePinBox(tbxPin, EventArgs.Empty);
                }
                else if (comboBox.SelectedItem.ToString() == "Card")
                {
                    txtSecChooseDevOrPin.Visible = false;
                    cbxBTSelect2.Visible = false;
                    errorControls.Remove(cbxBTSelect2);
                    tbxPin.Visible = false;
                    errorControls.Remove(tbxPin);
                }
            }
            OnValidate();
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

                // Close options and stop both timers. Go back to IDLE or UNINITIALIZED depending on number of users
                // registered in database after this user removal.
                StopTimer();

                // Query for number of users. If number of users is zero, go back to UNINITIALIZED
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

        protected override void OnValidate()
        {
            btnOptionsSave.Enabled = noErrors;
        }

        protected new void ValidateTextBox(object sender, EventArgs e)
            => base.ValidateTextBox(sender, e);
        protected new void ValidatePhoneBox(object sender, EventArgs e)
            => base.ValidatePhoneBox(sender, e);
        protected new void ValidatePinBox(object sender, EventArgs e)
            => base.ValidatePinBox(sender, e);
        protected new void ValidateComboBox(object sender, EventArgs e)
            => base.ValidateComboBox(sender, e);
    }

}
