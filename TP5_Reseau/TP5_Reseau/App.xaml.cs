using System;
using System.Linq;
using TP5_Reseau.View;
using TP5_Reseau.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP5_Reseau
{
    public partial class App : Application
    {
        public ListeViewFilm ListeFilms { get; private set; }
        public ListeViewPersonnage ListePersonnage { get; private set; }
        public NetworkStatus NetworkStatus { get; private set; }

        private TabbedPage tabbedPage = new TabbedPage();

        public App()
        {
            NetworkStatus = new NetworkStatus();

            ListeFilms = new ListeViewFilm(NetworkStatus);
            ListePersonnage = new ListeViewPersonnage(NetworkStatus);

            tabbedPage.Children.Add(new MainPage());
            tabbedPage.Children.Add(new FilmPage());
            tabbedPage.Children.Add(new PersonnagePage());
            tabbedPage.Children.Add(new PlanetePage());
            tabbedPage.Children.Add(new VaisseauPage());

            this.MainPage = tabbedPage;
        }

        public ContentPage PageCourante
        {
            get
            {
                // On récupère la page courante
                ContentPage page = this.tabbedPage.CurrentPage as ContentPage;
                // On regarde si la page courante possède des pages modales
                if (page.Navigation.ModalStack.Count > 0)
                {
                    // On récupère la dernière page modale (la plus récente)
                    page = page.Navigation.ModalStack.Last() as ContentPage;
                }
                return page;
            }
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
