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
    public partial class Giderler : Form
    {
        public Giderler()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }      

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        sqlbaglanti bgl = new sqlbaglanti();
        
        void ayekle()
        {
            CmbAy.Items.Add("Ocak");
            CmbAy.Items.Add("Şubat");
            CmbAy.Items.Add("Mart");
            CmbAy.Items.Add("Nisan");
            CmbAy.Items.Add("Mayıs");
            CmbAy.Items.Add("Haziran");
            CmbAy.Items.Add("Temmuz");
            CmbAy.Items.Add("Ağustos");
            CmbAy.Items.Add("Eylül");
            CmbAy.Items.Add("Ekim");
            CmbAy.Items.Add("Kasım");
            CmbAy.Items.Add("Aralık");
        }
        void yılekle() 
        {
            CmbYıl.Items.Add("2021");
            CmbYıl.Items.Add("2022");
            CmbYıl.Items.Add("2023");
            CmbYıl.Items.Add("2024");
            CmbYıl.Items.Add("2025");
            CmbYıl.Items.Add("2026");
            CmbYıl.Items.Add("2027");
        }

        void listele()
        {
            TxtID.Enabled = false;
            ayekle();
            yılekle();
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("Select * from tbl_giderler", bgl.baglanti());
            da.Fill(ds);
            DgwGiderler.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }
        void temizle()
        {
            TxtID.Clear();
            TxtElektrik.Clear();
            TxtSu.Clear();
            TxtDogalgaz.Clear();
            TxtInternet.Clear();
            TxtKira.Clear();
            TxtMaaslar.Clear();
            CmbAy.Items.Clear();
            CmbYıl.Items.Clear();
        }
        private void Giderler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtElektrik.Text == string.Empty)
                hata = 1;
            if (TxtSu.Text == string.Empty)
                hata = 1;
            if (TxtDogalgaz.Text == string.Empty)
                hata = 1;
            if (TxtInternet.Text == string.Empty)
                hata = 1;
            if (TxtKira.Text == string.Empty)
                hata = 1;
            if (TxtMaaslar.Text == string.Empty)
                hata = 1;
            if (CmbAy.Text == string.Empty)
                hata = 1;
            if (CmbYıl.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MySqlCommand cm = new MySqlCommand("insert into tbl_giderler(Elektrik,Su,Dogalgaz,Internet,Kira,Maaslar,Ay,Yıl) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                cm.Parameters.AddWithValue("@p1", TxtElektrik.Text);
                cm.Parameters.AddWithValue("@p2", TxtSu.Text);
                cm.Parameters.AddWithValue("@p3", TxtDogalgaz.Text);
                cm.Parameters.AddWithValue("@p4", TxtInternet.Text);
                cm.Parameters.AddWithValue("@p5", TxtKira.Text);
                cm.Parameters.AddWithValue("@p6", TxtMaaslar.Text);
                cm.Parameters.AddWithValue("@p7", CmbAy.Text);
                cm.Parameters.AddWithValue("@p8", CmbYıl.Text);
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

        private void DgwGiderler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = DgwGiderler.CurrentRow.Cells[0].Value.ToString();
            TxtElektrik.Text = DgwGiderler.CurrentRow.Cells[1].Value.ToString();
            TxtSu.Text = DgwGiderler.CurrentRow.Cells[2].Value.ToString();
            TxtDogalgaz.Text = DgwGiderler.CurrentRow.Cells[3].Value.ToString();
            TxtInternet.Text = DgwGiderler.CurrentRow.Cells[4].Value.ToString();
            TxtKira.Text = DgwGiderler.CurrentRow.Cells[5].Value.ToString();
            TxtMaaslar.Text = DgwGiderler.CurrentRow.Cells[6].Value.ToString();
            CmbAy.Text = DgwGiderler.CurrentRow.Cells[7].Value.ToString();
            CmbYıl.Text = DgwGiderler.CurrentRow.Cells[8].Value.ToString();
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
                MySqlCommand komut = new MySqlCommand("select * from tbl_giderler where ID= '" + TxtID.Text + "'", bgl.baglanti());
                komut.ExecuteNonQuery();
                MySqlDataReader dr = komut.ExecuteReader();//komutu  tutmak için
                if (dr.Read())
                {
                    MySqlCommand cm = new MySqlCommand("delete  from tbl_giderler where ID= '" + TxtID.Text + "'", bgl.baglanti());//ID'ye gçre silme komutu
                    int basari = cm.ExecuteNonQuery();//komutu okumak için
                    bgl.baglanti().Close();//bağlantı kapatma
                    if (basari == 1)
                    {
                        MessageBox.Show("GİDER SİLİNDİ", "MESAJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("GİDER SİLİMEDİ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else//aynı veriden varsa
                {
                    MessageBox.Show("BÖYLE BİR GİDER BULUNAMADI", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                temizle();
            }
            listele();        
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtElektrik.Text == string.Empty)
                hata = 1;
            if (TxtSu.Text == string.Empty)
                hata = 1;
            if (TxtDogalgaz.Text == string.Empty)
                hata = 1;
            if (TxtInternet.Text == string.Empty)
                hata = 1;
            if (TxtKira.Text == string.Empty)
                hata = 1;
            if (TxtMaaslar.Text == string.Empty)
                hata = 1;
            if (CmbAy.Text == string.Empty)
                hata = 1;
            if (CmbYıl.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { 
                MySqlCommand cm = new MySqlCommand("update tbl_giderler set Elektrik=@p1,Su=@p2,Dogalgaz=@p3,Internet=@p4,Kira=@p5,Maaslar=@p6,Ay=@p7,Yıl=@p8 where ID=@p9", bgl.baglanti());
            cm.Parameters.AddWithValue("@p1", decimal.Parse(TxtElektrik.Text));
            cm.Parameters.AddWithValue("@p2", decimal.Parse(TxtSu.Text));
            cm.Parameters.AddWithValue("@p3", decimal.Parse(TxtDogalgaz.Text));
            cm.Parameters.AddWithValue("@p4", decimal.Parse(TxtInternet.Text));
            cm.Parameters.AddWithValue("@p5", decimal.Parse(TxtKira.Text));
            cm.Parameters.AddWithValue("@p6", decimal.Parse(TxtMaaslar.Text));
            cm.Parameters.AddWithValue("@p7", CmbAy.Text);
            cm.Parameters.AddWithValue("@p8", CmbYıl.Text);
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
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_giderler where Ay like'" + textBox1.Text + "%'", bgl.baglanti());
            da.Fill(ds, "tbl_giderler");
            DgwGiderler.DataSource = ds.Tables["tbl_giderler"];
            bgl.baglanti().Close();
        }
    }
}
