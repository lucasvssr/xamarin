using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TP4_BD.Model;
using TP4_BD.ViewModel;
using Xamarin.Forms;

namespace TP4_BD
{
    public partial class MainPage : ContentPage
    {
        public ICommand SupprimerFilm { get; private set; }
        public ICommand ModifierFilm { get; private set; }
        public ICommand AjouterFilm { get; private set; }
        public ICommand VoirFilm { get; private set; }
        public Film Film { get; private set; }

        public ViewDepotFilms DépotFilms
        {
            get
            {
                return (Application.Current as App).DépotFilms;
            }
        }

        public MainPage()
        {
            AjouterFilm = new Command(OnAjouterFilm);
            VoirFilm = new Command<ViewFilm>(OnVoirFilm);
            ModifierFilm = new Command<ViewFilm>(OnModifierFilm);
            SupprimerFilm = new Command<ViewFilm>(OnSupprimerFilm);

            InitializeComponent();
        }

        /// <summary>
        /// Affiche la page AjoutPage en mode modal
        /// </summary>
        private void OnAjouterFilm()
        {
            this.Navigation.PushModalAsync(new AjoutPage());
        }

        /// <summary>
        /// Affiche la page VoirPage
        /// </summary>
        /// <param name="sender"></param>
        private async void OnVoirFilm(object sender)
        {
            ViewFilm film = (ViewFilm)sender;
            if (film != null)
            {
                await this.Navigation.PushAsync(new VoirPage(film));
            }

        }

        /// <summary>
        /// Méthode permettant de modifier un film
        /// </summary>
        /// <param name="sender"></param>
        private async void OnModifierFilm(object sender)
        {
            ViewFilm film = (ViewFilm)sender;
            if (film != null)
            {
                await this.Navigation.PushModalAsync(new AjoutPage(film));
            }
        }

        /// <summary>
        /// Méthode permettant de supprimer un film
        /// </summary>
        /// <param name="sender"></param>
        private void OnSupprimerFilm(object sender)
        {
            ViewFilm film = (ViewFilm)sender;
            if (film != null)
            {
                (Application.Current as App).DépotFilms.SupprimerFilm(film);
            }
        }
    }
}
