using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace PlanComptableW8.Modele
{
    public static class SaveHelper
    {
        public static void Sauvegarder(Favoris Liste)
        {
            StorageHelper.Serialiser<Favoris>(Liste);
        }
    }

    [DataContract]
    public class Favoris : ObservableCollection<CompteClass>
    {
      
        public Favoris() 
        {
            Restaurer();
        }

        public void Sauvegarder() 
        {
            //SaveHelper.Sauvegarder(this);
        }

        public void Restaurer()
        { 
        }

        public void Ajouter(CompteClass c)
        {
            if (c != null)
            {
                int idx = IndexOf(c);
                if (idx == -1)
                {
                    Add(c);
                    Sauvegarder();                    
                }
            }
        }

        public void Supprimer(CompteClass c)
        {
            if (c != null)
            {
                int idx = IndexOf(c);
                if ((idx >=0) && (idx < Count))
                {
                    RemoveAt(idx);
                    Sauvegarder();
                }
            }
        }

        public void SupprimerTous()
        {
            Clear();
            Sauvegarder();
        }
    }
}
