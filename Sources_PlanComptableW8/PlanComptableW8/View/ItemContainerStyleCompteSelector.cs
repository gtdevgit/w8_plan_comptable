using PlanComptableW8.Modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PlanComptableW8.View
{
    class ItemContainerStyleCompteSelector : StyleSelector
    {
        public Style ActifTemplate { get; set; }
        public Style InactifTemplate { get; set; }

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {            
            if (item is CompteClass)
            {
                Boolean aDesEnfants = ((CompteClass)item).ADesEnfants;
                if (aDesEnfants)
                {
                    return ActifTemplate;
                }
                else
                {
                    return InactifTemplate;
                }
            }
            else
            {
                return base.SelectStyleCore(item, container);
            }
        }
    }
}
