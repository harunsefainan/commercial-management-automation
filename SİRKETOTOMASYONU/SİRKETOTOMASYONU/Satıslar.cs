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
    public partial class Satıslar : Form
    {
        public Satıslar()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        void listele()
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter("select UrunAd as 'ÜrünAd',tbl_faturadetay.Adet," +
                "Fiyat,ToplamFiyat,Tarih from tbl_faturadetay " +
                "inner join tbl_urunler on tbl_faturadetay.UrunID = tbl_urunler.ID " +
                "inner join tbl_musteriler on tbl_faturadetay.MusteriID = tbl_musteriler.ID " +
                "inner join tbl_personel on tbl_faturadetay.Personel = tbl_personel.ID", bgl.baglanti());
            da.Fill(ds);
            DgwSatıslar.DataSource = ds.Tables[0];
            bgl.baglanti().Close();//bağlantı kapatma
        }
        private void Satıslar_Load(object sender, EventArgs e)
        {
            listele();
        }
    }
}
