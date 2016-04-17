using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatbot
{
    public partial class MainForm : Form
    {
        public tbUser currentUser;
        private tbUser bot;
        private dbDataContext db;
        public MainForm()
        {
            InitializeComponent();
            db = new dbDataContext();
            LoginForm login = new LoginForm(db);
            if (login.ShowDialog()==DialogResult.OK)
            {
                bot = db.tbUsers.Where(x => x.Id == 0).FirstOrDefault();
                currentUser = login.user;
                this.Text = bot.Name + " Chatbot";
            }
            else
            {
                if (System.Windows.Forms.Application.MessageLoop)
                    System.Windows.Forms.Application.Exit(); // WinForms app
                else
                    System.Environment.Exit(1); // Console app
                this.Close();
                this.Dispose();
            }
        }
    }
}
