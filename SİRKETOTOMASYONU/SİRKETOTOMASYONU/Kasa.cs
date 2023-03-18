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
    public partial class Kasa : Form
    {
        public Kasa()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        void listelesatıs()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select UrunAd as 'ÜrünAd',tbl_faturadetay.Adet,Fiyat,ToplamFiyat,Tarih from tbl_faturadetay " +
                "inner join tbl_urunler on tbl_faturadetay.UrunID = tbl_urunler.ID " +
                "inner join tbl_musteriler on tbl_faturadetay.MusteriID = tbl_musteriler.ID " +
                "inner join tbl_personel on tbl_faturadetay.Personel = tbl_personel.ID", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }

        void listelegider()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_giderler", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            bgl.baglanti().Close();
        }
        private void Kasa_Load(object sender, EventArgs e)
        {
            listelesatıs();
            listelegider();

            MySqlCommand cm = new MySqlCommand("select sum(ToplamFiyat) from tbl_faturadetay", bgl.baglanti());
            MySqlDataReader dr = cm.ExecuteReader();
            while(dr.Read())
            {
                label1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            MySqlCommand cm1 = new MySqlCommand("select (Elektrik+Su+Dogalgaz+Internet+Kira) from tbl_giderler order by ID ASC", bgl.baglanti());
            MySqlDataReader dr1 = cm1.ExecuteReader();
            while (dr1.Read())
            {
                label2.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            MySqlCommand cm2 = new MySqlCommand("select Maaslar from tbl_giderler order by ID ASC", bgl.baglanti());
            MySqlDataReader dr2 = cm2.ExecuteReader();
            while (dr2.Read())
            {
                label3.Text = dr2[0].ToString();
            }
            bgl.baglanti().Close();
        }


        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            groupBox1.Text = "ELEKTRİK";
            chart1.Series["AYLAR"].Points.Clear();
            sayac++;
            if (sayac>0 && sayac<=5)
            {
                MySqlCommand cm2 = new MySqlCommand("select Ay,Elektrik from tbl_giderler order by ID DESC LIMIT 4", bgl.baglanti());
                MySqlDataReader dr2 = cm2.ExecuteReader();
                while (dr2.Read())
                {
                    chart1.Series["AYLAR"].Points.AddXY(dr2[0],dr2[1]);
                }
                bgl.baglanti().Close();
            }
            if (sayac > 5 && sayac <= 10)
            {
                groupBox1.Text = "SU";
                chart1.Series["AYLAR"].Points.Clear();
                MySqlCommand cm2 = new MySqlCommand("select Ay,Su from tbl_giderler order by ID DESC LIMIT 4", bgl.baglanti());
                MySqlDataReader dr2 = cm2.ExecuteReader();
                while (dr2.Read())
                {
                    chart1.Series["AYLAR"].Points.AddXY(dr2[0], dr2[1]);
                }
                bgl.baglanti().Close();
            }
            if (sayac > 10 && sayac <= 15)
            {
                groupBox1.Text = "DOĞALGAZ";
                chart1.Series["AYLAR"].Points.Clear();
                MySqlCommand cm2 = new MySqlCommand("select Ay,Dogalgaz from tbl_giderler order by ID DESC LIMIT 4", bgl.baglanti());
                MySqlDataReader dr2 = cm2.ExecuteReader();
                while (dr2.Read())
                {
                    chart1.Series["AYLAR"].Points.AddXY(dr2[0], dr2[1]);
                }
                bgl.baglanti().Close();
            }
            if (sayac ==15)
            {
                sayac = 0;
            }
        }
    }
}
