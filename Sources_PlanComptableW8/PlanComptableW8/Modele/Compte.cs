using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;


namespace PlanComptableW8.Modele
{
    [DataContract]
    public class CompteClass
    {
        private int id = -1;
        private string racine = "";
        private string intitule = "";        
        private int parent = -1;
        private bool systemeAbrege = false;
        private bool systemeBase = false;

        private List<int> enfants = null;

        [DataMember]
        public int ID {
            get { return id; }
            set { id = value; }
        }
        
        [DataMember]
        public string Racine
        {
            get { return racine; }
            set { racine = value; }
        }

        [DataMember]
        public string Intitule
        {
            get { return intitule; }
            set { intitule = value; }
        }

        [DataMember]
        public int Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        [DataMember]
        public bool SystemeAbrege 
        {
            get { return systemeAbrege; }
            set { systemeAbrege = value; }
        }

        [DataMember]
        public bool SystemeBase
        {
            get { return systemeBase; }
            set { systemeBase = value; }
        }

        [DataMember]
        public List<int> Enfants
        {
            get { return enfants; }
            set { enfants = value; }
        }

        public string Titre
        {
            get { return Racine + " : " + Intitule; }
        }

        public bool IntituleContiend(string chaineRecherche)
        {
            string s1 = intitule;
            string s2 = chaineRecherche;
            // ce systéme permet d'ignorer la case et les accents
            var c2 = CompareInfo.GetCompareInfo(CultureInfo.CurrentCulture.Name).IndexOf(s1, s2, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
            return (c2 >= 0);
        }

        public bool ADesEnfants 
        {
            get
            {
                return ((Enfants != null) && (Enfants.Count > 0));
            }
        }

        public CompteClass()
        { }

        // new CompteClass("0", "1", "COMPTES DE CAPITAUX", "-1", "1;38;41;44;66;78;95;122;128"),
        public CompteClass(string _id, string _racine, string _intitule, string _parent, string _enfants, bool _systemeAbrege, bool _systemeBase) 
        {
            id = int.Parse(_id);
            racine = _racine;
            intitule = _intitule;
            parent = int.Parse(_parent);
            if (_enfants.Trim() != "")
            {
                string[] s = _enfants.Split(';');
                enfants = new List<int> { };
                for (int i = 0; i < s.Length; i++)
                    enfants.Add(int.Parse(s[i]));
            }
            systemeAbrege = _systemeAbrege;
            systemeBase = _systemeBase;
        }

        public string ToCsv()
        {
            string lstEnfants = string.Empty;
            for (int i = 0; i < Enfants.Count; i++)
                lstEnfants = lstEnfants + string.Format("{0};", Enfants[i]);

            string s = string.Empty;
            s = string.Format("{0};{1};{2};{3};{4}", ID, Racine, Intitule, Parent, lstEnfants);
            return s;
        }
    }
}
