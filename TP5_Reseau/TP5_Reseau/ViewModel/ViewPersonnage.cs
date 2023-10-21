using TP5_Reseau.SharpTrooper.Entities;

namespace TP5_Reseau.ViewModel
{
    public class ViewPersonnage : ViewEntity<People>
    {
        public string Nom { get => Entity.name; }

        public ViewPersonnage(People entity)
        {
            Entity = entity;
        }

        public ViewPersonnage()
        {
            Entity = new People();
        }
    }
}
