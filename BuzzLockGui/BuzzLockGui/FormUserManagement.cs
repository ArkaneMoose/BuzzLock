using BuzzLockGui.Backend;
using Force.DeepCloner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuzzLockGui
{
    public partial class FormUserManagement : FormBuzzLock
    {

        private FormStart _formStart;
        private FormOptions _formOptions;
        private User _selectedUser;
        private User _previousSelectedUser;
        private User _backupUser => (User)DeepClonerExtensions.DeepClone(_selectedUser);

        public FormUserManagement(FormStart formStart, FormOptions formOptions)
        {
            InitializeComponent();

            _formStart = formStart;
            _formOptions = formOptions;

            this.StartPosition = FormStartPosition.Manual;
            this.Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.KeyPreview = true;

            // Close keypad upon selection of most components
            foreach (Control control in Controls)
            {
                if (control != tbxPin && control != tbxUserPhone && control != btnClearTextBox
                    && control != tbxUserName)
                {
                    control.MouseClick += keyboardClose_Leave;
                }
            }
            this.MouseClick += keyboardClose_Leave;
        }

        private void FormUserManagement_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        public new void Show()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            base.Show();
        }

        public new void Hide()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            base.Hide();
        }

        private void formUserManagement_Activated(object sender, EventArgs e)
        {
            populateUsers(sender, e);
            UpdateComponents();
        }

        private void populateUsers(object sender, EventArgs e)
        {
            listUsers.Items.Clear();
            List<User> userList = User.GetAll();
            foreach (User user in userList)
            {
                listUsers.Items.Add(user);
                Console.WriteLine("Adding user: " + user);
            }

            // Make it so that after clicking save, it will keep the user you were editing selected
            if (_previousSelectedUser != null && listUsers.Items.Contains(_previousSelectedUser))
            {
                listUsers.SelectedItem = _previousSelectedUser;
            }
            else
            {
                listUsers.SelectedIndex = 0;
            }

            _selectedUser = (User)listUsers.SelectedItem;
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (_globalState == State.UserManagement_AddUser)
            {
                string name = tbxUserName.Text;
                string phone = tbxUserPhone.Text;
                User.PermissionLevels perm = ExtractPermissionLevel(cbxUserPermission);
                AuthenticationMethods auth = ExtractAuthenticationMethods(cbxPrimAuth, cbxSecAuth);
                _selectedUser = User.Create(name, perm, phone, null, auth);
            }
            else if (_globalState == State.UserManagement)
            {
                _selectedUser.Name = tbxUserName.Text;
                _selectedUser.PhoneNumber = tbxUserPhone.Text;
                _selectedUser.PermissionLevel = ExtractPermissionLevel(cbxUserPermission);
                _selectedUser.AuthenticationMethods = ExtractAuthenticationMethods(cbxPrimAuth, cbxSecAuth);
            }

            _globalState = State.UserManagement;
            populateUsers(null, EventArgs.Empty);
            UpdateComponents();
        }

        private User.PermissionLevels ExtractPermissionLevel(ComboBox permissionsComboBox)
        {
            switch (permissionsComboBox.SelectedItem.ToString())
            {
                case "FULL":
                     return User.PermissionLevels.FULL;
                case "LIMITED":
                    return User.PermissionLevels.LIMITED;
                default:
                    return User.PermissionLevels.NONE;
            }
        }

        private AuthenticationMethods ExtractAuthenticationMethods(ComboBox primAuth, ComboBox secAuth)
        {
            AuthenticationMethod primary = null;
            AuthenticationMethod secondary = null;
            switch (primAuth.SelectedItem.ToString())
            {
                case "Bluetooth":
                    primary = (BluetoothDevice)cbxBTSelect1.SelectedItem;
                    break;
                case "Card":
                    primary = new Card(tbxCard.Text);
                    break;
            }
            switch (secAuth.SelectedItem.ToString())
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
            return new AuthenticationMethods(primary, secondary);
        }

        private void ClearFields()
        {
            tbxCard.Text = "";
            tbxPin.Text = "";
            tbxUserName.Text = "";
            tbxUserPhone.Text = "";
        }

        private void ClearComboBoxes()
        {
            cbxPrimAuth.SelectedIndex = -1;
            cbxSecAuth.SelectedIndex = -1;
            cbxBTSelect1.SelectedIndex = -1;
            cbxBTSelect2.SelectedIndex = -1;
        }

        private void UpdateComponents()
        {
            ClearFields();

            btnAddNewUser.Enabled = _globalState != State.UserManagement_AddUser;
            btnRemoveUserOrCancel.Enabled = true;
            //btnClose.Enabled = _globalState != State.UserManagement_AddUser;

            // Multiple States
            acceptMagStripeInput = checkIfMagStripeNeeded();

            // Refresh and Update Bluetooth Lists
            RefreshBTDeviceLists(null, EventArgs.Empty);


            switch (_globalState)
            {
                case State.UserManagement:

                    _selectedUser = (User)listUsers.SelectedItem;

                    txtStatus.Text = $"Hi {_currentUser.Name}. Click on a user for details and actions.";
                    btnRemoveUserOrCancel.Text = "Remove user";
                    tbxUserName.Text = _selectedUser.Name;
                    tbxUserPhone.Text = _selectedUser.PhoneNumber;
                    cbxUserPermission.SelectedItem = _selectedUser.PermissionLevel.ToString();

                    AuthenticationMethod primary = _selectedUser.AuthenticationMethods.Primary;
                    AuthenticationMethod secondary = _selectedUser.AuthenticationMethods.Secondary;
                    cbxPrimAuth.SelectedItem = primary.ToString();
                    cbxSecAuth.SelectedItem = secondary.ToString();
                    switch (primary)
                    {
                        case Card card:
                            cbxPrimAuth.SelectedIndex = 0;
                            tbxCard.Text = card.Id;
                            break;
                        case BluetoothDevice btDevice:
                            cbxPrimAuth.SelectedIndex = 1;
                            if (!cbxBTSelect1.Items.Contains(btDevice))
                            {
                                cbxBTSelect1.Items.Insert(0, btDevice);
                                cbxBTSelect1.SelectedIndex = 0;
                                cbxBTSelect2.Items.Insert(0, btDevice);
                                cbxBTSelect2.SelectedIndex = 0;
                            }
                            break;
                    }
                    ModifyPrimaryAuthConfiguration(cbxPrimAuth, EventArgs.Empty);

                    switch (secondary)
                    {
                        case BluetoothDevice btDevice:
                            cbxSecAuth.SelectedIndex = 0;
                            if (!cbxBTSelect2.Items.Contains(btDevice))
                            {
                                cbxBTSelect1.Items.Insert(0, btDevice);
                                cbxBTSelect1.SelectedIndex = 0;
                                cbxBTSelect2.Items.Insert(0, btDevice);
                                cbxBTSelect2.SelectedIndex = 0;
                            }
                            break;
                        case Pin pin:
                            cbxSecAuth.SelectedIndex = 1;
                            tbxPin.Text = pin.PinValue;
                            break;
                    }
                    ModifySecondaryAuthConfiguration(cbxSecAuth, EventArgs.Empty);
                    break;
                case State.UserManagement_AddUser:
                    txtStatus.Text = "Creating a new user:";
                    btnRemoveUserOrCancel.Text = "Cancel";
                    ClearFields();
                    ClearComboBoxes();
                    UserInputValidation();
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

                // Null check
                if (comboBox.SelectedItem == null)
                {
                    cbxBTSelect1.Visible = false;
                    txtPrimChooseDev.Visible = false;
                    return;
                }

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
            ModifySecondaryAuthConfiguration(cbxSecAuth, EventArgs.Empty);
            OnValidate();
        }

        private void ModifySecondaryAuthConfiguration(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                ValidateComboBox(comboBox, e);

                // Null check
                if (comboBox.SelectedItem == null)
                {
                    // Reset to default
                    txtSecChooseDevOrPin.Visible = false;
                    cbxBTSelect2.Visible = false;
                    tbxPin.Visible = false;
                    ValidateComboBox(cbxBTSelect2, EventArgs.Empty);
                    ValidatePinBox(tbxPin, EventArgs.Empty);
                    return;
                }

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
                    {
                        userError.SetError(tbxCard, null);
                        errorControls.Remove(tbxCard);
                    }
                    userError.SetError(tbxPin, null);
                    errorControls.Remove(tbxPin);
                    ValidateComboBox(cbxBTSelect2, EventArgs.Empty);
                }
                else if (isPin)
                {
                    txtSecChooseDevOrPin.Text = "Insert PIN:";
                    if (cbxPrimAuth.SelectedItem.ToString() != "Card")
                    {
                        userError.SetError(cbxBTSelect2, null);
                        errorControls.Remove(tbxCard);
                    }
                    userError.SetError(cbxBTSelect2, null);
                    errorControls.Remove(cbxBTSelect2);
                    ValidatePinBox(tbxPin, EventArgs.Empty);
                }
                else if (isCard)
                {
                    userError.SetError(cbxBTSelect2, null);
                    errorControls.Remove(cbxBTSelect2);
                    userError.SetError(tbxPin, null);
                    errorControls.Remove(tbxPin);
                    ValidateTextBox(tbxCard, EventArgs.Empty);
                }
            }
            OnValidate();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            _globalState = State.UserManagement_AddUser;
            if (!listUsers.Items.Contains("New User (NONE)"))
            {
                listUsers.Items.Add("New User (NONE)"); 
            }
            listUsers.SelectedItem = "New User (NONE)";
            UpdateComponents();
        }

        private void listUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedUser == null) listUsers.SelectedIndex = 0;

            object curSelection = listUsers.SelectedItem;
            if (curSelection is User)
                _selectedUser = (User)curSelection;

            _previousSelectedUser = _selectedUser;
            UpdateComponents();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _globalState = State.Authenticated;
            this.Hide();
            _formStart.Show();
        }

        // Validation Stuff

        protected override void OnValidate() {
            btnSaveUser.Enabled = NoErrors;
        }

        protected new void ValidateTextBox(object sender, EventArgs e)
            => base.ValidateTextBox(sender, e);
        protected new void ValidatePhoneBox(object sender, EventArgs e)
            => base.ValidatePhoneBox(sender, e);
        protected new void ValidatePinBox(object sender, EventArgs e)
            => base.ValidatePinBox(sender, e);
        protected new void ValidateComboBox(object sender, EventArgs e)
            => base.ValidateComboBox(sender, e);

        // Virtual Keyboard Stuff

        protected new void keyboard_Click(object sender, EventArgs e)
        {
            base.keyboard_Click(sender, e);
            base.lastActiveTextBox = (TextBox)sender;
            btnClearTextBox.Visible = true;
        }
        protected new void keyboardClose_Leave(object sender, EventArgs e)
        {
            base.keyboardClose_Leave(sender, e);
            btnClearTextBox.Visible = false;
        }
        protected new void numberpad_Click(object sender, EventArgs e)
        {
            base.numberpad_Click(sender, e);
            base.lastActiveTextBox = (TextBox)sender;
            btnClearTextBox.Visible = true;
        }
        private void btnClearTextBox_Click(object sender, EventArgs e)
        {
            base.lastActiveTextBox.Text = "";
            this.ActiveControl = base.lastActiveTextBox;
        }

        // Bluetooth Stuff

        protected new List<BluetoothDevice> getBTDevicesInRange()
            => base.getBTDevicesInRange();

        protected new List<BluetoothDevice> getBTDevicesInRangeAndRecognized()
            => base.getBTDevicesInRangeAndRecognized();

        protected override void RefreshBTDeviceLists(object sender, EventArgs e)
        {
            // Backup user's already selected devices
            BluetoothDevice selected1 = (BluetoothDevice)cbxBTSelect1.SelectedItem;
            BluetoothDevice selected2 = (BluetoothDevice)cbxBTSelect2.SelectedItem;
            // Refresh combo box lists
            cbxBTSelect1.Items.Clear();
            cbxBTSelect2.Items.Clear();
            var inRange = getBTDevicesInRange();
            foreach (BluetoothDevice bt in inRange)
            {
                // Only add device to list if not in database
                if (AuthenticationSequence.Start(bt) == null)
                {
                    cbxBTSelect1.Items.Add(bt);
                    cbxBTSelect2.Items.Add(bt);
                    if (bt.Equals(selected1)) cbxBTSelect1.SelectedItem = selected1;
                    if (bt.Equals(selected2)) cbxBTSelect2.SelectedItem = selected2;
                }
            }
        }

        private bool newCardEntry = false;
        private string cardInput = "";
        private void FormUserManagement_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Console.WriteLine("Form Start Key Pressed");
            //Console.WriteLine("Pressed: " + e.KeyChar);
            //Console.WriteLine("Shift: " + shift);
            //Console.WriteLine("New Card Entry: " + newCardEntry);

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

                    // Don't allow duplicate cards in the database
                    AuthenticationSequence authSeq = AuthenticationSequence.Start(new Card(cardInput));
                    bool cardNotAlreadyInDatabase = authSeq == null;
                    if (cardNotAlreadyInDatabase) tbxCard.Text = cardInput;

                    Console.WriteLine(cardInput);

                    // Reset cardInput to allow for a new card swipe to be registered
                    newCardEntry = false;
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

        private void UserInputValidation()
        {
            tbxUserName.Tag = "Please enter your full name.";
            tbxUserPhone.Tag = "Please enter your phone number, no spaces or dashes.";
            cbxPrimAuth.Tag = "Please choose a primary authentication method.";
            cbxSecAuth.Tag = "Please choose a secondary authentication method.";
            cbxBTSelect1.Tag = cbxBTSelect2.Tag = "Please choose your Bluetooth device.";
            tbxPin.Tag = "Please enter a 6-digit PIN you will remember.";

            // Call these so that the red exclamations will appear immediately
            ValidateTextBox(tbxUserName, EventArgs.Empty);
            ValidatePhoneBox(tbxUserPhone, EventArgs.Empty);
            ValidateComboBox(cbxPrimAuth, EventArgs.Empty);
            ValidateComboBox(cbxSecAuth, EventArgs.Empty);
        }

        private void btnRemoveUserOrCancel_Click(object sender, EventArgs e)
        {
            if (_globalState == State.UserManagement)
            {
                List<User> userList = User.GetAll();
                int numUsersFULLPermission = -1;
                foreach (User user in userList)
                {
                    if (user.PermissionLevel == User.PermissionLevels.FULL) numUsersFULLPermission += 1;
                }

                string removalMsg = $"Are you sure you want to remove {_selectedUser.Name}? This cannot be undone.";
                if (numUsersFULLPermission == 0)
                {
                    removalMsg = $"{_selectedUser.Name} is the last user with FULL permissions. Deleting this user " +
                        $"will also delete any and all other users. Are you sure you want to proceed? " +
                        $"This cannot be undone.";
                }
                else if (_selectedUser == _currentUser)
                {
                    removalMsg = $"Do you want to delete yourself from the database? This cannot be undone. " +
                        $"Are you sure you want to proceed? \n\nWARNING: Pressing YES will close the user management " +
                        $"system, lock the door and return the system to IDLE.";
                }

                // Initializes and displays the MessageBox.
                var result = MessageBox.Show(
                    text: removalMsg,
                    caption: "Remove User",
                    buttons: MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Proceeding with User Removal

                    if (numUsersFULLPermission == 0)
                    {
                        foreach (User user in User.GetAll()) user.Delete();
                        _globalState = State.Uninitialized;
                        _currentUser = null;
                        _selectedUser = null;
                        _formStart.Show();
                        this.Hide();
                    }
                    else if (_selectedUser == _currentUser)
                    {
                        _selectedUser.Delete();
                        _globalState = State.Idle;
                        _currentUser = null;
                        _selectedUser = null;
                        _formStart.Show();
                        this.Hide();
                    } 
                    else
                    {
                        _selectedUser.Delete();
                        populateUsers(listUsers, EventArgs.Empty);
                        UpdateComponents();
                    }
                }
            }
            else if (_globalState == State.UserManagement_AddUser)
            {
                // TODO: cancel addition of a new user
                listUsers.Items.Remove("New User (NONE)");
                listUsers.SelectedIndex = 0;
                _selectedUser = (User)listUsers.SelectedItem;
                _globalState = State.UserManagement;
                UpdateComponents();
            }
        }

        private void FormUserManagement_MouseClick(object sender, MouseEventArgs e)
        {
            loseFocus();
        }

        private void loseFocus()
        {
            this.ActiveControl = txtStatus;
        }
    }
}
