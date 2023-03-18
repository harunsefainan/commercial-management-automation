using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SİRKETOTOMASYONU
{
   
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {

        }
        
        private void button12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var urun = new Urunler
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            urun.StartPosition = FormStartPosition.CenterScreen;
            urun.ShowDialog(this);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var stok = new Stoklar
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            stok.StartPosition = FormStartPosition.CenterScreen;
            stok.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var kasa = new Musteriler
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            kasa.StartPosition = FormStartPosition.CenterScreen;
            kasa.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var kasa = new Sirketler
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            kasa.StartPosition = FormStartPosition.CenterScreen;
            kasa.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var kasa = new Personel
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            kasa.StartPosition = FormStartPosition.CenterScreen;
            kasa.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var kasa = new Giderler
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            kasa.StartPosition = FormStartPosition.CenterScreen;
            kasa.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var kasa = new Kasa
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            kasa.StartPosition = FormStartPosition.CenterScreen;
            kasa.ShowDialog(this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var kasa = new Bankalar
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            kasa.StartPosition = FormStartPosition.CenterScreen;
            kasa.ShowDialog(this);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var kasa = new FaturalarDetay
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            kasa.StartPosition = FormStartPosition.CenterScreen;
            kasa.ShowDialog(this);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var kasa = new Satıslar
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            kasa.StartPosition = FormStartPosition.CenterScreen;
            kasa.ShowDialog(this);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var kasa = new Ayarlar
            {
                ShowInTaskbar = false,
                MinimizeBox = false,
                MaximizeBox = false,
            };
            kasa.StartPosition = FormStartPosition.CenterScreen;
            kasa.ShowDialog(this);
        }
    }
}
