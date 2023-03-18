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
    public partial class Musteriler : Form
    {
        public Musteriler()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();//Db çağırma
        void listele()
        {
            TxtID.Enabled = false;
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from tbl_musteriler",bgl.baglanti());
            da.Fill(ds);
            DgwMusteriler.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }

        void illistesi()
        {
            MySqlCommand cm = new MySqlCommand("select sehiradi from iller",bgl.baglanti());
            MySqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                CmbIl.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();//bağlantı kapatma
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
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Musteriler_Load(object sender, EventArgs e)
        {
            listele();
            illistesi();
        }
        void temizle()
        {
            TxtID.Clear();
            TxtAd.Clear();
            TxtTelefon.Clear();
            TxtTC.Clear();
            TxtMail.Clear();
            CmbIl.Items.Clear();
            CmbIlce.Items.Clear();
            TxtAdres.Clear();
            TxtVergiDairesi.Clear();
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

        

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtAd.Text == string.Empty)
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
                MySqlCommand cm = new MySqlCommand("insert into tbl_musteriler(Ad,Telefon,Tc,Mail,İl,İlçe,Adres,VergiDairesi) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",bgl.baglanti());
                cm.Parameters.AddWithValue("@p1",TxtAd.Text);
                cm.Parameters.AddWithValue("@p2",TxtTelefon.Text);
                cm.Parameters.AddWithValue("@p3",TxtTC.Text);
                cm.Parameters.AddWithValue("@p4",TxtMail.Text);
                cm.Parameters.AddWithValue("@p5",CmbIl.Text);
                cm.Parameters.AddWithValue("@p6",CmbIlce.Text);
                cm.Parameters.AddWithValue("@p7",TxtAdres.Text);
                cm.Parameters.AddWithValue("@p8",TxtVergiDairesi.Text);
                int basari=cm.ExecuteNonQuery();
                if (basari==1)
                {
                    MessageBox.Show("KAYIT EKLENDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);                  
                }
                else
                {
                    MessageBox.Show("KAYIT EKLENMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();//bağlantı kapatma
            }
            listele();
            temizle();
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
                MessageBox.Show("ALANI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MySqlCommand komut = new MySqlCommand("select * from tbl_musteriler where ID= '" + TxtID.Text + "'", bgl.baglanti());
                komut.ExecuteNonQuery();
                MySqlDataReader dr = komut.ExecuteReader();//komutu  tutmak için
                if (dr.Read())
                {
                    MySqlCommand cm = new MySqlCommand("delete  from tbl_musteriler where ID= '" + TxtID.Text + "'", bgl.baglanti());//ID'ye gçre silme komutu
                    int basari = cm.ExecuteNonQuery();//komutu okumak için
                    if (basari == 1)
                    {
                        MessageBox.Show("MÜŞTERİ SİLİNDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("MÜŞTERİ SİLİMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else//aynı veriden varsa
                {
                    MessageBox.Show("BÖYLE BİR MÜŞTERİ BULUNAMADI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                temizle();
                bgl.baglanti().Close();//bağlantı kapatma
            }
            listele();
            illistesi();
        }

        private void DgwMusteriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = DgwMusteriler.CurrentRow.Cells[0].Value.ToString();
            TxtAd.Text = DgwMusteriler.CurrentRow.Cells[1].Value.ToString();
            TxtTelefon.Text = DgwMusteriler.CurrentRow.Cells[2].Value.ToString();
            TxtTC.Text = DgwMusteriler.CurrentRow.Cells[3].Value.ToString();
            TxtMail.Text = DgwMusteriler.CurrentRow.Cells[4].Value.ToString();
            CmbIl.Text = DgwMusteriler.CurrentRow.Cells[5].Value.ToString();
            CmbIlce.Text = DgwMusteriler.CurrentRow.Cells[6].Value.ToString();
            TxtAdres.Text = DgwMusteriler.CurrentRow.Cells[7].Value.ToString();
            TxtVergiDairesi.Text = DgwMusteriler.CurrentRow.Cells[8].Value.ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtAd.Text == string.Empty)
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
                MySqlCommand cm = new MySqlCommand("update tbl_musteriler set Ad=@p1,Telefon=@p2,Tc=@p3,Mail=@p4,İl=@p5,İlçe=@p6,Adres=@p7,VergiDairesi=@p8 where ID=@p9", bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", TxtAd.Text);
            cm.Parameters.AddWithValue("@p2", TxtTelefon.Text);
            cm.Parameters.AddWithValue("@p3", TxtTC.Text);
            cm.Parameters.AddWithValue("@p4", TxtMail.Text);
            cm.Parameters.AddWithValue("@p5", CmbIl.Text);
            cm.Parameters.AddWithValue("@p6", CmbIlce.Text);
            cm.Parameters.AddWithValue("@p7", TxtAdres.Text);
            cm.Parameters.AddWithValue("@p8", TxtVergiDairesi.Text);
            cm.Parameters.AddWithValue("@p9", TxtID.Text);

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

        private void DgwMusteriler_DoubleClick(object sender, EventArgs e)
        {
            İletisim frm = new İletisim();
            frm.mail = this.DgwMusteriler.CurrentRow.Cells[4].Value.ToString();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_musteriler where Ad like'" + textBox1.Text + "%'", bgl.baglanti());
            da.Fill(ds, "tbl_musteriler");
            DgwMusteriler.DataSource = ds.Tables["tbl_musteriler"];
            bgl.baglanti();
        }
    }
}
