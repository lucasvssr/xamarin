using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TP4_BD.Model
{
    public class DepotFilms
    {
        private SQLiteAsyncConnection dbConn;

        #region Données internes
        private static List<Film> __data = new List<Film>
        {
            new Film() {
                Titre = "Les évadés",
                Année = 1994,Réalisateur = "Frank Darabont",
                Type = Categorie.Drame,
            },
            new Film() {
                Titre = "Le bon, la brute et le truand",
                Année = 1966,
                Réalisateur = "Sergio Leone",
                Type = Categorie.Western,
            },
            new Film() {
                Titre = "Matrix",
                Année = 1999,
                Réalisateur = "Lana Wachowski",
                Type = Categorie.ScienceFiction,
            },
            new Film() {
                Titre = "Le silence des agneaux",
                Année = 1991,
                Réalisateur = "Jonathan Demme",
                Type = Categorie.Thriller,
            },
            new Film() {
                Titre = "Le voyage de Chihiro",
                Année = 2001,
                Réalisateur = "Hayao Miyazaki",
                Type = Categorie.Animation,
            },
            new Film() {
                Titre = "Les aventuriers de l’arche perdue",
                Année = 1981,
                Réalisateur = "Steven Spielberg",
                Type = Categorie.Aventure,
            },
            new Film() {
                Titre = "Les temps modernes",
                Année = 1936,
                Réalisateur = "Charlie Chaplin",
                Type = Categorie.Comédie,
            },
            new Film() {
                Titre = "Poltergeist",
                Année = 1982,
                Réalisateur = "Steven Spielberg",
                Type = Categorie.Horreur,
            },
            new Film() {
                Titre = "Le garde du corps",
                Année = 1961,
                Réalisateur = "Akira Kurosawa",
                Type = Categorie.Action,
            },
        };
        #endregion

        public async Task<IEnumerable<Film>> TousLesFilms()
        {
            List<Film> lst = await dbConn.Table<Film>().ToListAsync();
            // Si la liste est vide, on la remplit avec les données locales
            if (lst.Count == 0)
            {
                await dbConn.InsertAllAsync(DepotFilms.__data);
                lst.AddRange(DepotFilms.__data);
            }
            // On simule un temps de chargement long...
            Thread.Sleep(3000);
            return lst;
        }

        public DepotFilms(string path)
        {
            dbConn = new SQLiteAsyncConnection(Path.Combine(path, "films.db3"));
            dbConn.CreateTableAsync<Film>();
        }

        /// <summary>
        /// Ajoute ou de mets à jour un film dans la base de données.
        /// </summary>
        /// <param name="film"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> AjouterFilm(Film film)
        {
            if (film.Id == 0)
            {
                await dbConn.InsertAsync(film);
                return true;
            }
            else
            {
                await dbConn.UpdateAsync(film);
                return false;
            }
        }

        /// <summary>
        /// Reçoit en paramètre un entier représentant l’identifiant du film que l’on 
        /// cherche et qui retourne le film correspondant à l’identifiant si celui-ci 
        /// existe ou null sinon
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Film</returns>
        public async Task<Film> ChercherFilm(int id)
        {
            return await dbConn.FindAsync<Film>(id);
        }

        /// <summary>
        /// Cette méthode renvoie l’entier retourné par la méthode DeleteAsync sur la 
        /// base de données.Cet entier correspond aux nombres de lignes supprimées.
        /// </summary>
        /// <param name="film"></param>
        /// <returns>Int</returns>
        public async Task<int> SupprimerFilm(Film film)
        {
            return await dbConn.DeleteAsync(film);
        }
    }
}
