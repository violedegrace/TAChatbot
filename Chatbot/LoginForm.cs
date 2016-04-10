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
    public partial class LoginForm : Form
    {
        /* Login Form
         * Melakukan proses login dan/atau signup bagi user
         * saat login, signup disable
         * saat signup, login disable
         * hanya salah satu yang dapat dijalankan di satu waktu
        */

        public tbUser user; // untuk user yang akan dikembalikan
        dbDataContext db; // koneksi ke db dibuat
        public LoginForm()
        {
            /* Ctor Login Form
             * menginisialisasi seluruh komponen, menghilgkan label warning dan
             * Diable SignUp serta membuat koneksi ke db
            */
            InitializeComponent();
            db = new dbDataContext();
            user = null;
            SignUpPanel.Enabled = false;
            labelWarning.Visible = false;
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            /* metode untuk Enable SignUp, dan disable login
             */
            SignUpPanel.Enabled = true;
            LoginPanel.Enabled = false;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancelCreate_Click(object sender, EventArgs e)
        {
            /* metode untuk Enable login dan disable signup karena
             * batal melakukan signup
             */
            SignUpPanel.Enabled = false;
            LoginPanel.Enabled = true;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            /* metode untuk login.
             * awal dilakukan validasi username password, jika berhasil akan keluar dari form
             * jika gagal login karena uname atau pass tidak dikenali maka akan ada warning
             * masukan dari user di validasi, kosong atau tidak
             */

            if (string.IsNullOrWhiteSpace(textBoxLoginPassword.Text) == true || string.IsNullOrWhiteSpace(textBoxLoginPassword.Text) == true)
            {
                MessageBox.Show("Username atau password kosong. Mohon diisi");
                this.DialogResult = DialogResult.Retry;
            }
            else
            {
                tbUser cek = db.tbUsers.Where(i => i.Username == textBoxLoginUsername.Text
                    && i.Password==textBoxLoginPassword.Text).FirstOrDefault();
                if (cek==null) // make or for the system name
                {
                    labelWarning.Visible = true;
                    labelWarning.Text = "Wrong Username Or Password";
                    labelWarning.ForeColor = Color.Red;
                    textBoxLoginUsername.Text = "";
                    textBoxLoginPassword.Text = "";
                    this.DialogResult = DialogResult.Retry;
                    this.OnClosing(new CancelEventArgs(true));
                }
                else
                {
                    user = cek;
                }
            }
        }
    }
}
