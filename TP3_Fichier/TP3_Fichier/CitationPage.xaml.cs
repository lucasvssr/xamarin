using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP3_Fichier.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP3_Fichier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CitationPage : ContentPage
    {
        private Citation _citation;

        public Citation Citation
        {
            get => _citation;
            set
            {
                if (value != null)
                {
                    _citation = value;
                    IList<Citation> list = App.GérerCitations.Citations;
                    list.Add(_citation);
                }
            }
        }

        public CitationPage(Citation c = null)
        {
            InitializeComponent();
            _citation = c;
            if (_citation != null)
            {
                edTexte.Text = _citation.Texte;
                entryAuteur.Text = _citation.Auteur;
            }
            else
            {
                butDelete.IsVisible = false;
                butDelete.IsEnabled = false;
            }
        }

        private async void butSave_Clicked(object sender, EventArgs e)
        {
            if (_citation != null)
            {
                _citation.Auteur = entryAuteur.Text;
                _citation.Texte = edTexte.Text;
            }
            else
            {
                _citation = new Citation
                {
                    Texte = edTexte.Text,
                    Auteur = entryAuteur.Text
                };
                App.GérerCitations.Citations.Add(_citation);
            }
            App.GérerCitations.Save();
            Debug.WriteLine("Liste des citations :");
            foreach (Citation citation in App.GérerCitations.Citations)
            {
                Debug.WriteLine(citation.Texte + " - " + citation.Auteur);
            }
            await this.Navigation.PopModalAsync();
        }

        private void butCancel_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }

        private async void butDelete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Suppression","Vous êtes sur le point de supprimer cette citation", "Confirmer", "Annuler");
            if (answer)
            {
                App.GérerCitations.Citations.Remove(_citation);
                App.GérerCitations.Save();
            }
            await this.Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            if (IsModified())
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (await DisplayAlert("Confirmation", "Voulez-vous vraiment quitter sans sauvegarder ?", "Oui", "Non"))
                    {
                        await Navigation.PopModalAsync();
                    }
                });
                // Annuler le retour en arrière (sortie de la page)
                return true;
            }
            else
            {
                // Sortir de la page sans confirmation
                return base.OnBackButtonPressed();
            }
        }

        private bool IsModified()
        {
            if(_citation.Auteur != entryAuteur.Text || _citation.Texte != edTexte.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}