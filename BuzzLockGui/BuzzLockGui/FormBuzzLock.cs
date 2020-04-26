using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BuzzLockGui.Backend;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BuzzLockGui
{
    public class FormBuzzLock : Form
    {
        protected enum State
        {
            Uninitialized,
            Initializing,
            Idle,
            SecondFactor,
            Authenticated, //TODO: spin servo to unlock door in Authenticated, and keep it unlocked during UserOptions
            AccessDenied,
            UserOptions,   //      and in Authenticated until timeout happens.
            UserOptions_EditProfile,
            UserOptions_EditAuth,
            UserManagement,
            UserManagement_AddUser
        }

        protected static State _globalState;
        protected static User _currentUser;
        protected bool NoErrors => errorControls.Count == 0;
        protected static readonly bool IS_LINUX = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        private static int keyboard_on = 0; //1 means on 0 means off
        private static int numberpad_on = 0; //1 means on 0 means off
        protected static bool acceptMagStripeInput = true;

        protected ErrorProvider userError = new ErrorProvider()
        {
            BlinkStyle = ErrorBlinkStyle.NeverBlink
        };

        protected static HashSet<Control> errorControls = new HashSet<Control>();

        protected void ValidateTextBox(object sender, EventArgs e)
        {
            // Type check to ensure passed in object is a TextBox
            if (sender is TextBox textBox) {
                if (textBox.Visible && textBox.Text == "")
                {
                    userError.SetError(textBox, (string)textBox.Tag);
                    errorControls.Add(textBox);
                }
                else
                {
                    userError.SetError(textBox, null);
                    errorControls.Remove(textBox);
                }
            }
            OnValidate();
        }

        protected void ValidatePhoneBox(object sender, EventArgs e)
        {
            // Type check to ensure passed in object is a TextBox
            if (sender is TextBox textBox)
            {
                string errorMessage;
                if (textBox.Visible && !ValidPhone(textBox.Text, out errorMessage))
                {
                    userError.SetError(textBox, errorMessage);
                    errorControls.Add(textBox);
                }
                else
                {
                    userError.SetError(textBox, null);
                    errorControls.Remove(textBox);
                }
            }
            OnValidate();
        }

        protected bool ValidPhone(string phone, out string errorMessage)
        {
            // Confirm that the phone number is the correct length
            if (phone.Length < 10)
            {
                errorMessage = "Phone number must be exactly 10 digits long (all numbers).";
                return false;
            }

            // Confirm that the phone number contains only numbers
            if (!Regex.IsMatch(phone, @"^\d+$"))
            {
                errorMessage = "Phone number must be all numbers.";
                return false;
            }

            errorMessage = "";
            return true;
        }

        protected void ValidatePinBox(object sender, EventArgs e)
        {
            // Type check to ensure passed in object is a TextBox
            if (sender is TextBox textBox)
            {
                string errorMessage;
                if (textBox.Visible && !ValidPin(textBox.Text, out errorMessage))
                {
                    userError.SetError(textBox, errorMessage);
                    errorControls.Add(textBox);
                }
                else
                {
                    userError.SetError(textBox, null);
                    errorControls.Remove(textBox);
                }
            }
            OnValidate();
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

        protected void ValidateComboBox(object sender, EventArgs e)
        {
            // Type check to ensure passed in object is a ComboBox
            if (sender is ComboBox comboBox)
            {
                if (comboBox.Visible && comboBox.SelectedItem == null)
                {
                    userError.SetError(comboBox, (string)comboBox.Tag);
                    errorControls.Add(comboBox);
                }
                else
                {
                    userError.SetError(comboBox, null);
                    errorControls.Remove(comboBox);
                }
            }
            OnValidate();
        }

        protected void keyboard_Click(object sender, EventArgs e)
        {
            if (keyboard_on == 0 && IS_LINUX)
            {
                System.Threading.Thread.Sleep(100);
                var args = string.Format("xvkbd -compact -geometry 800x200+0+280");
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

        protected void numberpad_Click(object sender, EventArgs e)
        {
            if (numberpad_on == 0 && IS_LINUX)
            {
                System.Threading.Thread.Sleep(100);
                var args = string.Format("xvkbd -keypad -geometry 260x230+0+250");
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
                numberpad_on = 1;
                //string result = process.StandardOutput.ReadToEnd();
                //Console.WriteLine(result);
            }
        }

        protected void keyboardClose_Leave(object sender, EventArgs e)
        {
            if (IS_LINUX)
            {
                var args = string.Format("sudo killall xvkbd");
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
                numberpad_on = 0;
                //string result = process.StandardOutput.ReadToEnd();
                //Console.WriteLine(result);
            }
        }

        protected virtual void OnValidate() { }
    }
}
