using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PlanComptableW8.Modele
{
    public class Onglet : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
      
        public bool Etat
        {
            get 
            {
                return etat;
            }

            set 
            {
                if (value != etat)
                {
                    etat = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("StyleOnglet"));
                }
            }
        }
        private bool etat = false;

        public Style StyleOnglet 
        {
            get 
            { 
                if (etat)
                    return (Style)App.Current.Resources["StyleButtonOngletActifKey"];
                else
                    return (Style)App.Current.Resources["StyleButtonOngletKey"];
            }
        }     
    }
}
