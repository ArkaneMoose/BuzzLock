namespace BuzzLockGui
{
    partial class FormStart : FormBuzzLock
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
            this.tbxAccessDenied = new System.Windows.Forms.Label();
            this.timerAccessDeniedTimeout = new System.Windows.Forms.Timer(this.components);
            this.timerTxtAccessDeniedStatus = new System.Windows.Forms.Timer(this.components);
            this.txtAccessDeniedStatus = new System.Windows.Forms.Label();
            this.btnDebugAccessDenied = new System.Windows.Forms.Button();
            this.btnDebugSecondFactor = new System.Windows.Forms.Button();
            this.txtSecondFactorStatus = new System.Windows.Forms.Label();
            this.tbxSecFactorPinOrCard = new System.Windows.Forms.TextBox();
            this.txtSecFactorPinOrCard = new System.Windows.Forms.Label();
            this.timerBTIdleBTDeviceListUpdate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.userError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errNewUser)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOptionsSave
            // 
            this.btnOptionsSave.Location = new System.Drawing.Point(605, 211);
            this.btnOptionsSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOptionsSave.Name = "btnOptionsSave";
            this.btnOptionsSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnOptionsSave.Size = new System.Drawing.Size(119, 32);
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
            this.txtStatus.Location = new System.Drawing.Point(12, 9);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(507, 26);
            this.txtStatus.TabIndex = 5;
            this.txtStatus.Text = "Hello! Please swipe your BuzzCard to begin set up.";
            // 
            // btnDebugSwipe
            // 
            this.btnDebugSwipe.CausesValidation = false;
            this.btnDebugSwipe.Location = new System.Drawing.Point(605, 41);
            this.btnDebugSwipe.Name = "btnDebugSwipe";
            this.btnDebugSwipe.Size = new System.Drawing.Size(109, 23);
            this.btnDebugSwipe.TabIndex = 6;
            this.btnDebugSwipe.Text = "Debug: Swipe card";
            this.btnDebugSwipe.UseVisualStyleBackColor = true;
            this.btnDebugSwipe.Click += new System.EventHandler(this.btnDebugSwipe_Click);
            // 
            // btnDebugBluetooth
            // 
            this.btnDebugBluetooth.Location = new System.Drawing.Point(605, 70);
            this.btnDebugBluetooth.Name = "btnDebugBluetooth";
            this.btnDebugBluetooth.Size = new System.Drawing.Size(137, 23);
            this.btnDebugBluetooth.TabIndex = 7;
            this.btnDebugBluetooth.Text = "Debug: Bluetooth found";
            this.btnDebugBluetooth.UseVisualStyleBackColor = true;
            this.btnDebugBluetooth.Click += new System.EventHandler(this.btnDebugBluetooth_Click);
            // 
            // txtCard
            // 
            this.txtCard.AutoSize = true;
            this.txtCard.Location = new System.Drawing.Point(16, 48);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(32, 13);
            this.txtCard.TabIndex = 8;
            this.txtCard.Text = "Card:";
            this.txtCard.Visible = false;
            // 
            // txtUserName
            // 
            this.txtUserName.AutoSize = true;
            this.txtUserName.Location = new System.Drawing.Point(15, 74);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(38, 13);
            this.txtUserName.TabIndex = 9;
            this.txtUserName.Text = "Name:";
            this.txtUserName.Visible = false;
            // 
            // txtUserPhone
            // 
            this.txtUserPhone.AutoSize = true;
            this.txtUserPhone.Location = new System.Drawing.Point(15, 100);
            this.txtUserPhone.Name = "txtUserPhone";
            this.txtUserPhone.Size = new System.Drawing.Size(41, 13);
            this.txtUserPhone.TabIndex = 10;
            this.txtUserPhone.Text = "Phone:";
            this.txtUserPhone.Visible = false;
            // 
            // tbxCard
            // 
            this.tbxCard.Location = new System.Drawing.Point(62, 45);
            this.tbxCard.Name = "tbxCard";
            this.tbxCard.ReadOnly = true;
            this.tbxCard.Size = new System.Drawing.Size(513, 20);
            this.tbxCard.TabIndex = 11;
            this.tbxCard.Visible = false;
            this.tbxCard.TextChanged += new System.EventHandler(this.ValidateTextBox);
            // 
            // tbxUserName
            // 
            this.tbxUserName.Location = new System.Drawing.Point(62, 71);
            this.tbxUserName.MaxLength = 20;
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(210, 20);
            this.tbxUserName.TabIndex = 12;
            this.tbxUserName.Visible = false;
            this.tbxUserName.Click += new System.EventHandler(this.keyboard_Click);
            this.tbxUserName.TextChanged += new System.EventHandler(this.ValidateTextBox);
            this.tbxUserName.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // tbxUserPhone
            // 
            this.tbxUserPhone.Location = new System.Drawing.Point(62, 97);
            this.tbxUserPhone.MaxLength = 10;
            this.tbxUserPhone.Name = "tbxUserPhone";
            this.tbxUserPhone.Size = new System.Drawing.Size(210, 20);
            this.tbxUserPhone.TabIndex = 13;
            this.tbxUserPhone.Visible = false;
            this.tbxUserPhone.Click += new System.EventHandler(this.numberpad_Click);
            this.tbxUserPhone.TextChanged += new System.EventHandler(this.ValidatePhoneBox);
            this.tbxUserPhone.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // cbxPrimAuth
            // 
            this.cbxPrimAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrimAuth.FormattingEnabled = true;
            this.cbxPrimAuth.Items.AddRange(new object[] {
            "Card",
            "Bluetooth"});
            this.cbxPrimAuth.Location = new System.Drawing.Point(135, 149);
            this.cbxPrimAuth.Name = "cbxPrimAuth";
            this.cbxPrimAuth.Size = new System.Drawing.Size(137, 21);
            this.cbxPrimAuth.TabIndex = 14;
            this.cbxPrimAuth.Visible = false;
            this.cbxPrimAuth.SelectedIndexChanged += new System.EventHandler(this.SetupPrimaryAuthConfiguration);
            this.cbxPrimAuth.TextUpdate += new System.EventHandler(this.ValidateComboBox);
            this.cbxPrimAuth.SelectedValueChanged += new System.EventHandler(this.SetupPrimaryAuthConfiguration);
            // 
            // cbxSecAuth
            // 
            this.cbxSecAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSecAuth.FormattingEnabled = true;
            this.cbxSecAuth.Items.AddRange(new object[] {
            "Bluetooth",
            "PIN"});
            this.cbxSecAuth.Location = new System.Drawing.Point(437, 149);
            this.cbxSecAuth.Name = "cbxSecAuth";
            this.cbxSecAuth.Size = new System.Drawing.Size(138, 21);
            this.cbxSecAuth.TabIndex = 15;
            this.cbxSecAuth.Visible = false;
            this.cbxSecAuth.SelectedIndexChanged += new System.EventHandler(this.SetupSecondaryAuthConfiguration);
            this.cbxSecAuth.SelectedValueChanged += new System.EventHandler(this.SetupSecondaryAuthConfiguration);
            // 
            // txtPrimAuth
            // 
            this.txtPrimAuth.AutoSize = true;
            this.txtPrimAuth.Location = new System.Drawing.Point(15, 153);
            this.txtPrimAuth.Name = "txtPrimAuth";
            this.txtPrimAuth.Size = new System.Drawing.Size(114, 13);
            this.txtPrimAuth.TabIndex = 17;
            this.txtPrimAuth.Text = "Primary authentication:";
            this.txtPrimAuth.Visible = false;
            // 
            // txtSecAuth
            // 
            this.txtSecAuth.AutoSize = true;
            this.txtSecAuth.Location = new System.Drawing.Point(300, 153);
            this.txtSecAuth.Name = "txtSecAuth";
            this.txtSecAuth.Size = new System.Drawing.Size(131, 13);
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
            this.txtPrimChooseDev.Location = new System.Drawing.Point(15, 179);
            this.txtPrimChooseDev.Name = "txtPrimChooseDev";
            this.txtPrimChooseDev.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPrimChooseDev.Size = new System.Drawing.Size(81, 13);
            this.txtPrimChooseDev.TabIndex = 21;
            this.txtPrimChooseDev.Text = "Choose device:";
            this.txtPrimChooseDev.Visible = false;
            // 
            // cbxBTSelect1
            // 
            this.cbxBTSelect1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect1.FormattingEnabled = true;
            this.cbxBTSelect1.Location = new System.Drawing.Point(135, 175);
            this.cbxBTSelect1.Name = "cbxBTSelect1";
            this.cbxBTSelect1.Size = new System.Drawing.Size(137, 21);
            this.cbxBTSelect1.TabIndex = 22;
            this.cbxBTSelect1.Visible = false;
            this.cbxBTSelect1.SelectedIndexChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // tbxPin
            // 
            this.tbxPin.Location = new System.Drawing.Point(437, 177);
            this.tbxPin.MaxLength = 6;
            this.tbxPin.Name = "tbxPin";
            this.tbxPin.Size = new System.Drawing.Size(138, 20);
            this.tbxPin.TabIndex = 23;
            this.tbxPin.Visible = false;
            this.tbxPin.Click += new System.EventHandler(this.numberpad_Click);
            this.tbxPin.TextChanged += new System.EventHandler(this.ValidatePinBox);
            this.tbxPin.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // cbxBTSelect2
            // 
            this.cbxBTSelect2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect2.FormattingEnabled = true;
            this.cbxBTSelect2.Location = new System.Drawing.Point(437, 175);
            this.cbxBTSelect2.Name = "cbxBTSelect2";
            this.cbxBTSelect2.Size = new System.Drawing.Size(138, 21);
            this.cbxBTSelect2.TabIndex = 24;
            this.cbxBTSelect2.Visible = false;
            this.cbxBTSelect2.SelectedIndexChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // txtSecChooseDevOrPin
            // 
            this.txtSecChooseDevOrPin.AutoSize = true;
            this.txtSecChooseDevOrPin.Location = new System.Drawing.Point(300, 179);
            this.txtSecChooseDevOrPin.Name = "txtSecChooseDevOrPin";
            this.txtSecChooseDevOrPin.Size = new System.Drawing.Size(57, 13);
            this.txtSecChooseDevOrPin.TabIndex = 25;
            this.txtSecChooseDevOrPin.Text = "Insert PIN:";
            this.txtSecChooseDevOrPin.Visible = false;
            // 
            // txtDate
            // 
            this.txtDate.AutoSize = true;
            this.txtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtDate.Location = new System.Drawing.Point(192, 75);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(80, 26);
            this.txtDate.TabIndex = 26;
            this.txtDate.Text = "<date>";
            this.txtDate.Visible = false;
            // 
            // txtTime
            // 
            this.txtTime.AutoSize = true;
            this.txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtTime.Location = new System.Drawing.Point(192, 108);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(80, 26);
            this.txtTime.TabIndex = 27;
            this.txtTime.Text = "<time>";
            this.txtTime.Visible = false;
            // 
            // timerDateTime
            // 
            this.timerDateTime.Enabled = true;
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(605, 14);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
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
            this.listIdleBTDevices.ItemHeight = 16;
            this.listIdleBTDevices.Location = new System.Drawing.Point(19, 75);
            this.listIdleBTDevices.Name = "listIdleBTDevices";
            this.listIdleBTDevices.Size = new System.Drawing.Size(160, 114);
            this.listIdleBTDevices.TabIndex = 29;
            this.listIdleBTDevices.Visible = false;
            this.listIdleBTDevices.SelectedIndexChanged += new System.EventHandler(this.enableBtnConfirmBTDevice);
            // 
            // btnConfirmBTDevices
            // 
            this.btnConfirmBTDevices.Location = new System.Drawing.Point(19, 211);
            this.btnConfirmBTDevices.Name = "btnConfirmBTDevices";
            this.btnConfirmBTDevices.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnConfirmBTDevices.Size = new System.Drawing.Size(119, 32);
            this.btnConfirmBTDevices.TabIndex = 30;
            this.btnConfirmBTDevices.Text = "Confirm Bluetooth";
            this.btnConfirmBTDevices.UseVisualStyleBackColor = true;
            this.btnConfirmBTDevices.Visible = false;
            this.btnConfirmBTDevices.Click += new System.EventHandler(this.btnConfirmBTDevices_Click);
            // 
            // txtChooseBTDevice
            // 
            this.txtChooseBTDevice.AutoSize = true;
            this.txtChooseBTDevice.Location = new System.Drawing.Point(16, 57);
            this.txtChooseBTDevice.Name = "txtChooseBTDevice";
            this.txtChooseBTDevice.Size = new System.Drawing.Size(128, 13);
            this.txtChooseBTDevice.TabIndex = 31;
            this.txtChooseBTDevice.Text = "Choose bluetooth device:";
            this.txtChooseBTDevice.Visible = false;
            // 
            // btnDebugAuthUser
            // 
            this.btnDebugAuthUser.Location = new System.Drawing.Point(605, 99);
            this.btnDebugAuthUser.Name = "btnDebugAuthUser";
            this.btnDebugAuthUser.Size = new System.Drawing.Size(144, 23);
            this.btnDebugAuthUser.TabIndex = 32;
            this.btnDebugAuthUser.Text = "Debug: Authenticate User";
            this.btnDebugAuthUser.UseVisualStyleBackColor = true;
            this.btnDebugAuthUser.Click += new System.EventHandler(this.btnDebugAuthUser_Click);
            // 
            // txtAuthStatus
            // 
            this.txtAuthStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAuthStatus.Location = new System.Drawing.Point(15, 39);
            this.txtAuthStatus.Name = "txtAuthStatus";
            this.txtAuthStatus.Size = new System.Drawing.Size(390, 36);
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
            // tbxAccessDenied
            // 
            this.tbxAccessDenied.Location = new System.Drawing.Point(0, 0);
            this.tbxAccessDenied.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tbxAccessDenied.Name = "tbxAccessDenied";
            this.tbxAccessDenied.Size = new System.Drawing.Size(67, 15);
            this.tbxAccessDenied.TabIndex = 39;
            // 
            // timerAccessDeniedTimeout
            // 
            this.timerAccessDeniedTimeout.Interval = 10000;
            this.timerAccessDeniedTimeout.Tick += new System.EventHandler(this.timeoutAccessDenied_Tick);
            // 
            // timerTxtAccessDeniedStatus
            // 
            this.timerTxtAccessDeniedStatus.Interval = 200;
            this.timerTxtAccessDeniedStatus.Tick += new System.EventHandler(this.timerTxtAccessDeniedStatus_Tick);
            // 
            // txtAccessDeniedStatus
            // 
            this.txtAccessDeniedStatus.Location = new System.Drawing.Point(0, 0);
            this.txtAccessDeniedStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtAccessDeniedStatus.Name = "txtAccessDeniedStatus";
            this.txtAccessDeniedStatus.Size = new System.Drawing.Size(67, 15);
            this.txtAccessDeniedStatus.TabIndex = 35;
            // 
            // btnDebugAccessDenied
            // 
            this.btnDebugAccessDenied.Location = new System.Drawing.Point(605, 128);
            this.btnDebugAccessDenied.Name = "btnDebugAccessDenied";
            this.btnDebugAccessDenied.Size = new System.Drawing.Size(144, 23);
            this.btnDebugAccessDenied.TabIndex = 36;
            this.btnDebugAccessDenied.Text = "Debug: Access Denied";
            this.btnDebugAccessDenied.UseVisualStyleBackColor = true;
            this.btnDebugAccessDenied.Click += new System.EventHandler(this.btnDebugAccessDenied_Click);
            // 
            // btnDebugSecondFactor
            // 
            this.btnDebugSecondFactor.Location = new System.Drawing.Point(605, 156);
            this.btnDebugSecondFactor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDebugSecondFactor.Name = "btnDebugSecondFactor";
            this.btnDebugSecondFactor.Size = new System.Drawing.Size(144, 23);
            this.btnDebugSecondFactor.TabIndex = 37;
            this.btnDebugSecondFactor.Text = "Debug: Second Factor";
            this.btnDebugSecondFactor.UseVisualStyleBackColor = true;
            this.btnDebugSecondFactor.Click += new System.EventHandler(this.btnDebugSecondFactor_Click);
            // 
            // txtSecondFactorStatus
            // 
            this.txtSecondFactorStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSecondFactorStatus.Location = new System.Drawing.Point(21, 322);
            this.txtSecondFactorStatus.Name = "txtSecondFactorStatus";
            this.txtSecondFactorStatus.Size = new System.Drawing.Size(520, 45);
            this.txtSecondFactorStatus.TabIndex = 38;
            this.txtSecondFactorStatus.Text = "Invalid authentication.";
            this.txtSecondFactorStatus.Visible = false;
            // 
            // tbxSecFactorPinOrCard
            // 
            this.tbxSecFactorPinOrCard.Location = new System.Drawing.Point(18, 95);
            this.tbxSecFactorPinOrCard.MaximumSize = new System.Drawing.Size(559, 20);
            this.tbxSecFactorPinOrCard.MinimumSize = new System.Drawing.Size(105, 20);
            this.tbxSecFactorPinOrCard.Name = "tbxSecFactorPinOrCard";
            this.tbxSecFactorPinOrCard.Size = new System.Drawing.Size(559, 20);
            this.tbxSecFactorPinOrCard.TabIndex = 40;
            this.tbxSecFactorPinOrCard.Visible = false;
            this.tbxSecFactorPinOrCard.Click += new System.EventHandler(this.numberpad_Click);
            this.tbxSecFactorPinOrCard.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // txtSecFactorPinOrCard
            // 
            this.txtSecFactorPinOrCard.AutoSize = true;
            this.txtSecFactorPinOrCard.Location = new System.Drawing.Point(18, 76);
            this.txtSecFactorPinOrCard.Name = "txtSecFactorPinOrCard";
            this.txtSecFactorPinOrCard.Size = new System.Drawing.Size(57, 13);
            this.txtSecFactorPinOrCard.TabIndex = 41;
            this.txtSecFactorPinOrCard.Text = "Insert PIN:";
            this.txtSecFactorPinOrCard.Visible = false;
            // 
            // timerBTIdleBTDeviceListUpdate
            // 
            this.timerBTIdleBTDeviceListUpdate.Enabled = true;
            this.timerBTIdleBTDeviceListUpdate.Interval = 5000;
            this.timerBTIdleBTDeviceListUpdate.Tick += new System.EventHandler(this.RefreshBTDeviceLists);
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.txtSecFactorPinOrCard);
            this.Controls.Add(this.tbxSecFactorPinOrCard);
            this.Controls.Add(this.txtSecondFactorStatus);
            this.Controls.Add(this.btnDebugSecondFactor);
            this.Controls.Add(this.btnDebugAccessDenied);
            this.Controls.Add(this.txtAccessDeniedStatus);
            this.Controls.Add(this.tbxAccessDenied);
            this.Controls.Add(this.btnDebugAuthUser);
            this.Controls.Add(this.txtChooseBTDevice);
            this.Controls.Add(this.btnConfirmBTDevices);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.btnDebugBluetooth);
            this.Controls.Add(this.btnDebugSwipe);
            this.Controls.Add(this.btnOptionsSave);
            this.Controls.Add(this.tbxCard);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtSecChooseDevOrPin);
            this.Controls.Add(this.txtPrimChooseDev);
            this.Controls.Add(this.txtSecAuth);
            this.Controls.Add(this.txtPrimAuth);
            this.Controls.Add(this.cbxSecAuth);
            this.Controls.Add(this.cbxPrimAuth);
            this.Controls.Add(this.tbxUserPhone);
            this.Controls.Add(this.tbxUserName);
            this.Controls.Add(this.txtUserPhone);
            this.Controls.Add(this.cbxBTSelect1);
            this.Controls.Add(this.tbxPin);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.listIdleBTDevices);
            this.Controls.Add(this.txtAuthStatus);
            this.Controls.Add(this.cbxBTSelect2);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "FormStart";
            this.Text = "BuzzLock";
            this.Activated += new System.EventHandler(this.FormStart_Activated);
            this.Load += new System.EventHandler(this.FormStart_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormStart_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormStart_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.userError)).EndInit();
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
        private System.Windows.Forms.Label tbxAccessDenied;
        private System.Windows.Forms.Timer timerAccessDeniedTimeout;
        private System.Windows.Forms.Timer timerTxtAccessDeniedStatus;
        private System.Windows.Forms.Label txtAccessDeniedStatus;
        private System.Windows.Forms.Button btnDebugAccessDenied;
        private System.Windows.Forms.Button btnDebugSecondFactor;
        private System.Windows.Forms.Label txtSecondFactorStatus;
        private System.Windows.Forms.Label txtSecFactorPinOrCard;
        private System.Windows.Forms.TextBox tbxSecFactorPinOrCard;
        private System.Windows.Forms.Timer timerBTIdleBTDeviceListUpdate;
    }
}

