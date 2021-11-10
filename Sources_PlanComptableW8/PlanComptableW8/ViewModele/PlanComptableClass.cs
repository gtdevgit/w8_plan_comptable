using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Globalization;
using System.Threading;
using Windows.UI.Xaml;
using PlanComptableW8.Modele;
using System.Windows.Input;

namespace PlanComptableW8.ViewModele
{
    public class PlanComptableClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IEnumerable<CompteClass> ListeCompte
        {
            get
            {
                if (listeCompte == null)
                {
                    listeCompte = from compte in Donnees.TAB_PC_DEVELOPPE
                                  where (compte.Parent == -1)
                                  select compte;                    
                }
                return listeCompte;
            }

            set
            {
                if (listeCompte != value)
                {
                    listeCompte = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("ListeCompte"));
                }
            }
        }
        private IEnumerable<CompteClass> listeCompte = null;

        public CompteClass CompteEnCours
        {
            get
            {
                if (compteEnCours == null)
                    compteEnCours = Donnees.TAB_PC_DEVELOPPE[0];
                return compteEnCours;
            }

            set 
            {
                if (compteEnCours != value)
                {                   
                    compteEnCours = value;
                    RazInfoRecherche();
                    if (compteEnCours.ID > -1)
                        SousTitre = compteEnCours.Racine + " : " + compteEnCours.Intitule;
                    else
                        SousTitre = "Classe de comptes";                    
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CompteEnCours"));
                        PropertyChanged(this, new PropertyChangedEventArgs("AfficherCompteEnCours"));
                        PropertyChanged(this, new PropertyChangedEventArgs("RetourPossible"));
                    }
                }
            }
        }
        private CompteClass compteEnCours = null;

        public string SousTitre
        {
            get
            {
                return sousTitre;
            }

            set 
            {
                if (sousTitre != value)
                {
                    sousTitre = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SousTitre"));
                    }
                }
            }
        }
        private string sousTitre = null;

        public string InfoRecherche
        {
            get
            {
                return infoRecherche;
            }

            set
            {
                if (infoRecherche != value)
                {
                    infoRecherche = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("InfoRecherche"));
                    }
                }
            }
        }
        private string infoRecherche = null;

        private void RazInfoRecherche()
        {
            InfoRecherche = string.Empty;
        }

        public void ChercherEnfant(CompteClass c)
        {
            if ((c != null) && (c.ADesEnfants))
            {
                RazInfoRecherche();
                CompteEnCours = c;
                IEnumerable<CompteClass> liste = from compte in Donnees.TAB_PC_DEVELOPPE
                                             where (compte.Parent == c.ID)
                                             select compte;

                if ((liste != null) && (liste.Count() > 0))
                {                  
                    ListeCompte = liste;
                }
            }
        }

        private void ChercherParent(CompteClass c)
        {
            if (c != null)
            {
                RazInfoRecherche();
                IEnumerable<CompteClass> liste = from compte in Donnees.TAB_PC_DEVELOPPE
                                                 where (compte.ID == c.Parent)
                                                 select compte;               
                // Si on trouve le prarent
                if ((liste != null) && (liste.Count() == 1))
                {
                    CompteEnCours = liste.ElementAt(0);
                    ChercherEnfant(CompteEnCours);
                }
            }
        }

        public bool RetourPossible
        {
            get
            {
                // interdit le retour si on est déjà à la racine des comptes
                return ((compteEnCours != null) && (compteEnCours.ID > -1));
            }
        }

        public void Chercher(string query) 
        {
            RazInfoRecherche();
            if (query == null)
                query = string.Empty;
            else
                query = query.Trim();

            if (string.IsNullOrEmpty(query))
            {
                CompteClass c = Donnees.TAB_PC_DEVELOPPE[0];
                ChercherEnfant(c);
            }
            else
            {
                IEnumerable<CompteClass> liste = from compte in Donnees.TAB_PC_DEVELOPPE
                                                 where (compte.ID != -1) && (compte.IntituleContiend(query) || (compte.Racine.IndexOf(query) == 0))
                                                 orderby (compte.Racine)
                                                 select compte;
                
                if ((liste != null) && (liste.Count() > 0))
                {
                    ListeCompte = liste;
                    SousTitre = string.Format("{0} résultat(s) pour \"{1}\"", liste.Count().ToString(), query);
                    CompteEnCours = ListeCompte.FirstOrDefault();
                    InfoRecherche = string.Format("{0} résultat(s) pour \"{1}\"", liste.Count().ToString(), query);                    
                }
            }  
        }

        public Favoris MesFavoris
        {
            get
            {
                if (mesFavoris == null)
                {
                    mesFavoris = new Favoris();
                }
                return mesFavoris;
            }
        }
        private Favoris mesFavoris = null;

        public IEnumerable<CompteClass> MesFavorisOrdones 
        {
            get 
            {
                mesFavorisOrdonnes = from compte in MesFavoris
                                     orderby compte.Racine
                                     select compte;
                return mesFavorisOrdonnes;                                     
            }        
        }
        private IEnumerable<CompteClass> mesFavorisOrdonnes = null;
        public void AjouterFavoris()
        {
            // MesFavoris hérite de ObservableCollection, donc pas la peine de faire un RaisePropertyChanged
            MesFavoris.Ajouter(compteEnCours);
            // Par contre il faut rafraichir la liste qui est ordonné.
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("MesFavorisOrdones"));
            }             
        }

        #region CommandClassRetour
        public void Retour()
        {
            RazInfoRecherche();
            if (compteEnCours != null)
            {
                if (compteEnCours.ID == -1)
                    ChercherEnfant(compteEnCours);
                else
                    ChercherParent(compteEnCours);
            }
        }

        // propriétée pour le binding
        public ICommand CommandRetour
        {
            get
            {
                return new CommandClassRetour(this);
            }
        }
        #endregion
    }
}
