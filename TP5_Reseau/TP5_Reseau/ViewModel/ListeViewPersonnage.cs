using TP5_Reseau.SharpTrooper.Entities;

namespace TP5_Reseau.ViewModel
{
    public class ListeViewPersonnage : ListeView<People, ViewPersonnage>
    {
        public ListeViewPersonnage(INetworkStatus networkStatus) : base("https://swapi.dev/api/people/", networkStatus) { }
    }
}
