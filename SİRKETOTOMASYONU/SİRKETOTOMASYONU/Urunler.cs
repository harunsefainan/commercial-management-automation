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
    public partial class Urunler : Form
    {
        public Urunler()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();

        void listele()
        {
            TxtID.Enabled = false;
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_urunler ",bgl.baglanti());
            da.Fill(ds);//doldurma
            DgwUrunler.DataSource = ds.Tables[0];//nereye listeleyecek
            bgl.baglanti().Close();//bağlantı kapatma
        }
        void temizle()
        {
            TxtID.Clear();
            TxtUrunAdı.Clear();
            TxtMarka.Clear();
            TxtAlısFiyatı.Clear();
            TxtSatısFiyatı.Clear();
            TxtYıl.Clear();
            TxtModel.Clear();
            TxtAdet.Value = 0;
        }
        private void Urunler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtUrunAdı.Text == string.Empty)
                hata = 1;
            if (TxtAdet.Text == string.Empty)
                hata = 1;
            if (TxtAlısFiyatı.Text == string.Empty)
                hata = 1;
            if (TxtSatısFiyatı.Text == string.Empty)
                hata = 1;
            if (TxtMarka.Text == string.Empty)
                hata = 1;
            if (TxtModel.Text == string.Empty)
                hata = 1;
            if (TxtYıl.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MySqlCommand cm = new MySqlCommand("insert into tbl_urunler(UrunAd,Adet,AlısFiyatı,SatısFiyatı,Marka,Model,Yıl) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
                cm.Parameters.AddWithValue("@p1", TxtUrunAdı.Text);
                cm.Parameters.AddWithValue("@p2", int.Parse((TxtAdet.Value).ToString()));
                cm.Parameters.AddWithValue("@p3", decimal.Parse(TxtAlısFiyatı.Text));
                cm.Parameters.AddWithValue("@p4", decimal.Parse(TxtSatısFiyatı.Text));
                cm.Parameters.AddWithValue("@p5", TxtMarka.Text);
                cm.Parameters.AddWithValue("@p6", TxtModel.Text);
                cm.Parameters.AddWithValue("@p7", TxtYıl.Text);
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
                temizle();
            }
            listele();        
        }

        private void TxtAlısFiyatı_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TxtSatısFiyatı_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if(TxtID.Text==string.Empty)
            {
                hata = 1;
            }
            if (hata==1)
            {
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MySqlCommand komut = new MySqlCommand("select * from tbl_urunler where ID= '" + TxtID.Text + "'", bgl.baglanti());
                komut.ExecuteNonQuery();
                
                MySqlDataReader dr = komut.ExecuteReader();//komutu  tutmak için
                if (dr.Read())
                {
                    MySqlCommand cm = new MySqlCommand("delete  from tbl_urunler where ID= '" + TxtID.Text + "'", bgl.baglanti());//ID'ye göre silme komutu
                    int basari = cm.ExecuteNonQuery();//komutu okumak için
                    bgl.baglanti().Close();//bağlantı kapatma
                    if (basari==1)
                    {
                        MessageBox.Show("ÜRÜN SİLİNDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ÜRÜN SİLİMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else//aynı veriden varsa
                {
                    MessageBox.Show("BÖYLE BİR ÜRÜN BULUNAMADI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            temizle();
            listele();
        }

        private void TxtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void DgwUrunler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = DgwUrunler.CurrentRow.Cells[0].Value.ToString();
            TxtUrunAdı.Text = DgwUrunler.CurrentRow.Cells[1].Value.ToString();
            TxtMarka.Text = DgwUrunler.CurrentRow.Cells[2].Value.ToString();
            TxtModel.Text = DgwUrunler.CurrentRow.Cells[3].Value.ToString();
            TxtYıl.Text = DgwUrunler.CurrentRow.Cells[4].Value.ToString();
            TxtAdet.Text = DgwUrunler.CurrentRow.Cells[5].Value.ToString();
            TxtAlısFiyatı.Text = DgwUrunler.CurrentRow.Cells[6].Value.ToString();
            TxtSatısFiyatı.Text = DgwUrunler.CurrentRow.Cells[7].Value.ToString();        
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_urunler where UrunAd like'" + textBox1.Text + "%'", bgl.baglanti());
            da.Fill(ds, "tbl_urunler");
            DgwUrunler.DataSource = ds.Tables["tbl_urunler"];
            bgl.baglanti();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtUrunAdı.Text == string.Empty)
                hata = 1;
            if (TxtAdet.Text == string.Empty)
                hata = 1;
            if (TxtAlısFiyatı.Text == string.Empty)
                hata = 1;
            if (TxtSatısFiyatı.Text == string.Empty)
                hata = 1;
            if (TxtMarka.Text == string.Empty)
                hata = 1;
            if (TxtModel.Text == string.Empty)
                hata = 1;
            if (TxtYıl.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { 
                MySqlCommand cm = new MySqlCommand("update tbl_urunler set UrunAd=@p1,Marka=@p2,Model=@p3,Yıl=@p4,Adet=@p5,AlısFiyatı=@p6,SatısFiyatı=@p7 where ID=@p8",bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", TxtUrunAdı.Text);
            cm.Parameters.AddWithValue("@p2", TxtMarka.Text); 
            cm.Parameters.AddWithValue("@p3", TxtModel.Text);
            cm.Parameters.AddWithValue("@p4", TxtYıl.Text);
            cm.Parameters.AddWithValue("@p5", int.Parse((TxtAdet.Value).ToString()));
            cm.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlısFiyatı.Text));
            cm.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatısFiyatı.Text));
            cm.Parameters.AddWithValue("@p8", TxtID.Text);

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

        
    }
}
