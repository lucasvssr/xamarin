using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TP4_BD.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP4_BD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjoutPage : ContentPage
    {
        public ViewFilm Film { get; set; }
        public Array TypesFilms => ViewFilm.TypesFilms;

        public ICommand Validate { get; set; }
        public ICommand Cancel { get; set; }

        private void PropriétéModifiée(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewFilm.Film.Titre))
            {
                (this.Validate as Command).ChangeCanExecute();
            }
        }

        public AjoutPage(ViewFilm film = null)
        {
            if (film != null)
            {
                this.Film = film;
                this.Title = "Modifier le film";
            }
            else
            {
                this.Film = new ViewFilm();
            }
            this.Film.PropertyChanged += PropriétéModifiée;

            this.Cancel = new Command(OnCancel);
            this.Validate = new Command(OnValidate, CanValidate);

            InitializeComponent();
        }

        /// <summary>
        /// La méthode retournera true si le titre n’est pas vide
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean CanValidate()
        {
            if (string.IsNullOrWhiteSpace(Film.Film.Titre))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Enregistre le film dans la base de données
        /// </summary>
        public async void OnValidate()
        {
            (Application.Current as App).DépotFilms.AjouterFilm(Film);
            await this.Navigation.PopModalAsync();
        }

        /// <summary>
        /// Restaure le film si l’utilisateur clique sur le bouton « Annuler ».
        /// </summary>
        public async void OnCancel()
        {
            (Application.Current as App).DépotFilms.RestaurerFilm(Film);
            await this.Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}