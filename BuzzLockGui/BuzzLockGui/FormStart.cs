using System;
using System.Collections.Generic;
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
            tbxUserPhone.Tag = "Please enter your phone number";
            errNewUser.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            ValidateTextBox(tbxUserName, EventArgs.Empty);
            ValidateTextBox(tbxUserPhone, EventArgs.Empty);

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

        private void SetupPrimaryAuthConfiguration(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                txtSecAuth.Visible = true;
                cbxSecAuth.Visible = true;
            }
            if (comboBox.SelectedItem.ToString() == "Bluetooth")
            {
                txtPrimChooseDev.Visible = true;
                cbxBTSelect1.Visible = true;
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
            var comboBox = sender as ComboBox;
            if (comboBox.SelectedItem.ToString() == "Bluetooth")
            {
                txtSecChooseDevOrPin.Text = "Choose device:";
                txtSecChooseDevOrPin.Visible = true;
                cbxBTSelect2.Visible = true;
                tbxPin.Visible = false;

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
                tbxPin.Visible = true;
            }
            else if (comboBox.SelectedItem.ToString() == "Card")
            {
                txtSecChooseDevOrPin.Visible = false;
                cbxBTSelect2.Visible = false;
                tbxPin.Visible = false;

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
                //tbxCard.Text = "%B6011001002725218^GAUKER/ANDREW";

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
                    tbxStatus.Text = "Hello! Please swipe your card to authenticate.";
                    txtDate.Visible = true;
                    txtTime.Visible = true;
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
        private string input = "";
        private bool shift = false;
        private void FormStart_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                shift = true;
            }
            if (e.KeyCode == Keys.OemSemicolon || (e.KeyCode == Keys.D5 && e.Shift))
            {
                newCardEntry = true;
                //shift = false;
            }

            Console.WriteLine("Form Start Key Down");
            Console.WriteLine("Pressed: " + e.KeyCode);
            Console.WriteLine("Shift: " + shift);
            Console.WriteLine("New Card Entry: " + newCardEntry);

            if (newCardEntry)
            {
                Keys key = e.KeyCode;
                switch(key)
                {
                    case Keys.Oem1:
                        input += ";";
                        break;
                    case Keys.ShiftKey:
                        break;
                    case Keys.OemQuestion:
                        input += "?";
                        break;
                    case Keys.Oemplus:
                        input += "=";
                        break;
                    case Keys.D0:
                        input += shift == true ? ")" : "0";
                        Console.WriteLine(shift == true ? ")" : "0");
                        shift = false;
                        break;
                    case Keys.D1:
                        input += shift == true ? "!" : "1";
                        Console.WriteLine(shift == true ? "!" : "1");
                        shift = false;
                        break;
                    case Keys.D2:
                        input += shift == true ? "@" : "2";
                        Console.WriteLine(shift == true ? "@" : "2");
                        shift = false;
                        break;
                    case Keys.D3:
                        input += shift == true ? "#" : "3";
                        Console.WriteLine(shift == true ? "#" : "3");
                        shift = false;
                        break;
                    case Keys.D4:
                        input += shift == true ? "$" : "4";
                        Console.WriteLine(shift == true ? "$" : "4");
                        shift = false;
                        break;
                    case Keys.D5:
                        input += shift == true ? "%" : "5";
                        Console.WriteLine(shift == true ? "%" : "5");
                        shift = false;
                        break;
                    case Keys.D6:
                        input += shift == true ? "^" : "6";
                        Console.WriteLine(shift == true ? "^" : "6");
                        shift = false;
                        break;
                    case Keys.D7:
                        input += shift == true ? "&" : "7";
                        shift = false;
                        break;
                    case Keys.D8:
                        input += shift == true ? "*" : "8";
                        shift = false;
                        break;
                    case Keys.D9:
                        input += shift == true ? "(" : "9";
                        shift = false;
                        break;
                    case Keys.Return:
                        break;
                    case Keys.Space:
                        input += " ";
                        shift = false;
                        break;
                    default:
                        input += key.ToString();
                        shift = false;
                        break;
                }
                //Console.WriteLine(key);
                if (e.KeyCode == Keys.Return)
                {
                    newCardEntry = false;
                    Console.WriteLine(input);
                    input = "";
                } else
                {
                    newCardEntry = true;
                }
            }
        }

        private void FormStart_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Form Start Clicked");
        }

       // private void FormStart_KeyPress(object sender, KeyPressEventArgs e)
       // {
       //     System.Diagnostics.Debug.WriteLine("Form Start Key Press");
       //     //String input = "";
       //     //do
       //     //{
       //     //    input += e.KeyChar.ToString();
       //     //} while (e.KeyChar != "\n");
       //     //System.Diagnostics.Debug.WriteLine(input);
       //     System.Diagnostics.Debug.WriteLine(e.KeyChar);
       // }

        private void FormStart_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Form Start Activated");
        }

        private void tbxUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString().Equals("%"))
            {
                Console.WriteLine("Percent entered");
            }
        }
        //newCardEntry = false;
        //input = "";
        //if (e.KeyCode == Keys.OemSemicolon || (e.KeyCode == Keys.D5 && e.Shift))
        //{
        //    //System.Diagnostics.Debug.WriteLine("Pressed ; or %");
        //    //Console.WriteLine("Pressed ; or %");
        //    newCardEntry = true;
        //    input += e.KeyCode.ToString();
        //}
        //if (newCardEntry)
        //{
        //    do
        //    {
        //        input += e.KeyCode.ToString();
        //    } while (e.KeyCode != Keys.Enter);
        //    tbxCard.Text = input;
        //}
    }
}
