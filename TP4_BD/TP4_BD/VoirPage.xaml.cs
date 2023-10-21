using System;
using System.Collections.Generic;
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
    public partial class VoirPage : ContentPage
    {
        public ViewFilm Film { get; set; }

        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }

        public VoirPage(ViewFilm film)
        {
            Film = film;
            Edit = new Command(OnEdit);
            Delete = new Command(OnDelete);
            InitializeComponent();
        }

        public async void OnEdit()
        {
            await this.Navigation.PushModalAsync(new AjoutPage(Film));
        }

        public void OnDelete()
        {
            (Application.Current as App).DépotFilms.SupprimerFilm(Film);
            this.Navigation.PopAsync();
        }
    }
}