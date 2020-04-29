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
using System.Runtime.CompilerServices;

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
        public static readonly bool IS_LINUX = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        private static int keyboard_on = 0; //1 means on 0 means off
        private static int numberpad_on = 0; //1 means on 0 means off
        protected static bool acceptMagStripeInput = false;
        protected ErrorProvider userError = new ErrorProvider()
        {
            BlinkStyle = ErrorBlinkStyle.NeverBlink
        };
        protected Timer timerBTIdleBTDeviceListUpdate;
        private System.ComponentModel.IContainer components;
        protected static HashSet<Control> errorControls = new HashSet<Control>();
        protected Control lastActiveTextBox;

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
            if (numberpad_on == 1 && IS_LINUX)
            {
                keyboardClose_Leave(sender, e);
            }
            if (keyboard_on == 0 && IS_LINUX)
            {
                System.Threading.Thread.Sleep(150);
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
            if (keyboard_on == 1 && IS_LINUX)
            {
                keyboardClose_Leave(sender, e);
            }
            if (numberpad_on == 0 && IS_LINUX)
            {
                System.Threading.Thread.Sleep(150);
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

                // Save active component for clear button
            }
        }

        protected void keyboardClose_Leave(object sender, EventArgs e)
        {
            if (IS_LINUX && (keyboard_on == 1 || numberpad_on == 1))
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


        // For debugging bluetooth authentication
        protected List<BluetoothDevice> getBTDevicesInRange()
        {
            List<BluetoothDevice> inRange = new List<BluetoothDevice>
            {
                new BluetoothDevice(new BluetoothAddress("00:11:22:33:44:55"), "Andrew's iPhone"),
                new BluetoothDevice(new BluetoothAddress("99:99:99:99:99:99"), "Big Bad Watch")
            };
            return inRange;
        }

        protected List<BluetoothDevice> getBTDevicesInRangeAndRecognized()
        {
            List<BluetoothDevice> inRange = getBTDevicesInRange();
            List<BluetoothDevice> inRangeAndRecognized = new List<BluetoothDevice>();
            foreach (BluetoothDevice bt in inRange)
            {
                if (AuthenticationSequence.Start(bt) != null) inRangeAndRecognized.Add(bt);
            }
            return inRangeAndRecognized;
        }

        protected bool checkIfMagStripeNeeded()
        {
            return _globalState == State.Uninitialized
                || _globalState == State.Initializing
                || _globalState == State.Idle
                || _globalState == State.UserOptions_EditAuth
                || _globalState == State.UserManagement
                || _globalState == State.UserManagement_AddUser;
        }

        protected void InitializeBTRefreshBTDeviceListsTimer()
        {
            this.components = new System.ComponentModel.Container();
            this.timerBTIdleBTDeviceListUpdate = new System.Windows.Forms.Timer(this.components);
            this.timerBTIdleBTDeviceListUpdate.Enabled = true;
            this.timerBTIdleBTDeviceListUpdate.Interval = 5000;
            this.timerBTIdleBTDeviceListUpdate.Tick += new System.EventHandler(this.RefreshBTDeviceLists);
        }

        protected virtual void RefreshBTDeviceLists(object sender, EventArgs e) { }

    }
}
