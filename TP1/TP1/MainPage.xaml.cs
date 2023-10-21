using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TP1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void entNuméro_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex exp = new Regex(@"0[1-9](\.?[0-9]{2}){4}");
            butAppeler.IsEnabled = exp.IsMatch(entNuméro.Text);
        }

        private async void butAppeler_ClickedAsync(object sender, EventArgs e)
        {
            /*
            var appeler = DependencyService.Get<IAppel>();
            if (appeler != null && await this.DisplayAlert("Composer un numéro","Voulez-vous composer le numéro " + entNuméro.Text + " ?","Oui", "Non"))
            {
                appeler.Appeler(entNuméro.Text);
            }
            else
            {
                await DisplayAlert("Erreur", "Fonctionnalité non implémentée", "Ok");
            }
            */
            try
            {
                PhoneDialer.Open(entNuméro.Text);
                App.NumérosAppelés.Add(entNuméro.Text);
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Erreur", "Fonctionnalité non implémentée", "Ok");
            }
        }

        private async void butPageNuméros_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageNumeros());
        }
    }
}
