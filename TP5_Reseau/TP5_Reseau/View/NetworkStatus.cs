using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using TP5_Reseau.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TP5_Reseau.View
{
    public class NetworkStatus : INetworkStatus, INotifyPropertyChanged
    {
        private bool? canAccess = null;
        private bool isAccessing = false;

        public ContentPage Page 
        {
            get => (Application.Current as App).PageCourante;
        }

        public bool IsAccessing
        {
            get => this.isAccessing;
            set
            {
                if (isAccessing != value)
                {
                    isAccessing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAccessing)));
                }
            }
        }

        public bool HasAccess { get => this.canAccess ?? false; }

        public event PropertyChangedEventHandler PropertyChanged;

        public NetworkStatus()
        {
            Connectivity.ConnectivityChanged += this.Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            this.CheckNetworkStatus();
        }

        public async Task<bool> GetNetworkStatus()
        {
            IEnumerable<ConnectionProfile> profiles = Connectivity.ConnectionProfiles;
            bool isCellular = false;


            foreach (ConnectionProfile profile in profiles)
            {
                if (profile == ConnectionProfile.Cellular)
                {
                    isCellular = true;
                }
            }

            isAccessing = Connectivity.NetworkAccess == NetworkAccess.Internet;

            if (isAccessing && isCellular)
            {
                bool? page = await Page.DisplayAlert("Connexion payante",
                    "Votre connexion peut être payante. Voulez-vous continuer?",
                    "Oui", "Non");

                if (page == null || page == false)
                {
                    // Si page est null, refuser l'accès par défaut
                    isAccessing = false;
                }
            }
            
            return isAccessing;
        }

        public async Task<bool> CanAccess()
        {
            if (this.canAccess == null)
            {
                this.canAccess = await this.GetNetworkStatus();
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasAccess"));
            }
            return this.canAccess.Value; // Ne peut pas être nul
        }

        public async void CheckNetworkStatus()
        {
            canAccess = await this.GetNetworkStatus();
        }
    }
}
