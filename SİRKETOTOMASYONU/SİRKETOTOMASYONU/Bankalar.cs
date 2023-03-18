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
    public partial class Bankalar : Form
    {
        sqlbaglanti bgl = new sqlbaglanti();
        public Bankalar()
        {
            InitializeComponent();
        }
        void sirketlistele()
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_sirketler", bgl.baglanti());
            da.Fill(ds);
            CmbSirketAd.ValueMember = "ID";
            CmbSirketAd.DisplayMember = "Ad";
            CmbSirketAd.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }
        
        void listele()
        {
            TxtID.Enabled = false;
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("BankalarListele", bgl.baglanti());
            da.Fill(ds);
            DgwBankalar.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        } 
        void temizle()
        {
            TxtID.Clear();
            TxtBankaAd.Clear();
            CmbIl.Items.Clear();
            CmbIlce.Items.Clear();
            TxtSube.Clear();
            TxtIban.Clear();
            TxtHesapNo.Clear();
            TxtYetkili.Clear();
            TxtTarih.Clear();
            TxtHesapTuru.Clear();
            //CmbSirketAd.Items.Clear();
        }

        private void Bankalar_Load(object sender, EventArgs e)
        {    
            listele();
            illistesi();
            sirketlistele();
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

        private void DgwBankalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = DgwBankalar.CurrentRow.Cells[0].Value.ToString();
            TxtBankaAd.Text = DgwBankalar.CurrentRow.Cells[1].Value.ToString();
            CmbIl.Text = DgwBankalar.CurrentRow.Cells[2].Value.ToString();
            CmbIlce.Text = DgwBankalar.CurrentRow.Cells[3].Value.ToString();
            TxtSube.Text = DgwBankalar.CurrentRow.Cells[4].Value.ToString();
            TxtIban.Text = DgwBankalar.CurrentRow.Cells[5].Value.ToString();
            TxtHesapNo.Text = DgwBankalar.CurrentRow.Cells[6].Value.ToString();
            TxtYetkili.Text = DgwBankalar.CurrentRow.Cells[7].Value.ToString();
            TxtTarih.Text = DgwBankalar.CurrentRow.Cells[8].Value.ToString();
            TxtHesapTuru.Text = DgwBankalar.CurrentRow.Cells[9].Value.ToString();
            CmbSirketAd.Text = DgwBankalar.CurrentRow.Cells[10].Value.ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtBankaAd.Text == string.Empty)
                hata = 1;
            if (CmbIl.Text == string.Empty)
                hata = 1;
            if (CmbIlce.Text == string.Empty)
                hata = 1;
            if (TxtSube.Text == string.Empty)
                hata = 1;
            if (TxtIban.Text == string.Empty)
                hata = 1;
            if (TxtHesapNo.Text == string.Empty)
                hata = 1;
            if (TxtYetkili.Text == string.Empty)
                hata = 1;
            if (TxtTarih.Text == string.Empty)
                hata = 1;
            if (TxtHesapTuru.Text == string.Empty)
                hata = 1;
            if (CmbSirketAd.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MySqlCommand cm = new MySqlCommand("insert into tbl_bankalar(BankaAd,İl,İlçe,Sube,Iban,HesapNo,Yetkili,Tarih,HesapTuru,Sirket) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
                cm.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
                cm.Parameters.AddWithValue("@p2", CmbIl.Text);
                cm.Parameters.AddWithValue("@p3", CmbIlce.Text);
                cm.Parameters.AddWithValue("@p4", TxtSube.Text);
                cm.Parameters.AddWithValue("@p5", TxtIban.Text);
                cm.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
                cm.Parameters.AddWithValue("@p7", TxtYetkili.Text);
                cm.Parameters.AddWithValue("@p8", TxtTarih.Text);
                cm.Parameters.AddWithValue("@p9", TxtHesapTuru.Text);
                cm.Parameters.AddWithValue("@p10", CmbSirketAd.SelectedValue.ToString());               
                int basari = cm.ExecuteNonQuery();
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
            illistesi();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtBankaAd.Text == string.Empty)
                hata = 1;
            if (CmbIl.Text == string.Empty)
                hata = 1;
            if (CmbIlce.Text == string.Empty)
                hata = 1;
            if (TxtSube.Text == string.Empty)
                hata = 1;
            if (TxtIban.Text == string.Empty)
                hata = 1;
            if (TxtHesapNo.Text == string.Empty)
                hata = 1;
            if (TxtYetkili.Text == string.Empty)
                hata = 1;
            if (TxtTarih.Text == string.Empty)
                hata = 1;
            if (TxtHesapTuru.Text == string.Empty)
                hata = 1;
            if (CmbSirketAd.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { 
                MySqlCommand cm = new MySqlCommand("update tbl_bankalar set BankaAd=@p1,İl=@p2,İlçe=@p3,Sube=@p4,Iban=@p5,HesapNo=@p6,Yetkili=@p7,Tarih=@p8,HesapTuru=@p9,Sirket=@p10 where ID=@p11", bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            cm.Parameters.AddWithValue("@p2", CmbIl.Text);
            cm.Parameters.AddWithValue("@p3", CmbIlce.Text);
            cm.Parameters.AddWithValue("@p4", TxtSube.Text);
            cm.Parameters.AddWithValue("@p5", TxtIban.Text);
            cm.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            cm.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            cm.Parameters.AddWithValue("@p8", TxtTarih.Text);
            cm.Parameters.AddWithValue("@p9", TxtHesapTuru.Text);
            cm.Parameters.AddWithValue("@p10", CmbSirketAd.SelectedValue.ToString());
            cm.Parameters.AddWithValue("@p11", TxtID.Text);
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
                MySqlCommand komut = new MySqlCommand("select * from tbl_bankalar where ID= '" + TxtID.Text + "'", bgl.baglanti());
                komut.ExecuteNonQuery();
                MySqlDataReader dr = komut.ExecuteReader();//komutu  tutmak için
                if (dr.Read())
                {
                    MySqlCommand cm = new MySqlCommand("delete  from tbl_bankalar    where ID= '" + TxtID.Text + "'", bgl.baglanti());//ID'ye gçre silme komutu
                    int basari = cm.ExecuteNonQuery();//komutu okumak için
                    bgl.baglanti().Close();//bağlantı kapatma
                    if (basari == 1)
                    {
                        MessageBox.Show("BANKA SİLİNDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("BANKA SİLİMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else//aynı veriden varsa
                {
                    MessageBox.Show("BÖYLE BİR BANKA BULUNAMADI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            listele();
            temizle();
            illistesi();
        }

        private void TxtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TxtHesapNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_bankalar where BankaAd like'" + textBox1.Text + "%'", bgl.baglanti());
            da.Fill(ds, "tbl_bankalar");
            DgwBankalar.DataSource = ds.Tables["tbl_bankalar"];
            bgl.baglanti();
        }
    }
}
