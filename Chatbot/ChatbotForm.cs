using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatbot
{
    public partial class ChatbotForm : Form
    {
        public tbUser currentUser;
        private tbUser bot;
        private dbDataContext db;

        public ChatbotForm()
        {
            InitializeComponent();
            Lingkungan.CreateLocation();
            db = new dbDataContext();
            LoginForm login = new LoginForm(db);
            if (true) //temporary code
            {
                bot = db.tbUsers.Where(x => x.Id == 0).FirstOrDefault();
                currentUser = db.tbUsers.Where(x => x.Id == 1).FirstOrDefault();
                this.Text = bot.Name + " Chatbot";
            }
            //if (login.ShowDialog() == DialogResult.OK)
            //{
            //    bot = db.tbUsers.Where(x => x.Id == 0).FirstOrDefault();
            //    currentUser = login.user;
            //    //currentUser = db.tbUsers.Where(x => x.Id == 1).FirstOrDefault();
            //    this.Text = bot.Name + " Chatbot";
            //}
            //else
            //{
            //    if (System.Windows.Forms.Application.MessageLoop)
            //        System.Windows.Forms.Application.Exit(); // WinForms app
            //    else
            //        System.Environment.Exit(1); // Console app
            //    this.Close();
            //    this.Dispose();
            //}
        }

        private void manageDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDBForm managedb = new ManageDBForm(this.Text,db);
            this.Visible = false;
            if (managedb.ShowDialog()==DialogResult.OK)
            {
                for (int i = 0; i < managedb.LokasibaruData.Count; i++)
                {
                    File.Copy(managedb.LokasilamaData[i], managedb.LokasibaruData[i]);
                    db.tbInformasis.InsertAllOnSubmit(managedb.DataBaru);
                    db.SubmitChanges();
                }
            }
            else
            {
                db = new dbDataContext();
            }
            this.Visible = true;
        }

        private void rebuildDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
