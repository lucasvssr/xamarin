using System.Collections.Generic;
using TP5_Reseau.SharpTrooper.Entities;

namespace TP5_Reseau.ViewModel
{
    public class ViewFilm : ViewEntity<Film>
    {
        public string Titre { get => Entity.title; }
        public string Réalisateur { get => Entity.director; }
        public string Production { get => Entity.producer; }
        public List<string> Personnages { get => Entity.characters; }

        public ViewFilm()
        {
            this.Entity = new Film();
        }

        public ViewFilm(Film film)
        {
            this.Entity = film;
        }
    }
}
