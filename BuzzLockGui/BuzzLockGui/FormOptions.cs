﻿using BuzzLockGui.Backend;
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
        private FormUserManagement _formUserManagement;
        private Stopwatch stopWatchOptionsStatus = new Stopwatch();
        private AuthenticationMethod origPrimary;
        private AuthenticationMethod origSecondary;
        private bool newCardEntry = false;
        private string cardInput = "";


        public FormOptions(FormStart formStart)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.KeyPreview = true;

            _formStart = formStart;
            _formUserManagement = new FormUserManagement(_formStart, this);


            foreach (Control control in Controls)
            {
                if (control.Name == "btnUserManagement") break;
                control.MouseClick += OnAnyMouseClick;
            }
        }
        private void FormOptions_Load(object sender, EventArgs e)
        {
            // this.TopMost = true;
            // this.FormBorderStyle = FormBorderStyle.None;
            // this.WindowState = FormWindowState.Maximized;
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
            txtOptionsStatus.Text = "This screen will timeout in "
                + Utility.Pluralize(30 - stopWatchOptionsStatus.Elapsed.Seconds, "second") + ".";
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
                AuthenticationMethod primary = null;
                AuthenticationMethod secondary = null;
                switch (cbxPrimAuth.SelectedItem.ToString())
                {
                    case "Bluetooth":
                        primary = (BluetoothDevice)cbxBTSelect1.SelectedItem;
                        break;
                    case "Card":
                        primary = new Card(tbxCard.Text);
                        break;
                }
                switch (cbxSecAuth.SelectedItem.ToString())
                {
                    case "Card":
                        secondary = new Card(tbxCard.Text);
                        break;
                    case "Bluetooth":
                        secondary = (BluetoothDevice)cbxBTSelect2.SelectedItem;
                        break;
                    case "PIN":
                        secondary = new Pin(tbxPin.Text);
                        break;
                }
                _currentUser.AuthenticationMethods = new AuthenticationMethods(primary, secondary);

                // ResetTimer();
                _globalState = State.UserOptions;
                this.UpdateComponents();
            }
        }

        private void ResetAuthMods_Click(object sender, EventArgs e)
        {
            switch(origPrimary)
            {
                case Card card:
                    cbxPrimAuth.SelectedIndex = 0;
                    tbxCard.Text = card.Id;
                    break;
                case BluetoothDevice btDevice:
                    cbxPrimAuth.SelectedIndex = 1;
                    cbxBTSelect1.Items.Insert(0, btDevice.Address);
                    break;
            }
            switch(origSecondary)
            {
                case BluetoothDevice btDevice:
                    cbxSecAuth.SelectedIndex = 0;
                    cbxBTSelect2.Items.Insert(0, btDevice.Address);
                    break;
                case Pin pin:
                    cbxSecAuth.SelectedIndex = 1;
                    tbxPin.Text = pin.PinValue;
                    break;
            }
            loseFocus();
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            // Check to see if this is the last user with FULL permissions
            List<User> userList = User.GetAll();
            int numUsersFULLPermission = -1;
            foreach (User user in userList)
            {
                numUsersFULLPermission += 1;
            }

            string removalMsg = "Are you sure you want to remove your user? This cannot be undone.";
            if (numUsersFULLPermission == 0 && _currentUser.PermissionLevel == User.PermissionLevels.FULL)
            {
                removalMsg = "Are you sure you want to remove your user? This cannot be undone. \n\n" +
                    "You are the last user with FULL permissions. Deleting yourself will also delete any other users.";
            } 
            

            // Initializes and displays the AutoClosingMessageBox.
            var result = AutoClosingMessageBox.Show(
                text: removalMsg,
                caption: "Remove User",
                timeout: 5000, //5 seconds
                buttons: MessageBoxButtons.YesNo,
                defaultResult: DialogResult.No);

            if (result == DialogResult.Yes)
            {
                // Remove the current user from the database
                _currentUser.Delete();

                // Delete all other users if needed
                if (numUsersFULLPermission == 0) 
                {
                    foreach (User user in userList)
                    {
                        if (!user.Equals(_currentUser))
                        {
                            user.Delete();
                        }
                    }
                }

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
            loseFocus();
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

        private void FormOptions_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Console.WriteLine("Form Start Key Pressed");
            //Console.WriteLine("Pressed: " + e.KeyChar);
            //Console.WriteLine("Shift: " + shift);
            //Console.WriteLine("New Card Entry: " + newCardEntry);

            RestartTimer();

            // Disallow unwanted mag stripe interference in other states
            if (!acceptMagStripeInput) return;

            // Check KeyPressed to see if it's the beginning of a new card entry
            if (e.KeyChar == ';' || (e.KeyChar == '%'))
            {
                newCardEntry = true;
            }

            if (newCardEntry)
            {
                // Stop adding to card input string when "Return" is entered
                if (e.KeyChar == '\r') //|| e.KeyChar == '\n')
                {
                    // Check for invalid read before anything else
                    if (cardInput.Contains(";E?") || cardInput.Contains("%E?") || cardInput.Contains("+E?"))
                    {
                        // Invalid read. Swipe again.
                        newCardEntry = false;
                        cardInput = "";
                        Console.WriteLine("Invalid read. Swipe again.");
                        return;
                    }

                    if (_globalState == State.UserOptions_EditAuth)
                    {
                        // Don't allow duplicate cards in the database
                        AuthenticationSequence authSeq = AuthenticationSequence.Start(new Card(cardInput));
                        bool cardNotAlreadyInDatabase = authSeq.NextAuthenticationMethod == null;
                        if (cardNotAlreadyInDatabase)
                        {
                            tbxCard.Text = cardInput;
                        }
                    }
                    newCardEntry = false;
                    // Reset cardInput to allow for a new card swipe to be registered
                    cardInput = "";
                    return;
                }
                else
                {
                    cardInput += e.KeyChar;
                    newCardEntry = true;
                }
            }
        }

        private void UpdateComponents()
        {
            //UserOptions
            btnEditAuth.Visible = _globalState == State.UserOptions;
            btnEditProfile.Visible = _globalState == State.UserOptions;
            btnRemoveUser.Visible = _globalState == State.UserOptions;
            btnUserManagement.Visible = _globalState == State.UserOptions 
                && _currentUser.PermissionLevel == User.PermissionLevels.FULL;
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
            btnResetAuthMods.Visible = _globalState == State.UserOptions_EditAuth;
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

            //UserManagement

            // Multiple States
            acceptMagStripeInput = _globalState == State.Uninitialized
                                || _globalState == State.Initializing
                                || _globalState == State.Idle
                                || _globalState == State.UserOptions_EditAuth
                                || _globalState == State.UserManagement
                                || _globalState == State.UserManagement_AddUser;

            //Set the welcome textbox with user name and permissions
            txtStatus.Text = $"Welcome, {_currentUser.Name}. You have {_currentUser.PermissionLevel} permissions.";

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
                    origPrimary = primary;
                    switch(primary)
                    {
                        case Card card:
                            cbxPrimAuth.SelectedIndex = 0;
                            tbxCard.Text = card.Id;
                            break;
                        case BluetoothDevice btDevice:
                            cbxPrimAuth.SelectedIndex = 1;
                            cbxBTSelect1.Items.Insert(0, btDevice.Address);
                            break;
                    }
                    ModifyPrimaryAuthConfiguration(cbxPrimAuth, EventArgs.Empty);

                    AuthenticationMethod secondary = _currentUser.AuthenticationMethods.Secondary;
                    origSecondary = secondary;
                    switch(secondary)
                    {
                        case BluetoothDevice btDevice:
                            cbxSecAuth.SelectedIndex = 0;
                            cbxBTSelect2.Items.Insert(0, btDevice.Address);
                            break;
                        case Pin pin:
                            cbxSecAuth.SelectedIndex = 1;
                            tbxPin.Text = pin.PinValue;
                            break;
                    }
                    ModifySecondaryAuthConfiguration(cbxSecAuth, EventArgs.Empty);
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
            if (sender is ComboBox comboBox)
            {
                ValidateComboBox(comboBox, e);

                string selected = comboBox.SelectedItem.ToString();
                bool isCard = selected == "Card";
                bool isBluetooth = selected == "Bluetooth";

                txtCard.Visible = isCard;
                tbxCard.Visible = isCard;
                txtPrimChooseDev.Visible = isBluetooth;
                cbxBTSelect1.Visible = isBluetooth;

                if (isBluetooth)
                {
                    ValidateComboBox(cbxBTSelect1, EventArgs.Empty);
                    errorControls.Remove(tbxCard);
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
                else if (isCard)
                {
                    ValidateTextBox(tbxCard, EventArgs.Empty);
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
            if (sender is ComboBox comboBox)
            {
                ValidateComboBox(comboBox, e);

                string selected = comboBox.SelectedItem.ToString();
                bool isCard = selected == "Card";
                bool isBluetooth = selected == "Bluetooth";
                bool isPin = selected == "PIN";

                txtCard.Visible = isCard || cbxPrimAuth.SelectedItem.ToString() == "Card";
                tbxCard.Visible = isCard || cbxPrimAuth.SelectedItem.ToString() == "Card";
                txtSecChooseDevOrPin.Visible = isBluetooth || isPin;
                cbxBTSelect2.Visible = isBluetooth;
                tbxPin.Visible = isPin;

                if (isBluetooth)
                {
                    txtSecChooseDevOrPin.Text = "Choose device:";
                    if (cbxPrimAuth.SelectedItem.ToString() != "Card")
                        errorControls.Remove(tbxCard);
                    errorControls.Remove(tbxPin);
                    ValidateComboBox(cbxBTSelect2, EventArgs.Empty);
                }
                else if (isPin)
                {
                    txtSecChooseDevOrPin.Text = "Insert PIN:";
                    if (cbxPrimAuth.SelectedItem.ToString() != "Card")
                        errorControls.Remove(tbxCard);
                    errorControls.Remove(cbxBTSelect2);
                    ValidatePinBox(tbxPin, EventArgs.Empty);
                }
                else if (isCard)
                {
                    errorControls.Remove(cbxBTSelect2);
                    errorControls.Remove(tbxPin);
                    ValidateTextBox(tbxCard, EventArgs.Empty);
                }
            }
            OnValidate();
        }

        private void loseFocus()
        {
            this.ActiveControl = tbxStatus;
        }

        protected override void OnValidate()
        {
            btnOptionsSave.Enabled = NoErrors;
        }

        protected new void ValidateTextBox(object sender, EventArgs e)
            => base.ValidateTextBox(sender, e);
        protected new void ValidatePhoneBox(object sender, EventArgs e)
            => base.ValidatePhoneBox(sender, e);
        protected new void ValidatePinBox(object sender, EventArgs e)
            => base.ValidatePinBox(sender, e);
        protected new void ValidateComboBox(object sender, EventArgs e)
            => base.ValidateComboBox(sender, e);

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            _globalState = State.UserManagement;
            StopTimer();
            _formUserManagement.Show();
            this.Hide();
        }

        protected new void numberpad_Click(object sender, EventArgs e)
            => base.numberpad_Click(sender, e);
        protected new void keyboard_Click(object sender, EventArgs e)
            => base.keyboard_Click(sender, e);
        protected new void keyboardClose_Leave(object sender, EventArgs e)
            => base.keyboardClose_Leave(sender, e);
    }

}
