using TP5_Reseau.SharpTrooper.Entities;

namespace TP5_Reseau.ViewModel
{
    public class ListeViewFilm : ListeView<Film, ViewFilm>
    {
        public ListeViewFilm(INetworkStatus networkStatus) : base("https://swapi.dev/api/films/", networkStatus) { }
    }
}
