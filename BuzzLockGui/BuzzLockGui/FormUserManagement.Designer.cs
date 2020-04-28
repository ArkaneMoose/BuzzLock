namespace BuzzLockGui
{
    partial class FormUserManagement
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
            this.listUsers = new System.Windows.Forms.ListBox();
            this.btnSaveUser = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.Label();
            this.txtPrimAuth = new System.Windows.Forms.Label();
            this.cbxPrimAuth = new System.Windows.Forms.ComboBox();
            this.txtSecAuth = new System.Windows.Forms.Label();
            this.cbxSecAuth = new System.Windows.Forms.ComboBox();
            this.txtPrimChooseDev = new System.Windows.Forms.Label();
            this.cbxBTSelect1 = new System.Windows.Forms.ComboBox();
            this.txtSecChooseDevOrPin = new System.Windows.Forms.Label();
            this.cbxBTSelect2 = new System.Windows.Forms.ComboBox();
            this.tbxPin = new System.Windows.Forms.TextBox();
            this.txtCard = new System.Windows.Forms.Label();
            this.tbxCard = new System.Windows.Forms.TextBox();
            this.txtUserPhone = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.Label();
            this.tbxUserName = new System.Windows.Forms.TextBox();
            this.tbxUserPhone = new System.Windows.Forms.TextBox();
            this.cbxUserPermission = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemoveUser = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.userError)).BeginInit();
            this.SuspendLayout();
            // 
            // listUsers
            // 
            this.listUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listUsers.FormattingEnabled = true;
            this.listUsers.ItemHeight = 31;
            this.listUsers.Location = new System.Drawing.Point(24, 23);
            this.listUsers.Margin = new System.Windows.Forms.Padding(6);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(398, 777);
            this.listUsers.Sorted = true;
            this.listUsers.TabIndex = 40;
            this.listUsers.SelectedIndexChanged += new System.EventHandler(this.listUsers_SelectedIndexChanged);
            // 
            // btnSaveUser
            // 
            this.btnSaveUser.Location = new System.Drawing.Point(1310, 390);
            this.btnSaveUser.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSaveUser.Name = "btnSaveUser";
            this.btnSaveUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSaveUser.Size = new System.Drawing.Size(236, 62);
            this.btnSaveUser.TabIndex = 41;
            this.btnSaveUser.Text = "Save user";
            this.btnSaveUser.UseVisualStyleBackColor = true;
            this.btnSaveUser.Click += new System.EventHandler(this.btnOptionsSave_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtStatus.Location = new System.Drawing.Point(436, 23);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(985, 52);
            this.txtStatus.TabIndex = 42;
            this.txtStatus.Text = "Click on a User to see details and perform actions";
            // 
            // txtPrimAuth
            // 
            this.txtPrimAuth.AutoSize = true;
            this.txtPrimAuth.Location = new System.Drawing.Point(444, 256);
            this.txtPrimAuth.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtPrimAuth.Name = "txtPrimAuth";
            this.txtPrimAuth.Size = new System.Drawing.Size(232, 25);
            this.txtPrimAuth.TabIndex = 43;
            this.txtPrimAuth.Text = "Primary authentication:";
            // 
            // cbxPrimAuth
            // 
            this.cbxPrimAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrimAuth.FormattingEnabled = true;
            this.cbxPrimAuth.Items.AddRange(new object[] {
            "Card",
            "Bluetooth"});
            this.cbxPrimAuth.Location = new System.Drawing.Point(684, 250);
            this.cbxPrimAuth.Margin = new System.Windows.Forms.Padding(6);
            this.cbxPrimAuth.Name = "cbxPrimAuth";
            this.cbxPrimAuth.Size = new System.Drawing.Size(262, 33);
            this.cbxPrimAuth.TabIndex = 44;
            this.cbxPrimAuth.SelectedIndexChanged += new System.EventHandler(this.ModifyPrimaryAuthConfiguration);
            this.cbxPrimAuth.SelectedValueChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // txtSecAuth
            // 
            this.txtSecAuth.AutoSize = true;
            this.txtSecAuth.Location = new System.Drawing.Point(992, 254);
            this.txtSecAuth.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtSecAuth.Name = "txtSecAuth";
            this.txtSecAuth.Size = new System.Drawing.Size(262, 25);
            this.txtSecAuth.TabIndex = 46;
            this.txtSecAuth.Text = "Secondary authentication:";
            // 
            // cbxSecAuth
            // 
            this.cbxSecAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSecAuth.FormattingEnabled = true;
            this.cbxSecAuth.Items.AddRange(new object[] {
            "Bluetooth",
            "PIN"});
            this.cbxSecAuth.Location = new System.Drawing.Point(1266, 248);
            this.cbxSecAuth.Margin = new System.Windows.Forms.Padding(6);
            this.cbxSecAuth.Name = "cbxSecAuth";
            this.cbxSecAuth.Size = new System.Drawing.Size(262, 33);
            this.cbxSecAuth.TabIndex = 45;
            this.cbxSecAuth.SelectedIndexChanged += new System.EventHandler(this.ModifySecondaryAuthConfiguration);
            this.cbxSecAuth.SelectedValueChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // txtPrimChooseDev
            // 
            this.txtPrimChooseDev.AutoSize = true;
            this.txtPrimChooseDev.Location = new System.Drawing.Point(444, 308);
            this.txtPrimChooseDev.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtPrimChooseDev.Name = "txtPrimChooseDev";
            this.txtPrimChooseDev.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPrimChooseDev.Size = new System.Drawing.Size(178, 25);
            this.txtPrimChooseDev.TabIndex = 47;
            this.txtPrimChooseDev.Text = "Bluetooth device:";
            this.txtPrimChooseDev.Visible = false;
            // 
            // cbxBTSelect1
            // 
            this.cbxBTSelect1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect1.FormattingEnabled = true;
            this.cbxBTSelect1.Location = new System.Drawing.Point(684, 302);
            this.cbxBTSelect1.Margin = new System.Windows.Forms.Padding(6);
            this.cbxBTSelect1.Name = "cbxBTSelect1";
            this.cbxBTSelect1.Size = new System.Drawing.Size(262, 33);
            this.cbxBTSelect1.TabIndex = 48;
            this.cbxBTSelect1.Visible = false;
            this.cbxBTSelect1.SelectedValueChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // txtSecChooseDevOrPin
            // 
            this.txtSecChooseDevOrPin.AutoSize = true;
            this.txtSecChooseDevOrPin.Location = new System.Drawing.Point(992, 306);
            this.txtSecChooseDevOrPin.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtSecChooseDevOrPin.Name = "txtSecChooseDevOrPin";
            this.txtSecChooseDevOrPin.Size = new System.Drawing.Size(103, 25);
            this.txtSecChooseDevOrPin.TabIndex = 49;
            this.txtSecChooseDevOrPin.Text = "User PIN:";
            this.txtSecChooseDevOrPin.Visible = false;
            // 
            // cbxBTSelect2
            // 
            this.cbxBTSelect2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect2.FormattingEnabled = true;
            this.cbxBTSelect2.Location = new System.Drawing.Point(1266, 306);
            this.cbxBTSelect2.Margin = new System.Windows.Forms.Padding(6);
            this.cbxBTSelect2.Name = "cbxBTSelect2";
            this.cbxBTSelect2.Size = new System.Drawing.Size(262, 33);
            this.cbxBTSelect2.TabIndex = 50;
            this.cbxBTSelect2.Visible = false;
            // 
            // tbxPin
            // 
            this.tbxPin.Location = new System.Drawing.Point(1266, 308);
            this.tbxPin.Margin = new System.Windows.Forms.Padding(6);
            this.tbxPin.MaxLength = 6;
            this.tbxPin.Name = "tbxPin";
            this.tbxPin.Size = new System.Drawing.Size(262, 31);
            this.tbxPin.TabIndex = 51;
            this.tbxPin.Visible = false;
            this.tbxPin.Click += new System.EventHandler(this.numberpad_Click);
            this.tbxPin.TextChanged += new System.EventHandler(this.ValidatePinBox);
            this.tbxPin.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // txtCard
            // 
            this.txtCard.AutoSize = true;
            this.txtCard.Location = new System.Drawing.Point(444, 206);
            this.txtCard.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(64, 25);
            this.txtCard.TabIndex = 52;
            this.txtCard.Text = "Card:";
            // 
            // tbxCard
            // 
            this.tbxCard.Location = new System.Drawing.Point(530, 200);
            this.tbxCard.Margin = new System.Windows.Forms.Padding(6);
            this.tbxCard.Name = "tbxCard";
            this.tbxCard.ReadOnly = true;
            this.tbxCard.Size = new System.Drawing.Size(998, 31);
            this.tbxCard.TabIndex = 53;
            this.tbxCard.TextChanged += new System.EventHandler(this.ValidateTextBox);
            // 
            // txtUserPhone
            // 
            this.txtUserPhone.AutoSize = true;
            this.txtUserPhone.Location = new System.Drawing.Point(442, 154);
            this.txtUserPhone.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtUserPhone.Name = "txtUserPhone";
            this.txtUserPhone.Size = new System.Drawing.Size(80, 25);
            this.txtUserPhone.TabIndex = 55;
            this.txtUserPhone.Text = "Phone:";
            // 
            // txtUserName
            // 
            this.txtUserName.AutoSize = true;
            this.txtUserName.Location = new System.Drawing.Point(442, 104);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(74, 25);
            this.txtUserName.TabIndex = 54;
            this.txtUserName.Text = "Name:";
            // 
            // tbxUserName
            // 
            this.tbxUserName.Location = new System.Drawing.Point(530, 98);
            this.tbxUserName.Margin = new System.Windows.Forms.Padding(6);
            this.tbxUserName.MaxLength = 20;
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(538, 31);
            this.tbxUserName.TabIndex = 56;
            this.tbxUserName.Tag = "Please enter your full name.";
            this.tbxUserName.Click += new System.EventHandler(this.keyboard_Click);
            this.tbxUserName.TextChanged += new System.EventHandler(this.ValidateTextBox);
            this.tbxUserName.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // tbxUserPhone
            // 
            this.tbxUserPhone.Location = new System.Drawing.Point(530, 148);
            this.tbxUserPhone.Margin = new System.Windows.Forms.Padding(6);
            this.tbxUserPhone.MaxLength = 10;
            this.tbxUserPhone.Name = "tbxUserPhone";
            this.tbxUserPhone.Size = new System.Drawing.Size(538, 31);
            this.tbxUserPhone.TabIndex = 57;
            this.tbxUserPhone.Click += new System.EventHandler(this.numberpad_Click);
            this.tbxUserPhone.TextChanged += new System.EventHandler(this.ValidatePhoneBox);
            this.tbxUserPhone.Leave += new System.EventHandler(this.keyboardClose_Leave);
            // 
            // cbxUserPermission
            // 
            this.cbxUserPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUserPermission.FormattingEnabled = true;
            this.cbxUserPermission.Items.AddRange(new object[] {
            "FULL",
            "LIMITED",
            "NONE"});
            this.cbxUserPermission.Location = new System.Drawing.Point(1310, 98);
            this.cbxUserPermission.Margin = new System.Windows.Forms.Padding(6);
            this.cbxUserPermission.Name = "cbxUserPermission";
            this.cbxUserPermission.Size = new System.Drawing.Size(218, 33);
            this.cbxUserPermission.TabIndex = 59;
            this.cbxUserPermission.SelectedValueChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1178, 104);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(124, 25);
            this.label1.TabIndex = 58;
            this.label1.Text = "Permission:";
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Location = new System.Drawing.Point(446, 390);
            this.btnRemoveUser.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRemoveUser.Size = new System.Drawing.Size(236, 62);
            this.btnRemoveUser.TabIndex = 60;
            this.btnRemoveUser.Text = "Remove user";
            this.btnRemoveUser.UseVisualStyleBackColor = true;
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(886, 390);
            this.btnAddUser.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnAddUser.Size = new System.Drawing.Size(236, 62);
            this.btnAddUser.TabIndex = 61;
            this.btnAddUser.Text = "Add a new user";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1234, 765);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnClose.Size = new System.Drawing.Size(312, 62);
            this.btnClose.TabIndex = 62;
            this.btnClose.Text = "Close User Management";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1568, 850);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.btnRemoveUser);
            this.Controls.Add(this.cbxUserPermission);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxUserPhone);
            this.Controls.Add(this.tbxUserName);
            this.Controls.Add(this.txtUserPhone);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.tbxCard);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.tbxPin);
            this.Controls.Add(this.cbxBTSelect2);
            this.Controls.Add(this.txtSecChooseDevOrPin);
            this.Controls.Add(this.cbxBTSelect1);
            this.Controls.Add(this.txtPrimChooseDev);
            this.Controls.Add(this.txtSecAuth);
            this.Controls.Add(this.cbxSecAuth);
            this.Controls.Add(this.cbxPrimAuth);
            this.Controls.Add(this.txtPrimAuth);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnSaveUser);
            this.Controls.Add(this.listUsers);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FormUserManagement";
            this.Text = "User Management";
            this.Activated += new System.EventHandler(this.formUserManagement_Activated);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormUserManagement_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.userError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listUsers;
        private System.Windows.Forms.Button btnSaveUser;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.Label txtPrimAuth;
        private System.Windows.Forms.ComboBox cbxPrimAuth;
        private System.Windows.Forms.Label txtSecAuth;
        private System.Windows.Forms.ComboBox cbxSecAuth;
        private System.Windows.Forms.Label txtPrimChooseDev;
        private System.Windows.Forms.ComboBox cbxBTSelect1;
        private System.Windows.Forms.Label txtSecChooseDevOrPin;
        private System.Windows.Forms.ComboBox cbxBTSelect2;
        private System.Windows.Forms.TextBox tbxPin;
        private System.Windows.Forms.Label txtCard;
        private System.Windows.Forms.TextBox tbxCard;
        private System.Windows.Forms.Label txtUserPhone;
        private System.Windows.Forms.Label txtUserName;
        private System.Windows.Forms.TextBox tbxUserName;
        private System.Windows.Forms.TextBox tbxUserPhone;
        private System.Windows.Forms.ComboBox cbxUserPermission;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRemoveUser;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnClose;
    }
}