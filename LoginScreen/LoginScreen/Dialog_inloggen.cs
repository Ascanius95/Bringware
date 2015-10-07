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
    public class LoginEvent : EventArgs 
    {
        private string mEmail, mWachtwoord;

        /* constructor */
        public LoginEvent(string email, string wachtwoord) : base()
        {
            Email = email;
            Wachtwoord = wachtwoord;
        }

        public string Wachtwoord
        {
            get { return mWachtwoord; }
            set { mWachtwoord = value; }
        }
        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
    }
    class Dialog_inloggen : DialogFragment
    {
        /* Benodigde variabelen */
        private EditText txt_email, txt_wachtwoord;
        private Button btn_login;
        public event EventHandler<LoginEvent> logincompleet;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            /* Layout toewijzen */
            var view = inflater.Inflate(Resource.Layout.Dialog_inloggen, container, false);
            /* Attributen uit layout halen */
            txt_email = view.FindViewById<EditText>(Resource.Id.txt_login_email);
            txt_wachtwoord = view.FindViewById<EditText>(Resource.Id.txt_login_wachtwoord);
            btn_login = view.FindViewById<Button>(Resource.Id.btn_dialog_inloggen);

            /* de login klik */
            btn_login.Click += Btn_login_Click;

            return View;
        }

        private void Btn_login_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri("http://bringmans.be/trylogin.php");
            NameValueCollection parameters = new NameValueCollection();

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

                if (logincompleet != null)
                {
                    if (id.Equals("0"))
                    {
                        Console.WriteLine("Niet ingelogd");
                    }
                    else
                    {
                        Console.WriteLine("Je bent ingelogd");
                    }
                    //Broadcast event
                    logincompleet.Invoke(this, new LoginEvent(txt_email.Text, txt_wachtwoord.Text));
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