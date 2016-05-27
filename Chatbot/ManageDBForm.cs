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
        List<tbDomain> DataDomain;
        tbDomain current;
        public List<string> LokasibaruData;
        public List<string> LokasilamaData;
        public List<tbInformasi> DataBaru;
        public ManageDBForm(string args, dbDataContext database)
        {
            InitializeComponent();
            buttonDelete.Enabled = false;
            this.Text = args;

            //Inisialisasi Variable
            LokasibaruData = new List<string>();
            LokasilamaData = new List<string>();
            DataBaru = new List<tbInformasi>();
            db = database;
            DataDomain = db.tbDomains.ToList();

            //Inisialisasi Grid
            comboBoxDomain.DataSource = DataDomain.Select(x => x.Name).ToList();
            comboBoxDomain.SelectedIndex = 1;
        }
        private void IndexChange(object sender, EventArgs e)
        {
            //perubahan
            current = DataDomain.Where(x => x.Name.Equals(comboBoxDomain.SelectedValue)).FirstOrDefault();
            if (current != null)
            {
//                MessageBox.Show("Test");
                if (current.Id == 0)
                {
                    buttonAdd.Enabled = false;
                    buttonDelete.Enabled = false;
                    textBoxLocation.Enabled = false;
                    List<tbInformasi> temp = new List<tbInformasi>();
                    foreach (var item in DataDomain)
                    {
                        temp.AddRange(item.tbInformasis.ToList());
                    }
                    dataGridView1.DataSource = temp.Select(x => new { ID = x.Id, Domain = x.tbDomain.Name, Judul = x.Judul, Idexed = (x.Indexed > 0 ? "True" : "False"), Lokasi = x.Lokasi }).OrderBy(x => x.ID).ToList();
                }
                else
                {
                    buttonAdd.Enabled = true;
                    buttonDelete.Enabled = true;
                    textBoxLocation.Enabled = true;
                    //tambahkan data
                    dataGridView1.DataSource = current.tbInformasis.Where(x => x.tbDomain.Name == comboBoxDomain.SelectedValue.ToString()).Select(x => new { ID = x.Id, Judul = x.Judul, Idexed = (x.Indexed > 0 ? "True" : "False"), Lokasi = x.Lokasi }).OrderBy(x => x.ID).ToList();
                }
            }
            else
            {
                MessageBox.Show("Data tidak diketahui");
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string newLocation = "";
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxLocation.Text))
                {
                    TextboxLocationDoubleClick(null, null);
                }
                foreach (var item in textBoxLocation.Text.Split('+').ToList())
                {
                    newLocation = "";
                    string[] fragment = item.Split('\\').ToArray();
                    if (!File.Exists(Lingkungan.getDataBaru() + fragment[fragment.Length - 1]) && db.tbInformasis.Where(x => x.Judul.ToString() == fragment[fragment.Length - 1].ToString()).FirstOrDefault() == null)
                    {
                        tbInformasi baru = new tbInformasi();
                        baru.Judul = fragment[fragment.Length - 1];
                        baru.Indexed = 0;
                        baru.Lokasi = Lingkungan.getDataBaru() + fragment[fragment.Length - 1];

                        current.tbInformasis.Add(baru);
                        newLocation = Lingkungan.getDataBaru() + fragment[fragment.Length - 1];

                        LokasilamaData.Add(item);
                        LokasibaruData.Add(newLocation);
                        DataBaru.Add(baru);
//                        File.Copy(item, newLocation);
//                        db.SubmitChanges();
 //                        MessageBox.Show("Data " + fragment[fragment.Length - 1] + " berhasil ditambahkan");
                    }
                    else
                    {
                        //MessageBox.Show("Data " + fragment[fragment.Length - 1] + " sudah ada");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    MessageBox.Show("Masukkan data");
                }
                else
                {
                    MessageBox.Show("Error! " + ex.Message);
                }
            }
            finally
            {
                textBoxLocation.Text = "";
                IndexChange(null, null);
            }

        }
        private void TextboxLocationDoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLocation.Text))
            {
                OpenFileDialog browse = new OpenFileDialog();
                browse.Filter = "Text Files (.txt)|*.txt";
                browse.FilterIndex = 0;
                browse.Multiselect = true;
                textBoxLocation.Text = "";
                if (browse.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < browse.FileNames.Length; i++)
                    {
                        if (i < browse.FileNames.Length - 1)
                        {
                            textBoxLocation.Text += browse.FileNames[i] + "+";
                        }
                        else
                        {
                            textBoxLocation.Text += browse.FileNames[i];
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fitur Belum tersedia");
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }

    }
}
