namespace BuzzLockGui
{
    partial class FormStart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOptionsSave = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.Label();
            this.btnDebugSwipe = new System.Windows.Forms.Button();
            this.btnDebugBluetooth = new System.Windows.Forms.Button();
            this.txtCard = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.Label();
            this.txtUserPhone = new System.Windows.Forms.Label();
            this.tbxCard = new System.Windows.Forms.TextBox();
            this.tbxUserName = new System.Windows.Forms.TextBox();
            this.tbxUserPhone = new System.Windows.Forms.TextBox();
            this.cbxPrimAuth = new System.Windows.Forms.ComboBox();
            this.cbxSecAuth = new System.Windows.Forms.ComboBox();
            this.txtPrimAuth = new System.Windows.Forms.Label();
            this.txtSecAuth = new System.Windows.Forms.Label();
            this.errNewUser = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtPrimChooseDev = new System.Windows.Forms.Label();
            this.cbxBTSelect1 = new System.Windows.Forms.ComboBox();
            this.tbxPin = new System.Windows.Forms.TextBox();
            this.cbxBTSelect2 = new System.Windows.Forms.ComboBox();
            this.txtSecChooseDevOrPin = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.Label();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.listIdleBTDevices = new System.Windows.Forms.ListBox();
            this.btnConfirmBTDevices = new System.Windows.Forms.Button();
            this.txtChooseBTDevice = new System.Windows.Forms.Label();
            this.btnDebugAuthUser = new System.Windows.Forms.Button();
            this.txtAuthStatus = new System.Windows.Forms.Label();
            this.timerAuthTimeout = new System.Windows.Forms.Timer(this.components);
            this.timerTxtAuthStatus = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errNewUser)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOptionsSave
            // 
            this.btnOptionsSave.Location = new System.Drawing.Point(874, 325);
            this.btnOptionsSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOptionsSave.Name = "btnOptionsSave";
            this.btnOptionsSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnOptionsSave.Size = new System.Drawing.Size(178, 50);
            this.btnOptionsSave.TabIndex = 2;
            this.btnOptionsSave.Text = "Options";
            this.btnOptionsSave.UseVisualStyleBackColor = true;
            this.btnOptionsSave.Visible = false;
            this.btnOptionsSave.Click += new System.EventHandler(this.btnOptionsSave_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtStatus.Location = new System.Drawing.Point(18, 14);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(760, 38);
            this.txtStatus.TabIndex = 5;
            this.txtStatus.Text = "Hello! Please swipe your BuzzCard to begin set up.";
            // 
            // btnDebugSwipe
            // 
            this.btnDebugSwipe.CausesValidation = false;
            this.btnDebugSwipe.Location = new System.Drawing.Point(874, 115);
            this.btnDebugSwipe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDebugSwipe.Name = "btnDebugSwipe";
            this.btnDebugSwipe.Size = new System.Drawing.Size(164, 35);
            this.btnDebugSwipe.TabIndex = 6;
            this.btnDebugSwipe.Text = "Debug: Swipe card";
            this.btnDebugSwipe.UseVisualStyleBackColor = true;
            this.btnDebugSwipe.Click += new System.EventHandler(this.btnDebugSwipe_Click);
            // 
            // btnDebugBluetooth
            // 
            this.btnDebugBluetooth.Location = new System.Drawing.Point(874, 160);
            this.btnDebugBluetooth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDebugBluetooth.Name = "btnDebugBluetooth";
            this.btnDebugBluetooth.Size = new System.Drawing.Size(206, 35);
            this.btnDebugBluetooth.TabIndex = 7;
            this.btnDebugBluetooth.Text = "Debug: Bluetooth found";
            this.btnDebugBluetooth.UseVisualStyleBackColor = true;
            // 
            // txtCard
            // 
            this.txtCard.AutoSize = true;
            this.txtCard.Location = new System.Drawing.Point(24, 74);
            this.txtCard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(47, 20);
            this.txtCard.TabIndex = 8;
            this.txtCard.Text = "Card:";
            this.txtCard.Visible = false;
            // 
            // txtUserName
            // 
            this.txtUserName.AutoSize = true;
            this.txtUserName.Location = new System.Drawing.Point(22, 114);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(55, 20);
            this.txtUserName.TabIndex = 9;
            this.txtUserName.Text = "Name:";
            this.txtUserName.Visible = false;
            // 
            // txtUserPhone
            // 
            this.txtUserPhone.AutoSize = true;
            this.txtUserPhone.Location = new System.Drawing.Point(22, 154);
            this.txtUserPhone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtUserPhone.Name = "txtUserPhone";
            this.txtUserPhone.Size = new System.Drawing.Size(59, 20);
            this.txtUserPhone.TabIndex = 10;
            this.txtUserPhone.Text = "Phone:";
            this.txtUserPhone.Visible = false;
            // 
            // tbxCard
            // 
            this.tbxCard.Location = new System.Drawing.Point(93, 70);
            this.tbxCard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxCard.Name = "tbxCard";
            this.tbxCard.ReadOnly = true;
            this.tbxCard.Size = new System.Drawing.Size(680, 26);
            this.tbxCard.TabIndex = 11;
            this.tbxCard.Visible = false;
            this.tbxCard.TextChanged += new System.EventHandler(this.ValidateTextBox);
            // 
            // tbxUserName
            // 
            this.tbxUserName.Location = new System.Drawing.Point(93, 110);
            this.tbxUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(313, 26);
            this.tbxUserName.TabIndex = 12;
            this.tbxUserName.Visible = false;
            this.tbxUserName.Click += new System.EventHandler(this.keyboard_Click);
            this.tbxUserName.TextChanged += new System.EventHandler(this.ValidateTextBox);
            this.tbxUserName.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // tbxUserPhone
            // 
            this.tbxUserPhone.Location = new System.Drawing.Point(93, 150);
            this.tbxUserPhone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxUserPhone.Name = "tbxUserPhone";
            this.tbxUserPhone.Size = new System.Drawing.Size(313, 26);
            this.tbxUserPhone.TabIndex = 13;
            this.tbxUserPhone.Visible = false;
            this.tbxUserPhone.Click += new System.EventHandler(this.keyboard_Click);
            this.tbxUserPhone.TextChanged += new System.EventHandler(this.ValidateTextBox);
            this.tbxUserPhone.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // cbxPrimAuth
            // 
            this.cbxPrimAuth.FormattingEnabled = true;
            this.cbxPrimAuth.Items.AddRange(new object[] {
            "Card",
            "Bluetooth"});
            this.cbxPrimAuth.Location = new System.Drawing.Point(202, 230);
            this.cbxPrimAuth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxPrimAuth.Name = "cbxPrimAuth";
            this.cbxPrimAuth.Size = new System.Drawing.Size(204, 28);
            this.cbxPrimAuth.TabIndex = 14;
            this.cbxPrimAuth.Visible = false;
            this.cbxPrimAuth.SelectedIndexChanged += new System.EventHandler(this.SetupPrimaryAuthConfiguration);
            this.cbxPrimAuth.TextUpdate += new System.EventHandler(this.ValidateComboBox);
            this.cbxPrimAuth.SelectedValueChanged += new System.EventHandler(this.SetupPrimaryAuthConfiguration);
            // 
            // cbxSecAuth
            // 
            this.cbxSecAuth.FormattingEnabled = true;
            this.cbxSecAuth.Items.AddRange(new object[] {
            "Bluetooth",
            "PIN"});
            this.cbxSecAuth.Location = new System.Drawing.Point(666, 230);
            this.cbxSecAuth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxSecAuth.Name = "cbxSecAuth";
            this.cbxSecAuth.Size = new System.Drawing.Size(205, 28);
            this.cbxSecAuth.TabIndex = 15;
            this.cbxSecAuth.Visible = false;
            this.cbxSecAuth.SelectedIndexChanged += new System.EventHandler(this.SetupSecondaryAuthConfiguration);
            this.cbxSecAuth.SelectedValueChanged += new System.EventHandler(this.SetupSecondaryAuthConfiguration);
            // 
            // txtPrimAuth
            // 
            this.txtPrimAuth.AutoSize = true;
            this.txtPrimAuth.Location = new System.Drawing.Point(22, 235);
            this.txtPrimAuth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtPrimAuth.Name = "txtPrimAuth";
            this.txtPrimAuth.Size = new System.Drawing.Size(170, 20);
            this.txtPrimAuth.TabIndex = 17;
            this.txtPrimAuth.Text = "Primary authentication:";
            this.txtPrimAuth.Visible = false;
            // 
            // txtSecAuth
            // 
            this.txtSecAuth.AutoSize = true;
            this.txtSecAuth.Location = new System.Drawing.Point(460, 235);
            this.txtSecAuth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtSecAuth.Name = "txtSecAuth";
            this.txtSecAuth.Size = new System.Drawing.Size(194, 20);
            this.txtSecAuth.TabIndex = 18;
            this.txtSecAuth.Text = "Secondary authentication:";
            this.txtSecAuth.Visible = false;
            // 
            // errNewUser
            // 
            this.errNewUser.ContainerControl = this;
            // 
            // txtPrimChooseDev
            // 
            this.txtPrimChooseDev.AutoSize = true;
            this.txtPrimChooseDev.Location = new System.Drawing.Point(22, 275);
            this.txtPrimChooseDev.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtPrimChooseDev.Name = "txtPrimChooseDev";
            this.txtPrimChooseDev.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPrimChooseDev.Size = new System.Drawing.Size(117, 20);
            this.txtPrimChooseDev.TabIndex = 21;
            this.txtPrimChooseDev.Text = "Choose device:";
            this.txtPrimChooseDev.Visible = false;
            // 
            // cbxBTSelect1
            // 
            this.cbxBTSelect1.FormattingEnabled = true;
            this.cbxBTSelect1.Location = new System.Drawing.Point(202, 270);
            this.cbxBTSelect1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxBTSelect1.Name = "cbxBTSelect1";
            this.cbxBTSelect1.Size = new System.Drawing.Size(204, 28);
            this.cbxBTSelect1.TabIndex = 22;
            this.cbxBTSelect1.Visible = false;
            this.cbxBTSelect1.SelectedIndexChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // tbxPin
            // 
            this.tbxPin.Location = new System.Drawing.Point(666, 272);
            this.tbxPin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxPin.MaxLength = 6;
            this.tbxPin.Name = "tbxPin";
            this.tbxPin.Size = new System.Drawing.Size(205, 26);
            this.tbxPin.TabIndex = 23;
            this.tbxPin.Visible = false;
            this.tbxPin.TextChanged += new System.EventHandler(this.ValidatePinBox);
            // 
            // cbxBTSelect2
            // 
            this.cbxBTSelect2.FormattingEnabled = true;
            this.cbxBTSelect2.Location = new System.Drawing.Point(666, 270);
            this.cbxBTSelect2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbxBTSelect2.Name = "cbxBTSelect2";
            this.cbxBTSelect2.Size = new System.Drawing.Size(205, 28);
            this.cbxBTSelect2.TabIndex = 24;
            this.cbxBTSelect2.Visible = false;
            this.cbxBTSelect2.SelectedIndexChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // txtSecChooseDevOrPin
            // 
            this.txtSecChooseDevOrPin.AutoSize = true;
            this.txtSecChooseDevOrPin.Location = new System.Drawing.Point(460, 275);
            this.txtSecChooseDevOrPin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtSecChooseDevOrPin.Name = "txtSecChooseDevOrPin";
            this.txtSecChooseDevOrPin.Size = new System.Drawing.Size(84, 20);
            this.txtSecChooseDevOrPin.TabIndex = 25;
            this.txtSecChooseDevOrPin.Text = "Insert PIN:";
            this.txtSecChooseDevOrPin.Visible = false;
            // 
            // txtDate
            // 
            this.txtDate.AutoSize = true;
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtDate.Location = new System.Drawing.Point(288, 115);
            this.txtDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(118, 38);
            this.txtDate.TabIndex = 26;
            this.txtDate.Text = "<date>";
            this.txtDate.Visible = false;
            // 
            // txtTime
            // 
            this.txtTime.AutoSize = true;
            this.txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtTime.Location = new System.Drawing.Point(288, 166);
            this.txtTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(116, 38);
            this.txtTime.TabIndex = 27;
            this.txtTime.Text = "<time>";
            this.txtTime.Visible = false;
            // 
            // timerDateTime
            // 
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(874, 74);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(112, 35);
            this.btnExit.TabIndex = 28;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // listIdleBTDevices
            // 
            this.listIdleBTDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listIdleBTDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listIdleBTDevices.FormattingEnabled = true;
            this.listIdleBTDevices.ItemHeight = 25;
            this.listIdleBTDevices.Location = new System.Drawing.Point(28, 115);
            this.listIdleBTDevices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listIdleBTDevices.Name = "listIdleBTDevices";
            this.listIdleBTDevices.Size = new System.Drawing.Size(239, 152);
            this.listIdleBTDevices.TabIndex = 29;
            this.listIdleBTDevices.Visible = false;
            // 
            // btnConfirmBTDevices
            // 
            this.btnConfirmBTDevices.Location = new System.Drawing.Point(28, 325);
            this.btnConfirmBTDevices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnConfirmBTDevices.Name = "btnConfirmBTDevices";
            this.btnConfirmBTDevices.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnConfirmBTDevices.Size = new System.Drawing.Size(178, 50);
            this.btnConfirmBTDevices.TabIndex = 30;
            this.btnConfirmBTDevices.Text = "Confirm Bluetooth";
            this.btnConfirmBTDevices.UseVisualStyleBackColor = true;
            this.btnConfirmBTDevices.Visible = false;
            // 
            // txtChooseBTDevice
            // 
            this.txtChooseBTDevice.AutoSize = true;
            this.txtChooseBTDevice.Location = new System.Drawing.Point(24, 88);
            this.txtChooseBTDevice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtChooseBTDevice.Name = "txtChooseBTDevice";
            this.txtChooseBTDevice.Size = new System.Drawing.Size(188, 20);
            this.txtChooseBTDevice.TabIndex = 31;
            this.txtChooseBTDevice.Text = "Choose bluetooth device:";
            this.txtChooseBTDevice.Visible = false;
            // 
            // btnDebugAuthUser
            // 
            this.btnDebugAuthUser.Location = new System.Drawing.Point(874, 205);
            this.btnDebugAuthUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDebugAuthUser.Name = "btnDebugAuthUser";
            this.btnDebugAuthUser.Size = new System.Drawing.Size(216, 35);
            this.btnDebugAuthUser.TabIndex = 32;
            this.btnDebugAuthUser.Text = "Debug: Authenticate User";
            this.btnDebugAuthUser.UseVisualStyleBackColor = true;
            this.btnDebugAuthUser.Click += new System.EventHandler(this.btnDebugAuthUser_Click);
            // 
            // txtAuthStatus
            // 
            this.txtAuthStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAuthStatus.Location = new System.Drawing.Point(22, 60);
            this.txtAuthStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtAuthStatus.Name = "txtAuthStatus";
            this.txtAuthStatus.Size = new System.Drawing.Size(585, 55);
            this.txtAuthStatus.TabIndex = 34;
            this.txtAuthStatus.Text = "If you wish to edit your account, click Options. Otherwise, this screen will time" +
    "out in 10 seconds.";
            this.txtAuthStatus.Visible = false;
            // 
            // timerAuthTimeout
            // 
            this.timerAuthTimeout.Interval = 10000;
            this.timerAuthTimeout.Tick += new System.EventHandler(this.timeoutAuth_Tick);
            // 
            // timerTxtAuthStatus
            // 
            this.timerTxtAuthStatus.Interval = 200;
            this.timerTxtAuthStatus.Tick += new System.EventHandler(this.timerTxtAuthStatus_Tick);
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 680);
            this.Controls.Add(this.btnDebugAuthUser);
            this.Controls.Add(this.txtChooseBTDevice);
            this.Controls.Add(this.btnConfirmBTDevices);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtSecChooseDevOrPin);
            this.Controls.Add(this.txtPrimChooseDev);
            this.Controls.Add(this.txtSecAuth);
            this.Controls.Add(this.txtPrimAuth);
            this.Controls.Add(this.cbxSecAuth);
            this.Controls.Add(this.cbxPrimAuth);
            this.Controls.Add(this.tbxUserPhone);
            this.Controls.Add(this.tbxUserName);
            this.Controls.Add(this.txtUserPhone);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.btnDebugBluetooth);
            this.Controls.Add(this.btnDebugSwipe);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnOptionsSave);
            this.Controls.Add(this.cbxBTSelect1);
            this.Controls.Add(this.tbxPin);
            this.Controls.Add(this.cbxBTSelect2);
            this.Controls.Add(this.txtAuthStatus);
            this.Controls.Add(this.tbxCard);
            this.Controls.Add(this.listIdleBTDevices);
            this.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.Name = "FormStart";
            this.Text = "BuzzLock";
            this.Activated += new System.EventHandler(this.FormStart_Activated);
            this.Load += new System.EventHandler(this.FormStart_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormStart_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormStart_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.errNewUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOptionsSave;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.Button btnDebugSwipe;
        private System.Windows.Forms.Button btnDebugBluetooth;
        private System.Windows.Forms.Label txtCard;
        private System.Windows.Forms.Label txtUserName;
        private System.Windows.Forms.Label txtUserPhone;
        private System.Windows.Forms.TextBox tbxCard;
        private System.Windows.Forms.TextBox tbxUserName;
        private System.Windows.Forms.TextBox tbxUserPhone;
        private System.Windows.Forms.ComboBox cbxPrimAuth;
        private System.Windows.Forms.ComboBox cbxSecAuth;
        private System.Windows.Forms.Label txtPrimAuth;
        private System.Windows.Forms.Label txtSecAuth;
        private System.Windows.Forms.ErrorProvider errNewUser;
        private System.Windows.Forms.Label txtPrimChooseDev;
        private System.Windows.Forms.TextBox tbxPin;
        private System.Windows.Forms.ComboBox cbxBTSelect2;
        private System.Windows.Forms.ComboBox cbxBTSelect1;
        private System.Windows.Forms.Label txtSecChooseDevOrPin;
        private System.Windows.Forms.Label txtTime;
        private System.Windows.Forms.Label txtDate;
        private System.Windows.Forms.Timer timerDateTime;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ListBox listIdleBTDevices;
        private System.Windows.Forms.Label txtChooseBTDevice;
        private System.Windows.Forms.Button btnConfirmBTDevices;
        private System.Windows.Forms.Button btnDebugAuthUser;
        private System.Windows.Forms.Label txtAuthStatus;
        private System.Windows.Forms.Timer timerAuthTimeout;
        private System.Windows.Forms.Timer timerTxtAuthStatus;
    }
}

