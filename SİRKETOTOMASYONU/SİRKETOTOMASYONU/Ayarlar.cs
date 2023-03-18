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
using Microsoft.VisualBasic;

namespace SİRKETOTOMASYONU
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        void listele()
        {
            TxtID.Enabled = false;
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("AdminListele", bgl.baglanti());
            da.Fill(ds);//doldurma
            dataGridView1.DataSource = ds.Tables[0];//nereye listeleyecek
            bgl.baglanti().Close();
        }
        void temizle()
        {
            TxtID.Clear();
            TxtKullanıcı.Clear();
            TxtSifre.Clear();        
        }
        private void Ayarlar_Load(object sender, EventArgs e)
        {
            listele();
            //setDeactive();
        }
        //void setActive()
        //{
        //    //butonları aktif etme
        //    btnKaydet.Enabled = true;
        //    btnsil.Enabled = true;
        //    btnİptal.Enabled = true;
        //    btnguncelle.Enabled = false;
        //    TxtSifre.Enabled = true;
        //    dataGridView1.Enabled = false;
        //}

        //void setDeactive()
        //{
        //    //butonları deaktif etme
        //    btnKaydet.Enabled=false;
        //    btnsil.Enabled=false;
        //    btnİptal.Enabled = false;
        //    btnguncelle.Enabled=true;
        //    TxtSifre.Enabled = false;
        //    dataGridView1.Enabled=true;
        //}

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtKullanıcı.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtSifre.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtKullanıcı.Text == string.Empty)
                hata = 1;
            if (TxtSifre.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { 
            MySqlCommand cm = new MySqlCommand("update tbl_admin set ID=@p1,Ad=@p2,Sifre=@p3 where ID=@p4", bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", TxtID.Text);
            cm.Parameters.AddWithValue("@p2", TxtKullanıcı.Text);
            cm.Parameters.AddWithValue("@p3", TxtSifre.Text);
            cm.Parameters.AddWithValue("@p4", TxtID.Text);
            int basari = cm.ExecuteNonQuery();//yazdırma
            bgl.baglanti().Close();//bağlantı kapatma       
            if (basari == 1)
            {
                MessageBox.Show("KAYIT GÜNCELLENDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("KAYIT GÜNCELLENMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                temizle();
            }
            listele();
            
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtID.Text == string.Empty)
            {
                hata = 1;
            }
            if (hata == 1)
            {
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MySqlCommand komut = new MySqlCommand("select * from tbl_admin where ID= '" + TxtID.Text + "'", bgl.baglanti());
                komut.ExecuteNonQuery();
                MySqlDataReader dr = komut.ExecuteReader();//komutu  tutmak için
                if (dr.Read())
                {
                    MySqlCommand cm = new MySqlCommand("delete  from tbl_admin where ID= '" + TxtID.Text + "'", bgl.baglanti());//ID'ye gçre silme komutu
                    int basari = cm.ExecuteNonQuery();//komutu okumak için
                    bgl.baglanti().Close();//bağlantı kapatma
                    if (basari == 1)
                    {
                        MessageBox.Show("KULLANICI SİLİNDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("KULLANICI SİLİMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else//aynı veriden varsa
                {
                    MessageBox.Show("BÖYLE BİR KULLANICI BULUNAMADI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            temizle();
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtKullanıcı.Text == string.Empty)
                hata = 1;
            if (TxtSifre.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MySqlCommand cm = new MySqlCommand("insert into tbl_admin(Ad,Sifre) values (@p1,@p2);", bgl.baglanti());
                cm.Parameters.AddWithValue("@p1", TxtKullanıcı.Text);
                cm.Parameters.AddWithValue("@p2", TxtSifre.Text);
                int basari = cm.ExecuteNonQuery();//yazdırma
                bgl.baglanti().Close();//bağlantı kapatma       
                if (basari == 1)
                {
                    MessageBox.Show("KAYIT EKLENDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("KAYIT EKLENMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            listele();
            temizle();
        }

        private void Ayarlar_Shown(object sender, EventArgs e)
        {
            //var giris = new Giris
            //{
            //    ShowInTaskbar = false,
            //    MinimizeBox = false,
            //    MaximizeBox = false,
            //};
            //giris.StartPosition = FormStartPosition.CenterScreen;
            //giris.ShowDialog(this);
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            //setDeactive();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.Value != null)
            {
                e.Value = new String('*', e.Value.ToString().Length);
            }
        }
    }
}
