using TP3_Fichier.Data;
using Xamarin.Forms;

namespace TP3_Fichier
{
    public partial class App : Application
    {
        public static GestionCitations GérerCitations { get; internal set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            GérerCitations = new GestionCitations();
        }

        protected override void OnStart()
        {
           GérerCitations.Save();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
