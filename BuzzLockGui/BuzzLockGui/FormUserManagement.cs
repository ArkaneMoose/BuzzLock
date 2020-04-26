using BuzzLockGui.Backend;
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
        private User _selectedUser => (User)listUsers.SelectedItem;
        private User _backupUser;

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

            //foreach (Control control in Controls)
            //{
            //    control.MouseClick += OnAnyMouseClick;
            //}
        }

        public new void Show()
        {
            base.Show();
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
        }

        private void btnOptionsSave_Click(object sender, EventArgs e)
        {
            _selectedUser.Name = tbxUserName.Text;
            _selectedUser.PhoneNumber = tbxUserPhone.Text;
            
            switch (cbxUserPermission.SelectedItem.ToString())
            {
                case "FULL":
                    _selectedUser.PermissionLevel = User.PermissionLevels.FULL;
                    break;
                case "LIMITED":
                    _selectedUser.PermissionLevel = User.PermissionLevels.LIMITED;
                    break;
                default:
                    _selectedUser.PermissionLevel = User.PermissionLevels.NONE;
                    break;
            }

            populateUsers(null, EventArgs.Empty);
        }

        private void UpdateComponents()
        {
            btnAddUser.Enabled = _globalState != State.UserManagement_AddUser;
            btnRemoveUser.Enabled = _currentUser != _selectedUser;
            btnClose.Enabled = _globalState != State.UserManagement_AddUser;

            AuthenticationMethod primary = _selectedUser.AuthenticationMethods.Primary;
            AuthenticationMethod secondary = _selectedUser.AuthenticationMethods.Secondary;

            cbxPrimAuth.SelectedItem = primary.ToString();
            cbxSecAuth.SelectedItem = secondary.ToString();

            //origPrimary = primary;
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

            //origSecondary = secondary;
            switch (secondary)
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

            switch (_globalState)
            {
                case State.UserManagement:
                    txtStatus.Text = "Click on a user to see details and perform actions";
                    btnRemoveUser.Text = "Remove user";
                    tbxUserName.Text = _selectedUser.Name;
                    tbxUserPhone.Text = _selectedUser.PhoneNumber;
                    cbxUserPermission.SelectedItem = _selectedUser.PermissionLevel.ToString();
                    break;
                case State.UserManagement_AddUser:
                    txtStatus.Text = "Creating a new user:";
                    btnRemoveUser.Text = "Cancel";
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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            _globalState = State.UserManagement_AddUser;
            UpdateComponents();
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            if (_globalState == State.UserManagement)
            {
                // Initializes and displays the AutoClosingMessageBox.
                var result = AutoClosingMessageBox.Show(
                    text: "Are you sure you want to remove this user? This cannot be undone.",
                    caption: "Remove User",
                    timeout: 5000, //5 seconds
                    buttons: MessageBoxButtons.YesNo,
                    defaultResult: DialogResult.No);

                if (result == DialogResult.Yes)
                {
                    listUsers.Items.Remove(_selectedUser);
                    _selectedUser.Delete();
                    UpdateComponents();
                }
            }
            else if (_globalState == State.UserManagement_AddUser)
            {
                // TODO: cancel addition of a new user
                _globalState = State.UserManagement;
                UpdateComponents();
            }
        }

        private void listUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedUser == null) listUsers.SelectedIndex = 0;

            UpdateComponents();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _globalState = State.UserOptions;
            this.Hide();
            _formOptions.Show();
        }
    }
}
