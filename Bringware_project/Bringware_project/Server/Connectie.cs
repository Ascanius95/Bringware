using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bringware_project.Server
{
    public class Connectie
    {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Connectie()
        {
            Initialiseer();
        }

        //Initialize values
        private void Initialiseer()
        {
            server = "176.31.191.246";
            database = "BringWare";
            uid = "*****";
            password = "*******";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        public bool Open()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //0: server niet beschikbaar
                //1045: foute username of password
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Server niet beschikbaar", "Fout", MessageBoxButton.OK, MessageBoxImage.Question);
                        break;

                    case 1045:
                        MessageBox.Show("Foute username of password", "Fout", MessageBoxButton.OK, MessageBoxImage.Question);
                        break;
                }
                return false;
            }
        }
        public bool Sluit()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
