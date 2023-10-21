using System.ComponentModel;

namespace TP3_Fichier.Data
{
    public class Citation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string texte;
        private string auteur;

        public string Texte
        {
            get => texte;
            set
            {
                texte = value;
                OnPropertyChanged("Texte");
            }
        }

        public string Auteur
        {
            get => auteur;
            set
            {
                auteur = value;
                OnPropertyChanged("Auteur");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
