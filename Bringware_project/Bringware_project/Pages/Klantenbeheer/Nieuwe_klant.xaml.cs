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
    /// Interaction logic for nieuwe_klant.xaml
    /// </summary>
    public partial class nieuwe_klant : UserControl
    {
        public nieuwe_klant()
        {
            InitializeComponent();
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            Query voegtoe = new Query();
            voegtoe.Nieuwe_klant(txt_voornaam.Text, txt_achternaam.Text, txt_adres.Text, txt_postcode.Text, "gemeenteplaceholder", txt_gsm.Text, txt_email.Text, txt_btw.Text, SoortProject(cmb_project.Text), AangenomenCheck(chk_aangenomen));
        }

        public char SoortProject(string item)
        {
            return item[0];
        }
        public int AangenomenCheck(CheckBox check)
        {
            if(check.IsChecked.Value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            
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
            if(txt_postcode.Text.Length == 4)
            {
                updateGemeente();
                cmb_Gem.IsDropDownOpen = true;
            }
        }
    }
}
