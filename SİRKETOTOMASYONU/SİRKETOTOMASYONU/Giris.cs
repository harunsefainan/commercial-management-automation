using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SİRKETOTOMASYONU
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cm = new MySqlCommand("select * from tbl_admin where Ad=@p1 and Sifre=@p2",bgl.baglanti());
            cm.Parameters.AddWithValue("@p1",TxtKullanıcı.Text);
            cm.Parameters.AddWithValue("@p2", TxtSifre.Text);
            MySqlDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                var anasayfa = new Anasayfa
                {
                    ShowInTaskbar = false,
                    MinimizeBox = false,
                    MaximizeBox = false,
                  
                };
                anasayfa.StartPosition = FormStartPosition.CenterScreen;
                anasayfa.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("KULLANICI ADI VEYA ŞİFRE HATALI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtKullanıcı.Clear();
                TxtSifre.Clear();
            }
            bgl.baglanti().Close();//bağlantı kapatma
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
}
