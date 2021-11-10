using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanComptableW8.Modele
{
    public class ListeOnglets : ObservableCollection<Onglet>
    {

        public Onglet OngletCompte
        {
            get { return ongletCompte; }
        }
        private Onglet ongletCompte = null;


        public Onglet OngletFavoris
        {
            get { return ongletFavoris; }
        }
        private Onglet ongletFavoris = null;


        public Onglet OngletAPropos
        {
            get { return ongletAPropos; }
        }
        private Onglet ongletAPropos = null;


        public void ActiverOnglet(Onglet onglet) 
        {
            foreach (Onglet o in this)
            {
                o.Etat = (o == onglet);
            }
        }

        public Onglet OngletActif()
        {
            Onglet r = null;
            foreach (Onglet o in this)
            {
                if (o.Etat)
                {
                    r = o;
                    break;
                }
            }
            return r;
        }
        
        public ListeOnglets()
        {
            ongletCompte = new Onglet();
            ongletFavoris = new Onglet();
            ongletAPropos = new Onglet();

            Add(ongletCompte);
            Add(ongletFavoris);
            Add(ongletAPropos);
        }
    }
}
