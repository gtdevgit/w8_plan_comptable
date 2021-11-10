using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PlanComptableW8.Modele;

// Pour en savoir plus sur le modèle d'élément Page de base, consultez la page http://go.microsoft.com/fwlink/?LinkId=234237

namespace PlanComptableW8
{
    /// <summary>
    /// Page de base qui inclut des caractéristiques communes à la plupart des applications.
    /// </summary>
    public sealed partial class MainPage : PlanComptableW8.Common.LayoutAwarePage
    {
        private PlanComptableClass planComptable = new PlanComptableClass();        

        public MainPage()
        {
            this.InitializeComponent();
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            DataContext = planComptable;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string queryText = e.Parameter.ToString();
            NaviguerVersLaFrame(planComptable.MesOnglets.OngletCompte);
            planComptable.Chercher(queryText);
        }

        /// <summary>
        /// Remplit la page à l'aide du contenu passé lors de la navigation. Tout état enregistré est également
        /// fourni lorsqu'une page est recréée à partir d'une session antérieure.
        /// </summary>
        /// <param name="navigationParameter">Valeur de paramètre passée à
        /// <see cref="Frame.Navigate(Type, Object)"/> lors de la requête initiale de cette page.
        /// </param>
        /// <param name="pageState">Dictionnaire d'état conservé par cette page durant une session
        /// antérieure. Null lors de la première visite de la page.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Conserve l'état associé à cette page en cas de suspension de l'application ou de la
        /// suppression de la page du cache de navigation. Les valeurs doivent être conformes aux
        /// exigences en matière de sérialisation de <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">Dictionnaire vide à remplir à l'aide de l'état sérialisable.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void ListViewPC_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            CompteClass C = null;
            if (ListViewPC.SelectedItem is CompteClass)
                C = (ListViewPC.SelectedItem as CompteClass);
            planComptable.ChercherEnfant(C);
        }

        private void NaviguerVersLaFrame(Onglet onglet)
        {
            Onglet ongletEnCours = planComptable.OngletActif();
            if (ongletEnCours != onglet)
            {
                if (onglet == planComptable.MesOnglets.OngletFavoris)
                {
                    frame.Navigate(typeof(ListeDesFavoris));
                    planComptable.ActiverOnglet(planComptable.MesOnglets.OngletFavoris);                    
                }
                else
                    if (onglet == planComptable.MesOnglets.OngletAPropos)
                    {
                        frame.Navigate(typeof(APropos));                       
                        planComptable.ActiverOnglet(planComptable.MesOnglets.OngletAPropos);
                    }
                    else
                    {
                        frame.Navigate(typeof(ListeDesComptes), planComptable);                       
                        planComptable.ActiverOnglet(planComptable.MesOnglets.OngletCompte);
                    }
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Onglet ongletEnCours = planComptable.OngletActif();
            if (ongletEnCours != planComptable.MesOnglets.OngletCompte)
                NaviguerVersLaFrame(planComptable.MesOnglets.OngletCompte);
            else
                planComptable.Retour();
        }

        private void ButtonOngletCompte_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            NaviguerVersLaFrame(planComptable.MesOnglets.OngletCompte);
        }

        private void ButtonOngletFavoris_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            NaviguerVersLaFrame(planComptable.MesOnglets.OngletFavoris);
        }

        private void ButtonOngletAPropos_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            NaviguerVersLaFrame(planComptable.MesOnglets.OngletAPropos);
        }
    }
}
