using Bringware_project.Server;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bringware_project.Pages.Klantenbeheer
{
    /// <summary>
    /// Interaction logic for Nieuwe_aannemer.xaml
    /// </summary>
    public partial class Nieuwe_aannemer : UserControl
    {
        public Nieuwe_aannemer()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            Query voegtoe = new Query();
            voegtoe.Nieuwe_Aannemer(txt_bedrijfsnaam.Text ,txt_voornaam.Text, txt_achternaam.Text, txt_adres.Text, txt_postcode.Text, cmb_Gem.Text, txt_gsm.Text, txt_email.Text, txt_btw.Text);
        }

        
        
        /* Gemeentes automatisch inladen in combobox a.d.h.v. postcode */
        public void updateGemeente()
        {
            string query = "SELECT Gemeente, PC FROM Gemeentes WHERE PC = '" + txt_postcode.Text + "'";
            var con = new Connectie();
            if (con.Open() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, con.connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader["Gemeente"]);
                    cmb_Gem.Items.Add(dataReader["Gemeente"]);
                }
                con.Sluit();
            }
        }

        private void cmb_Gem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            cmb_Gem.Items.Clear();
            if (!(txt_postcode.Text == null || txt_postcode.Text.Equals("")))
            {
                //
                updateGemeente();
                cmb_Gem.IsDropDownOpen = true;
            }
        }

        private void cmb_Gem_DropDownOpened(object sender, EventArgs e)
        {
            cmb_Gem.Items.Clear();
            updateGemeente();
            cmb_Gem.IsDropDownOpen = true;
        }

        private void txt_postcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_postcode.Text.Length == 4)
            {
                updateGemeente();
                cmb_Gem.IsDropDownOpen = true;
            }
        }
    }
}
