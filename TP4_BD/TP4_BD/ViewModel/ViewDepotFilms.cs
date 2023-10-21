using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP4_BD.Model;
using Xamarin.Forms;

namespace TP4_BD.ViewModel
{
    public class ViewDepotFilms
    {
        private DepotFilms __depotFilms;

        private ObservableCollection<ViewFilm> __films;

        public ObservableCollection<ViewFilm> Films
        {
            get
            {
                if (__films.Count == 0)
                {
                    Task.Run(async () =>
                    {
                        var allFilms = await __depotFilms.TousLesFilms();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            foreach (var f in allFilms)
                            {
                                __films.Add(new ViewFilm(f));
                            }
                        });
                    });
                }
                return __films;
            }
        }

        public ViewDepotFilms(string path)
        {
            __depotFilms = new DepotFilms(path);
            __films = new ObservableCollection<ViewFilm>();
        }

        /// <summary>
        /// Cette méthode appelle la méthode AjouterFilm de la classe DepotFilms et 
        /// l’ajoute à la collection observable uniquement si la méthode appelée
        /// retourne true.
        /// </summary>
        /// <param name="film"></param>
        public async void AjouterFilm(ViewFilm film)
        {
            bool res = await __depotFilms.AjouterFilm(film.Film);

            if (res) __films.Add(film);
        }

        /// <summary>
        /// Cette méthode récupère depuis la base de données le film correspondant à 
        /// l’identifiant du ViewFilm, si ce dernier est non nul.Si le film retourné
        /// est non nul, la méthode restaure les propriétés du paramètre à l’aide de 
        /// ce film.
        /// </summary>
        /// <param name="viewFilm"></param>
        public async void RestaurerFilm(ViewFilm viewFilm)
        {
            Film film = await __depotFilms.ChercherFilm(viewFilm.Film.Id);
            if (film != null)
            {
                viewFilm.Film = film;
            }
        }

        /// <summary>
        /// Cette méthode tente de supprimer le film correspondant de la base de 
        /// données.Si l’opération réussit, la méthode supprime le film de la
        /// collection observable.
        /// </summary>
        /// <param name="viewFilm"></param>
        public async void SupprimerFilm(ViewFilm viewFilm)
        {
            int res = await __depotFilms.SupprimerFilm(viewFilm.Film);
            if (res != 0) __films.Remove(viewFilm);
        }
    }
}
