using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bringware_project.Server
{
    class Query : Connectie
    {
        public void Nieuwe_Edwin(string naam)
        {
            string query = "INSERT INTO klantenbeheer_particulier (voornaam) VALUES('" + naam + "')";
            if (this.Open() == true)
            {
                MessageBox.Show("Verbinding gelukt");
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message);
                }
                finally
                {
                    this.Sluit();
                }
            }
            else
            {
                MessageBox.Show("Verbinding mislukt");
            }
        }

        public bool Nieuwe_klant(string voornaam, string achternaam, string adres, string postcode, string gemeente, string tel, string mail, string btw_nr, char soort_project, int aangenomen)
        {
            /* De query */
            string query = "INSERT INTO klantenbeheer_particulier (voornaam,achternaam,adres,postcode,gemeente,tel,mail,datum,btw_nr,soort_project,aangenomen) VALUES('" + voornaam + "','" + achternaam + "','" + adres + "','" + postcode + "','" + gemeente + "','" + tel + "','" + mail + "','@1','" + btw_nr + "','" + soort_project + "','" + aangenomen + "')";
            /* Einde query */
            if (this.Open() == true)
            {
                MessageBox.Show("Verbinding gelukt");
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, this.connection);
                    /* Datum MYSQL */
                    cmd.Parameters.AddWithValue("@1", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message);
                }
                finally
                {
                    this.Sluit();
                }
                return true;
                
            }
            else
            {
                MessageBox.Show("FOUT -> Nieuwe_klant() | Query.cs");
                return false;
            }
        }
    }
}
