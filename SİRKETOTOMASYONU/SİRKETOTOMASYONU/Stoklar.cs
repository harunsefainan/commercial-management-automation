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
    public partial class Stoklar : Form
    {
        public Stoklar()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        private void Stoklar_Load(object sender, EventArgs e)
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select UrunAd,sum(Adet) as 'Adet' from tbl_urunler group by UrunAd", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;    

            MySqlCommand cm= new MySqlCommand("select UrunAd,sum(Adet) as 'Adet' from tbl_urunler group by UrunAd", bgl.baglanti());
            MySqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Series1"].Points.AddXY(dr[0], dr[1]);
            }
            bgl.baglanti().Close();//bağlantı kapatma
        }
    }
}
