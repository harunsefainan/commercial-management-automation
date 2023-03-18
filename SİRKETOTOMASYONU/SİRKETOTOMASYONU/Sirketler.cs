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
    public partial class Sirketler : Form
    {
        public Sirketler()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();//Db çağırma
        void listele()
        {
            TxtID.Enabled = false;
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from tbl_sirketler", bgl.baglanti());
            da.Fill(ds);
            DgwSirketler.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Sirketler_Load(object sender, EventArgs e)
        {
            listele();
            illistesi();
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
        void temizle()
        {
            TxtTC.Clear();
            TxtID.Clear();
            TxtAd.Clear();
            TxtYetkili.Clear();
            TxtYetkiliAdSoyad.Clear();
            TxtTelefon1.Clear();
            TxtTelefon2.Clear();
            TxtMail.Clear();
            CmbIl.Items.Clear();
            CmbIlce.Items.Clear();
            TxtVergiDairesi.Clear();
            TxtAdres.Clear();
            TxtFaaliyet1.Clear();
            TxtFaaliyet2.Clear();
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
            if (TxtYetkili.Text == string.Empty)
                hata = 1;
            if (TxtTC.Text == string.Empty)
                hata = 1;
            if (TxtYetkiliAdSoyad.Text == string.Empty)
                hata = 1;
            if (TxtTelefon1.Text == string.Empty)
                hata = 1;
            if (TxtTelefon2.Text == string.Empty)
                hata = 1;
            if (TxtMail.Text == string.Empty)
                hata = 1;
            if (CmbIl.Text == string.Empty)
                hata = 1;
            if (CmbIlce.Text == string.Empty)
                hata = 1;
            if (TxtVergiDairesi.Text == string.Empty)
                hata = 1;
            if (TxtAdres.Text == string.Empty)
                hata = 1;
            if (TxtFaaliyet1.Text == string.Empty)
                hata = 1;
            if (TxtFaaliyet2.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MySqlCommand cm = new MySqlCommand("insert into tbl_sirketler(Ad,Yetkili,YetkiliAdSoyad,Telefon1,Telefon2,Mail,İl,İlçe,VergiDairesi,Adres,Faaliyet1,Faaliyet2,Tc) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13)", bgl.baglanti());
                cm.Parameters.AddWithValue("@p1", TxtAd.Text);
                cm.Parameters.AddWithValue("@p2", TxtYetkili.Text);
                cm.Parameters.AddWithValue("@p3", TxtYetkiliAdSoyad.Text);
                cm.Parameters.AddWithValue("@p4", TxtTelefon1.Text);
                cm.Parameters.AddWithValue("@p5", TxtTelefon2.Text);
                cm.Parameters.AddWithValue("@p6", TxtMail.Text);
                cm.Parameters.AddWithValue("@p7", CmbIl.Text);
                cm.Parameters.AddWithValue("@p8", CmbIlce.Text);
                cm.Parameters.AddWithValue("@p9", TxtVergiDairesi.Text);
                cm.Parameters.AddWithValue("@p10", TxtAdres.Text);
                cm.Parameters.AddWithValue("@p11", TxtFaaliyet1.Text);
                cm.Parameters.AddWithValue("@p12", TxtFaaliyet2.Text);
                cm.Parameters.AddWithValue("@p13", TxtTC.Text);
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
                MySqlCommand komut = new MySqlCommand("select * from tbl_sirketler where ID= '" + TxtID.Text + "'", bgl.baglanti());
                komut.ExecuteNonQuery();
                MySqlDataReader dr = komut.ExecuteReader();//komutu  tutmak için
                if (dr.Read())
                {
                    MySqlCommand cm = new MySqlCommand("delete  from tbl_sirketler where ID= '" + TxtID.Text + "'", bgl.baglanti());//ID'ye gçre silme komutu
                    int basari = cm.ExecuteNonQuery();//komutu okumak için
                    bgl.baglanti().Close();//bağlantı kapatma
                    if (basari == 1)
                    {
                        MessageBox.Show("ŞİRKET SİLİNDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ŞİRKET SİLİMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else//aynı veriden varsa
                {
                    MessageBox.Show("BÖYLE BİR ŞİRKET BULUNAMADI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                temizle();
            }
            listele();
            illistesi();
        }
        
        private void DgwSirketler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = DgwSirketler.CurrentRow.Cells[0].Value.ToString();
            TxtAd.Text = DgwSirketler.CurrentRow.Cells[1].Value.ToString();
            TxtYetkili.Text = DgwSirketler.CurrentRow.Cells[2].Value.ToString();
            TxtYetkiliAdSoyad.Text = DgwSirketler.CurrentRow.Cells[3].Value.ToString();
            TxtTC.Text = DgwSirketler.CurrentRow.Cells[4].Value.ToString();
            TxtTelefon1.Text = DgwSirketler.CurrentRow.Cells[5].Value.ToString();
            TxtTelefon2.Text = DgwSirketler.CurrentRow.Cells[6].Value.ToString();
            TxtMail.Text = DgwSirketler.CurrentRow.Cells[7].Value.ToString();
            CmbIl.SelectedItem = DgwSirketler.CurrentRow.Cells[8].Value.ToString();
            CmbIlce.SelectedItem = DgwSirketler.CurrentRow.Cells[9].Value.ToString();
            TxtVergiDairesi.Text = DgwSirketler.CurrentRow.Cells[10].Value.ToString();
            TxtAdres.Text = DgwSirketler.CurrentRow.Cells[11].Value.ToString();
            TxtFaaliyet1.Text = DgwSirketler.CurrentRow.Cells[12].Value.ToString();
            TxtFaaliyet2.Text = DgwSirketler.CurrentRow.Cells[13].Value.ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtAd.Text == string.Empty)
                hata = 1;
            if (TxtYetkili.Text == string.Empty)
                hata = 1;
            if (TxtTC.Text == string.Empty)
                hata = 1;
            if (TxtYetkiliAdSoyad.Text == string.Empty)
                hata = 1;
            if (TxtTelefon1.Text == string.Empty)
                hata = 1;
            if (TxtTelefon2.Text == string.Empty)
                hata = 1;
            if (TxtMail.Text == string.Empty)
                hata = 1;
            if (CmbIl.Text == string.Empty)
                hata = 1;
            if (CmbIlce.Text == string.Empty)
                hata = 1;
            if (TxtVergiDairesi.Text == string.Empty)
                hata = 1;
            if (TxtAdres.Text == string.Empty)
                hata = 1;
            if (TxtFaaliyet1.Text == string.Empty)
                hata = 1;
            if (TxtFaaliyet2.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { 
                MySqlCommand cm = new MySqlCommand("update tbl_sirketler set Ad=@p1,Yetkili=@p2,YetkiliAdSoyad=@p3,Telefon1=@p4,Telefon2=@p5,Mail=@p6,İl=@p7,İlçe=@p8,VergiDairesi=@p9,Adres=@p10,Faaliyet1=@p11,Faaliyet2=@p12,Tc=@p14 where ID=@p13", bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", TxtAd.Text);
            cm.Parameters.AddWithValue("@p2", TxtYetkili.Text);
            cm.Parameters.AddWithValue("@p3", TxtYetkiliAdSoyad.Text);
            cm.Parameters.AddWithValue("@p4", TxtTelefon1.Text);
            cm.Parameters.AddWithValue("@p5", TxtTelefon2.Text);
            cm.Parameters.AddWithValue("@p6", TxtMail.Text);
            cm.Parameters.AddWithValue("@p7", CmbIl.Text);
            cm.Parameters.AddWithValue("@p8", CmbIlce.Text);
            cm.Parameters.AddWithValue("@p9", TxtVergiDairesi.Text);
            cm.Parameters.AddWithValue("@p10", TxtAdres.Text);
            cm.Parameters.AddWithValue("@p11", TxtFaaliyet1.Text);
            cm.Parameters.AddWithValue("@p12", TxtFaaliyet2.Text);
            cm.Parameters.AddWithValue("@p13", TxtID.Text);
            cm.Parameters.AddWithValue("@p14", TxtTC.Text);

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

        private void DgwSirketler_DoubleClick(object sender, EventArgs e)
        {
            İletisim frm = new İletisim();
            frm.mail = this.DgwSirketler.CurrentRow.Cells[7].Value.ToString();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void TxtYetkili_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void TxtYetkiliAdSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void TxtFaaliyet1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void TxtFaaliyet2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_sirketler where Ad like'" + textBox1.Text + "%'", bgl.baglanti());
            da.Fill(ds, "tbl_sirketler");
            DgwSirketler.DataSource = ds.Tables["tbl_sirketler"];
            bgl.baglanti();
        }
    }
}
