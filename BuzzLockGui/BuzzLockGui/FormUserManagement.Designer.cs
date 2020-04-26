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
            this.listUsers.ItemHeight = 16;
            this.listUsers.Location = new System.Drawing.Point(12, 12);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(200, 418);
            this.listUsers.Sorted = true;
            this.listUsers.TabIndex = 40;
            this.listUsers.SelectedIndexChanged += new System.EventHandler(this.listUsers_SelectedIndexChanged);
            // 
            // btnSaveUser
            // 
            this.btnSaveUser.Location = new System.Drawing.Point(655, 203);
            this.btnSaveUser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSaveUser.Name = "btnSaveUser";
            this.btnSaveUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSaveUser.Size = new System.Drawing.Size(118, 32);
            this.btnSaveUser.TabIndex = 41;
            this.btnSaveUser.Text = "Save user";
            this.btnSaveUser.UseVisualStyleBackColor = true;
            this.btnSaveUser.Click += new System.EventHandler(this.btnOptionsSave_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.5F);
            this.txtStatus.Location = new System.Drawing.Point(218, 12);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(492, 26);
            this.txtStatus.TabIndex = 42;
            this.txtStatus.Text = "Click on a User to see details and perform actions";
            // 
            // txtPrimAuth
            // 
            this.txtPrimAuth.AutoSize = true;
            this.txtPrimAuth.Location = new System.Drawing.Point(222, 133);
            this.txtPrimAuth.Name = "txtPrimAuth";
            this.txtPrimAuth.Size = new System.Drawing.Size(114, 13);
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
            this.cbxPrimAuth.Location = new System.Drawing.Point(342, 130);
            this.cbxPrimAuth.Name = "cbxPrimAuth";
            this.cbxPrimAuth.Size = new System.Drawing.Size(133, 21);
            this.cbxPrimAuth.TabIndex = 44;
            // 
            // txtSecAuth
            // 
            this.txtSecAuth.AutoSize = true;
            this.txtSecAuth.Location = new System.Drawing.Point(496, 132);
            this.txtSecAuth.Name = "txtSecAuth";
            this.txtSecAuth.Size = new System.Drawing.Size(131, 13);
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
            this.cbxSecAuth.Location = new System.Drawing.Point(633, 129);
            this.cbxSecAuth.Name = "cbxSecAuth";
            this.cbxSecAuth.Size = new System.Drawing.Size(133, 21);
            this.cbxSecAuth.TabIndex = 45;
            // 
            // txtPrimChooseDev
            // 
            this.txtPrimChooseDev.AutoSize = true;
            this.txtPrimChooseDev.Location = new System.Drawing.Point(222, 160);
            this.txtPrimChooseDev.Name = "txtPrimChooseDev";
            this.txtPrimChooseDev.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPrimChooseDev.Size = new System.Drawing.Size(90, 13);
            this.txtPrimChooseDev.TabIndex = 47;
            this.txtPrimChooseDev.Text = "Bluetooth device:";
            this.txtPrimChooseDev.Visible = false;
            // 
            // cbxBTSelect1
            // 
            this.cbxBTSelect1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect1.FormattingEnabled = true;
            this.cbxBTSelect1.Location = new System.Drawing.Point(342, 157);
            this.cbxBTSelect1.Name = "cbxBTSelect1";
            this.cbxBTSelect1.Size = new System.Drawing.Size(133, 21);
            this.cbxBTSelect1.TabIndex = 48;
            this.cbxBTSelect1.Visible = false;
            // 
            // txtSecChooseDevOrPin
            // 
            this.txtSecChooseDevOrPin.AutoSize = true;
            this.txtSecChooseDevOrPin.Location = new System.Drawing.Point(496, 159);
            this.txtSecChooseDevOrPin.Name = "txtSecChooseDevOrPin";
            this.txtSecChooseDevOrPin.Size = new System.Drawing.Size(53, 13);
            this.txtSecChooseDevOrPin.TabIndex = 49;
            this.txtSecChooseDevOrPin.Text = "User PIN:";
            this.txtSecChooseDevOrPin.Visible = false;
            // 
            // cbxBTSelect2
            // 
            this.cbxBTSelect2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBTSelect2.FormattingEnabled = true;
            this.cbxBTSelect2.Location = new System.Drawing.Point(633, 159);
            this.cbxBTSelect2.Name = "cbxBTSelect2";
            this.cbxBTSelect2.Size = new System.Drawing.Size(133, 21);
            this.cbxBTSelect2.TabIndex = 50;
            this.cbxBTSelect2.Visible = false;
            // 
            // tbxPin
            // 
            this.tbxPin.Location = new System.Drawing.Point(633, 160);
            this.tbxPin.MaxLength = 6;
            this.tbxPin.Name = "tbxPin";
            this.tbxPin.Size = new System.Drawing.Size(133, 20);
            this.tbxPin.TabIndex = 51;
            this.tbxPin.Visible = false;
            // 
            // txtCard
            // 
            this.txtCard.AutoSize = true;
            this.txtCard.Location = new System.Drawing.Point(222, 107);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(32, 13);
            this.txtCard.TabIndex = 52;
            this.txtCard.Text = "Card:";
            // 
            // tbxCard
            // 
            this.tbxCard.Location = new System.Drawing.Point(265, 104);
            this.tbxCard.Name = "tbxCard";
            this.tbxCard.ReadOnly = true;
            this.tbxCard.Size = new System.Drawing.Size(501, 20);
            this.tbxCard.TabIndex = 53;
            // 
            // txtUserPhone
            // 
            this.txtUserPhone.AutoSize = true;
            this.txtUserPhone.Location = new System.Drawing.Point(221, 80);
            this.txtUserPhone.Name = "txtUserPhone";
            this.txtUserPhone.Size = new System.Drawing.Size(41, 13);
            this.txtUserPhone.TabIndex = 55;
            this.txtUserPhone.Text = "Phone:";
            // 
            // txtUserName
            // 
            this.txtUserName.AutoSize = true;
            this.txtUserName.Location = new System.Drawing.Point(221, 54);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(38, 13);
            this.txtUserName.TabIndex = 54;
            this.txtUserName.Text = "Name:";
            // 
            // tbxUserName
            // 
            this.tbxUserName.Location = new System.Drawing.Point(265, 51);
            this.tbxUserName.MaxLength = 20;
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new System.Drawing.Size(271, 20);
            this.tbxUserName.TabIndex = 56;
            // 
            // tbxUserPhone
            // 
            this.tbxUserPhone.Location = new System.Drawing.Point(265, 77);
            this.tbxUserPhone.MaxLength = 10;
            this.tbxUserPhone.Name = "tbxUserPhone";
            this.tbxUserPhone.Size = new System.Drawing.Size(271, 20);
            this.tbxUserPhone.TabIndex = 57;
            // 
            // cbxUserPermission
            // 
            this.cbxUserPermission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUserPermission.FormattingEnabled = true;
            this.cbxUserPermission.Items.AddRange(new object[] {
            "FULL",
            "LIMITED",
            "NONE"});
            this.cbxUserPermission.Location = new System.Drawing.Point(655, 51);
            this.cbxUserPermission.Name = "cbxUserPermission";
            this.cbxUserPermission.Size = new System.Drawing.Size(111, 21);
            this.cbxUserPermission.TabIndex = 59;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(589, 54);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Permission:";
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.Location = new System.Drawing.Point(223, 203);
            this.btnRemoveUser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRemoveUser.Size = new System.Drawing.Size(118, 32);
            this.btnRemoveUser.TabIndex = 60;
            this.btnRemoveUser.Text = "Remove user";
            this.btnRemoveUser.UseVisualStyleBackColor = true;
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(443, 203);
            this.btnAddUser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnAddUser.Size = new System.Drawing.Size(118, 32);
            this.btnAddUser.TabIndex = 61;
            this.btnAddUser.Text = "Add a new user";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(617, 398);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnClose.Size = new System.Drawing.Size(156, 32);
            this.btnClose.TabIndex = 62;
            this.btnClose.Text = "Close User Management";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 442);
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
            this.Name = "FormUserManagement";
            this.Text = "User Management";
            this.Activated += new System.EventHandler(this.populateUsers);
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