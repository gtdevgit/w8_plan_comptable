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
using Windows.ApplicationModel.DataTransfer;
using PlanComptableW8.Modele;

// Pour en savoir plus sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace PlanComptableW8
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ListeDesComptes : Page
    {
        private PlanComptableClass planComptable = null;

        public ListeDesComptes()
        {
            this.InitializeComponent();
            //this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Invoqué lorsque cette page est sur le point d'être affichée dans un frame.
        /// </summary>
        /// <param name="e">Données d'événement décrivant la manière dont l'utilisateur a accédé à cette page. La propriété Parameter
        /// est généralement utilisée pour configurer la page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                DataTransferManager.GetForCurrentView().DataRequested += Compte_DataRequested;
            }
            catch
            { }
            if (e.Parameter != null)
                planComptable = (PlanComptableClass)e.Parameter;
        }

        // Pour le partage
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested -= Compte_DataRequested;

        }

        void Compte_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataPackage data = args.Request.Data;
            data.Properties.Title = String.Format("Compte : {0}", planComptable.CompteEnCours.Racine);
            data.Properties.Description = String.Format("Intitulé : {0}", planComptable.CompteEnCours.Intitule);

            string text = "Plan comptable";
            data.SetText(text);
        }

        private void TextBlockFavoris_Click_1(object sender, RoutedEventArgs e)
        {
            planComptable.AjouterFavoris();
        }

        private void GridViewPC_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            CompteClass C = null;
            if (GridViewPC.SelectedItem is CompteClass)
                C = (GridViewPC.SelectedItem as CompteClass);
            planComptable.ChercherEnfant(C);
        }
    }
}
