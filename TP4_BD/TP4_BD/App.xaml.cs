using System;
using TP4_BD.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP4_BD
{
    public partial class App : Application
    {
        public ViewDepotFilms DépotFilms { get; }

        public App()
        {
            InitializeComponent();

            DépotFilms = new ViewDepotFilms(FileSystem.AppDataDirectory);
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
