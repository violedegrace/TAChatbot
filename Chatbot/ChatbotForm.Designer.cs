namespace Chatbot
{
    partial class ChatbotForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginlogoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebuildDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateMixtureLanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.listBoxConv = new System.Windows.Forms.ListBox();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBoxMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(341, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoginlogoutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.userToolStripMenuItem.Text = "User";
            // 
            // LoginlogoutToolStripMenuItem
            // 
            this.LoginlogoutToolStripMenuItem.Name = "LoginlogoutToolStripMenuItem";
            this.LoginlogoutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.LoginlogoutToolStripMenuItem.Text = "Logout";
            this.LoginlogoutToolStripMenuItem.Click += new System.EventHandler(this.LoginlogoutToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageDataToolStripMenuItem,
            this.rebuildDatabaseToolStripMenuItem,
            this.calculateMixtureLanToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // manageDataToolStripMenuItem
            // 
            this.manageDataToolStripMenuItem.Name = "manageDataToolStripMenuItem";
            this.manageDataToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.manageDataToolStripMenuItem.Text = "Manage Data";
            this.manageDataToolStripMenuItem.Click += new System.EventHandler(this.manageDataToolStripMenuItem_Click);
            // 
            // rebuildDatabaseToolStripMenuItem
            // 
            this.rebuildDatabaseToolStripMenuItem.Name = "rebuildDatabaseToolStripMenuItem";
            this.rebuildDatabaseToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.rebuildDatabaseToolStripMenuItem.Text = "Rebuild Database";
            this.rebuildDatabaseToolStripMenuItem.Click += new System.EventHandler(this.rebuildDatabaseToolStripMenuItem_Click);
            // 
            // calculateMixtureLanToolStripMenuItem
            // 
            this.calculateMixtureLanToolStripMenuItem.Name = "calculateMixtureLanToolStripMenuItem";
            this.calculateMixtureLanToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.calculateMixtureLanToolStripMenuItem.Text = "Calculate Model";
            this.calculateMixtureLanToolStripMenuItem.Click += new System.EventHandler(this.calculateMixtureLanToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.buttonSend);
            this.groupBoxMain.Controls.Add(this.textBoxInput);
            this.groupBoxMain.Controls.Add(this.listBoxConv);
            this.groupBoxMain.Location = new System.Drawing.Point(12, 27);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(317, 400);
            this.groupBoxMain.TabIndex = 1;
            this.groupBoxMain.TabStop = false;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(252, 374);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(58, 23);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "send";
            this.buttonSend.UseVisualStyleBackColor = true;
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(0, 377);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(239, 20);
            this.textBoxInput.TabIndex = 1;
            // 
            // listBoxConv
            // 
            this.listBoxConv.FormattingEnabled = true;
            this.listBoxConv.Location = new System.Drawing.Point(0, 8);
            this.listBoxConv.Name = "listBoxConv";
            this.listBoxConv.Size = new System.Drawing.Size(317, 355);
            this.listBoxConv.TabIndex = 0;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ChatbotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 439);
            this.Controls.Add(this.groupBoxMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ChatbotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatbotForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxMain.ResumeLayout(false);
            this.groupBoxMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rebuildDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoginlogoutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.ListBox listBoxConv;
        private System.Windows.Forms.ToolStripMenuItem calculateMixtureLanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}