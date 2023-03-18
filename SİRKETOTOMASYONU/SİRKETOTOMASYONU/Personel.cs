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
    public partial class Personel : Form
    {
        public Personel()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        void listele()
        {
            TxtID.Enabled = false;
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from tbl_personel", bgl.baglanti());
            da.Fill(ds);
            DgwMusteriler.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }

        void illistesi()
        {
            MySqlCommand cm = new MySqlCommand("select sehiradi from iller", bgl.baglanti());
            MySqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();//bağlantı kapatma
        }

        private void Personel_Load(object sender, EventArgs e)
        {
            listele();
            illistesi();
        }
        void temizle()
        {
            TxtID.Clear();
            TxtAd.Clear();
            TxtSoyad.Clear();
            TxtTelefon.Clear();
            TxtTC.Clear();
            TxtMail.Clear();
            CmbIl.Items.Clear();
            CmbIlce.Items.Clear();
            TxtAdres.Clear();
            TxtVergiDairesi.Clear();
        }
        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Items.Clear();
            MySqlCommand cm = new MySqlCommand("select ilceadi from ilceler where sehirid=@p1", bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex + 1);
            MySqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                CmbIlce.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtAd.Text == string.Empty)
                hata = 1;
            if (TxtSoyad.Text == string.Empty)
                hata = 1;
            if (TxtTelefon.Text == string.Empty)
                hata = 1;
            if (TxtTC.Text == string.Empty)
                hata = 1;
            if (TxtMail.Text == string.Empty)
                hata = 1;
            if (CmbIl.Text == string.Empty)
                hata = 1;
            if (CmbIlce.Text == string.Empty)
                hata = 1;
            if (TxtAdres.Text == string.Empty)
                hata = 1;
            if (TxtVergiDairesi.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MySqlCommand cm = new MySqlCommand("insert into tbl_personel(Ad,Soyad,Tc,Telefon,Mail,İl,İlçe,Adres,Görevi) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
                cm.Parameters.AddWithValue("@p1", TxtAd.Text);
                cm.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                cm.Parameters.AddWithValue("@p3", TxtTC.Text); 
                cm.Parameters.AddWithValue("@p4", TxtTelefon.Text);
                cm.Parameters.AddWithValue("@p5", TxtMail.Text);
                cm.Parameters.AddWithValue("@p6", CmbIl.Text);
                cm.Parameters.AddWithValue("@p7", CmbIlce.Text);
                cm.Parameters.AddWithValue("@p8", TxtAdres.Text);
                cm.Parameters.AddWithValue("@p9", TxtVergiDairesi.Text);
                int basari = cm.ExecuteNonQuery();
                if (basari == 1)
                {
                    MessageBox.Show("KAYIT EKLENDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("KAYIT EKLENMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();//bağlantı kapatma
                temizle();
            }
            listele();
            illistesi();
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
                MySqlCommand komut = new MySqlCommand("select * from tbl_personel where ID= '" + TxtID.Text + "'", bgl.baglanti());
                komut.ExecuteNonQuery();
                MySqlDataReader dr = komut.ExecuteReader();//komutu  tutmak için
                if (dr.Read())
                {
                    MySqlCommand cm = new MySqlCommand("delete  from tbl_personel where ID= '" + TxtID.Text + "'", bgl.baglanti());//ID'ye gçre silme komutu
                    int basari = cm.ExecuteNonQuery();//komutu okumak için
                    bgl.baglanti().Close();//bağlantı kapatma
                    if (basari == 1)
                    {
                        MessageBox.Show("PERSONEL SİLİNDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("PERSONEL SİLİMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else//aynı veriden varsa
                {
                    MessageBox.Show("BÖYLE BİR PERSONEL BULUNAMADI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                temizle();
            }
            listele();
            illistesi();
        }

        private void DgwMusteriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = DgwMusteriler.CurrentRow.Cells[0].Value.ToString();
            TxtAd.Text = DgwMusteriler.CurrentRow.Cells[1].Value.ToString();
            TxtSoyad.Text = DgwMusteriler.CurrentRow.Cells[2].Value.ToString();
            TxtTC.Text = DgwMusteriler.CurrentRow.Cells[3].Value.ToString();
            TxtTelefon.Text = DgwMusteriler.CurrentRow.Cells[4].Value.ToString();
            TxtMail.Text = DgwMusteriler.CurrentRow.Cells[5].Value.ToString();
            CmbIl.Text = DgwMusteriler.CurrentRow.Cells[6].Value.ToString();
            CmbIlce.Text = DgwMusteriler.CurrentRow.Cells[7].Value.ToString();
            TxtAdres.Text = DgwMusteriler.CurrentRow.Cells[8].Value.ToString();
            TxtVergiDairesi.Text = DgwMusteriler.CurrentRow.Cells[9].Value.ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtAd.Text == string.Empty)
                hata = 1;
            if (TxtSoyad.Text == string.Empty)
                hata = 1;
            if (TxtTelefon.Text == string.Empty)
                hata = 1;
            if (TxtTC.Text == string.Empty)
                hata = 1;
            if (TxtMail.Text == string.Empty)
                hata = 1;
            if (CmbIl.Text == string.Empty)
                hata = 1;
            if (CmbIlce.Text == string.Empty)
                hata = 1;
            if (TxtAdres.Text == string.Empty)
                hata = 1;
            if (TxtVergiDairesi.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { 
                MySqlCommand cm = new MySqlCommand("update tbl_personel set Ad=@p1,Soyad=@p2,Tc=@p3,Telefon=@p4,Mail=@p5,İl=@p6,İlçe=@p7,Adres=@p8,Görevi=@p9 where ID=@p10", bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", TxtAd.Text);
            cm.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            cm.Parameters.AddWithValue("@p3", TxtTC.Text);
            cm.Parameters.AddWithValue("@p4", TxtTelefon.Text);
            cm.Parameters.AddWithValue("@p5", TxtMail.Text);
            cm.Parameters.AddWithValue("@p6", CmbIl.Text);
            cm.Parameters.AddWithValue("@p7", CmbIlce.Text);
            cm.Parameters.AddWithValue("@p8", TxtAdres.Text);
            cm.Parameters.AddWithValue("@p9", TxtVergiDairesi.Text);
            cm.Parameters.AddWithValue("@p10", TxtID.Text);

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
            illistesi();
        }

        private void TxtAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void TxtSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void TxtVergiDairesi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_personel where Ad like'" + textBox1.Text + "%'", bgl.baglanti());
            da.Fill(ds, "tbl_personel");
            DgwMusteriler.DataSource = ds.Tables["tbl_personel"];
            bgl.baglanti();
        }
    }
}
