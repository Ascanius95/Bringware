using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;

namespace LoginScreen
{
    [Activity(Label = "LoginScreen", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnRegistreer;
        private ProgressBar mProgressbar;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            mBtnRegistreer = FindViewById<Button>(Resource.Id.btn_registreer);
            mProgressbar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            mBtnRegistreer.Click += (object sender, EventArgs args) => //Click methode in zelfde methode
            {
                // dialog weergeven
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                Dialog_registreer regdialog = new Dialog_registreer();
                regdialog.Show(transaction, "dialog fragment");

                regdialog.registreerCompleet += Regdialog_registreerCompleet;
            };
        }

        private void Regdialog_registreerCompleet(object sender, RegistreerEvent e)
        {
            /* mooie code om loading icon te laten draaien terwijl de thrad slaap */
            mProgressbar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeRequest);
            thread.Start();

        }
        private void ActLikeRequest()
        {
            Thread.Sleep(3000);
            RunOnUiThread(() => { mProgressbar.Visibility = ViewStates.Invisible; }); // #mooiecode
        }
    }
}

