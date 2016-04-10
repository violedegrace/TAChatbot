namespace Chatbot
{
    partial class LoginForm
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
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.SignUpPanel = new System.Windows.Forms.Panel();
            this.LoginGroupBox = new System.Windows.Forms.GroupBox();
            this.SignUpGroupBox = new System.Windows.Forms.GroupBox();
            this.LoginUsernameLabel = new System.Windows.Forms.Label();
            this.LoginPasswordLabel = new System.Windows.Forms.Label();
            this.SignUpUsernameLabel = new System.Windows.Forms.Label();
            this.SignUpPasswordLabel = new System.Windows.Forms.Label();
            this.SignUpNameLabel = new System.Windows.Forms.Label();
            this.textBoxLoginUsername = new System.Windows.Forms.TextBox();
            this.textBoxLoginPassword = new System.Windows.Forms.TextBox();
            this.textBoxSignUpUsername = new System.Windows.Forms.TextBox();
            this.textBoxSignUpPassword = new System.Windows.Forms.TextBox();
            this.textBoxSignUpName = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSignup = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonCancelCreate = new System.Windows.Forms.Button();
            this.labelWarning = new System.Windows.Forms.Label();
            this.LoginPanel.SuspendLayout();
            this.SignUpPanel.SuspendLayout();
            this.LoginGroupBox.SuspendLayout();
            this.SignUpGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginPanel
            // 
            this.LoginPanel.Controls.Add(this.LoginGroupBox);
            this.LoginPanel.Location = new System.Drawing.Point(12, 12);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(257, 152);
            this.LoginPanel.TabIndex = 0;
            // 
            // SignUpPanel
            // 
            this.SignUpPanel.Controls.Add(this.SignUpGroupBox);
            this.SignUpPanel.Location = new System.Drawing.Point(275, 12);
            this.SignUpPanel.Name = "SignUpPanel";
            this.SignUpPanel.Size = new System.Drawing.Size(251, 152);
            this.SignUpPanel.TabIndex = 1;
            // 
            // LoginGroupBox
            // 
            this.LoginGroupBox.Controls.Add(this.labelWarning);
            this.LoginGroupBox.Controls.Add(this.buttonSignup);
            this.LoginGroupBox.Controls.Add(this.buttonCancel);
            this.LoginGroupBox.Controls.Add(this.buttonLogin);
            this.LoginGroupBox.Controls.Add(this.textBoxLoginPassword);
            this.LoginGroupBox.Controls.Add(this.textBoxLoginUsername);
            this.LoginGroupBox.Controls.Add(this.LoginUsernameLabel);
            this.LoginGroupBox.Controls.Add(this.LoginPasswordLabel);
            this.LoginGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoginGroupBox.Location = new System.Drawing.Point(0, 0);
            this.LoginGroupBox.Name = "LoginGroupBox";
            this.LoginGroupBox.Size = new System.Drawing.Size(257, 152);
            this.LoginGroupBox.TabIndex = 0;
            this.LoginGroupBox.TabStop = false;
            this.LoginGroupBox.Text = "Login";
            // 
            // SignUpGroupBox
            // 
            this.SignUpGroupBox.Controls.Add(this.buttonCancelCreate);
            this.SignUpGroupBox.Controls.Add(this.buttonCreate);
            this.SignUpGroupBox.Controls.Add(this.textBoxSignUpName);
            this.SignUpGroupBox.Controls.Add(this.textBoxSignUpPassword);
            this.SignUpGroupBox.Controls.Add(this.textBoxSignUpUsername);
            this.SignUpGroupBox.Controls.Add(this.SignUpNameLabel);
            this.SignUpGroupBox.Controls.Add(this.SignUpPasswordLabel);
            this.SignUpGroupBox.Controls.Add(this.SignUpUsernameLabel);
            this.SignUpGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SignUpGroupBox.Location = new System.Drawing.Point(0, 0);
            this.SignUpGroupBox.Name = "SignUpGroupBox";
            this.SignUpGroupBox.Size = new System.Drawing.Size(251, 152);
            this.SignUpGroupBox.TabIndex = 0;
            this.SignUpGroupBox.TabStop = false;
            this.SignUpGroupBox.Text = "SignUp";
            // 
            // LoginUsernameLabel
            // 
            this.LoginUsernameLabel.AutoSize = true;
            this.LoginUsernameLabel.Location = new System.Drawing.Point(6, 29);
            this.LoginUsernameLabel.Name = "LoginUsernameLabel";
            this.LoginUsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.LoginUsernameLabel.TabIndex = 2;
            this.LoginUsernameLabel.Text = "Username";
            // 
            // LoginPasswordLabel
            // 
            this.LoginPasswordLabel.AutoSize = true;
            this.LoginPasswordLabel.Location = new System.Drawing.Point(6, 56);
            this.LoginPasswordLabel.Name = "LoginPasswordLabel";
            this.LoginPasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.LoginPasswordLabel.TabIndex = 3;
            this.LoginPasswordLabel.Text = "Password";
            // 
            // SignUpUsernameLabel
            // 
            this.SignUpUsernameLabel.AutoSize = true;
            this.SignUpUsernameLabel.Location = new System.Drawing.Point(7, 29);
            this.SignUpUsernameLabel.Name = "SignUpUsernameLabel";
            this.SignUpUsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.SignUpUsernameLabel.TabIndex = 0;
            this.SignUpUsernameLabel.Text = "Username";
            // 
            // SignUpPasswordLabel
            // 
            this.SignUpPasswordLabel.AutoSize = true;
            this.SignUpPasswordLabel.Location = new System.Drawing.Point(7, 56);
            this.SignUpPasswordLabel.Name = "SignUpPasswordLabel";
            this.SignUpPasswordLabel.Size = new System.Drawing.Size(53, 13);
            this.SignUpPasswordLabel.TabIndex = 1;
            this.SignUpPasswordLabel.Text = "Password";
            // 
            // SignUpNameLabel
            // 
            this.SignUpNameLabel.AutoSize = true;
            this.SignUpNameLabel.Location = new System.Drawing.Point(7, 83);
            this.SignUpNameLabel.Name = "SignUpNameLabel";
            this.SignUpNameLabel.Size = new System.Drawing.Size(35, 13);
            this.SignUpNameLabel.TabIndex = 2;
            this.SignUpNameLabel.Text = "Name";
            // 
            // textBoxLoginUsername
            // 
            this.textBoxLoginUsername.Location = new System.Drawing.Point(67, 30);
            this.textBoxLoginUsername.Name = "textBoxLoginUsername";
            this.textBoxLoginUsername.Size = new System.Drawing.Size(184, 20);
            this.textBoxLoginUsername.TabIndex = 4;
            // 
            // textBoxLoginPassword
            // 
            this.textBoxLoginPassword.Location = new System.Drawing.Point(67, 56);
            this.textBoxLoginPassword.Name = "textBoxLoginPassword";
            this.textBoxLoginPassword.PasswordChar = '*';
            this.textBoxLoginPassword.Size = new System.Drawing.Size(184, 20);
            this.textBoxLoginPassword.TabIndex = 5;
            // 
            // textBoxSignUpUsername
            // 
            this.textBoxSignUpUsername.Location = new System.Drawing.Point(68, 29);
            this.textBoxSignUpUsername.Name = "textBoxSignUpUsername";
            this.textBoxSignUpUsername.Size = new System.Drawing.Size(177, 20);
            this.textBoxSignUpUsername.TabIndex = 3;
            // 
            // textBoxSignUpPassword
            // 
            this.textBoxSignUpPassword.Location = new System.Drawing.Point(68, 56);
            this.textBoxSignUpPassword.Name = "textBoxSignUpPassword";
            this.textBoxSignUpPassword.PasswordChar = '*';
            this.textBoxSignUpPassword.Size = new System.Drawing.Size(177, 20);
            this.textBoxSignUpPassword.TabIndex = 4;
            // 
            // textBoxSignUpName
            // 
            this.textBoxSignUpName.Location = new System.Drawing.Point(68, 83);
            this.textBoxSignUpName.Name = "textBoxSignUpName";
            this.textBoxSignUpName.Size = new System.Drawing.Size(177, 20);
            this.textBoxSignUpName.TabIndex = 5;
            // 
            // buttonLogin
            // 
            this.buttonLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonLogin.Location = new System.Drawing.Point(9, 115);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 6;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(90, 115);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSignup
            // 
            this.buttonSignup.Location = new System.Drawing.Point(171, 115);
            this.buttonSignup.Name = "buttonSignup";
            this.buttonSignup.Size = new System.Drawing.Size(75, 23);
            this.buttonSignup.TabIndex = 8;
            this.buttonSignup.Text = "SignUp";
            this.buttonSignup.UseVisualStyleBackColor = true;
            this.buttonSignup.Click += new System.EventHandler(this.buttonSignup_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(10, 115);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 6;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonCancelCreate
            // 
            this.buttonCancelCreate.Location = new System.Drawing.Point(92, 114);
            this.buttonCancelCreate.Name = "buttonCancelCreate";
            this.buttonCancelCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelCreate.TabIndex = 7;
            this.buttonCancelCreate.Text = "Cancel";
            this.buttonCancelCreate.UseVisualStyleBackColor = true;
            this.buttonCancelCreate.Click += new System.EventHandler(this.buttonCancelCreate_Click);
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.Location = new System.Drawing.Point(6, 86);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(47, 13);
            this.labelWarning.TabIndex = 9;
            this.labelWarning.Text = "Warning";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 174);
            this.Controls.Add(this.SignUpPanel);
            this.Controls.Add(this.LoginPanel);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.LoginPanel.ResumeLayout(false);
            this.SignUpPanel.ResumeLayout(false);
            this.LoginGroupBox.ResumeLayout(false);
            this.LoginGroupBox.PerformLayout();
            this.SignUpGroupBox.ResumeLayout(false);
            this.SignUpGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.GroupBox LoginGroupBox;
        private System.Windows.Forms.Panel SignUpPanel;
        private System.Windows.Forms.GroupBox SignUpGroupBox;
        private System.Windows.Forms.Button buttonSignup;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxLoginPassword;
        private System.Windows.Forms.TextBox textBoxLoginUsername;
        private System.Windows.Forms.Label LoginUsernameLabel;
        private System.Windows.Forms.Label LoginPasswordLabel;
        private System.Windows.Forms.Button buttonCancelCreate;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.TextBox textBoxSignUpName;
        private System.Windows.Forms.TextBox textBoxSignUpPassword;
        private System.Windows.Forms.TextBox textBoxSignUpUsername;
        private System.Windows.Forms.Label SignUpNameLabel;
        private System.Windows.Forms.Label SignUpPasswordLabel;
        private System.Windows.Forms.Label SignUpUsernameLabel;
        private System.Windows.Forms.Label labelWarning;

    }
}