using System;
using TP3_Fichier.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TP3_Fichier
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListePage : ContentPage
	{
		public ListePage ()
		{
			InitializeComponent ();
			lstCitations.ItemsSource = App.GérerCitations.Citations;
		}

        private async void butAjouterCitation_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new CitationPage());
        }

        private async void lstCitations_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item;
            if (item != null)
            {           
                Citation citation = item as Citation;
                if (citation != null && App.GérerCitations.Citations.Contains(citation))
                {
                    await this.Navigation.PushModalAsync(new CitationPage(citation));
                }               
            }
        }
    }
}