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
            this.tbxStatus = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.errNewUser)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOptionsSave
            // 
            this.btnOptionsSave.Location = new System.Drawing.Point(477, 263);
            this.btnOptionsSave.Name = "btnOptionsSave";
            this.btnOptionsSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnOptionsSave.Size = new System.Drawing.Size(119, 41);
            this.btnOptionsSave.TabIndex = 2;
            this.btnOptionsSave.Text = "Options";
            this.btnOptionsSave.UseVisualStyleBackColor = true;
            this.btnOptionsSave.Visible = false;
            this.btnOptionsSave.Click += new System.EventHandler(this.btnOptionsSave_Click);
            // 
            // tbxStatus
            // 
            this.tbxStatus.AutoSize = true;
            this.tbxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.tbxStatus.Location = new System.Drawing.Point(32, 35);
            this.tbxStatus.Name = "tbxStatus";
            this.tbxStatus.Size = new System.Drawing.Size(499, 26);
            this.tbxStatus.TabIndex = 5;
            this.tbxStatus.Text = "Hello! Please swipe your buzzcard to begin set up.";
            // 
            // btnDebugSwipe
            // 
            this.btnDebugSwipe.Location = new System.Drawing.Point(174, 263);
            this.btnDebugSwipe.Name = "btnDebugSwipe";
            this.btnDebugSwipe.Size = new System.Drawing.Size(112, 23);
            this.btnDebugSwipe.TabIndex = 6;
            this.btnDebugSwipe.Text = "Debug: Swipe card";
            this.btnDebugSwipe.UseVisualStyleBackColor = true;
            this.btnDebugSwipe.Click += new System.EventHandler(this.btnDebugSwipe_Click);
            // 
            // btnDebugBluetooth
            // 
            this.btnDebugBluetooth.Location = new System.Drawing.Point(292, 263);
            this.btnDebugBluetooth.Name = "btnDebugBluetooth";
            this.btnDebugBluetooth.Size = new System.Drawing.Size(137, 23);
            this.btnDebugBluetooth.TabIndex = 7;
            this.btnDebugBluetooth.Text = "Debug: Bluetooth found";
            this.btnDebugBluetooth.UseVisualStyleBackColor = true;
            // 
            // txtCard
            // 
            this.txtCard.AutoSize = true;
            this.txtCard.Location = new System.Drawing.Point(30, 91);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(32, 13);
            this.txtCard.TabIndex = 8;
            this.txtCard.Text = "Card:";
            this.txtCard.Visible = false;
            // 
            // txtUserName
            // 
            this.txtUserName.AutoSize = true;
            this.txtUserName.Location = new System.Drawing.Point(29, 117);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(38, 13);
            this.txtUserName.TabIndex = 9;
            this.txtUserName.Text = "Name:";
            this.txtUserName.Visible = false;
            // 
            // txtUserPhone
            // 
            this.txtUserPhone.AutoSize = true;
            this.txtUserPhone.Location = new System.Drawing.Point(29, 143);
            this.txtUserPhone.Name = "txtUserPhone";
            this.txtUserPhone.Size = new System.Drawing.Size(41, 13);
            this.txtUserPhone.TabIndex = 10;
            this.txtUserPhone.Text = "Phone:";
            this.txtUserPhone.Visible = false;
            // 
            // tbxCard
            // 
            this.tbxCard.Location = new System.Drawing.Point(76, 88);
            this.tbxCard.Name = "tbxCard";
            this.tbxCard.ReadOnly = true;
            this.tbxCard.Size = new System.Drawing.Size(455, 20);
            this.tbxCard.TabIndex = 11;
            this.tbxCard.Visible = false;
            this.tbxCard.TextChanged += new System.EventHandler(this.ValidateTextBox);
            // 
            // tbxUserName
            // 
            this.tbxUserName.Location = new System.Drawing.Point(76, 114);
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(210, 20);
            this.tbxUserName.TabIndex = 12;
            this.tbxUserName.Visible = false;
            this.tbxUserName.TextChanged += new System.EventHandler(this.ValidateTextBox);
            // 
            // tbxUserPhone
            // 
            this.tbxUserPhone.Location = new System.Drawing.Point(76, 140);
            this.tbxUserPhone.Name = "tbxUserPhone";
            this.tbxUserPhone.Size = new System.Drawing.Size(210, 20);
            this.tbxUserPhone.TabIndex = 13;
            this.tbxUserPhone.Visible = false;
            this.tbxUserPhone.TextChanged += new System.EventHandler(this.ValidateTextBox);
            // 
            // cbxPrimAuth
            // 
            this.cbxPrimAuth.FormattingEnabled = true;
            this.cbxPrimAuth.Items.AddRange(new object[] {
            "Card",
            "Bluetooth"});
            this.cbxPrimAuth.Location = new System.Drawing.Point(149, 193);
            this.cbxPrimAuth.Name = "cbxPrimAuth";
            this.cbxPrimAuth.Size = new System.Drawing.Size(137, 21);
            this.cbxPrimAuth.TabIndex = 14;
            this.cbxPrimAuth.Visible = false;
            this.cbxPrimAuth.SelectedIndexChanged += new System.EventHandler(this.SetupPrimaryAuthConfiguration);
            this.cbxPrimAuth.SelectedValueChanged += new System.EventHandler(this.SetupPrimaryAuthConfiguration);
            // 
            // cbxSecAuth
            // 
            this.cbxSecAuth.FormattingEnabled = true;
            this.cbxSecAuth.Items.AddRange(new object[] {
            "Bluetooth",
            "Pin"});
            this.cbxSecAuth.Location = new System.Drawing.Point(458, 193);
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
            this.txtPrimAuth.Location = new System.Drawing.Point(29, 196);
            this.txtPrimAuth.Name = "txtPrimAuth";
            this.txtPrimAuth.Size = new System.Drawing.Size(114, 13);
            this.txtPrimAuth.TabIndex = 17;
            this.txtPrimAuth.Text = "Primary authentication:";
            this.txtPrimAuth.Visible = false;
            // 
            // txtSecAuth
            // 
            this.txtSecAuth.AutoSize = true;
            this.txtSecAuth.Location = new System.Drawing.Point(321, 196);
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
            this.txtPrimChooseDev.Location = new System.Drawing.Point(29, 222);
            this.txtPrimChooseDev.Name = "txtPrimChooseDev";
            this.txtPrimChooseDev.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPrimChooseDev.Size = new System.Drawing.Size(81, 13);
            this.txtPrimChooseDev.TabIndex = 21;
            this.txtPrimChooseDev.Text = "Choose device:";
            this.txtPrimChooseDev.Visible = false;
            // 
            // cbxBTSelect1
            // 
            this.cbxBTSelect1.FormattingEnabled = true;
            this.cbxBTSelect1.Location = new System.Drawing.Point(149, 219);
            this.cbxBTSelect1.Name = "cbxBTSelect1";
            this.cbxBTSelect1.Size = new System.Drawing.Size(137, 21);
            this.cbxBTSelect1.TabIndex = 22;
            this.cbxBTSelect1.Visible = false;
            // 
            // tbxPin
            // 
            this.tbxPin.Location = new System.Drawing.Point(458, 220);
            this.tbxPin.Name = "tbxPin";
            this.tbxPin.Size = new System.Drawing.Size(138, 20);
            this.tbxPin.TabIndex = 23;
            this.tbxPin.Visible = false;
            // 
            // cbxBTSelect2
            // 
            this.cbxBTSelect2.FormattingEnabled = true;
            this.cbxBTSelect2.Location = new System.Drawing.Point(458, 219);
            this.cbxBTSelect2.Name = "cbxBTSelect2";
            this.cbxBTSelect2.Size = new System.Drawing.Size(138, 21);
            this.cbxBTSelect2.TabIndex = 24;
            this.cbxBTSelect2.Visible = false;
            // 
            // txtSecChooseDevOrPin
            // 
            this.txtSecChooseDevOrPin.AutoSize = true;
            this.txtSecChooseDevOrPin.Location = new System.Drawing.Point(321, 222);
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
            this.txtDate.Location = new System.Drawing.Point(32, 78);
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
            this.txtTime.Location = new System.Drawing.Point(32, 111);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(80, 26);
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
            this.btnExit.Location = new System.Drawing.Point(93, 263);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 28;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
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
            this.Controls.Add(this.tbxCard);
            this.Controls.Add(this.txtUserPhone);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.btnDebugBluetooth);
            this.Controls.Add(this.btnDebugSwipe);
            this.Controls.Add(this.tbxStatus);
            this.Controls.Add(this.btnOptionsSave);
            this.Controls.Add(this.cbxBTSelect1);
            this.Controls.Add(this.tbxPin);
            this.Controls.Add(this.cbxBTSelect2);
            this.Name = "FormStart";
            this.Text = "BuzzLock";
            this.Load += new System.EventHandler(this.FormStart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errNewUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOptionsSave;
        private System.Windows.Forms.Label tbxStatus;
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
    }
}

