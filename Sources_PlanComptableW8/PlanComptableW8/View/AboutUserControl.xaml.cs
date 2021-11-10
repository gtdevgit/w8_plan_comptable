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

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page http://go.microsoft.com/fwlink/?LinkId=234236

namespace PlanComptableW8.View
{
    public sealed partial class AboutUserControl : UserControl
    {
        public AboutUserControl()
        {
            this.InitializeComponent();
        }

        async void wwwBlog()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://pedaloproduction.blogspot.fr/"));
        }

        async void mailTo()
        {
            var mail = new Uri("mailto:?to=pedaloproduction@yahoo.fr&subject=Plan comptable");
            await Windows.System.Launcher.LaunchUriAsync(mail);
        }

        private void Grid_Tapped_Blog(object sender, TappedRoutedEventArgs e)
        {
            wwwBlog();
        }

        private void Grid_Tapped_MailTo(object sender, TappedRoutedEventArgs e)
        {
            mailTo();
        }
    }
}
