using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.Collections.Specialized;

namespace LoginScreen
{
    public class RegistreerEvent : EventArgs
    {
        private string mVoornaam, mEmail, mWachtwoord;
        private int mId;
        public RegistreerEvent(int id, string voornaam, string email, string wachtwoord) : base()
        {
            Id = id;
            Voornaam = voornaam;
            Email = email;
            Wachtwoord = wachtwoord;
        }
        
        public string Voornaam
        {
            get { return mVoornaam; }
            set { mVoornaam = value; }
        }

        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public string Wachtwoord
        {
            get { return mWachtwoord; }
            set { mWachtwoord = value; }
        }
        
        public int Id
        {
            get { return mId; }
            set { mId = value; }
        }


    }
    class Dialog_registreer : DialogFragment
    {
        private EditText txt_voornaam, txt_email, txt_wachtwoord;
        private Button btn_registreer;

        public event EventHandler<RegistreerEvent> registreerCompleet;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Dialog_registreer, container, false);

            txt_voornaam = view.FindViewById<EditText>(Resource.Id.txt_voornaam);
            txt_email = view.FindViewById<EditText>(Resource.Id.txt_mail);
            txt_wachtwoord = view.FindViewById<EditText>(Resource.Id.txt_wachtwoord);
            btn_registreer = view.FindViewById<Button>(Resource.Id.btn_dialog_registreer);

            btn_registreer.Click += Btn_registreer_Click;
            return view;
        }

        private void Btn_registreer_Click(object sender, EventArgs e)
        {
            /* Gebruiker klikte op registreer */

            WebClient client = new WebClient();
            Uri uri = new Uri("http://bringmans.be/Flyer_gebruiker.php");
            NameValueCollection parameters = new NameValueCollection();

            parameters.Add("naam", txt_voornaam.Text);
            parameters.Add("email", txt_email.Text);
            parameters.Add("wachtwoord", txt_wachtwoord.Text);

            client.UploadValuesCompleted += client_UploadValuesCompleted;
            client.UploadValuesAsync(uri, parameters);
        }
        void client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            Activity.RunOnUiThread(() =>
            {
                string id = Encoding.UTF8.GetString(e.Result); //Get the data echo backed from PHP
                int newID = 0;

               // int.TryParse(id, out newID); //Cast the id to an integer

                if (registreerCompleet != null)
                {
                    Console.WriteLine(id);
                    Console.WriteLine("GELUKT");
                    //Broadcast event
                    registreerCompleet.Invoke(this, new RegistreerEvent(newID, txt_voornaam.Text, txt_email.Text, txt_wachtwoord.Text));
                }

                this.Dismiss();
            });

        }
      
        public override void OnActivityCreated(Bundle savedInstanceState) // dialog animatie
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); // titel bar onzichtbaar maken
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animatie; // animatie toevoegen
        }
    }
}