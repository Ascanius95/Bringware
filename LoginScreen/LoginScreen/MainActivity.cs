using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;
using System.Collections.Generic;

namespace LoginScreen
{
    [Activity(Label = "LoginScreen", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnRegistreer, mBtnLogin;
        private ProgressBar mProgressbar;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            /* Layout attributen toewijzen aan code */ 
            SetContentView(Resource.Layout.Main);
            mBtnLogin = FindViewById<Button>(Resource.Id.btn_login);
            mBtnRegistreer = FindViewById<Button>(Resource.Id.btn_registreer);
            mProgressbar = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            /* Click events */
            mBtnRegistreer.Click += (object sender, EventArgs args) => //Click methode in zelfde methode
            {
                // dialog weergeven
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_registreer regdialog = new Dialog_registreer();
                regdialog.Show(transaction, "dialog fragment");

                regdialog.registreerCompleet += Regdialog_registreerCompleet;
            };
            mBtnLogin.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_inloggen inlogdialog = new Dialog_inloggen();
                inlogdialog.Show(transaction, "dialog fragment");

                inlogdialog.logincompleet += Inlogdialog_logincompleet;


            };

        }

        private void Inlogdialog_logincompleet(object sender, LoginEvent e)
        {
          //  code hier als ingelogd
        }

        private void Regdialog_registreerCompleet(object sender, RegistreerEvent e)
        {
            ShowAlert("U bent geregistreerd");
        }
        public void ShowAlert(string str)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(str);
            alert.SetPositiveButton("OK", (senderAlert, args) => {
                // ja code
            });

            RunOnUiThread(() => {
                alert.Show();
            });
        }
    }
}

