using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace SİRKETOTOMASYONU
{
    class sqlbaglanti
    {
        public MySqlConnection baglanti()
        {
            string myConnectionString;
            myConnectionString = "server=localhost;user id=root;database=sirket;pwd=harun;";
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            conn.Open();
            return conn;
        }

        
    }
}
