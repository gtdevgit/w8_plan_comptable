using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Callisto.Controls;
using Windows.UI.Xaml.Media;

// Pour en savoir plus sur le modèle d'élément Page Éléments, consultez la page http://go.microsoft.com/fwlink/?LinkId=234233

namespace PlanComptableW8.View
{
    /// <summary>
    /// Page affichant une collection d'aperçus d'éléments. Dans le modèle Application fractionnée, cette page
    /// est utilisée pour afficher et sélectionner l'un des groupes disponibles.
    /// </summary>
    public sealed partial class PageRacine : PlanComptableW8.Common.LayoutAwarePage
    {
        public PageRacine()
        {
            this.InitializeComponent();
            SettingsPane.GetForCurrentView().CommandsRequested += onCommandsRequested;
            KeyDown += PageRacine_KeyDown;
        }

        void PageRacine_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            // ESCAPE = goBack            
            if (e.Key == Windows.System.VirtualKey.Escape)
            {
                var vm = App.Current.Resources["ViewModeleKey"] as PlanComptableW8.ViewModele.PlanComptableClass;
                if (vm.RetourPossible) 
                    vm.Retour();
            }
        }

        void onCommandsRequested(SettingsPane settingsPane, SettingsPaneCommandsRequestedEventArgs eventArgs)
        {
            // UICommandInvokedHandler handler = new UICommandInvokedHandler(onSettingsCommand);
            // SettingsCommand about = new SettingsCommand("about", "A propos", handler);
            // eventArgs.Request.ApplicationCommands.Add(about);
            
            var about = new SettingsCommand("about", "A propos", (handler) =>
                {
                    Color _background = Color.FromArgb(255, 0, 77, 96);
                    var settings = new SettingsFlyout();
                    settings.Content = new AboutUserControl();
                    settings.HeaderBrush = new SolidColorBrush(_background);
                    settings.Background = new SolidColorBrush(_background);
                    settings.HeaderText = "A propos";
                    settings.IsOpen = true;
                });
            eventArgs.Request.ApplicationCommands.Add(about); 
        }

        // This is the container that will hold our custom content.
        private Popup settingsPopup;

        void onSettingsCommand(IUICommand command)
        {
            //rootPage.NotifyUser("Defaults command invoked", NotifyType.StatusMessage);

            // Used to determine the correct height to ensure our custom UI fills the screen.
            Rect windowBounds = Window.Current.Bounds;
            // Desired width for the settings UI. UI guidelines specify this should be 346 or 646 depending on your needs.
            double settingsWidth = 646;

            // Create a Popup window which will contain our flyout.
            settingsPopup = new Popup();
            
            //settingsPopup.Closed += OnPopupClosed;
            //Window.Current.Activated += OnWindowActivated;
            
            settingsPopup.IsLightDismissEnabled = true;
            settingsPopup.Width = settingsWidth;
            settingsPopup.Height = windowBounds.Height;

            // Add the proper animation for the panel.
            settingsPopup.ChildTransitions = new TransitionCollection();
            settingsPopup.ChildTransitions.Add(new PaneThemeTransition()
            {
                Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right) ?
                       EdgeTransitionLocation.Right :
                       EdgeTransitionLocation.Left
            });

            // Create a SettingsFlyout the same dimenssions as the Popup.
            APropos mypane = new APropos();
            mypane.Width = settingsWidth;
            mypane.Height = windowBounds.Height;

            // Place the SettingsFlyout inside our Popup window.
            settingsPopup.Child = mypane;

            // Let's define the location of our Popup.
            settingsPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - settingsWidth) : 0);
            settingsPopup.SetValue(Canvas.TopProperty, 0);            
            settingsPopup.IsOpen = true;
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
            // TODO: assignez une collection d'éléments pouvant être liée à this.DefaultViewModel["Items"]
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //base.OnNavigatedTo(e);
            if (e != null)
            {
                string queryText = e.Parameter.ToString();
                var vm = App.Current.Resources["ViewModeleKey"] as PlanComptableW8.ViewModele.PlanComptableClass;
                vm.Chercher(queryText);
            }
        }

        private void itemGridView_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            // Compte selectionné
            PlanComptableW8.Modele.CompteClass compte = (PlanComptableW8.Modele.CompteClass)e.ClickedItem;
            var vm = App.Current.Resources["ViewModeleKey"] as PlanComptableW8.ViewModele.PlanComptableClass;
            vm.ChercherEnfant(compte);
        }
        /// <summary>
        /// <summary>
        /// Invoqué lorsque l'utilisateur clique sur le bouton Précédent.
        /// </summary>
        /// <param name="sender">Instance du bouton Précédent.</param>
        /// <param name="e">Données d'événement décrivant la façon dont l'utilisateur a cliqué sur le bouton Précédent.</param>
        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            var vm = App.Current.Resources["ViewModeleKey"] as PlanComptableW8.ViewModele.PlanComptableClass;
            vm.Retour();
        }


    }
}
