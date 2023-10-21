using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.Xaml;

namespace TP1
{
    public partial class App : Application
    {
        public static List<string> NumérosAppelés { get; set; }
        const string NUMEROS_APPELES = "";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            NumérosAppelés = new List<string>();
        }

        protected override void OnStart()
        {
            if (Properties.ContainsKey(NUMEROS_APPELES))
            {
                NumérosAppelés = Properties[NUMEROS_APPELES].ToString().Split(';').ToList();
            }
        }

        protected override void OnSleep()
        {
            Properties[NUMEROS_APPELES] = string.Join(";", NumérosAppelés);
        }

        protected override void OnResume()
        {
        }
    }
}
