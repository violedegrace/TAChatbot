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
        public tbUser currentUser=null;
        private tbUser bot;
        private dbDataContext db;
        private EngineActuator Engine;
        private DialogResult Login;
        private Stack<string> Conversation;

        public ChatbotForm()
        {
            //Inisialisasi Program
            InitializeComponent();
            Lingkungan.CreateLocation();
            db = new dbDataContext();
            Engine = new EngineActuator("MLM", db);
            Conversation = new Stack<string>();

            //Login
            if (true) //temporary code
            {
                bot = db.tbUsers.Where(x => x.Id == 0).FirstOrDefault();
                currentUser = db.tbUsers.Where(x => x.Id == 1).FirstOrDefault();
                this.Text = bot.Name + " Chatbot";
            }

            bot = db.tbUsers.Where(x => x.Id == 0).FirstOrDefault();
            //this.Text = bot.Name + " Chatbot";
            //try
            //{
            //    LoginlogoutToolStripMenuItem.PerformClick();
            //    if (Login == DialogResult.Cancel)
            //    {
            //        throw new Exception();
            //    }
            //}
            //catch (Exception)
            //{
            //    if (System.Windows.Forms.Application.MessageLoop)
            //        System.Windows.Forms.Application.Exit(); // WinForms app
            //    else
            //        System.Environment.Exit(1); // Console app
            //    this.Close();
            //    this.Dispose();
            //}

            string welcome ="Welcome "+currentUser.Name+" to "+bot.Name+" Chatbot.";
            Conversation.Push(welcome);
            refreshListBoxChat();

        }

        private void refreshListBoxChat()
        {
            listBoxConv.DataSource = Conversation.ToList();
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
            DialogResult reIdx = MessageBox.Show("Indexing seluruh data ulang?","Indexing Data",MessageBoxButtons.YesNoCancel);
            if (reIdx==DialogResult.Yes)
            {
                Engine.RebuildDatabase("all");
            }
            else if (reIdx == DialogResult.No)
            {
                Engine.RebuildDatabase("New");
            }
        }

        private void calculateMixtureLanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.CaclculateMixtureLanguageModel();
        }

        private void LoginlogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Jika baru dibuka (currentUser nya null) maka cukup lakukan proses login,
            // jika sudah ada user maka semua proses di save

            LoginForm login = new LoginForm(db);
            DialogResult loginDS;
            do
            {
                loginDS = login.ShowDialog();
            } while (loginDS == DialogResult.Retry);

            if (loginDS == DialogResult.OK && currentUser!=null)
            {
                //save progress
                MessageBox.Show("Saving Progress...\nCreating new Conversation");
            }
            if (loginDS == DialogResult.OK)// && login.user.Id!=currentUser.Id)
            {
                this.Login = DialogResult.OK;
                currentUser = login.user;
                cekAdmin();
            }
            else
        	{
                this.Login = DialogResult.Cancel;
            }
        }
        private void cekAdmin()
        {
            if (currentUser.Id > 1)
            {
                dataToolStripMenuItem.Enabled = false;
                dataToolStripMenuItem.Visible = false;
            }
            else
            {
                dataToolStripMenuItem.Enabled = true;
                dataToolStripMenuItem.Visible = true;
            }

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Program??","Warning",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                //Save Progress dengan didahului messagebox ya tidak

                
                if (System.Windows.Forms.Application.MessageLoop)
                    System.Windows.Forms.Application.Exit(); // WinForms app
                else
                    System.Environment.Exit(1); // Console app
                this.Close();
                this.Dispose();                
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {

        }

    }
}
