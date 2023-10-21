using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using TP4_BD.Model;

namespace TP4_BD.ViewModel
{
    public class ViewFilm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Film _film { get; set; }

        public Film Film
        {
            get => _film;
            set
            {
                if (value != _film)
                {
                    _film = value;
                    NotifyPropertyChanged(nameof(Film));
                }
            }
        }

        public string Titre
        {
            get => Film.Titre;
            set
            {
                if (value != _film.Titre)
                {
                    _film.Titre = value;
                    NotifyPropertyChanged(nameof(Film.Titre));
                }
            }
        }

        public int Année
        {
            get => Film.Année;
            set
            {
                if (value != _film.Année)
                {
                    _film.Année = value;
                    NotifyPropertyChanged(nameof(Film.Année));
                }
            }
        }

        public string Réalisateur
        {
            get => Film.Réalisateur;
            set
            {
                if (value != _film.Réalisateur)
                {
                    _film.Réalisateur = value;
                    NotifyPropertyChanged(nameof(Film.Réalisateur));
                }
            }
        }

        public static Array TypesFilms => Enum.GetValues(typeof(Categorie));

        public ViewFilm(Film film = null)
        {
            Film = film ?? new Film();
        }
    }
}
