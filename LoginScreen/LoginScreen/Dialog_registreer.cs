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

namespace LoginScreen
{
    public class RegistreerEvent : EventArgs
    {
        private string mVoornaam, mEmail, mWachtwoord;
        public RegistreerEvent(string Voornaam, string Email, string Wachtwoord) : base()
        {
            this.Voornaam = Voornaam;
            this.Email = Email;
            this.Wachtwoord = Wachtwoord;
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
            registreerCompleet.Invoke(this, new RegistreerEvent(txt_voornaam.Text, txt_email.Text, txt_wachtwoord.Text));
            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState) // dialog animatie
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); // titel bar onzichtbaar maken
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animatie; // animatie toevoegen
        }
    }
}