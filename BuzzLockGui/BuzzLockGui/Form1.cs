using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BuzzLockGui
{
    enum State
    {
        Uninitialized,
        Initializing,
        Idle
    }

    public partial class Form1 : Form
    {
        private Form1 _f1;
        private Form2 _f2;
        private State _state;

        // state = 0: uninitialized
        // state = 1: after swiping card to initialize, but before finishing init process
        // state = 2: idle, showing time

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            _f1 = this;
            _f2 = new Form2(this);

            // Query database and set flags
            _state = State.Uninitialized;

            // Update visibility of form components
            UpdateComponents();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Normal;
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_state == State.Initializing)
            {
                // Save initial user preferences
                
                //TODO: Send user name and permissinons to database
                _state = State.Idle;
                UpdateComponents();

            } else if (_state == State.Idle)
            {
                _f2.Show();
                this.Hide();
            }   
        }

        private HashSet<Control> errorControls = new HashSet<Control>();

        private void ValidateTextBox(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                errorProvider1.SetError(textBox, (string)textBox.Tag);
                errorControls.Add(textBox);
            }
            else
            {
                errorProvider1.SetError(textBox, null);
                errorControls.Remove(textBox);
            }
            btnOptionsSave.Enabled = errorControls.Count == 0;
        }

        private void SetupPrimaryAuthConfiguration(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                label7.Visible = true;
                cbxSecAuth.Visible = true;
            }
            if (comboBox.SelectedItem.ToString() == "Bluetooth")
            {
                label5.Visible = true;
                comboBox3.Visible = true;
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
                label5.Visible = false;
                comboBox3.Visible = false;

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
                label8.Text = "Choose device:";
                label8.Visible = true;
                comboBox4.Visible = true;
                textBox2.Visible = false;

                //// if Bluetooth is selected as primary, make primary Card instead
                //if (cbxPrimAuth.SelectedItem.ToString() == "Bluetooth")
                //{
                //    cbxPrimAuth.SelectedItem = "Card";
                //    cbxPrimAuth.Update();
                //}
            }
            else if (comboBox.SelectedItem.ToString() == "Pin")
            {
                label8.Text = "Insert PIN:";
                label8.Visible = true;
                comboBox4.Visible = false;
                textBox2.Visible = true;
            }
            else if (comboBox.SelectedItem.ToString() == "Card")
            {
                label8.Visible = false;
                comboBox4.Visible = false;
                textBox2.Visible = false;

                //// if Card is selected as primary, make primary bluetooth instead
                //if (cbxPrimAuth.SelectedItem.ToString() == "Card")
                //{
                //    cbxPrimAuth.SelectedItem = "Bluetooth";
                //    cbxPrimAuth.Update();
                //}
            }
        }

        private void UserInputValidation()
        {
            txtCard.Tag = "Please swipe your card.";
            txtUserName.Tag = "Please enter your full name.";
            txtUserPhone.Tag = "Please enter your phone number";
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            ValidateTextBox(txtCard, EventArgs.Empty);
            ValidateTextBox(txtUserName, EventArgs.Empty);
            ValidateTextBox(txtUserPhone, EventArgs.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_state == 0)
            {
                _state = State.Initializing;
                UpdateComponents();

                // Open initial setup page
                txtCard.Text = "%B6011001002725218^GAUKER/ANDREW";

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
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    label5.Visible = false;
                    label6.Visible = true;
                    label7.Visible = false;
                    label8.Visible = false;
                    txtCard.Visible = true;
                    txtUserName.Visible = true;
                    txtUserPhone.Visible = true;
                    cbxPrimAuth.Visible = true;
                    cbxSecAuth.Visible = false;
                    comboBox3.Visible = false;
                    comboBox4.Visible = false;
                    btnOptionsSave.Visible = true;
                    btnOptionsSave.Text = "Save";
                    label1.Text = "Create your profile and choose how you want to unlock the door:";
                    UserInputValidation();
                    timer1.Enabled = true;
                    break;
                case State.Idle:
                    label2.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    label8.Visible = false;
                    txtCard.Visible = false;
                    textBox2.Visible = false;
                    txtUserName.Visible = false;
                    txtUserPhone.Visible = false;
                    cbxPrimAuth.Visible = false;
                    cbxSecAuth.Visible = false;
                    comboBox3.Visible = false;
                    comboBox4.Visible = false;
                    btnOptionsSave.Visible = true;
                    btnOptionsSave.Text = "Options";
                    label1.Text = "Hello! Please swipe your card to authenticate.";
                    txtDate.Visible = true;
                    txtTime.Visible = true;
                    break;
                
                default:
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.ToShortTimeString();
            txtDate.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
