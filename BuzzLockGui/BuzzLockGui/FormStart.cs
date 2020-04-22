using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BuzzLockGui
{
    //TODO: currently, State and _state are public. Place them in a BuzzLock class, 
    //      and make _formOptions and _formStart instance variables of that class instead.
    public enum State
    {
        Uninitialized,
        Initializing,
        Idle,
        GenericOptions,
        Authenticated, //TODO: spin servo to unlock door in Authenticated, and keep it unlocked during UserOptions
        UserOptions,    //      and in Authenticated until timeout happens.
        UserOptions_EditProfile,
        UserOptions_EditAuth
    }

    // reading card swipe, verification for combo box authentication

    public partial class FormStart : Form
    {
        private FormStart _formStart;
        private FormOptions _formOptions;
        public State _state;
        public int keyboard_on = 0; //1 means on 0 means off

        public FormStart()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            this.KeyPreview = true;

            _formStart = this;
            _formOptions = new FormOptions(this);

            // Query database and set state
            _state = State.Uninitialized;

            // Update visibility of form components
            UpdateComponents();
        }

        private void FormStart_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Normal;
            loseFocus(); 
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
        }

        private void btnOptionsSave_Click(object sender, EventArgs e)
        {
            if (_state == State.Initializing)
            {
                //TODO: Save initial user preferences to database

                // Go to idle state
                _state = State.Idle;
                UpdateComponents();

            }
            else if (_state == State.Idle)
            {
                //TODO: Show generic options form

                //_state = State.GenericOptions;
                
            }
            else if (_state == State.Authenticated)
            {
                _state = State.UserOptions;
                UpdateComponents();

                // Update FormOptions Components
                _formOptions.timerOptionsTimeout.Enabled = true;
                _formOptions.timerOptionsStatus.Enabled = true;
                _formOptions.stopWatchOptionsStatus.Start();

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
            //TODO: ValidatePhoneBox
            ValidateTextBox(tbxUserPhone, EventArgs.Empty);
            ValidateComboBox(cbxPrimAuth, EventArgs.Empty);
        }

        private HashSet<Control> errorControls = new HashSet<Control>();

        private void ValidateTextBox(object sender, EventArgs e)
        {
            // Type check to ensure passed in object is a TextBox
            if (sender.GetType().Name == "TextBox") {
                TextBox textBox = (TextBox) sender;
                if (textBox.Text == "")
                {
                    errNewUser.SetError(textBox, (string)textBox.Tag);
                    errorControls.Add(textBox);
                }
                else
                {
                    errNewUser.SetError(textBox, null);
                    errorControls.Remove(textBox);
                }
            }
            btnOptionsSave.Enabled = errorControls.Count == 0;
        }

        private void ValidatePinBox(object sender, EventArgs e)
        {
            // Type check to ensure passed in object is a TextBox
            if (sender.GetType().Name == "TextBox")
            {
                TextBox textBox = (TextBox) sender;
                string errorMessage;
                if (!ValidPin(textBox.Text, out errorMessage))
                {
                    errNewUser.SetError(textBox, errorMessage);
                    errorControls.Add(textBox);
                }
                else
                {
                    errNewUser.SetError(textBox, null);
                    errorControls.Remove(textBox);
                }
            }
            btnOptionsSave.Enabled = errorControls.Count == 0;
        }

        private bool ValidPin(string pin, out string errorMessage)
        {
            // Confirm that the pin is the correct length
            if (pin.Length < 6)
            {
                errorMessage = "PIN must be exactly 6 digits long.";
                return false;
            }

            // Confirm that the pin contains only numbers
            if (!Regex.IsMatch(pin, @"^\d+$"))
            {
                errorMessage = "PIN must be all numbers.";
                return false;
            }

            errorMessage = "";
            return true;
        }

        private void ValidateComboBox(object sender, EventArgs e)
        {
            // Type check to ensure passed in object is a ComboBox
            if (sender.GetType().Name == "ComboBox")
            {
                ComboBox comboBox = (ComboBox) sender;
                if (comboBox.SelectedItem == null)
                {
                    errNewUser.SetError(comboBox, (string)comboBox.Tag);
                    errorControls.Add(comboBox);
                }
                else
                {
                    errNewUser.SetError(comboBox, null);
                    errorControls.Remove(comboBox);
                }
            }
            btnOptionsSave.Enabled = errorControls.Count == 0;
        }

        private void SetupPrimaryAuthConfiguration(object sender, EventArgs e)
        {
            ValidateComboBox(sender, e);
            var comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                txtSecAuth.Visible = true;
                cbxSecAuth.Visible = true;
                ValidateComboBox(cbxSecAuth, EventArgs.Empty);
            }
            if (comboBox.SelectedItem.ToString() == "Bluetooth")
            {
                txtPrimChooseDev.Visible = true;
                cbxBTSelect1.Visible = true;
                ValidateComboBox(cbxBTSelect1, EventArgs.Empty);
                //TODO: get bluetooth devices and place them in the combo box with their names

                // Add Card to Secondary Auth if it has been removed
                if (!cbxSecAuth.Items.Contains("Card"))
                {
                    cbxSecAuth.Items.Insert(0, "Card");
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

        private void SetupSecondaryAuthConfiguration(object sender, EventArgs e)
        {
            ValidateComboBox(sender, e);
            var comboBox = sender as ComboBox;
            if (comboBox.SelectedItem.ToString() == "Bluetooth")
            {
                txtSecChooseDevOrPin.Text = "Choose device:";
                txtSecChooseDevOrPin.Visible = true;
                cbxBTSelect2.Visible = true;
                tbxPin.Visible = false;
                errorControls.Remove(tbxPin);
                ValidateComboBox(cbxBTSelect2, EventArgs.Empty);
                //// if Bluetooth is selected as primary, make primary Card instead
                //if (cbxPrimAuth.SelectedItem.ToString() == "Bluetooth")
                //{
                //    cbxPrimAuth.SelectedItem = "Card";
                //    cbxPrimAuth.Update();
                //}
            }
            else if (comboBox.SelectedItem.ToString() == "PIN")
            {
                //TODO: Limit number of characters to 4 or 6 for PIN w/ validation
                txtSecChooseDevOrPin.Text = "Insert PIN:";
                txtSecChooseDevOrPin.Visible = true;
                cbxBTSelect2.Visible = false;
                errorControls.Remove(cbxBTSelect2);
                tbxPin.Visible = true;
                ValidateTextBox(tbxPin, EventArgs.Empty);
            }
            else if (comboBox.SelectedItem.ToString() == "Card")
            {
                txtSecChooseDevOrPin.Visible = false;
                cbxBTSelect2.Visible = false;
                errorControls.Remove(cbxBTSelect2);
                tbxPin.Visible = false;
                errorControls.Remove(tbxPin);

                //// if Card is selected as primary, make primary bluetooth instead
                //if (cbxPrimAuth.SelectedItem.ToString() == "Card")
                //{
                //    cbxPrimAuth.SelectedItem = "Bluetooth";
                //    cbxPrimAuth.Update();
                //}
            }
        }

        //TODO: Auto detect mag stripe (keyboard) input that looks like a card
        //See idle state to-do
        private void btnDebugSwipe_Click(object sender, EventArgs e)
        {
            if (_state == 0)
            {
                _state = State.Initializing;
                UpdateComponents();

                // Open initial setup page
                tbxCard.Text = "Test Card for people without card swipers";

            }
            else
            {
                // Test authentication


            }
        }

    private Stopwatch stopWatchAuthStatus = new Stopwatch();
      
//TODO: Find out a way to make this private, like placing this functions inside of BuzzLock class instead
public void UpdateComponents()
        {
            // Initializing State
            txtCard.Visible = _state == State.Initializing;
            tbxCard.Visible = _state == State.Initializing;
            txtUserName.Visible = _state == State.Initializing;
            tbxUserName.Visible = _state == State.Initializing;
            txtUserPhone.Visible = _state == State.Initializing;
            tbxUserPhone.Visible = _state == State.Initializing;
            txtPrimAuth.Visible = _state == State.Initializing;
            cbxPrimAuth.Visible = _state == State.Initializing;
            txtPrimChooseDev.Visible = false;
            cbxBTSelect1.Visible = false;
            txtSecAuth.Visible = false;
            cbxSecAuth.Visible = false;
            txtSecChooseDevOrPin.Visible = false;
            tbxPin.Visible = false;
            cbxBTSelect2.Visible = false;
            timerDateTime.Enabled = _state == State.Initializing;

            // Idle State
            btnDebugAuthUser.Enabled = (_state == State.Idle);
            txtDate.Visible = _state == State.Idle;
            txtTime.Visible = _state == State.Idle;
            listIdleBTDevices.Visible = _state == State.Idle;
            btnConfirmBTDevices.Visible = _state == State.Idle;
            txtChooseBTDevice.Visible = _state == State.Idle;

            // Authenticated State
            txtAuthStatus.Visible = _state == State.Authenticated;
            timerAuthTimeout.Enabled = _state == State.Authenticated;
            timerTxtAuthStatus.Enabled = _state == State.Authenticated;

            // Multiple States
            btnOptionsSave.Visible = _state == State.Initializing || _state == State.Authenticated;

            switch (_state)
            {
                case State.Uninitialized:
                    break;
                case State.Initializing:
                    btnOptionsSave.Text = "Save";
                    txtStatus.Text = "Create your profile and choose how you want to unlock the door:";
                    UserInputValidation();
                    break;
                case State.Idle:
                    
                    //TODO: Combo box for selecting bluetooth devices already in database that are also in range
                    //TODO: Swipe from idle screen for primary authentication
                    btnOptionsSave.Text = "Options";
                    txtStatus.Text = "Hello! Please swipe your card or choose your device.";
                    loseFocus();
                    enableBtnConfirmBTDevice(listIdleBTDevices, EventArgs.Empty);
                    break;
                case State.Authenticated:
                    btnOptionsSave.Text = "Options";
                    //TODO: display user name here
                    txtStatus.Text = "Welcome, <user>. Door is unlocked.";

                    // Timeout stopwatch
                    txtAuthStatus.Text = "If you wish to edit your account, click Options. Otherwise, this screen will timeout in 10 seconds.";
                    stopWatchAuthStatus.Start();

                    loseFocus();
                    break;
                case State.UserOptions:
                    stopWatchAuthStatus.Stop();
                    stopWatchAuthStatus.Reset();

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
            stopWatchAuthStatus.Stop();
            stopWatchAuthStatus.Reset();
            _state = State.Idle;
            UpdateComponents();
        }

        private void timerTxtAuthStatus_Tick(object sender, EventArgs e)
        {
            txtAuthStatus.Text = "If you wish to edit your account, click Options. Otherwise, this screen will timeout in " + (10 - stopWatchAuthStatus.Elapsed.Seconds) + " seconds.";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool newCardEntry = false;
        private string cardInput = "";
        private void FormStart_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Form Start Clicked");
            // If the user clicks on the form, then active control leaves whatever it was and goes to default
            loseFocus();
        }

        private void FormStart_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form Start Activated");
        }

        private void btnDebugAuthUser_Click(object sender, EventArgs e)
        {
            _state = State.Authenticated;
            UpdateComponents();
        }

        private void keyboard_Click(object sender, EventArgs e)
        {
            if (keyboard_on == 0)
            {
                var args = string.Format("florence & echo $!");
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{args}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo = startInfo;
                process.Start();
                keyboard_on = 1;
                //string result = process.StandardOutput.ReadToEnd();
                //Console.WriteLine(result);
            }
        }

        private void keyboardClose_Leave(object sender, EventArgs e)
        {
            var args = string.Format("sudo killall florence");
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{args}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = startInfo;
            process.Start();
            keyboard_on = 0;
            //string result = process.StandardOutput.ReadToEnd();
            //Console.WriteLine(result);
        }

        private void FormStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("Form Start Key Pressed");
            Console.WriteLine("Pressed: " + e.KeyChar);
            //Console.WriteLine("Shift: " + shift);
            //Console.WriteLine("New Card Entry: " + newCardEntry);

            if (e.KeyChar == ';' || (e.KeyChar == '%'))
            {
                // TODO: Check database to see if this is a new card entry or a recognized card associated with a user.
                newCardEntry = true;
            }

            if (newCardEntry)
            {

                // Stop adding to card input string when "Return" is entered
                if (e.KeyChar == '\r') //|| e.KeyChar == '\n')
                {
                    if (cardInput.Contains(";E?") || cardInput.Contains("%E?") || cardInput.Contains("+E?"))
                    {
                        // Invalid read. Swipe again.
                        newCardEntry = false;
                        cardInput = "";
                        Console.WriteLine("Invalid read. Swipe again.");
                        return;
                    }
                    tbxCard.Text = cardInput;
                    Console.WriteLine(cardInput);
                    newCardEntry = false;
                    cardInput = "";

                    if (_state == 0)
                    {
                        _state = State.Initializing;
                        UpdateComponents();
                        // Open initial setup page
                    }
                    else
                    {
                        // Test authentication
                    }
                } else
                {
                    cardInput += e.KeyChar;
                    newCardEntry = true;
                }
            }
        }

        private void enableBtnConfirmBTDevice(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox) sender;
            btnConfirmBTDevices.Enabled = listBox.SelectedItem != null;
        }

        private void btnDebugBluetooth_Click(object sender, EventArgs e)
        {
            listIdleBTDevices.Items.Add("Dummy BT device name or address");
        }

        private void btnConfirmBTDevices_Click(object sender, EventArgs e)
        {
            //TODO: Request user second authentication, either PIN or Card swipe.

        }
    }
}
