using PlanComptableW8.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PlanComptableW8.View
{
    class CompteToGoBackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is CompteClass)
            {
                CompteClass compte = (CompteClass)value;
                return (compte.ID >= 0);
            }
            else 
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
