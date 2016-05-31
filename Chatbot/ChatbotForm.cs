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
        private dbDataContext db;
        private EngineActuator Engine;
        private DialogueManager DM;

        public tbUser currentUser = null;
        private DialogResult Login;
        private tbUser bot;

        public ChatbotForm()
        {
            //Inisialisasi Program
            InitializeComponent();
            Lingkungan.CreateLocation();
            db = new dbDataContext();
            Engine = new EngineActuator("MLM", db);
            bot = db.tbUsers.Where(x => x.Id == 0).FirstOrDefault();
            DM = new DialogueManager(db,bot);

            //bot define
            bool testing = true;
            //Login
            if (!testing) //temporary code
            {
                currentUser = db.tbUsers.Where(x => x.Id == 1).FirstOrDefault();
                this.Text = bot.Name + " Chatbot";
            }
            else
            {
                #region True Initiation
                this.Text = bot.Name + " Chatbot";
                try
                {
                    LoginlogoutToolStripMenuItem.PerformClick();
                    if (Login == DialogResult.Cancel)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    if (System.Windows.Forms.Application.MessageLoop)
                        System.Windows.Forms.Application.Exit(); // WinForms app
                    else
                        System.Environment.Exit(1); // Console app
                    this.Close();
                    this.Dispose();
                }
                #endregion
            }
//            DM.ManageDialogue(bot, "Welcome " + currentUser.Name + " to " + bot.Name + " Chatbot.",null);
            refreshListBoxChat();
            this.AcceptButton = this.buttonSend;
        }
        #region DataBase
        private void manageDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageDBForm managedb = new ManageDBForm(this.Text,db);
            this.Visible = false;
            if (managedb.ShowDialog()==DialogResult.OK)
            {
                for (int i = 0; i < managedb.LokasibaruData.Count; i++)
                {
                    foreach (var item in managedb.DataBaru)
                    {
                        if (!File.Exists(managedb.LokasibaruData[i]))
                            File.Copy(managedb.LokasilamaData[i], managedb.LokasibaruData[i]);                            
                        if (db.tbInformasis.Where(x=>x.Judul.ToString()==item.Judul).FirstOrDefault()==null)
                            db.tbInformasis.InsertOnSubmit(item);   
                    }
//                    db.tbInformasis.InsertAllOnSubmit(managedb.DataBaru.ToList());
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
        #endregion

        #region Manage User
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

            if (loginDS == DialogResult.OK && currentUser != null)
            {
                //save progress
                MessageBox.Show("Saving Progress...\nCreating new Conversation");
            }
            if (loginDS == DialogResult.OK)// && login.user.Id!=currentUser.Id)
            {
                this.Login = DialogResult.OK;
                currentUser = login.user;
                DM = new DialogueManager(db, bot);
                DM.ManageDialogue(bot, "Welcome " + currentUser.Name + " to " + bot.Name + " Chatbot.", null);
                cekAdmin();
                refreshListBoxChat();
                textBoxInput.Enabled = true;
                buttonSend.Enabled = true;
                listBoxConv.Enabled = true;

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
            if (MessageBox.Show("Exit Program??", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
        #endregion

        #region Dialogue
        private void refreshListBoxChat()
        {
            List<string> lst = DM.Conversation.Where(x=>x!=null).Select(x => x.ToString()).ToList();
            lst.Reverse();
            listBoxConv.DataSource = lst;
            int visibleItems = listBoxConv.ClientSize.Height / listBoxConv.ItemHeight;
            listBoxConv.TopIndex = Math.Max(listBoxConv.Items.Count - visibleItems + 1, 0);
            if (DM.LastPossibleAnswer != null)
                listBoxPosJawaban.DataSource = DM.LastPossibleAnswer.Select(x => x.info).ToList();
            else
                listBoxPosJawaban.DataSource = null;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxInput.Text))
            {
                buttonSend.Enabled = false;
                DM.ManageDialogue(currentUser, textBoxInput.Text, Engine);
                //            MessageBox.Show(DM.Conversation.Count.ToString());
                refreshListBoxChat();
                textBoxInput.Text = "";
                if (DM.CurrentState.Id < 6)
                    buttonSend.Enabled = true;
                else
                {
                    textBoxInput.Enabled = false;
                    buttonSend.Enabled = false;
                    listBoxConv.Enabled = false;
                    MessageBox.Show("Silahkan Melakukan login Kembali");
                }
            }
        }        
        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Dibuat untuk memenuhi TA oleh:\n1272106 - Benny Gunawan");
        }


    }
}
