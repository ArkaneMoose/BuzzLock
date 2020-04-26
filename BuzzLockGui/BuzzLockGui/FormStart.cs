using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BuzzLockGui.Backend;
using ModernMessageBoxLib;
using System.Diagnostics;
using System.Linq;
using Unosquare.WiringPi;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using System.Threading;

namespace BuzzLockGui
{
    public partial class FormStart : FormBuzzLock
    {
        private FormOptions _formOptions;
        private AuthenticationSequence _currentAuthSequence;
        private Stopwatch stopWatchAuthStatus = new Stopwatch();
        private Stopwatch stopWatchAccessDeniedStatus = new Stopwatch();
        private bool newCardEntry = false;
        private string cardInput = "";
        private bool bluetoothFound = false;
        private GpioPin servo;
        private bool lock_open = false;

        public FormStart()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.KeyPreview = true;

            _formOptions = new FormOptions(this);

            // Query database and set state
            // Check to see if there's at least one user in the database
            if (User.GetAll().Count == 0)
            {
                _globalState = State.Uninitialized;
            } else
            {
                _globalState = State.Idle;
            }

            if (IS_LINUX)
            {
                Pi.Init<BootstrapWiringPi>();
                servo = (GpioPin)Pi.Gpio[13];
                servo.PinMode = GpioPinDriveMode.Output;
                servo.StartSoftPwm(0, 100);
            }

            // Update visibility of form components
            UpdateComponents();

            foreach (Control control in Controls)
            {
                control.MouseClick += OnAnyMouseClick;
            }
        }

        private void FormStart_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Normal;
            loseFocus();
            // this.TopMost = true;
            // this.FormBorderStyle = FormBorderStyle.None;
            // this.WindowState = FormWindowState.Maximized;
        }

        public new void Show()
        {
            this.UpdateComponents();
            RestartTimer();
            RestartAccessDeniedTimer();
            base.Show();
        }

        public new void Hide()
        {
            StopTimer();
            StopAccessDeniedTimer();
            base.Hide();
        }

        private void OnAnyMouseClick(object sender, EventArgs e)
        {
            RestartTimer();
        }

        private void StopTimer()
        {
            stopWatchAuthStatus.Reset();
            timerAuthTimeout.Enabled = false;
            timerTxtAuthStatus.Enabled = false;
        }

        private void StopAccessDeniedTimer()
        {
            stopWatchAccessDeniedStatus.Reset();
            timerAccessDeniedTimeout.Enabled = false;
            timerTxtAccessDeniedStatus.Enabled = false;
        }


        private void StartTimer()
        {
            if (_globalState == State.Authenticated)
            {
                timerTxtAuthStatus_Tick(timerTxtAuthStatus, EventArgs.Empty);
                stopWatchAuthStatus.Start();
                timerAuthTimeout.Enabled = true;
                timerTxtAuthStatus.Enabled = true;
            }
        }

        private void StartAccessDeniedTimer()
        {
            if (_globalState == State.AccessDenied)
            {
                timerTxtAccessDeniedStatus_Tick(timerTxtAccessDeniedStatus, EventArgs.Empty);
                stopWatchAccessDeniedStatus.Start();
                timerAccessDeniedTimeout.Enabled = true;
                timerTxtAccessDeniedStatus.Enabled = true;
            }
        }

        private void RestartTimer()
        {
            StopTimer();
            StartTimer();
        }

        private void RestartAccessDeniedTimer()
        {
            StopAccessDeniedTimer();
            StartAccessDeniedTimer();
        }


        private void btnOptionsSave_Click(object sender, EventArgs e)
        {
            if (_globalState == State.Initializing)
            {
                // TODO: Add capability for additional users with User.PermissionLevels = NONE

                // Save user preferences and information to database
                string name = tbxUserName.Text;
                string phoneNumber = tbxUserPhone.Text;
                User.PermissionLevels permissionLevel = (User.GetAll().Count() == 0) ? User.PermissionLevels.FULL : User.PermissionLevels.NONE;
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
                AuthenticationMethods authenticationMethods = new AuthenticationMethods(primary, secondary);
                User.Create(name, permissionLevel, phoneNumber, null, authenticationMethods);

                // Go to idle state
                _globalState = State.Idle;
                UpdateComponents();

            }
            else if (_globalState == State.SecondFactor)
            {

                bool success = false;
                switch (_currentAuthSequence.NextAuthenticationMethod)
                {
                    case Card card:
                        success = _currentAuthSequence.Continue(new Card(tbxSecFactorPinOrCard.Text));
                        break;
                    case Pin pin:
                        success = _currentAuthSequence.Continue(new Pin(tbxSecFactorPinOrCard.Text));
                        break;
                    case BluetoothDevice btDevice:
                        success = bluetoothFound && _currentAuthSequence.Continue(btDevice);
                        break;
                    default:
                        break;
                }
                
                if (success && _currentAuthSequence.NextAuthenticationMethod == null)
                {
                    // Successful Authentication
                    _globalState = State.Authenticated;
                    _currentUser = _currentAuthSequence.User;
                    UpdateComponents();
                } 
                else
                {
                    txtSecondFactorStatus.Visible = true;
                    txtSecondFactorStatus.Text = "Secondary authentication failed. Please try again.";
                    tbxSecFactorPinOrCard.Text = "";
                    cardInput = ""; // just in case
                }

                //bool primaryAuthenticated = false;
                //AuthenticationMethod primary = _currentAuthSequence.User.AuthenticationMethods.Primary;
                //switch (primary)
                //{
                //    case Card card:
                //        if (tbxCard.Text == card.Id)
                //        {
                //            primaryAuthenticated = true;
                //        }
                //        break;
                //    case BluetoothDevice btDevice:
                //        if (cbxBTSelect1.SelectedItem.ToString() == btDevice.Address)
                //        {
                //            primaryAuthenticated = true;
                //        }
                //        break;
                //}

                //bool secondaryAuthenticated = false;
                //AuthenticationMethod secondary = _currentAuthSequence.User.AuthenticationMethods.Secondary;
                //switch (secondary)
                //{
                //    case BluetoothDevice btDevice:
                //        if (cbxBTSelect2.SelectedItem.ToString() == btDevice.Address)
                //        {
                //            secondaryAuthenticated = true;
                //        }
                //        break;
                //    case Pin pin:
                //        if (tbxPin.Text == pin.PinValue)
                //        {
                //            secondaryAuthenticated = true;
                //        }
                //        break;
                //}

                //if (primaryAuthenticated && secondaryAuthenticated)
                //{
                //    // Succesful authentication
                //    _globalState = State.Authenticated;
                //    _currentUser = _currentAuthSequence.User;
                //    UpdateComponents();
                //}
                //else
                //{
                //    txtSecondFactorStatus.Visible = true;
                //    if (!primaryAuthenticated && !secondaryAuthenticated)
                //    {
                //        txtSecondFactorStatus.Text = "Both primary and secondary authentication failed. Please try again.";
                //    }
                //    else if (!primaryAuthenticated)
                //    {
                //        txtSecondFactorStatus.Text = "Primary authentication failed. Please try again.";
                //    }
                //    else if (!secondaryAuthenticated)
                //    {
                //        txtSecondFactorStatus.Text = "Secondary authentication failed. Please try again.";
                //    }
                //}
            }
            else if (_globalState == State.Authenticated)
            {
                _globalState = State.UserOptions;
                UpdateComponents();
                _formOptions.Show();
                this.Hide();
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

            errNewUser.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            // Call these so that the red exclamations will appear immediately
            ValidateTextBox(tbxUserName, EventArgs.Empty);
            ValidatePhoneBox(tbxUserPhone, EventArgs.Empty);
            ValidateComboBox(cbxPrimAuth, EventArgs.Empty);
        }

        private void SetupPrimaryAuthConfiguration(object sender, EventArgs e)
        {
            // Type check to ensure passed in object is a ComboBox
            if (sender is ComboBox comboBox)
            {
                // Call error provider validation
                ValidateComboBox(comboBox, e);

                // Null check
                if (comboBox.SelectedItem == null)
                {
                    return;
                }
                
                // If an item is selected, then we need to show the second authentication box
                txtSecAuth.Visible = true;
                cbxSecAuth.Visible = true;
                ValidateComboBox(cbxSecAuth, EventArgs.Empty);

                if (comboBox.SelectedItem.ToString() == "Bluetooth")
                {
                    // Show the select bluetooth device combo box
                    //TODO: get bluetooth devices and place them in the combo box with their names
                    txtPrimChooseDev.Visible = true;
                    cbxBTSelect1.Visible = true;
                    ValidateComboBox(cbxBTSelect1, EventArgs.Empty);

                    // Add Card to Secondary Auth if it has been removed
                    if (!cbxSecAuth.Items.Contains("Card"))
                    {
                        cbxSecAuth.Items.Insert(1, "Card");
                    }

                    // Dissalow selecting bluetooth for both primary and secondary authentication
                    if (cbxSecAuth.SelectedItem != null && cbxSecAuth.SelectedItem.ToString() == "Bluetooth")
                    {
                        SetupSecondaryAuthConfiguration(cbxSecAuth, EventArgs.Empty);
                    }

                    // Remove Bluetooth from Secondary Auth
                    if (cbxSecAuth.Items.Contains("Bluetooth"))
                    {
                        cbxSecAuth.Items.Remove("Bluetooth");
                    }
                }
                else if (comboBox.SelectedItem.ToString() == "Card")
                {
                    // Hide the select bluetooth device combobox
                    txtPrimChooseDev.Visible = false;
                    cbxBTSelect1.Visible = false;
                    ValidateComboBox(cbxBTSelect1, EventArgs.Empty);

                    // Add Blutetooth to Secondary Auth if it has been removed
                    if (!cbxSecAuth.Items.Contains("Bluetooth"))
                    {
                        cbxSecAuth.Items.Insert(1, "Bluetooth");
                    }

                    // Dissalow selecting card for both primary and secondary authentication
                    if (cbxSecAuth.SelectedItem != null && cbxSecAuth.SelectedItem.ToString() == "Card")
                    {
                        SetupSecondaryAuthConfiguration(cbxSecAuth, EventArgs.Empty);
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

        private void SetupSecondaryAuthConfiguration(object sender, EventArgs e)
        {
            // Type check to ensure passed in object is a ComboBox
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

                if (comboBox.SelectedItem.ToString() == "Bluetooth")
                {
                    // Hide pinBox just in case it was visible
                    tbxPin.Visible = false;
                    ValidatePinBox(tbxPin, EventArgs.Empty);

                    // Show bluetooth combo box
                    txtSecChooseDevOrPin.Text = "Choose device:";
                    txtSecChooseDevOrPin.Visible = true;
                    cbxBTSelect2.Visible = true;
                    ValidateComboBox(cbxBTSelect2, EventArgs.Empty);
                }
                else if (comboBox.SelectedItem.ToString() == "PIN")
                {
                    // Hide bluetooth combo box just in case it was visible
                    cbxBTSelect2.Visible = false;
                    ValidateComboBox(cbxBTSelect2, EventArgs.Empty);

                    // Show pin box
                    txtSecChooseDevOrPin.Text = "Insert PIN:";
                    txtSecChooseDevOrPin.Visible = true;
                    tbxPin.Visible = true;
                    ValidatePinBox(tbxPin, EventArgs.Empty);
                }
                else if (comboBox.SelectedItem.ToString() == "Card")
                {
                    // Hide bluetooth and pin boxes just in case they were visible
                    txtSecChooseDevOrPin.Visible = false;
                    cbxBTSelect2.Visible = false;
                    tbxPin.Visible = false;
                    ValidateComboBox(cbxBTSelect2, EventArgs.Empty);
                    ValidatePinBox(tbxPin, EventArgs.Empty);
                }
            }
            OnValidate();
        }

        private void btnDebugSwipe_Click(object sender, EventArgs e)
        {
            tbxCard.Text = "Test Card for people without card swipers";
            if (_globalState == State.Uninitialized)
            {
                _globalState = State.Initializing;
                UpdateComponents();

                // Open initial setup page
            }
            else if (_globalState == State.Idle)
            {
                // Test authentication
                _globalState = State.SecondFactor;
                _currentUser = new User(1);
                UpdateComponents();
            }
        }

        //TODO: Find out a way to make this private, like placing this functions inside of BuzzLock class instead
        public void UpdateComponents()
        {
            // Reset card input buffer when we change state. This intends to avoid things the user types
            // from incorrectly getting added to the start of a card string, which was happening. 
            cardInput = "";

            // Initializing State
            txtCard.Visible = _globalState == State.Initializing;
            tbxCard.Visible = _globalState == State.Initializing;
            txtUserName.Visible = _globalState == State.Initializing;
            tbxUserName.Visible = _globalState == State.Initializing;
            txtUserPhone.Visible = _globalState == State.Initializing;
            tbxUserPhone.Visible = _globalState == State.Initializing;
            txtPrimAuth.Visible = _globalState == State.Initializing;
            cbxPrimAuth.Visible = _globalState == State.Initializing;
            txtPrimChooseDev.Visible = false;
            cbxBTSelect1.Visible = false;
            txtSecAuth.Visible = _globalState == State.Initializing;
            cbxSecAuth.Visible = _globalState == State.Initializing;
            txtSecChooseDevOrPin.Visible = false;
            tbxPin.Visible = false;
            cbxBTSelect2.Visible = false;

            // Idle State
            btnDebugAuthUser.Enabled = (_globalState == State.Idle);
            txtDate.Visible = _globalState == State.Idle;
            txtTime.Visible = _globalState == State.Idle;
            listIdleBTDevices.Visible = _globalState == State.Idle;
            btnConfirmBTDevices.Visible = _globalState == State.Idle;
            txtChooseBTDevice.Visible = _globalState == State.Idle;

            // Second Factor State
            txtSecondFactorStatus.Visible = false;
            txtSecFactorPinOrCard.Visible = _globalState == State.SecondFactor;
            tbxSecFactorPinOrCard.Visible = _globalState == State.SecondFactor;

            // Authenticated State
            txtAuthStatus.Visible = _globalState == State.Authenticated;
            timerAuthTimeout.Enabled = _globalState == State.Authenticated;
            timerTxtAuthStatus.Enabled = _globalState == State.Authenticated;

            // AccessDenied State
            timerAccessDeniedTimeout.Enabled = _globalState == State.AccessDenied;
            timerTxtAccessDeniedStatus.Enabled = _globalState == State.AccessDenied;

            // Multiple States
            btnOptionsSave.Visible = _globalState == State.Initializing 
                                  || _globalState == State.SecondFactor 
                                  || _globalState == State.Authenticated;
            acceptMagStripeInput = _globalState == State.Uninitialized
                                || _globalState == State.Initializing
                                || _globalState == State.Idle 
                                || _globalState == State.UserOptions_EditAuth;

            switch (_globalState)
            {
                case State.Uninitialized:
                    _currentUser = null;
                    bluetoothFound = false;
                    // Message for tbxStatus.Text for when the system is uninitialized
                    txtStatus.Text = "Hello! Please swipe your BuzzCard to begin set up.";
                    break;
                case State.Initializing:
                    btnOptionsSave.Text = "Save";
                    txtStatus.Text = "Create your profile and choose how you want to unlock the door:";
                    UserInputValidation();

                    // Update BT Devices list
                    RefreshBTDeviceLists(timerBTIdleBTDeviceListUpdate, EventArgs.Empty);

                    //reset boxes
                    tbxPin.Text = "";
                    tbxUserName.Text = "";
                    tbxUserPhone.Text = "";
                    cbxBTSelect1.Items.Clear();
                    cbxBTSelect2.Items.Clear();
                    cbxPrimAuth.SelectedItem = null;
                    cbxSecAuth.SelectedItem = null;

                    break;
                case State.Idle:

                    // Date and Time
                    timerDateTime_Tick(timerDateTime, EventArgs.Empty);

                    // Update BT Devices list
                    RefreshBTDeviceLists(timerBTIdleBTDeviceListUpdate, EventArgs.Empty);

                    // Reset _currentUser since we are in Idle State
                    _currentUser = null;
                    bluetoothFound = false;

                    btnOptionsSave.Text = "Options";
                    txtStatus.Text = "Hello! Please swipe your card or choose your device.";
                    close_lock();
                    loseFocus();
                    enableBtnConfirmBTDevice(listIdleBTDevices, EventArgs.Empty);
                    break;
                case State.SecondFactor:

                    txtStatus.Text = "Please enter your second factor to authenticate.";
                    btnOptionsSave.Text = "Authenticate";
                    tbxSecFactorPinOrCard.Text = "";
                    AuthenticationMethod next = _currentAuthSequence.NextAuthenticationMethod;
                    tbxSecFactorPinOrCard.ReadOnly = next is Card;
                    acceptMagStripeInput = next is Card;
                    switch (next)
                    {
                        case Card card:
                            txtSecFactorPinOrCard.Text = "Please swipe your card.";
                            tbxSecFactorPinOrCard.Size = tbxSecFactorPinOrCard.MaximumSize;
                            tbxSecFactorPinOrCard.MaxLength = 32767; //default
                            tbxSecFactorPinOrCard.TextChanged -= new System.EventHandler(ValidatePinBox);
                            break;
                        case Pin pin:
                            // Request for the user to enter their PIN.
                            txtSecFactorPinOrCard.Text = "Please insert your PIN:";
                            tbxSecFactorPinOrCard.Size = tbxSecFactorPinOrCard.MinimumSize;
                            tbxSecFactorPinOrCard.MaxLength = 6;
                            tbxSecFactorPinOrCard.TextChanged += new System.EventHandler(ValidatePinBox);
                            ValidatePinBox(tbxSecFactorPinOrCard, EventArgs.Empty);
                            break;
                        case BluetoothDevice btDevice:
                            tbxSecFactorPinOrCard.Visible = false;
                            txtSecFactorPinOrCard.Visible = false;
                            List<BluetoothDevice> btDevicesInRange = getBTDevicesInRange();
                            if (btDevicesInRange.Contains(btDevice))
                            {
                                bluetoothFound = true;
                            }
                            else
                            {
                                bluetoothFound = false;
                            }
                            btnOptionsSave_Click(btnOptionsSave, EventArgs.Empty);
                            break;
                    }

                    //switch(primary)
                    //{
                    //    case Card card:
                    //        cbxPrimAuth.Items.Insert(0, "Card");
                    //        txtCard.Visible = true;
                    //        tbxCard.Visible = true;
                    //        tbxCard.Text = card.Id;
                    //        break;
                    //    case BluetoothDevice btDevice:
                    //        cbxPrimAuth.Items.Insert(0, "Bluetooth");
                    //        txtPrimChooseDev.Visible = true;
                    //        cbxBTSelect1.Visible = true;
                    //        // TODO: Populate with all bluetooth devices associated with this user
                    //        cbxBTSelect1.Items.Insert(0, btDevice.Address);
                    //        cbxBTSelect1.SelectedIndex = 0;
                    //        break;
                    //}
                    //cbxPrimAuth.SelectedIndex = 0;

                    //cbxSecAuth.Items.Clear();
                    //AuthenticationMethod secondary = _currentAuthSequence.User.AuthenticationMethods.Secondary;
                    //txtSecChooseDevOrPin.Visible = true;
                    //switch(secondary)
                    //{
                    //    case BluetoothDevice btDevice:
                    //        cbxSecAuth.Items.Insert(0, "Bluetooth");
                    //        txtSecChooseDevOrPin.Text = "Choose device: ";
                    //        cbxBTSelect2.Visible = true;
                    //        // TODO: Populate with all bluetooth devices associated with this user
                    //        cbxBTSelect2.Items.Insert(0, btDevice.Address);
                    //        break;
                    //    case Pin pin:
                    //        cbxSecAuth.Items.Insert(0, "PIN");
                    //        txtSecChooseDevOrPin.Text = "Insert PIN:";
                    //        tbxPin.Visible = true;
                    //        break;
                    //}
                    //cbxSecAuth.SelectedIndex = 0;
                    break;
                case State.Authenticated:
                    btnOptionsSave.Text = "Options";

                    txtStatus.Text = $"Welcome, {_currentUser.Name}. Door is unlocked.";

                    // Timeout stopwatch
                    txtAuthStatus.Text = "If you wish to edit your account, click Options. Otherwise, this screen will timeout in 10 seconds.";
                    stopWatchAuthStatus.Reset();
                    stopWatchAuthStatus.Start();
                    open_lock();
                    loseFocus();
                    break;
                case State.AccessDenied:
                    //Timeout stopwatch
                    stopWatchAccessDeniedStatus.Reset();
                    stopWatchAccessDeniedStatus.Start();
                    loseFocus();
                    break;
                default:
                    break;
            }
        }

        private void loseFocus()
        {
            this.ActiveControl = txtStatus;
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToShortTimeString();
            txtDate.Text = DateTime.Now.ToLongDateString();
        }

        private void timeoutAuth_Tick(object sender, EventArgs e)
        {
            stopWatchAuthStatus.Reset();
            _globalState = State.Idle;
            UpdateComponents();
        }

        private void timeoutAccessDenied_Tick(object sender, EventArgs e)
        {
            stopWatchAccessDeniedStatus.Reset();
            _globalState = State.Idle;
            UpdateComponents();
        }

        private void timerTxtAuthStatus_Tick(object sender, EventArgs e)
        {
            txtAuthStatus.Text = "If you wish to edit your account, click Options."
                    + "Otherwise, this screen will timeout in "
                    + Utility.Pluralize((10 - stopWatchAuthStatus.Elapsed.Seconds), "second") + ".";
        }

        private void timerTxtAccessDeniedStatus_Tick(object sender, EventArgs e)
        {
            txtStatus.Text = "Your access was denied, you may try again in "
                    + Utility.Pluralize((10 - stopWatchAccessDeniedStatus.Elapsed.Seconds), "second") + ".";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void FormStart_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Form Start Clicked");
            // If the user clicks on the form, then active control leaves whatever it was and goes to default
            loseFocus();
            RestartTimer();
        }

        private void FormStart_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form Start Activated");
        }

        private void btnDebugAuthUser_Click(object sender, EventArgs e)
        {
            _globalState = State.Authenticated;
            _currentUser = new User(1);
            UpdateComponents();
        }

        private void btnDebugAccessDenied_Click(object sender, EventArgs e)
        {
            _globalState = State.AccessDenied;
            _currentUser = new User(1);
            UpdateComponents();
        }

        private void FormStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("Form Start Key Pressed");
            Console.WriteLine("Pressed: " + e.KeyChar);
            //Console.WriteLine("Shift: " + shift);
            //Console.WriteLine("New Card Entry: " + newCardEntry);

            RestartTimer();
            RestartAccessDeniedTimer(); //??
            
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
                if (e.KeyChar == '\r' || e.KeyChar == '\n')
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

                    Console.WriteLine(cardInput);

                    // Check database to see if this is a new card entry or a recognized card associated with a user.
                    _currentAuthSequence = AuthenticationSequence.Start(new Card(cardInput));
                    bool cardRecognized = (_currentAuthSequence != null);

                    if (!cardRecognized)
                    {
                        // this is a new card
                        tbxCard.Text = cardInput;
                        if (_globalState != State.Initializing)
                        {
                            _globalState = State.Initializing;
                            UpdateComponents();
                        }
                    }
                    else
                    {
                        // this card is already in the database
                        if (_globalState == State.Initializing)
                        {
                            // Error: Two separate users cannot use the same card for authentication
                            // TODO: Set Validation of some kind telling them to swipe a different card
                        }
                        else if (_currentAuthSequence.User.PermissionLevel == User.PermissionLevels.NONE)
                        {
                            // user does not have door unlocking permission
                            _globalState = State.AccessDenied;
                            UpdateComponents();
                        }
                        else
                        {
                            // request second factor authentication
                            _globalState = State.SecondFactor;
                            UpdateComponents();
                        }
                    }
                    

                    // Reset cardInput to allow for a new card swipe to be registered
                    cardInput = "";
                }
                else
                {
                    cardInput += e.KeyChar;
                    newCardEntry = true;
                }
            }
        }

        private void enableBtnConfirmBTDevice(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            btnConfirmBTDevices.Enabled = listBox.Items.Count > 0;
        }

        private void btnDebugBluetooth_Click(object sender, EventArgs e)
        {
            var btDevicesInRange = getBTDevicesInRange();
            BluetoothDevice device = btDevicesInRange.First();
            if (_globalState == State.Initializing)
            {
                cbxBTSelect1.Items.Add(device);
                cbxBTSelect2.Items.Add(device);
            }
            else
            {
                listIdleBTDevices.Items.Add(device);
            }
            
        }

        private void btnConfirmBTDevices_Click(object sender, EventArgs e)
        {
            BluetoothDevice selectedBTDevice = (BluetoothDevice) listIdleBTDevices.SelectedItem;
            _currentAuthSequence = AuthenticationSequence.Start(selectedBTDevice);
            bool bluetoothRecognized = (_currentAuthSequence != null);

            if (!bluetoothRecognized)
            {
                // this is a new BT device
                _globalState = State.Initializing;
            }
            else if(_currentAuthSequence.User.PermissionLevel == User.PermissionLevels.NONE)
            {
                // user does not have door unlocking permission
                _globalState = State.AccessDenied;
            }
            else
            {
                // request second factor authentication
                _globalState = State.SecondFactor;
            }
            UpdateComponents();
        }

        //rotate the servo
        private void open_lock()
        {
            if (IS_LINUX && !lock_open)
            {
                servo.SoftPwmValue = 25;
                Thread.Sleep(850);
                servo.SoftPwmValue = 0;
                lock_open = true;

            }
        }

        private void close_lock()
        {
            if(IS_LINUX && lock_open)
            {
                servo.SoftPwmValue = 5;
                Thread.Sleep(850);
                servo.SoftPwmValue = 0;
                lock_open = false;
            }
        }

        private void btnDebugSecondFactor_Click(object sender, EventArgs e)
        {
            _globalState = State.SecondFactor;
            _currentUser = new User(1);
            UpdateComponents();
        }

        protected override void OnValidate()
        {
            btnOptionsSave.Enabled = NoErrors;
        }

        // For debugging bluetooth authentication
        private List<BluetoothDevice> getBTDevicesInRange()
        {
            List<BluetoothDevice> dummyList = new List<BluetoothDevice>
            {
                new BluetoothDevice(new BluetoothAddress("00:11:22:33:44:55"), "Andrew's iPhone")
            };
            return dummyList;
        }

        private List<BluetoothDevice> getBTDevicesInRangeAndRecognized()
        {
            List<BluetoothDevice> inRange = getBTDevicesInRange();
            List<BluetoothDevice> inRangeAndRecognized = new List<BluetoothDevice>();
            foreach (BluetoothDevice bt in inRange)
            {
                if (AuthenticationSequence.Start(bt) != null) inRangeAndRecognized.Add(bt);
            }
            return inRangeAndRecognized;
        }

        protected new void ValidateTextBox(object sender, EventArgs e)
            => base.ValidateTextBox(sender, e);
        protected new void ValidatePhoneBox(object sender, EventArgs e)
            => base.ValidatePhoneBox(sender, e);
        protected new void ValidatePinBox(object sender, EventArgs e)
            => base.ValidatePinBox(sender, e);
        protected new void ValidateComboBox(object sender, EventArgs e)
            => base.ValidateComboBox(sender, e);
        protected new void keyboard_Click(object sender, EventArgs e)
            => base.keyboard_Click(sender, e);
        protected new void keyboardClose_Leave(object sender, EventArgs e)
            => base.keyboardClose_Leave(sender, e);
        protected new void numberpad_Click(object sender, EventArgs e)
            => base.numberpad_Click(sender, e);

        private void RefreshBTDeviceLists(object sender, EventArgs e)
        {
            
            switch (_globalState)
            {
                case State.Initializing:
                    // Backup user's already selected devices
                    BluetoothDevice selected1 = (BluetoothDevice)cbxBTSelect1.SelectedItem;
                    BluetoothDevice selected2 = (BluetoothDevice)cbxBTSelect2.SelectedItem;
                    // Refresh combo box lists
                    cbxBTSelect1.Items.Clear();
                    cbxBTSelect2.Items.Clear();
                    var inRange = getBTDevicesInRange();
                    foreach (BluetoothDevice bt in inRange)
                    {
                        cbxBTSelect1.Items.Add(bt);
                        cbxBTSelect2.Items.Add(bt);
                        if (bt.Equals(selected1)) cbxBTSelect1.SelectedItem = selected1;
                        if (bt.Equals(selected2)) cbxBTSelect2.SelectedItem = selected2;
                    }
                    break;
                case State.Idle:
                    listIdleBTDevices.Items.Clear();
                    var inRangeAndRecognized = getBTDevicesInRangeAndRecognized();
                    foreach (BluetoothDevice bt in inRangeAndRecognized)
                    {
                        listIdleBTDevices.Items.Add(bt);
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IS_LINUX)
            {
                servo.SoftPwmValue = 25;
                Thread.Sleep(850);
                servo.SoftPwmValue = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IS_LINUX)
            {
                servo.SoftPwmValue = 5;
                Thread.Sleep(850);
                servo.SoftPwmValue = 0;
            }
        }
    }
}
