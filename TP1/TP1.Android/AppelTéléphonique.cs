using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TP1.Droid;
using Xamarin.Forms;

using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(AppelTéléphonique))]
namespace TP1.Droid
{
    public  class AppelTéléphonique : IAppel
    {
        public bool Appeler(string numéro)
        {
            var context = MainActivity.Instance;

            if (context == null) return false;

            var intent = new Intent(Intent.ActionDial);
            intent.SetData(Uri.Parse("tel:" + numéro));

            if (IsIntentAvailable(context, intent))
            {
                context.StartActivity(intent);
                return true;
            }

            return false;
        }

        public static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;
            var list = packageManager.QueryIntentServices(intent, 0).Union(packageManager.QueryIntentActivities(intent, 0));
            
            if (list.Any()) return true;

            return false;
        }
    }
}