using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BuzzLockGui
{
    enum State
    {
        Uninitialized,
        Initializing,
        Idle,
        Options
    }

    // reading card swipe, verification for combo box authentication

    public partial class FormStart : Form
    {
        private FormStart _formStart;
        private FormOptions _formOptions;
        private State _state;

        // state = 0: uninitialized
        // state = 1: after swiping card to initialize, but before finishing init process
        // state = 2: idle, showing time

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
            this.ActiveControl = tbxStatus; 
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
                // Show options form
                _state = State.Options;
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
            cbxBTSelect1.Tag = cbxBTSelect2.Tag = "Please choose your bluetooth device.";
            tbxPin.Tag = "Please enter a 6-digit pin you will remember.";

            errNewUser.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            ValidateTextBox(tbxUserName, EventArgs.Empty);
            ValidateTextBox(tbxUserPhone, EventArgs.Empty);
            ValidateComboBox(cbxPrimAuth, EventArgs.Empty);

            // TODO: Validate combo boxes for authentication
        }

        private HashSet<Control> errorControls = new HashSet<Control>();

        private void ValidateTextBox(object sender, EventArgs e)
        {
            //TODO: InstanceOF Check and exception handling / equals instanceOf
            var textBox = sender as TextBox;
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
            btnOptionsSave.Enabled = errorControls.Count == 0;
        }

        private void ValidatePinBox(object sender, EventArgs e)
        {
            //TODO: InstanceOF Check and exception handling / equals instanceOf
            var textBox = sender as TextBox;
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
            btnOptionsSave.Enabled = errorControls.Count == 0;
        }



        private bool ValidPin(string pin, out string errorMessage)
        {
            // Confirm that the pin is the correct length
            if (pin.Length < 6)
            {
                errorMessage = "Pin must be exactly 6 digits long";
                return false;
            }

            // Confirim that the pin contains only numbers
            if (!Regex.IsMatch(pin, @"^\d+$"))
            {
                errorMessage = "Pin must be all numbers.";
                return false;
            }

            errorMessage = "";
            return true;
        }

        private void ValidateComboBox(object sender, EventArgs e)
        {
            //TODO: InstanceOF Check and exception handling / equals instanceOf
            var comboBox = sender as ComboBox;
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
            else if (comboBox.SelectedItem.ToString() == "Pin")
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

      
private void UpdateComponents()
        {
            switch (_state)
            {
                case State.Uninitialized:
                    break;
                case State.Initializing:
                    txtCard.Visible = true;
                    txtUserName.Visible = true;
                    txtUserPhone.Visible = true;
                    txtPrimChooseDev.Visible = false;
                    txtPrimAuth.Visible = true;
                    txtSecAuth.Visible = false;
                    txtSecChooseDevOrPin.Visible = false;
                    tbxCard.Visible = true;
                    tbxUserName.Visible = true;
                    tbxUserPhone.Visible = true;
                    cbxPrimAuth.Visible = true;
                    cbxSecAuth.Visible = false;
                    cbxBTSelect1.Visible = false;
                    cbxBTSelect2.Visible = false;
                    btnOptionsSave.Visible = true;
                    btnOptionsSave.Text = "Save";
                    tbxStatus.Text = "Create your profile and choose how you want to unlock the door:";
                    UserInputValidation();
                    timerDateTime.Enabled = true;
                    break;
                case State.Idle:
                    
                    //TODO: Combo box for selecting bluetooth devices already in database that are also in range
                    //TODO: Swipe from idle screen for primary authentication
                    txtCard.Visible = false;
                    txtUserName.Visible = false;
                    txtUserPhone.Visible = false;
                    txtPrimChooseDev.Visible = false;
                    txtPrimAuth.Visible = false;
                    txtSecAuth.Visible = false;
                    txtSecChooseDevOrPin.Visible = false;
                    tbxCard.Visible = false;
                    tbxPin.Visible = false;
                    tbxUserName.Visible = false;
                    tbxUserPhone.Visible = false;
                    cbxPrimAuth.Visible = false;
                    cbxSecAuth.Visible = false;
                    cbxBTSelect1.Visible = false;
                    cbxBTSelect2.Visible = false;
                    btnOptionsSave.Visible = true;
                    btnOptionsSave.Text = "Options";
                    tbxStatus.Text = "Hello! Please swipe your card or choose your device.";
                    txtDate.Visible = true;
                    txtTime.Visible = true;
                    listIdleBTDevices.Visible = true;
                    btnConfirmBTDevices.Visible = true;
                    txtChooseBTDevice.Visible = true;
                    this.ActiveControl = tbxStatus;
                    break;
                
                default:
                    break;
            }
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToShortTimeString();
            txtDate.Text = DateTime.Now.ToLongDateString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool newCardEntry = false;
        private string cardInput = "";
        private bool shift = false;
        private void FormStart_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                shift = true;
            }
            if (e.KeyCode == Keys.OemSemicolon || (e.KeyCode == Keys.D5 && e.Shift))
            {
                // TODO: Check database to see if this is a new card entry or a recognized card associated with a user.
                newCardEntry = true;
            }

            //Console.WriteLine("Form Start Key Down");
            //Console.WriteLine("Pressed: " + e.KeyCode);
            //Console.WriteLine("Shift: " + shift);
            //Console.WriteLine("New Card Entry: " + newCardEntry);

            if (newCardEntry)
            {
                Keys key = e.KeyCode;

                // Decode key press and add to card input string
                switch(key)
                {
                    case Keys.Oem1:
                        cardInput += shift == true ? ":" : ";";
                        break;
                    case Keys.Oem5:
                        cardInput += shift == true ? "~" : "`";
                        break;
                    case Keys.Oem6:
                        cardInput += shift == true ? "}" : "]";
                        break;
                    case Keys.Oem7:
                        cardInput += shift == true ? "|" : "\\";
                        break;
                    case Keys.OemQuestion:
                        cardInput += shift == true ? "?" : "/";
                        break;
                    case Keys.Oemplus:
                        cardInput += shift == true ? "+" : "=";
                        break;
                    case Keys.OemMinus:
                        cardInput += shift == true ? "_" : "-";
                        break;
                    case Keys.Oemtilde:
                        cardInput += shift == true ? "\"" : "'";
                        break;
                    case Keys.Oemcomma:
                        cardInput += shift == true ? "<" : ",";
                        break;
                    case Keys.OemPeriod:
                        cardInput += shift == true ? ">" : ".";
                        break;
                    case Keys.OemOpenBrackets:
                        cardInput += shift == true ? "{" : "[";
                        break;
                    case Keys.D0:
                        cardInput += shift == true ? ")" : "0";
                        break;
                    case Keys.D1:
                        cardInput += shift == true ? "!" : "1";
                        break;
                    case Keys.D2:
                        cardInput += shift == true ? "@" : "2";
                        break;
                    case Keys.D3:
                        cardInput += shift == true ? "#" : "3";
                        break;
                    case Keys.D4:
                        cardInput += shift == true ? "$" : "4";
                        break;
                    case Keys.D5:
                        cardInput += shift == true ? "%" : "5";
                        break;
                    case Keys.D6:
                        cardInput += shift == true ? "^" : "6";
                        break;
                    case Keys.D7:
                        cardInput += shift == true ? "&" : "7";
                        break;
                    case Keys.D8:
                        cardInput += shift == true ? "*" : "8";
                        break;
                    case Keys.D9:
                        cardInput += shift == true ? "(" : "9";
                        break;
                    case Keys.Space:
                        cardInput += " ";
                        break;
                    case Keys.ShiftKey:
                        break;
                    case Keys.Return:
                        break;
                    default:
                        cardInput += key.ToString();
                        break;
                }

                // Stop adding to card input string when "Return" is entered
                if (e.KeyCode == Keys.Return)
                {
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
                    newCardEntry = true;
                }
            }
        }
        private void FormStart_KeyUp(object sender, KeyEventArgs e)
        {
            // When shift key is released, adjust "shift" value to false
            if (e.KeyCode == Keys.ShiftKey)
            {
                //Console.WriteLine("Shift key released");
                shift = false;
            }
        }

        private void FormStart_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Form Start Clicked");
        }

        private void FormStart_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form Start Activated");
        }
    }
}
