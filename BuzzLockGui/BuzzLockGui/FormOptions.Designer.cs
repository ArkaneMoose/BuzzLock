﻿namespace BuzzLockGui
{
    partial class FormOptions : FormBuzzLock
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
            this.btnEditProfile = new System.Windows.Forms.Button();
            this.btnEditAuth = new System.Windows.Forms.Button();
            this.btnRemoveUser = new System.Windows.Forms.Button();
            this.txtEditProfile = new System.Windows.Forms.Label();
            this.txtEditAuth = new System.Windows.Forms.Label();
            this.txtRemoveUser = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.Label();
            this.txtOptionsTitle = new System.Windows.Forms.Label();
            this.timerOptionsTimeout = new System.Windows.Forms.Timer(this.components);
            this.txtOptionsStatus = new System.Windows.Forms.Label();
            this.timerOptionsStatus = new System.Windows.Forms.Timer(this.components);
            this.txtCurrentName = new System.Windows.Forms.Label();
            this.txtCurrentPhone = new System.Windows.Forms.Label();
            this.tbxNewName = new System.Windows.Forms.TextBox();
            this.tbxNewPhone = new System.Windows.Forms.TextBox();
            this.txtSecChooseDevOrPin = new System.Windows.Forms.Label();
            this.txtPrimChooseDev = new System.Windows.Forms.Label();
            this.txtSecAuth = new System.Windows.Forms.Label();
            this.txtPrimAuth = new System.Windows.Forms.Label();
            this.cbxSecAuth = new System.Windows.Forms.ComboBox();
            this.cbxPrimAuth = new System.Windows.Forms.ComboBox();
            this.cbxBTSelect1 = new System.Windows.Forms.ComboBox();
            this.tbxPin = new System.Windows.Forms.TextBox();
            this.cbxBTSelect2 = new System.Windows.Forms.ComboBox();
            this.tbxCard = new System.Windows.Forms.TextBox();
            this.txtCard = new System.Windows.Forms.Label();
            this.btnResetAuthMods = new System.Windows.Forms.Button();
            this.btnUserManagement = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.userError)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOptionsSave
            // 
            this.btnOptionsSave.Location = new System.Drawing.Point(1210, 406);
            this.btnOptionsSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnOptionsSave.Name = "btnOptionsSave";
            this.btnOptionsSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnOptionsSave.Size = new System.Drawing.Size(236, 62);
            this.btnOptionsSave.TabIndex = 4;
            this.btnOptionsSave.Text = "Save";
            this.btnOptionsSave.UseVisualStyleBackColor = true;
            this.btnOptionsSave.Click += new System.EventHandler(this.btnOptionsSave_Click);
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.Location = new System.Drawing.Point(24, 233);
            this.btnEditProfile.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnEditProfile.Size = new System.Drawing.Size(236, 62);
            this.btnEditProfile.TabIndex = 5;
            this.btnEditProfile.Text = "Profile";
            this.btnEditProfile.UseVisualStyleBackColor = true;
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // btnEditAuth
            // 
            this.btnEditAuth.Location = new System.Drawing.Point(24, 306);
            this.btnEditAuth.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEditAuth.Name = "btnEditAuth";
            this.btnEditAuth.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnEditAuth.Size = new System.Drawing.Size(236, 62);
            this.btnEditAuth.TabIndex = 6;
            this.btnEditAuth.Text = "Authentication";
            this.btnEditAuth.UseVisualStyleBackColor = true;
            this.btnEditAuth.Click += new System.EventHandler(this.btnEditAuth_Click);
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Location = new System.Drawing.Point(24, 379);
            this.btnRemoveUser.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRemoveUser.Size = new System.Drawing.Size(236, 62);
            this.btnRemoveUser.TabIndex = 7;
            this.btnRemoveUser.Text = "Remove user";
            this.btnRemoveUser.UseVisualStyleBackColor = true;
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // txtEditProfile
            // 
            this.txtEditProfile.AutoSize = true;
            this.txtEditProfile.Location = new System.Drawing.Point(276, 252);
            this.txtEditProfile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtEditProfile.Name = "txtEditProfile";
            this.txtEditProfile.Size = new System.Drawing.Size(257, 25);
            this.txtEditProfile.TabIndex = 8;
            this.txtEditProfile.Text = "Edit name, phone, picture";
            // 
            // txtEditAuth
            // 
            this.txtEditAuth.AutoSize = true;
            this.txtEditAuth.Location = new System.Drawing.Point(276, 325);
            this.txtEditAuth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtEditAuth.Name = "txtEditAuth";
            this.txtEditAuth.Size = new System.Drawing.Size(326, 25);
            this.txtEditAuth.TabIndex = 9;
            this.txtEditAuth.Text = "Edit your authentication methods";
            // 
            // txtRemoveUser
            // 
            this.txtRemoveUser.AutoSize = true;
            this.txtRemoveUser.Location = new System.Drawing.Point(276, 398);
            this.txtRemoveUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtRemoveUser.Name = "txtRemoveUser";
            this.txtRemoveUser.Size = new System.Drawing.Size(375, 25);
            this.txtRemoveUser.TabIndex = 10;
            this.txtRemoveUser.Text = "Permanently delete your user account";
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtStatus.Location = new System.Drawing.Point(24, 17);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(951, 52);
            this.txtStatus.TabIndex = 11;
            this.txtStatus.Text = "Welcome, <user>. You have FULL permissions.";
            // 
            // txtOptionsTitle
            // 
            this.txtOptionsTitle.AutoSize = true;
            this.txtOptionsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtOptionsTitle.Location = new System.Drawing.Point(24, 160);
            this.txtOptionsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtOptionsTitle.Name = "txtOptionsTitle";
            this.txtOptionsTitle.Size = new System.Drawing.Size(495, 52);
            this.txtOptionsTitle.TabIndex = 12;
            this.txtOptionsTitle.Text = "BuzzLock Options Menu";
            // 
            // timerOptionsTimeout
            // 
            this.timerOptionsTimeout.Interval = 30000;
            this.timerOptionsTimeout.Tick += new System.EventHandler(this.timerOptionsTimeout_Tick);
            // 
            // txtOptionsStatus
            // 
            this.txtOptionsStatus.AutoSize = true;
            this.txtOptionsStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOptionsStatus.Location = new System.Drawing.Point(30, 75);
            this.txtOptionsStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtOptionsStatus.Name = "txtOptionsStatus";
            this.txtOptionsStatus.Size = new System.Drawing.Size(452, 30);
            this.txtOptionsStatus.TabIndex = 14;
            this.txtOptionsStatus.Text = "This screen will timeout in 30 seconds.";
            // 
            // timerOptionsStatus
            // 
            this.timerOptionsStatus.Interval = 200;
            this.timerOptionsStatus.Tick += new System.EventHandler(this.timerOptionsStatus_Tick);
            // 
            // txtCurrentName
            // 
            this.txtCurrentName.AutoSize = true;
            this.txtCurrentName.Location = new System.Drawing.Point(28, 254);
            this.txtCurrentName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtCurrentName.Name = "txtCurrentName";
            this.txtCurrentName.Size = new System.Drawing.Size(74, 25);
            this.txtCurrentName.TabIndex = 15;
            this.txtCurrentName.Text = "Name:";
            // 
            // txtCurrentPhone
            // 
            this.txtCurrentPhone.AutoSize = true;
            this.txtCurrentPhone.Location = new System.Drawing.Point(28, 365);
            this.txtCurrentPhone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtCurrentPhone.Name = "txtCurrentPhone";
            this.txtCurrentPhone.Size = new System.Drawing.Size(158, 25);
            this.txtCurrentPhone.TabIndex = 17;
            this.txtCurrentPhone.Text = "Phone number:";
            // 
            // tbxNewName
            // 
            this.tbxNewName.Location = new System.Drawing.Point(32, 288);
            this.tbxNewName.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tbxNewName.MaxLength = 20;
            this.tbxNewName.Name = "tbxNewName";
            this.tbxNewName.Size = new System.Drawing.Size(368, 31);
            this.tbxNewName.TabIndex = 20;
            this.tbxNewName.Click += new System.EventHandler(this.keyboard_Click);
            this.tbxNewName.TextChanged += new System.EventHandler(this.ValidateTextBox);
            this.tbxNewName.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // tbxNewPhone
            // 
            this.tbxNewPhone.Location = new System.Drawing.Point(32, 398);
            this.tbxNewPhone.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tbxNewPhone.MaxLength = 10;
            this.tbxNewPhone.Name = "tbxNewPhone";
            this.tbxNewPhone.Size = new System.Drawing.Size(368, 31);
            this.tbxNewPhone.TabIndex = 22;
            this.tbxNewPhone.Click += new System.EventHandler(this.numberpad_Click);
            this.tbxNewPhone.TextChanged += new System.EventHandler(this.ValidatePhoneBox);
            this.tbxNewPhone.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // txtSecChooseDevOrPin
            // 
            this.txtSecChooseDevOrPin.AutoSize = true;
            this.txtSecChooseDevOrPin.Location = new System.Drawing.Point(612, 312);
            this.txtSecChooseDevOrPin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtSecChooseDevOrPin.Name = "txtSecChooseDevOrPin";
            this.txtSecChooseDevOrPin.Size = new System.Drawing.Size(111, 25);
            this.txtSecChooseDevOrPin.TabIndex = 33;
            this.txtSecChooseDevOrPin.Text = "Insert PIN:";
            this.txtSecChooseDevOrPin.Visible = false;
            // 
            // txtPrimChooseDev
            // 
            this.txtPrimChooseDev.AutoSize = true;
            this.txtPrimChooseDev.Location = new System.Drawing.Point(28, 313);
            this.txtPrimChooseDev.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtPrimChooseDev.Name = "txtPrimChooseDev";
            this.txtPrimChooseDev.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPrimChooseDev.Size = new System.Drawing.Size(161, 25);
            this.txtPrimChooseDev.TabIndex = 30;
            this.txtPrimChooseDev.Text = "Choose device:";
            this.txtPrimChooseDev.Visible = false;
            // 
            // txtSecAuth
            // 
            this.txtSecAuth.AutoSize = true;
            this.txtSecAuth.Location = new System.Drawing.Point(612, 262);
            this.txtSecAuth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtSecAuth.Name = "txtSecAuth";
            this.txtSecAuth.Size = new System.Drawing.Size(262, 25);
            this.txtSecAuth.TabIndex = 29;
            this.txtSecAuth.Text = "Secondary authentication:";
            this.txtSecAuth.Visible = false;
            // 
            // txtPrimAuth
            // 
            this.txtPrimAuth.AutoSize = true;
            this.txtPrimAuth.Location = new System.Drawing.Point(28, 262);
            this.txtPrimAuth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtPrimAuth.Name = "txtPrimAuth";
            this.txtPrimAuth.Size = new System.Drawing.Size(232, 25);
            this.txtPrimAuth.TabIndex = 28;
            this.txtPrimAuth.Text = "Primary authentication:";
            this.txtPrimAuth.Visible = false;
            // 
            // cbxSecAuth
            // 
            this.cbxSecAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSecAuth.FormattingEnabled = true;
            this.cbxSecAuth.Items.AddRange(new object[] {
            "Bluetooth",
            "PIN"});
            this.cbxSecAuth.Location = new System.Drawing.Point(888, 254);
            this.cbxSecAuth.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cbxSecAuth.Name = "cbxSecAuth";
            this.cbxSecAuth.Size = new System.Drawing.Size(272, 33);
            this.cbxSecAuth.TabIndex = 27;
            this.cbxSecAuth.Visible = false;
            this.cbxSecAuth.SelectedIndexChanged += new System.EventHandler(this.ModifySecondaryAuthConfiguration);
            // 
            // cbxPrimAuth
            // 
            this.cbxPrimAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrimAuth.FormattingEnabled = true;
            this.cbxPrimAuth.Items.AddRange(new object[] {
            "Card",
            "Bluetooth"});
            this.cbxPrimAuth.Location = new System.Drawing.Point(268, 254);
            this.cbxPrimAuth.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cbxPrimAuth.Name = "cbxPrimAuth";
            this.cbxPrimAuth.Size = new System.Drawing.Size(272, 33);
            this.cbxPrimAuth.TabIndex = 26;
            this.cbxPrimAuth.Visible = false;
            this.cbxPrimAuth.SelectedIndexChanged += new System.EventHandler(this.ModifyPrimaryAuthConfiguration);
            // 
            // cbxBTSelect1
            // 
            this.cbxBTSelect1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect1.FormattingEnabled = true;
            this.cbxBTSelect1.Location = new System.Drawing.Point(268, 306);
            this.cbxBTSelect1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cbxBTSelect1.Name = "cbxBTSelect1";
            this.cbxBTSelect1.Size = new System.Drawing.Size(272, 33);
            this.cbxBTSelect1.TabIndex = 31;
            this.cbxBTSelect1.Visible = false;
            this.cbxBTSelect1.SelectedIndexChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // tbxPin
            // 
            this.tbxPin.Location = new System.Drawing.Point(888, 308);
            this.tbxPin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tbxPin.MaxLength = 6;
            this.tbxPin.Name = "tbxPin";
            this.tbxPin.Size = new System.Drawing.Size(272, 31);
            this.tbxPin.TabIndex = 32;
            this.tbxPin.Visible = false;
            this.tbxPin.Click += new System.EventHandler(this.numberpad_Click);
            this.tbxPin.TextChanged += new System.EventHandler(this.ValidatePinBox);
            this.tbxPin.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // cbxBTSelect2
            // 
            this.cbxBTSelect2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect2.FormattingEnabled = true;
            this.cbxBTSelect2.Location = new System.Drawing.Point(888, 308);
            this.cbxBTSelect2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.cbxBTSelect2.Name = "cbxBTSelect2";
            this.cbxBTSelect2.Size = new System.Drawing.Size(272, 33);
            this.cbxBTSelect2.TabIndex = 34;
            this.cbxBTSelect2.Visible = false;
            this.cbxBTSelect2.SelectedIndexChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // tbxCard
            // 
            this.tbxCard.BackColor = System.Drawing.SystemColors.Control;
            this.tbxCard.Location = new System.Drawing.Point(100, 429);
            this.tbxCard.Margin = new System.Windows.Forms.Padding(4);
            this.tbxCard.Name = "tbxCard";
            this.tbxCard.Size = new System.Drawing.Size(1060, 31);
            this.tbxCard.TabIndex = 35;
            // 
            // txtCard
            // 
            this.txtCard.AutoSize = true;
            this.txtCard.Location = new System.Drawing.Point(28, 435);
            this.txtCard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(64, 25);
            this.txtCard.TabIndex = 36;
            this.txtCard.Text = "Card:";
            // 
            // btnResetAuthMods
            // 
            this.btnResetAuthMods.Location = new System.Drawing.Point(1210, 254);
            this.btnResetAuthMods.Margin = new System.Windows.Forms.Padding(4);
            this.btnResetAuthMods.Name = "btnResetAuthMods";
            this.btnResetAuthMods.Size = new System.Drawing.Size(236, 62);
            this.btnResetAuthMods.TabIndex = 37;
            this.btnResetAuthMods.Text = "Reset";
            this.btnResetAuthMods.UseVisualStyleBackColor = true;
            this.btnResetAuthMods.Click += new System.EventHandler(this.ResetAuthMods_Click);
            // 
            // btnUserManagement
            // 
            this.btnUserManagement.Location = new System.Drawing.Point(1210, 325);
            this.btnUserManagement.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnUserManagement.Name = "btnUserManagement";
            this.btnUserManagement.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnUserManagement.Size = new System.Drawing.Size(236, 62);
            this.btnUserManagement.TabIndex = 38;
            this.btnUserManagement.Text = "User Management";
            this.btnUserManagement.UseVisualStyleBackColor = true;
            this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1568, 850);
            this.Controls.Add(this.btnUserManagement);
            this.Controls.Add(this.btnResetAuthMods);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.tbxCard);
            this.Controls.Add(this.txtPrimChooseDev);
            this.Controls.Add(this.txtSecAuth);
            this.Controls.Add(this.txtPrimAuth);
            this.Controls.Add(this.cbxPrimAuth);
            this.Controls.Add(this.cbxBTSelect1);
            this.Controls.Add(this.tbxNewPhone);
            this.Controls.Add(this.tbxNewName);
            this.Controls.Add(this.txtCurrentPhone);
            this.Controls.Add(this.txtCurrentName);
            this.Controls.Add(this.txtOptionsStatus);
            this.Controls.Add(this.txtOptionsTitle);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.txtRemoveUser);
            this.Controls.Add(this.txtEditAuth);
            this.Controls.Add(this.txtEditProfile);
            this.Controls.Add(this.btnRemoveUser);
            this.Controls.Add(this.btnEditAuth);
            this.Controls.Add(this.btnEditProfile);
            this.Controls.Add(this.cbxSecAuth);
            this.Controls.Add(this.txtSecChooseDevOrPin);
            this.Controls.Add(this.tbxPin);
            this.Controls.Add(this.cbxBTSelect2);
            this.Controls.Add(this.btnOptionsSave);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "FormOptions";
            this.Text = "BuzzLock Options";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormOptions_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormOptions_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.userError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOptionsSave;
        private System.Windows.Forms.Button btnEditProfile;
        private System.Windows.Forms.Button btnEditAuth;
        private System.Windows.Forms.Button btnRemoveUser;
        private System.Windows.Forms.Label txtEditProfile;
        private System.Windows.Forms.Label txtEditAuth;
        private System.Windows.Forms.Label txtRemoveUser;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.Label txtOptionsTitle;
        private System.Windows.Forms.Label txtOptionsStatus;
        private System.Windows.Forms.Label txtCurrentName;
        private System.Windows.Forms.Label txtCurrentPhone;
        private System.Windows.Forms.TextBox tbxNewName;
        private System.Windows.Forms.TextBox tbxNewPhone;
        private System.Windows.Forms.Label txtSecChooseDevOrPin;
        private System.Windows.Forms.Label txtPrimChooseDev;
        private System.Windows.Forms.Label txtSecAuth;
        private System.Windows.Forms.Label txtPrimAuth;
        private System.Windows.Forms.ComboBox cbxSecAuth;
        private System.Windows.Forms.ComboBox cbxPrimAuth;
        private System.Windows.Forms.ComboBox cbxBTSelect1;
        private System.Windows.Forms.TextBox tbxPin;
        private System.Windows.Forms.ComboBox cbxBTSelect2;
        private System.Windows.Forms.TextBox tbxCard;
        private System.Windows.Forms.Label txtCard;
        private System.Windows.Forms.Button btnResetAuthMods;
        private System.Windows.Forms.Button btnUserManagement;
        private System.Windows.Forms.Timer timerOptionsTimeout;
        private System.Windows.Forms.Timer timerOptionsStatus;
    }
}