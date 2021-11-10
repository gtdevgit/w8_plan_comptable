using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using PlanComptableW8.Modele;

namespace PlanComptableW8.View
{
    class OngletStyleConverter : IValueConverter
    {
        public Style StyleOnglet(Onglet o)
        {
                if (o.Etat)
                    return (Style)App.Current.Resources["StyleButtonOngletActifKey"];
                else
                    return (Style)App.Current.Resources["StyleButtonOngletKey"];
            
        } 

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((value != null) && (value is Onglet))
            {
                Onglet o = value as Onglet;
                return StyleOnglet(o);
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
