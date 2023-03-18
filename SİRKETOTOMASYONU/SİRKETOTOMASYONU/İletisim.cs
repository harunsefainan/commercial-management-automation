using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace SİRKETOTOMASYONU
{
    public partial class İletisim : Form
    {
        public İletisim()
        {
            InitializeComponent();
        }
        public string mail;
        private void İletisim_Load(object sender, EventArgs e)
        {
            TxtMail.Text = mail;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (TxtMail.Text == string.Empty)
                hata = 1;
            if (TxtKonu.Text == string.Empty)
                hata = 1;
            if (TxtMesaj.Text == string.Empty)
                hata = 1;
            if (hata == 1)
                MessageBox.Show("BÜTÜN ALANLARI DOLDURUNUZ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else { 
            MailMessage msg = new MailMessage();
            SmtpClient gonder = new SmtpClient();
            gonder.Credentials = new NetworkCredential("harunsefa.inan@gmail.com", ".Harunfb1907");
            gonder.Port = 587;//kullanılıcak port
            gonder.Host = "smtp.gmail.com";
            gonder.EnableSsl = true;
            msg.To.Add(TxtMail.Text);
            msg.From = new MailAddress("harunsefa.inan@gmail.com");
            msg.Subject = TxtKonu.Text.ToString();
            msg.Body = TxtMesaj.Text.ToString();
            gonder.Send(msg);
            }
        }
    }
}
