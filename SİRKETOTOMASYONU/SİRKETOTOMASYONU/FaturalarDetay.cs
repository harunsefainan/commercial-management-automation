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
    public partial class FaturalarDetay : Form
    {
        public FaturalarDetay()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        void Müsterilistele()
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_musteriler", bgl.baglanti());
            da.Fill(ds);
            CmbMusteri.ValueMember = "ID";
            CmbMusteri.DisplayMember = "Ad";
            CmbMusteri.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }
        void ürünlistele()
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_urunler", bgl.baglanti());
            da.Fill(ds);
            CmbUrun.ValueMember = "ID";
            CmbUrun.DisplayMember = "UrunAd";
            CmbUrun.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }
        void Personellistele()
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_personel", bgl.baglanti());
            da.Fill(ds);
            CmbPersonel.ValueMember = "ID";
            CmbPersonel.DisplayMember = "Ad";
            CmbPersonel.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }
        void listele()
        {
            TxtID.Enabled = false;
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("FaturaListele", bgl.baglanti());
            da.Fill(ds);
            DgwFaturalarDetay.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }

        void temizle()
        {
            TxtID.Clear();
            TxtAdet.Value = 0;
            TxtFiyat.Clear();
            TxtToplamFiyat.Clear();
            TxtTarih.Clear();
        }
        private void FaturalarDetay_Load(object sender, EventArgs e)
        {
            listele();
            ürünlistele();
            Müsterilistele();
            Personellistele();
        }

        private void DgwFaturalarDetay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = DgwFaturalarDetay.CurrentRow.Cells[0].Value.ToString();
            CmbMusteri.Text = DgwFaturalarDetay.CurrentRow.Cells[1].Value.ToString();
            CmbPersonel.Text = DgwFaturalarDetay.CurrentRow.Cells[2].Value.ToString();
            CmbUrun.Text = DgwFaturalarDetay.CurrentRow.Cells[3].Value.ToString();
            TxtAdet.Text = DgwFaturalarDetay.CurrentRow.Cells[4].Value.ToString();
            TxtFiyat.Text = DgwFaturalarDetay.CurrentRow.Cells[5].Value.ToString();
            TxtToplamFiyat.Text = DgwFaturalarDetay.CurrentRow.Cells[6].Value.ToString();
            TxtTarih.Text = DgwFaturalarDetay.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (CmbUrun.Text == string.Empty)
                hata = 1;
            if (TxtAdet.Text == string.Empty)
                hata = 1;
            if (TxtFiyat.Text == string.Empty)
                hata = 1;
            if (TxtToplamFiyat.Text == string.Empty)
                hata = 1;
            if (CmbMusteri.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MySqlCommand cm = new MySqlCommand("insert into tbl_faturadetay(UrunID,MusteriID,Adet,Personel,Fiyat,ToplamFiyat,Tarih) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
                cm.Parameters.AddWithValue("@p1", CmbUrun.SelectedValue.ToString());
                cm.Parameters.AddWithValue("@p2", CmbMusteri.SelectedValue.ToString());
                cm.Parameters.AddWithValue("@p3", int.Parse(TxtAdet.Text));
                cm.Parameters.AddWithValue("@p4", CmbPersonel.SelectedValue.ToString());
                cm.Parameters.AddWithValue("@p5", decimal.Parse(TxtFiyat.Text));
                cm.Parameters.AddWithValue("@p6", decimal.Parse(TxtToplamFiyat.Text));
                cm.Parameters.AddWithValue("@p7", TxtTarih.Text);
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
            
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (CmbUrun.Text == string.Empty)
                hata = 1;
            if (TxtAdet.Text == string.Empty)
                hata = 1;
            if (TxtFiyat.Text == string.Empty)
                hata = 1;
            if (TxtToplamFiyat.Text == string.Empty)
                hata = 1;
            if (CmbMusteri.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { 
            MySqlCommand cm = new MySqlCommand("update tbl_faturadetay set UrunID=@p1,MusteriID=@p2,Adet=@p3,Personel=@p4,Fiyat=@p5,ToplamFiyat=@p6,Tarih=@p7 where ID=@p8", bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", CmbUrun.SelectedValue.ToString());
            cm.Parameters.AddWithValue("@p2", CmbMusteri.SelectedValue.ToString());
            cm.Parameters.AddWithValue("@p3", int.Parse(TxtAdet.Text));
            cm.Parameters.AddWithValue("@p4", CmbPersonel.SelectedValue.ToString());
            cm.Parameters.AddWithValue("@p5", decimal.Parse(TxtFiyat.Text));
            cm.Parameters.AddWithValue("@p6", decimal.Parse(TxtToplamFiyat.Text));
            cm.Parameters.AddWithValue("@p7", TxtTarih.Text);
            cm.Parameters.AddWithValue("@p8", TxtID.Text);



            int basari = cm.ExecuteNonQuery();//yazdırma
     
            if (basari == 1)
            {
                MessageBox.Show("KAYIT GÜNCELLENDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("KAYIT GÜNCELLENMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                bgl.baglanti().Close();//bağlantı kapatma  
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
                MySqlCommand komut = new MySqlCommand("select * from tbl_faturadetay where ID= '" + TxtID.Text + "'", bgl.baglanti());
                komut.ExecuteNonQuery();
                MySqlDataReader dr = komut.ExecuteReader();//komutu  tutmak için
                if (dr.Read())
                {
                    MySqlCommand cm = new MySqlCommand("delete  from tbl_faturadetay   where ID= '" + TxtID.Text + "'", bgl.baglanti());//ID'ye gçre silme komutu
                    int basari = cm.ExecuteNonQuery();//komutu okumak için
                    if (basari == 1)
                    {
                        MessageBox.Show("FATURA DETAY SİLİNDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("FATURA DETAY SİLİMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else//aynı veriden varsa
                {
                    MessageBox.Show("BÖYLE BİR FATURA DETAY BULUNAMADI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();//bağlantı kapatma
            }
            listele();
            temizle();
        }

        private void CmbUrun_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CmbUrun.Items.Clear();
            MySqlCommand cm = new MySqlCommand("select SatısFiyatı from tbl_urunler where ID=@p1", bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", CmbUrun.SelectedIndex+1);
            MySqlDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                TxtFiyat.Text = dr["SatısFiyatı"].ToString();
            }
            bgl.baglanti().Close();
        }

        private void TxtFiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TxtToplamFiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select tbl_faturadetay.ID,tbl_musteriler.Ad as 'MüşteriAd'," +
                "tbl_personel.Ad as 'PersonelAd',UrunAd as 'ÜrünAd',tbl_faturadetay.Adet,Fiyat,ToplamFiyat,Tarih from tbl_faturadetay " +
                "inner join tbl_urunler on tbl_faturadetay.UrunID = tbl_urunler.ID " +
                "inner join tbl_musteriler on tbl_faturadetay.MusteriID = tbl_musteriler.ID " +
                "inner join tbl_personel on tbl_faturadetay.Personel = tbl_personel.ID " +
                "where MusteriID like '" + textBox1.Text + "%'", bgl.baglanti());
            da.Fill(ds, "tbl_faturadetay");
            DgwFaturalarDetay.DataSource = ds.Tables["tbl_faturadetay"];
            bgl.baglanti().Close();
        }
    }
}
