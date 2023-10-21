using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace TP3_Fichier
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            this.IsBusy = true;
            int nbCitations = await Task<int>.Run(() =>
            {
                return App.GérerCitations.Citations.Count;
            });
            lblNombreCitations.Text = nbCitations.ToString();
            butAfficherCitations.IsEnabled = true;
            butAfficherCitations.Text = "Afficher les citations";
            this.IsBusy = false;
        }

        private async void butAfficherCitations_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new ListePage());
            Thread.Sleep(2_000);
        }
    }
}
