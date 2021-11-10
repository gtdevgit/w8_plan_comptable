using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace PlanComptableW8.View
{
    class CompteToColorConverter: IValueConverter
    {
        //1 Amber ~ Gold 	    #FFF0A30A
        //2 Violet	    #FFAA00FF
        //3 Orange	    #FFF09609
        //4 Vert	    #FF60A917
        //5 Indigo	    #FF6A00FF
        //6 Brown	    #FF825A2C
        //7 Pink	    #FFF472D0
        //8 Emerald	    #FF008A00

        private Color[] TAB_COULEUR = { Colors.DimGray, Colors.Gold, Colors.Violet, Colors.Orange, Colors.LimeGreen, Colors.Indigo, Colors.Brown, Colors.Pink, Colors.ForestGreen };

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Color c = new Color();
            if (value is String) 
            {
                string racine = (string)value;
                if (String.IsNullOrEmpty(racine))
                {
                    c = TAB_COULEUR[0];
                }
                else 
                {
                    if (racine.Length >= 1)
                    {
                        string premierCar = racine.Substring(0, 1);
                        int idx = 0;
                        int.TryParse(premierCar, out idx);
                        if ((idx >= 1) && (idx <= 8))
                            c = TAB_COULEUR[idx];
                        else
                            c = TAB_COULEUR[0];
                    }
                    else
                    {
                        c = TAB_COULEUR[0];
                    }
                }
            }
            else
            {
                c = TAB_COULEUR[0];
            }
            return new SolidColorBrush(c);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
