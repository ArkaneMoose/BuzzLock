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
            this.btnRemoveUserOrCancel = new System.Windows.Forms.Button();
            this.btnAddNewUser = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClearTextBox = new System.Windows.Forms.Button();
            this.txtAddNewUserStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.userError)).BeginInit();
            this.SuspendLayout();
            // 
            // listUsers
            // 
            this.listUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.listUsers.FormattingEnabled = true;
            this.listUsers.ItemHeight = 16;
            this.listUsers.Location = new System.Drawing.Point(12, 12);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(137, 386);
            this.listUsers.Sorted = true;
            this.listUsers.TabIndex = 40;
            this.listUsers.SelectedIndexChanged += new System.EventHandler(this.listUsers_SelectedIndexChanged);
            // 
            // btnSaveUser
            // 
            this.btnSaveUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveUser.Location = new System.Drawing.Point(538, 217);
            this.btnSaveUser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSaveUser.Name = "btnSaveUser";
            this.btnSaveUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSaveUser.Size = new System.Drawing.Size(128, 33);
            this.btnSaveUser.TabIndex = 41;
            this.btnSaveUser.Text = "Save changes";
            this.btnSaveUser.UseVisualStyleBackColor = true;
            this.btnSaveUser.Click += new System.EventHandler(this.btnSaveUser_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtStatus.Location = new System.Drawing.Point(164, 13);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(492, 26);
            this.txtStatus.TabIndex = 42;
            this.txtStatus.Text = "Click on a User to see details and perform actions";
            // 
            // txtPrimAuth
            // 
            this.txtPrimAuth.AutoSize = true;
            this.txtPrimAuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrimAuth.Location = new System.Drawing.Point(165, 130);
            this.txtPrimAuth.Name = "txtPrimAuth";
            this.txtPrimAuth.Size = new System.Drawing.Size(101, 34);
            this.txtPrimAuth.TabIndex = 43;
            this.txtPrimAuth.Text = "Primary\r\nauthentication:";
            // 
            // cbxPrimAuth
            // 
            this.cbxPrimAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrimAuth.FormattingEnabled = true;
            this.cbxPrimAuth.Items.AddRange(new object[] {
            "Card",
            "Bluetooth"});
            this.cbxPrimAuth.Location = new System.Drawing.Point(274, 143);
            this.cbxPrimAuth.Name = "cbxPrimAuth";
            this.cbxPrimAuth.Size = new System.Drawing.Size(133, 21);
            this.cbxPrimAuth.TabIndex = 44;
            this.cbxPrimAuth.SelectedIndexChanged += new System.EventHandler(this.ModifyPrimaryAuthConfiguration);
            this.cbxPrimAuth.SelectedValueChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // txtSecAuth
            // 
            this.txtSecAuth.AutoSize = true;
            this.txtSecAuth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecAuth.Location = new System.Drawing.Point(428, 130);
            this.txtSecAuth.Name = "txtSecAuth";
            this.txtSecAuth.Size = new System.Drawing.Size(101, 34);
            this.txtSecAuth.TabIndex = 46;
            this.txtSecAuth.Text = "Secondary\r\nauthentication:";
            // 
            // cbxSecAuth
            // 
            this.cbxSecAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSecAuth.FormattingEnabled = true;
            this.cbxSecAuth.Items.AddRange(new object[] {
            "Bluetooth",
            "PIN"});
            this.cbxSecAuth.Location = new System.Drawing.Point(533, 143);
            this.cbxSecAuth.Name = "cbxSecAuth";
            this.cbxSecAuth.Size = new System.Drawing.Size(133, 21);
            this.cbxSecAuth.TabIndex = 45;
            this.cbxSecAuth.SelectedIndexChanged += new System.EventHandler(this.ModifySecondaryAuthConfiguration);
            this.cbxSecAuth.SelectedValueChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // txtPrimChooseDev
            // 
            this.txtPrimChooseDev.AutoSize = true;
            this.txtPrimChooseDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrimChooseDev.Location = new System.Drawing.Point(166, 171);
            this.txtPrimChooseDev.Name = "txtPrimChooseDev";
            this.txtPrimChooseDev.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPrimChooseDev.Size = new System.Drawing.Size(72, 34);
            this.txtPrimChooseDev.TabIndex = 47;
            this.txtPrimChooseDev.Text = "Bluetooth \r\ndevice:";
            this.txtPrimChooseDev.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.txtPrimChooseDev.Visible = false;
            // 
            // cbxBTSelect1
            // 
            this.cbxBTSelect1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect1.FormattingEnabled = true;
            this.cbxBTSelect1.Location = new System.Drawing.Point(274, 182);
            this.cbxBTSelect1.Name = "cbxBTSelect1";
            this.cbxBTSelect1.Size = new System.Drawing.Size(133, 21);
            this.cbxBTSelect1.TabIndex = 48;
            this.cbxBTSelect1.Visible = false;
            this.cbxBTSelect1.SelectedIndexChanged += new System.EventHandler(this.ValidateComboBox);
            this.cbxBTSelect1.SelectedValueChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // txtSecChooseDevOrPin
            // 
            this.txtSecChooseDevOrPin.AutoSize = true;
            this.txtSecChooseDevOrPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecChooseDevOrPin.Location = new System.Drawing.Point(428, 171);
            this.txtSecChooseDevOrPin.Name = "txtSecChooseDevOrPin";
            this.txtSecChooseDevOrPin.Size = new System.Drawing.Size(72, 34);
            this.txtSecChooseDevOrPin.TabIndex = 49;
            this.txtSecChooseDevOrPin.Text = "Bluetooth \r\ndevice:";
            this.txtSecChooseDevOrPin.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.txtSecChooseDevOrPin.Visible = false;
            // 
            // cbxBTSelect2
            // 
            this.cbxBTSelect2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect2.FormattingEnabled = true;
            this.cbxBTSelect2.Location = new System.Drawing.Point(533, 182);
            this.cbxBTSelect2.Name = "cbxBTSelect2";
            this.cbxBTSelect2.Size = new System.Drawing.Size(133, 21);
            this.cbxBTSelect2.TabIndex = 50;
            this.cbxBTSelect2.Visible = false;
            this.cbxBTSelect2.SelectedIndexChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // tbxPin
            // 
            this.tbxPin.Location = new System.Drawing.Point(533, 183);
            this.tbxPin.MaxLength = 6;
            this.tbxPin.Name = "tbxPin";
            this.tbxPin.Size = new System.Drawing.Size(133, 20);
            this.tbxPin.TabIndex = 51;
            this.tbxPin.Visible = false;
            this.tbxPin.Click += new System.EventHandler(this.numberpad_Click);
            this.tbxPin.TextChanged += new System.EventHandler(this.ValidatePinBox);
            // 
            // txtCard
            // 
            this.txtCard.AutoSize = true;
            this.txtCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCard.Location = new System.Drawing.Point(166, 105);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(42, 17);
            this.txtCard.TabIndex = 52;
            this.txtCard.Text = "Card:";
            // 
            // tbxCard
            // 
            this.tbxCard.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbxCard.Location = new System.Drawing.Point(218, 105);
            this.tbxCard.Name = "tbxCard";
            this.tbxCard.ReadOnly = true;
            this.tbxCard.Size = new System.Drawing.Size(446, 20);
            this.tbxCard.TabIndex = 53;
            this.tbxCard.Tag = "Please swipe a card to populate this field.";
            this.tbxCard.TextChanged += new System.EventHandler(this.ValidateTextBox);
            // 
            // txtUserPhone
            // 
            this.txtUserPhone.AutoSize = true;
            this.txtUserPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserPhone.Location = new System.Drawing.Point(165, 78);
            this.txtUserPhone.Name = "txtUserPhone";
            this.txtUserPhone.Size = new System.Drawing.Size(53, 17);
            this.txtUserPhone.TabIndex = 55;
            this.txtUserPhone.Text = "Phone:";
            // 
            // txtUserName
            // 
            this.txtUserName.AutoSize = true;
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(165, 52);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(49, 17);
            this.txtUserName.TabIndex = 54;
            this.txtUserName.Text = "Name:";
            // 
            // tbxUserName
            // 
            this.tbxUserName.Location = new System.Drawing.Point(218, 52);
            this.tbxUserName.MaxLength = 20;
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(222, 20);
            this.tbxUserName.TabIndex = 56;
            this.tbxUserName.Tag = "Please enter your full name.";
            this.tbxUserName.Click += new System.EventHandler(this.keyboard_Click);
            this.tbxUserName.TextChanged += new System.EventHandler(this.ValidateTextBox);
            // 
            // tbxUserPhone
            // 
            this.tbxUserPhone.Location = new System.Drawing.Point(218, 78);
            this.tbxUserPhone.MaxLength = 10;
            this.tbxUserPhone.Name = "tbxUserPhone";
            this.tbxUserPhone.Size = new System.Drawing.Size(222, 20);
            this.tbxUserPhone.TabIndex = 57;
            this.tbxUserPhone.Click += new System.EventHandler(this.numberpad_Click);
            this.tbxUserPhone.TextChanged += new System.EventHandler(this.ValidatePhoneBox);
            // 
            // cbxUserPermission
            // 
            this.cbxUserPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUserPermission.FormattingEnabled = true;
            this.cbxUserPermission.Items.AddRange(new object[] {
            "FULL",
            "LIMITED",
            "NONE"});
            this.cbxUserPermission.Location = new System.Drawing.Point(553, 77);
            this.cbxUserPermission.Name = "cbxUserPermission";
            this.cbxUserPermission.Size = new System.Drawing.Size(111, 21);
            this.cbxUserPermission.TabIndex = 59;
            this.cbxUserPermission.SelectedValueChanged += new System.EventHandler(this.ValidateComboBox);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(550, 59);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 58;
            this.label1.Text = "Permission:";
            // 
            // btnRemoveUserOrCancel
            // 
            this.btnRemoveUserOrCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveUserOrCancel.Location = new System.Drawing.Point(168, 217);
            this.btnRemoveUserOrCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRemoveUserOrCancel.Name = "btnRemoveUserOrCancel";
            this.btnRemoveUserOrCancel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRemoveUserOrCancel.Size = new System.Drawing.Size(128, 33);
            this.btnRemoveUserOrCancel.TabIndex = 60;
            this.btnRemoveUserOrCancel.Text = "Remove this user";
            this.btnRemoveUserOrCancel.UseVisualStyleBackColor = true;
            this.btnRemoveUserOrCancel.Click += new System.EventHandler(this.btnRemoveUserOrCancel_Click);
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewUser.Location = new System.Drawing.Point(351, 217);
            this.btnAddNewUser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnAddNewUser.Size = new System.Drawing.Size(128, 33);
            this.btnAddNewUser.TabIndex = 61;
            this.btnAddNewUser.Text = "Add a new user";
            this.btnAddNewUser.UseVisualStyleBackColor = true;
            this.btnAddNewUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(168, 266);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnClose.Size = new System.Drawing.Size(166, 48);
            this.btnClose.TabIndex = 62;
            this.btnClose.Text = "Close User Management";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClearTextBox
            // 
            this.btnClearTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearTextBox.Location = new System.Drawing.Point(451, 51);
            this.btnClearTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClearTextBox.Name = "btnClearTextBox";
            this.btnClearTextBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnClearTextBox.Size = new System.Drawing.Size(93, 47);
            this.btnClearTextBox.TabIndex = 63;
            this.btnClearTextBox.Text = "Clear";
            this.btnClearTextBox.UseVisualStyleBackColor = true;
            this.btnClearTextBox.Visible = false;
            this.btnClearTextBox.Click += new System.EventHandler(this.btnClearTextBox_Click);
            // 
            // txtAddNewUserStatus
            // 
            this.txtAddNewUserStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddNewUserStatus.Location = new System.Drawing.Point(165, 329);
            this.txtAddNewUserStatus.Name = "txtAddNewUserStatus";
            this.txtAddNewUserStatus.Size = new System.Drawing.Size(511, 52);
            this.txtAddNewUserStatus.TabIndex = 64;
            this.txtAddNewUserStatus.Text = "Cannot add an authentication method which already exists in the database. Please " +
    "try again.";
            this.txtAddNewUserStatus.Visible = false;
            // 
            // FormUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.txtAddNewUserStatus);
            this.Controls.Add(this.btnClearTextBox);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddNewUser);
            this.Controls.Add(this.btnRemoveUserOrCancel);
            this.Controls.Add(this.cbxUserPermission);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxUserPhone);
            this.Controls.Add(this.tbxUserName);
            this.Controls.Add(this.txtUserPhone);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.tbxCard);
            this.Controls.Add(this.txtCard);
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
            this.Controls.Add(this.tbxPin);
            this.Controls.Add(this.cbxBTSelect2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormUserManagement";
            this.Text = "User Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.formUserManagement_Activated);
            this.Load += new System.EventHandler(this.FormUserManagement_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormUserManagement_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormUserManagement_MouseClick);
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
        private System.Windows.Forms.Button btnRemoveUserOrCancel;
        private System.Windows.Forms.Button btnAddNewUser;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClearTextBox;
        private System.Windows.Forms.Label txtAddNewUserStatus;
    }
}
