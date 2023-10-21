using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TP5_Reseau.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP5_Reseau
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmPage : ContentPage
    {
        public ListeViewFilm Films 
        { 
            get => (Application.Current as App).ListeFilms; 
        }

        public ICommand SélectionFilm { get; private set; }

        public FilmPage()
        {
            this.SélectionFilm = new Command<ViewFilm>(OnSélectionFilm);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Films.Initialiser();
        }

        private void OnSélectionFilm(ViewFilm film)
        {
            this.Navigation.PushModalAsync(new FilmDetailPage(film));
        }
    }
}