using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class FilmDetailPage : ContentPage
    {
        private ViewFilm viewFilm { get; set; }

        public string Titre { get => viewFilm.Titre; set => Titre = viewFilm.Titre; }
        public string Réalisateur { get => viewFilm.Réalisateur; set => Réalisateur = viewFilm.Réalisateur; }
        public string Production { get => viewFilm.Production; set => Production = viewFilm.Production; }
        public ObservableCollection<ViewPersonnage> Personnages { get; set; }

        public ICommand Retour { get; private set; }

        public FilmDetailPage(ViewFilm Film)
        {
            viewFilm = Film;
            Personnages = new ObservableCollection<ViewPersonnage>();
            Retour = new Command(OnRetour);
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ListeViewPersonnage personnage = (Application.Current as App).ListePersonnage;

            foreach (string person in viewFilm.Personnages)
            {
                ViewPersonnage viewPerson = await personnage.GetEntity(person);

                if (viewPerson != null)
                {
                    Personnages.Add(viewPerson);
                }
            }
        }

        private void OnRetour()
        {
            this.Navigation.PopModalAsync();
        }
    }
}