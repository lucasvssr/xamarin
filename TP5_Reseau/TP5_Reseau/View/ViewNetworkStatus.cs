using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace TP5_Reseau.View
{
    public class ViewNetworkStatus : StackLayout
    {
        public Label lblHorsConnexion;
        public Image imgStatus;

        private bool isAccessing;
        private bool hasAccess;
        private Animation animation;
        private bool animationFinished = true;

        private ContentPage page = null;
        public ContentPage Page
        {
            get
            {
                if (this.page == null)
                {
                    Element e = this.Parent;
                    this.page = e as ContentPage;
                    while (this.page == null && e != null)
                    {
                        e = e.Parent;
                        this.page = e as ContentPage;
                    }
                }
                return this.page;
            }
        }

        public ViewNetworkStatus()
        {
            lblHorsConnexion = new Label
            {
                Text = "Hors Connexion",
                FontSize = 10,
                TextColor = Color.Orange,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = false
            };

            imgStatus = new Image
            {
                Source = ImageSource.FromResource("TP5_Reseau.img.wifi_off.png", typeof(ViewNetworkStatus)),
                WidthRequest = 50,
                IsVisible = false
            };

            this.Children.Add(this.imgStatus);
            this.Children.Add(this.lblHorsConnexion);

            NetworkStatus status = (Application.Current as App).NetworkStatus;
            // Le statut ne peut être nul car c’est le premier construit
            // dans le constructeur de la classe App.
            // Abonnement à l’événement PropertyChanged
            status.PropertyChanged += OnPropertyChanged;
            // Initialisation des « propriétés »
            this.OnPropertyChanged(status, new PropertyChangedEventArgs("IsAccessing"));
            this.OnPropertyChanged(status, new PropertyChangedEventArgs("HasAccess"));

            this.animation = new Animation();
            this.animation.Add(0, 0.5, new Animation(v => this.imgStatus.Opacity = v, 1.0, 0.0, Easing.Linear));
            this.animation.Add(0.5, 1.0, new Animation(v => this.imgStatus.Opacity = v, 0.0, 1.0, Easing.Linear, () =>
            {
                this.animationFinished = true;
                if (this.hasAccess)
                    this.imgStatus.IsVisible = false;
            }));
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NetworkStatus networkStatus = (NetworkStatus)sender;

            if (e.PropertyName == "IsAccessing")
            {
                // Début du bloc correspondant à e.PropertyName=="IsAccessing"
                this.isAccessing = (sender as NetworkStatus).IsAccessing;
                if (this.animationFinished && this.isAccessing)
                {
                    // Si la page de l’animation n’est pas la page courante,
                    // on ne lance pas l’animation.
                    if (this.Page != (Application.Current as App).PageCourante) return;
                    this.animationFinished = false;
                    this.imgStatus.IsVisible = true;
                    this.animation.Commit(this, "NetworkRunning",
                    length: 1000,
                    repeat: () =>
                    {
                        if (this.isAccessing)
                        {
                            this.animationFinished = false;
                            this.imgStatus.IsVisible = true;
                        }
                        return this.isAccessing;
                    });
                }
                // Fin du bloc correspondant à e.PropertyName=="IsAccessing"
            }

            if (e.PropertyName == "HasAccess")
            {
                hasAccess = networkStatus.HasAccess;

                if (hasAccess)
                {
                    imgStatus.Source = ImageSource.FromResource("TP5_Reseau.img.wifi_on.png", typeof(ViewNetworkStatus));
                    lblHorsConnexion.IsVisible = false;
                }
                else
                {
                    imgStatus.Source = ImageSource.FromResource("TP5_Reseau.img.wifi_off.png", typeof(ViewNetworkStatus));
                    lblHorsConnexion.IsVisible = true;
                }
            }
        }
    }
}
