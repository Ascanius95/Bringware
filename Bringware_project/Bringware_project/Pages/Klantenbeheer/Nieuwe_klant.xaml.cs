using Bringware_project.Server;
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
        public Boolean AangenomenCheck(CheckBox check)
        {
            if(check.IsChecked.Value == true)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
