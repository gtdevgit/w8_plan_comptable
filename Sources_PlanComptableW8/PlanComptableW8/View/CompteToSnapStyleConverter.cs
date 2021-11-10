using PlanComptableW8.Modele;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PlanComptableW8.View
{
    class CompteToSnapStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (value is CompteClass)
            {
                string racine = ((CompteClass)value).Racine;
                if (String.IsNullOrEmpty(racine))
                {
                    return (DataTemplate)App.Current.Resources["Standard80ItemTemplateClasseKey"];
                }
                else
                {
                    int i = 0;
                    int.TryParse(racine, out i);
                    if ((i >= 1) && (i <= 8))
                        return (DataTemplate)App.Current.Resources["Standard80ItemTemplateClasseKey"];
                    else
                        return (DataTemplate)App.Current.Resources["Standard80ItemTemplateRacineKey"];
                }
            }
            else
            {
                return (DataTemplate)App.Current.Resources["Standard80ItemTemplateClasseKey"];
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
