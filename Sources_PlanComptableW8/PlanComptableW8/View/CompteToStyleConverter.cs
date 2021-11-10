using PlanComptableW8.Modele;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PlanComptableW8.View
{
    class CompteToStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (value is CompteClass)
            {
                string racine = ((CompteClass)value).Racine;
                if (String.IsNullOrEmpty(racine))
                {
                    return (DataTemplate)App.Current.Resources["Standard250x250ItemTemplateKey"];
                }
                else
                {
                    int i = 0;
                    int.TryParse(racine, out i);
                    var vm = App.Current.Resources["ViewModeleKey"] as PlanComptableW8.ViewModele.PlanComptableClass;
                    if ((i >= 1) && (i <= 8) && string.IsNullOrEmpty(vm.InfoRecherche))
                        return (DataTemplate)App.Current.Resources["Standard250x250ItemTemplateKey"];
                    else
                        return (DataTemplate)App.Current.Resources["Standard130ItemTemplateKey"];
                }
            }
            else
            {
                return (DataTemplate)App.Current.Resources["Standard250x250ItemTemplateKey"];
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
