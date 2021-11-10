using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlanComptableW8.ViewModele
{   
    public class CommandClassRetour : ICommand
    {

        private PlanComptableClass myGlobal;

        public CommandClassRetour(PlanComptableClass _myGlobal)
        {
            this.myGlobal = _myGlobal;
        }


        public bool CanExecute(Object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(Object parameter)
        {
            this.myGlobal.Retour();
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(null, null);
            }
        }
    }
}
