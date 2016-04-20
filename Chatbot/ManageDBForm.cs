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
    public partial class ManageDBForm : Form
    {
        dbDataContext db;
        tbDomain current;
        List<tbInformasi> DataBaru;
        public ManageDBForm(string args, dbDataContext database)
        {
            InitializeComponent();
            this.Text = args;
            db = database;
            DataBaru = new List<tbInformasi>();
            comboBoxDomain.DataSource = db.tbDomains.Select(x => x.Name).ToList();
        }

        private void IndexChange(object sender, EventArgs e)
        {
            current = db.tbDomains.Where(x => x.Name.Equals(comboBoxDomain.SelectedValue)).FirstOrDefault();
            if (current != null)
            {
                if (current.Id==0)
                {

                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Data tidak diketahui");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLocation.Text))
            {
                OpenFileDialog browse = new OpenFileDialog();
                //browse.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
                browse.Filter = "Text Files (.txt)|*.txt";
                browse.FilterIndex = 0;
                browse.Multiselect = true;
                if (browse.ShowDialog()==DialogResult.OK)
                {
                    foreach (var files in browse.FileNames)
                    {
                        string[] nama = files.Split('\\').ToArray();
                        if (!File.Exists(Lingkungan.getDataCache() + "/" + current.Name + "/" + nama[nama.Length - 1]))
                        {
                            File.Copy(files, Lingkungan.getDataCache() + "/" + current.Name + "/" + nama[nama.Length - 1]);
                            tbInformasi baru = new tbInformasi();
                            baru.Judul = nama[nama.Length - 1].Substring(0, nama[nama.Length - 1].Length-5);
                            DataBaru.Add(baru);
                        }
                    }
                }
            }
        }

    }
}
