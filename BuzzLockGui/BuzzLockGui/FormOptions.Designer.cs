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
            this.tbxStatus = new System.Windows.Forms.Label();
            this.txtOptionsTitle = new System.Windows.Forms.Label();
            this.txtUserPermission = new System.Windows.Forms.Label();
            this.timerOptionsTimeout = new System.Windows.Forms.Timer(this.components);
            this.txtOptionsStatus = new System.Windows.Forms.Label();
            this.timerOptionsStatus = new System.Windows.Forms.Timer(this.components);
            this.txtCurrentName = new System.Windows.Forms.Label();
            this.txtNewName = new System.Windows.Forms.Label();
            this.txtCurrentPhone = new System.Windows.Forms.Label();
            this.txtNewPhone = new System.Windows.Forms.Label();
            this.tbxCurrentName = new System.Windows.Forms.TextBox();
            this.tbxNewName = new System.Windows.Forms.TextBox();
            this.tbxNewPhone = new System.Windows.Forms.TextBox();
            this.tbxCurrentPhone = new System.Windows.Forms.TextBox();
            this.btnChangeName = new System.Windows.Forms.Button();
            this.btnChangePhone = new System.Windows.Forms.Button();
            this.txtSecChooseDevOrPin = new System.Windows.Forms.Label();
            this.txtPrimChooseDev = new System.Windows.Forms.Label();
            this.txtSecAuth = new System.Windows.Forms.Label();
            this.txtPrimAuth = new System.Windows.Forms.Label();
            this.cbxSecAuth = new System.Windows.Forms.ComboBox();
            this.cbxPrimAuth = new System.Windows.Forms.ComboBox();
            this.cbxBTSelect1 = new System.Windows.Forms.ComboBox();
            this.tbxPin = new System.Windows.Forms.TextBox();
            this.cbxBTSelect2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOptionsSave
            // 
            this.btnOptionsSave.Location = new System.Drawing.Point(653, 235);
            this.btnOptionsSave.Name = "btnOptionsSave";
            this.btnOptionsSave.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnOptionsSave.Size = new System.Drawing.Size(119, 32);
            this.btnOptionsSave.TabIndex = 4;
            this.btnOptionsSave.Text = "Save";
            this.btnOptionsSave.UseVisualStyleBackColor = true;
            this.btnOptionsSave.Click += new System.EventHandler(this.btnOptionsSave_Click);
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.Location = new System.Drawing.Point(12, 121);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnEditProfile.Size = new System.Drawing.Size(119, 32);
            this.btnEditProfile.TabIndex = 5;
            this.btnEditProfile.Text = "Profile";
            this.btnEditProfile.UseVisualStyleBackColor = true;
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // btnEditAuth
            // 
            this.btnEditAuth.Location = new System.Drawing.Point(12, 159);
            this.btnEditAuth.Name = "btnEditAuth";
            this.btnEditAuth.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnEditAuth.Size = new System.Drawing.Size(119, 32);
            this.btnEditAuth.TabIndex = 6;
            this.btnEditAuth.Text = "Authentication";
            this.btnEditAuth.UseVisualStyleBackColor = true;
            this.btnEditAuth.Click += new System.EventHandler(this.btnEditAuth_Click);
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Location = new System.Drawing.Point(12, 197);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRemoveUser.Size = new System.Drawing.Size(119, 32);
            this.btnRemoveUser.TabIndex = 7;
            this.btnRemoveUser.Text = "Remove user";
            this.btnRemoveUser.UseVisualStyleBackColor = true;
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // txtEditProfile
            // 
            this.txtEditProfile.AutoSize = true;
            this.txtEditProfile.Location = new System.Drawing.Point(137, 131);
            this.txtEditProfile.Name = "txtEditProfile";
            this.txtEditProfile.Size = new System.Drawing.Size(128, 13);
            this.txtEditProfile.TabIndex = 8;
            this.txtEditProfile.Text = "Edit name, phone, picture";
            // 
            // txtEditAuth
            // 
            this.txtEditAuth.AutoSize = true;
            this.txtEditAuth.Location = new System.Drawing.Point(137, 169);
            this.txtEditAuth.Name = "txtEditAuth";
            this.txtEditAuth.Size = new System.Drawing.Size(161, 13);
            this.txtEditAuth.TabIndex = 9;
            this.txtEditAuth.Text = "Edit your authentication methods";
            // 
            // txtRemoveUser
            // 
            this.txtRemoveUser.AutoSize = true;
            this.txtRemoveUser.Location = new System.Drawing.Point(137, 207);
            this.txtRemoveUser.Name = "txtRemoveUser";
            this.txtRemoveUser.Size = new System.Drawing.Size(185, 13);
            this.txtRemoveUser.TabIndex = 10;
            this.txtRemoveUser.Text = "Permanently delete your user account";
            // 
            // tbxStatus
            // 
            this.tbxStatus.AutoSize = true;
            this.tbxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.tbxStatus.Location = new System.Drawing.Point(12, 9);
            this.tbxStatus.Name = "tbxStatus";
            this.tbxStatus.Size = new System.Drawing.Size(190, 26);
            this.tbxStatus.TabIndex = 11;
            this.tbxStatus.Text = "Welcome, <user>.";
            // 
            // txtOptionsTitle
            // 
            this.txtOptionsTitle.AutoSize = true;
            this.txtOptionsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtOptionsTitle.Location = new System.Drawing.Point(12, 83);
            this.txtOptionsTitle.Name = "txtOptionsTitle";
            this.txtOptionsTitle.Size = new System.Drawing.Size(248, 26);
            this.txtOptionsTitle.TabIndex = 12;
            this.txtOptionsTitle.Text = "BuzzLock Options Menu";
            // 
            // txtUserPermission
            // 
            this.txtUserPermission.AutoSize = true;
            this.txtUserPermission.Location = new System.Drawing.Point(15, 39);
            this.txtUserPermission.Name = "txtUserPermission";
            this.txtUserPermission.Size = new System.Drawing.Size(104, 13);
            this.txtUserPermission.TabIndex = 13;
            this.txtUserPermission.Text = "Permission Level: <>";
            // 
            // timerOptionsTimeout
            // 
            this.timerOptionsTimeout.Interval = 30000;
            this.timerOptionsTimeout.Tick += new System.EventHandler(this.timerOptionsTimeout_Tick);
            // 
            // txtOptionsStatus
            // 
            this.txtOptionsStatus.AutoSize = true;
            this.txtOptionsStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOptionsStatus.Location = new System.Drawing.Point(15, 61);
            this.txtOptionsStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtOptionsStatus.Name = "txtOptionsStatus";
            this.txtOptionsStatus.Size = new System.Drawing.Size(165, 17);
            this.txtOptionsStatus.TabIndex = 14;
            this.txtOptionsStatus.Text = "30 seconds until timeout.";
            // 
            // timerOptionsStatus
            // 
            this.timerOptionsStatus.Interval = 200;
            this.timerOptionsStatus.Tick += new System.EventHandler(this.timerOptionsStatus_Tick);
            // 
            // txtCurrentName
            // 
            this.txtCurrentName.AutoSize = true;
            this.txtCurrentName.Location = new System.Drawing.Point(15, 132);
            this.txtCurrentName.Name = "txtCurrentName";
            this.txtCurrentName.Size = new System.Drawing.Size(73, 13);
            this.txtCurrentName.TabIndex = 15;
            this.txtCurrentName.Text = "Current name:";
            this.txtCurrentName.Click += new System.EventHandler(this.txtCurrentName_Click);
            // 
            // txtNewName
            // 
            this.txtNewName.AutoSize = true;
            this.txtNewName.Location = new System.Drawing.Point(237, 132);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(61, 13);
            this.txtNewName.TabIndex = 16;
            this.txtNewName.Text = "New name:";
            // 
            // txtCurrentPhone
            // 
            this.txtCurrentPhone.AutoSize = true;
            this.txtCurrentPhone.Location = new System.Drawing.Point(14, 190);
            this.txtCurrentPhone.Name = "txtCurrentPhone";
            this.txtCurrentPhone.Size = new System.Drawing.Size(115, 13);
            this.txtCurrentPhone.TabIndex = 17;
            this.txtCurrentPhone.Text = "Current phone number:";
            // 
            // txtNewPhone
            // 
            this.txtNewPhone.AutoSize = true;
            this.txtNewPhone.Location = new System.Drawing.Point(237, 190);
            this.txtNewPhone.Name = "txtNewPhone";
            this.txtNewPhone.Size = new System.Drawing.Size(103, 13);
            this.txtNewPhone.TabIndex = 18;
            this.txtNewPhone.Text = "New phone number:";
            // 
            // tbxCurrentName
            // 
            this.tbxCurrentName.Location = new System.Drawing.Point(17, 149);
            this.tbxCurrentName.Name = "tbxCurrentName";
            this.tbxCurrentName.ReadOnly = true;
            this.tbxCurrentName.Size = new System.Drawing.Size(185, 20);
            this.tbxCurrentName.TabIndex = 19;
            this.tbxCurrentName.TextChanged += new System.EventHandler(this.tbxCurrentName_TextChanged);
            // 
            // tbxNewName
            // 
            this.tbxNewName.Location = new System.Drawing.Point(240, 149);
            this.tbxNewName.Name = "tbxNewName";
            this.tbxNewName.Size = new System.Drawing.Size(185, 20);
            this.tbxNewName.TabIndex = 20;
            this.tbxNewName.TextChanged += new System.EventHandler(this.validateNewName);
            // 
            // tbxNewPhone
            // 
            this.tbxNewPhone.Location = new System.Drawing.Point(241, 206);
            this.tbxNewPhone.Name = "tbxNewPhone";
            this.tbxNewPhone.Size = new System.Drawing.Size(185, 20);
            this.tbxNewPhone.TabIndex = 22;
            this.tbxNewPhone.TextChanged += new System.EventHandler(this.validateNewPhone);
            // 
            // tbxCurrentPhone
            // 
            this.tbxCurrentPhone.Location = new System.Drawing.Point(18, 206);
            this.tbxCurrentPhone.Name = "tbxCurrentPhone";
            this.tbxCurrentPhone.ReadOnly = true;
            this.tbxCurrentPhone.Size = new System.Drawing.Size(185, 20);
            this.tbxCurrentPhone.TabIndex = 21;
            // 
            // btnChangeName
            // 
            this.btnChangeName.Location = new System.Drawing.Point(457, 142);
            this.btnChangeName.Name = "btnChangeName";
            this.btnChangeName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnChangeName.Size = new System.Drawing.Size(119, 32);
            this.btnChangeName.TabIndex = 23;
            this.btnChangeName.Text = "Change name";
            this.btnChangeName.UseVisualStyleBackColor = true;
            this.btnChangeName.Click += new System.EventHandler(this.btnChangeName_Click);
            // 
            // btnChangePhone
            // 
            this.btnChangePhone.Location = new System.Drawing.Point(457, 194);
            this.btnChangePhone.Name = "btnChangePhone";
            this.btnChangePhone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnChangePhone.Size = new System.Drawing.Size(119, 32);
            this.btnChangePhone.TabIndex = 24;
            this.btnChangePhone.Text = "Change phone";
            this.btnChangePhone.UseVisualStyleBackColor = true;
            this.btnChangePhone.Click += new System.EventHandler(this.btnChangePhone_Click);
            // 
            // txtSecChooseDevOrPin
            // 
            this.txtSecChooseDevOrPin.AutoSize = true;
            this.txtSecChooseDevOrPin.Location = new System.Drawing.Point(307, 275);
            this.txtSecChooseDevOrPin.Name = "txtSecChooseDevOrPin";
            this.txtSecChooseDevOrPin.Size = new System.Drawing.Size(57, 13);
            this.txtSecChooseDevOrPin.TabIndex = 33;
            this.txtSecChooseDevOrPin.Text = "Insert PIN:";
            this.txtSecChooseDevOrPin.Visible = false;
            // 
            // txtPrimChooseDev
            // 
            this.txtPrimChooseDev.AutoSize = true;
            this.txtPrimChooseDev.Location = new System.Drawing.Point(15, 275);
            this.txtPrimChooseDev.Name = "txtPrimChooseDev";
            this.txtPrimChooseDev.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPrimChooseDev.Size = new System.Drawing.Size(81, 13);
            this.txtPrimChooseDev.TabIndex = 30;
            this.txtPrimChooseDev.Text = "Choose device:";
            this.txtPrimChooseDev.Visible = false;
            // 
            // txtSecAuth
            // 
            this.txtSecAuth.AutoSize = true;
            this.txtSecAuth.Location = new System.Drawing.Point(307, 249);
            this.txtSecAuth.Name = "txtSecAuth";
            this.txtSecAuth.Size = new System.Drawing.Size(131, 13);
            this.txtSecAuth.TabIndex = 29;
            this.txtSecAuth.Text = "Secondary authentication:";
            this.txtSecAuth.Visible = false;
            // 
            // txtPrimAuth
            // 
            this.txtPrimAuth.AutoSize = true;
            this.txtPrimAuth.Location = new System.Drawing.Point(15, 249);
            this.txtPrimAuth.Name = "txtPrimAuth";
            this.txtPrimAuth.Size = new System.Drawing.Size(114, 13);
            this.txtPrimAuth.TabIndex = 28;
            this.txtPrimAuth.Text = "Primary authentication:";
            this.txtPrimAuth.Visible = false;
            // 
            // cbxSecAuth
            // 
            this.cbxSecAuth.FormattingEnabled = true;
            this.cbxSecAuth.Items.AddRange(new object[] {
            "Bluetooth",
            "PIN"});
            this.cbxSecAuth.Location = new System.Drawing.Point(444, 245);
            this.cbxSecAuth.Name = "cbxSecAuth";
            this.cbxSecAuth.Size = new System.Drawing.Size(138, 21);
            this.cbxSecAuth.TabIndex = 27;
            this.cbxSecAuth.Visible = false;
            // 
            // cbxPrimAuth
            // 
            this.cbxPrimAuth.FormattingEnabled = true;
            this.cbxPrimAuth.Items.AddRange(new object[] {
            "Card",
            "Bluetooth"});
            this.cbxPrimAuth.Location = new System.Drawing.Point(135, 245);
            this.cbxPrimAuth.Name = "cbxPrimAuth";
            this.cbxPrimAuth.Size = new System.Drawing.Size(137, 21);
            this.cbxPrimAuth.TabIndex = 26;
            this.cbxPrimAuth.Visible = false;
            // 
            // cbxBTSelect1
            // 
            this.cbxBTSelect1.FormattingEnabled = true;
            this.cbxBTSelect1.Location = new System.Drawing.Point(135, 271);
            this.cbxBTSelect1.Name = "cbxBTSelect1";
            this.cbxBTSelect1.Size = new System.Drawing.Size(137, 21);
            this.cbxBTSelect1.TabIndex = 31;
            this.cbxBTSelect1.Visible = false;
            // 
            // tbxPin
            // 
            this.tbxPin.Location = new System.Drawing.Point(444, 273);
            this.tbxPin.MaxLength = 6;
            this.tbxPin.Name = "tbxPin";
            this.tbxPin.Size = new System.Drawing.Size(138, 20);
            this.tbxPin.TabIndex = 32;
            this.tbxPin.Visible = false;
            // 
            // cbxBTSelect2
            // 
            this.cbxBTSelect2.FormattingEnabled = true;
            this.cbxBTSelect2.Location = new System.Drawing.Point(444, 273);
            this.cbxBTSelect2.Name = "cbxBTSelect2";
            this.cbxBTSelect2.Size = new System.Drawing.Size(138, 21);
            this.cbxBTSelect2.TabIndex = 34;
            this.cbxBTSelect2.Visible = false;
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.cbxBTSelect2);
            this.Controls.Add(this.txtSecChooseDevOrPin);
            this.Controls.Add(this.txtPrimChooseDev);
            this.Controls.Add(this.txtSecAuth);
            this.Controls.Add(this.txtPrimAuth);
            this.Controls.Add(this.cbxSecAuth);
            this.Controls.Add(this.cbxPrimAuth);
            this.Controls.Add(this.cbxBTSelect1);
            this.Controls.Add(this.tbxPin);
            this.Controls.Add(this.btnChangePhone);
            this.Controls.Add(this.btnChangeName);
            this.Controls.Add(this.tbxNewPhone);
            this.Controls.Add(this.tbxCurrentPhone);
            this.Controls.Add(this.tbxNewName);
            this.Controls.Add(this.tbxCurrentName);
            this.Controls.Add(this.txtNewPhone);
            this.Controls.Add(this.txtCurrentPhone);
            this.Controls.Add(this.txtNewName);
            this.Controls.Add(this.txtCurrentName);
            this.Controls.Add(this.txtOptionsStatus);
            this.Controls.Add(this.txtUserPermission);
            this.Controls.Add(this.txtOptionsTitle);
            this.Controls.Add(this.tbxStatus);
            this.Controls.Add(this.txtRemoveUser);
            this.Controls.Add(this.txtEditAuth);
            this.Controls.Add(this.txtEditProfile);
            this.Controls.Add(this.btnRemoveUser);
            this.Controls.Add(this.btnEditAuth);
            this.Controls.Add(this.btnEditProfile);
            this.Controls.Add(this.btnOptionsSave);
            this.Name = "FormOptions";
            this.Text = "BuzzLock Options";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormOptions_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormOptions_MouseClick);
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
        private System.Windows.Forms.Label tbxStatus;
        private System.Windows.Forms.Label txtOptionsTitle;
        private System.Windows.Forms.Label txtUserPermission;
        private System.Windows.Forms.Label txtOptionsStatus;
        public System.Windows.Forms.Timer timerOptionsTimeout;
        public System.Windows.Forms.Timer timerOptionsStatus;
        private System.Windows.Forms.Label txtCurrentName;
        private System.Windows.Forms.Label txtNewName;
        private System.Windows.Forms.Label txtCurrentPhone;
        private System.Windows.Forms.Label txtNewPhone;
        private System.Windows.Forms.TextBox tbxCurrentName;
        private System.Windows.Forms.TextBox tbxNewName;
        private System.Windows.Forms.TextBox tbxNewPhone;
        private System.Windows.Forms.TextBox tbxCurrentPhone;
        private System.Windows.Forms.Button btnChangeName;
        private System.Windows.Forms.Button btnChangePhone;
        private System.Windows.Forms.Label txtSecChooseDevOrPin;
        private System.Windows.Forms.Label txtPrimChooseDev;
        private System.Windows.Forms.Label txtSecAuth;
        private System.Windows.Forms.Label txtPrimAuth;
        private System.Windows.Forms.ComboBox cbxSecAuth;
        private System.Windows.Forms.ComboBox cbxPrimAuth;
        private System.Windows.Forms.ComboBox cbxBTSelect1;
        private System.Windows.Forms.TextBox tbxPin;
        private System.Windows.Forms.ComboBox cbxBTSelect2;
    }
}