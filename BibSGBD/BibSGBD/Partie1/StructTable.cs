namespace BibSGBD.Partie1
{
    public class StructTable : IStructTable
    {
        string nomTable;
        List<Field> fieldList;

        public StructTable()
        {
            NomTable = "vide";
            FieldList = new List<Field>();
        }

        public bool ExistantPrimaryKey()
        {
            foreach (Field field in fieldList)
            {
                if (field.Contrainte.Equals(Constraint.PrimaryKey)) return true;
            }
            return false;
        }

        public Field RechercherChamp(string nom)
        {
            foreach (Field item in fieldList)
            {
                if (item.Nom.Equals(nom)) return item;
            }
            return null;
        }
        public void AjouterChamp()
        {
            try
            {
                Field field = new Field();
                //Console.WriteLine("Saisir les informations suivantes à propos du CHAMP :\n");
                Console.Write("\nNom : ");
                string nom = Console.ReadLine();
                if (nom == "") throw new Exception("Un champ ne peut pas porter un nom vide");
                else if (RechercherChamp(nom) != null) throw new Exception($"Un Chammp deja existant porte le nom '{nom}'");
                else field.Nom = nom;

                Console.Write("Type : ");
                string typefield = Console.ReadLine();
                string[] typeNoms = System.Enum.GetNames(typeof(TypeField));
                if (!typeNoms.Contains(typefield)) throw new Exception($"La valeur demandée '{typefield}' est introuvable.");//System.OverflowException
                field.Type = (TypeField)System.Enum.Parse(typeof(TypeField), typefield);

                Console.Write("Contrainte : ");
                string contraintefield = Console.ReadLine();
                string[] contrainteNoms = System.Enum.GetNames(typeof(Constraint));
                if (!contrainteNoms.Contains(contraintefield)) throw new Exception($"La valeur demandée '{contraintefield}' est introuvable.");//System.OverflowException
                else if (ExistantPrimaryKey() && contraintefield.Equals(System.Enum.GetName(typeof(Constraint), Constraint.PrimaryKey))) throw new Exception("Impossible d'avoir plus d'une Clé Primaire dans une table");
                field.Contrainte = (Constraint)System.Enum.Parse(typeof(Constraint), contraintefield);

                fieldList.Add(field);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AjouterChamp(string nom, string typefield, string contraintefield)
        {
            Field field = new Field { Nom = nom };
            field.Type = (TypeField)System.Enum.Parse(typeof(TypeField), typefield);
            field.Contrainte = (Constraint)System.Enum.Parse(typeof(Constraint), contraintefield);
            fieldList.Add(field);
        }

        public void ModifierChamp()
        {
            try
            {
                Console.Write("Saisir le nom du champ à modifier\nNom : ");
                string nom = Console.ReadLine();
                Field field = RechercherChamp(nom);
                if (field == null) Console.WriteLine($"Aucun Champ porte le nom '{nom}'");
                else
                {
                    Console.Write("Saisir le nouveau Type : ");
                    string typefield = Console.ReadLine();
                    string[] typeNoms = System.Enum.GetNames(typeof(TypeField));
                    if (!typeNoms.Contains(typefield)) throw new Exception($"La valeur demandée '{typefield}' est introuvable.");//System.OverflowException
                    field.Type = (TypeField)System.Enum.Parse(typeof(TypeField), typefield);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ModifierChamp(string nom, string typefield)
        {
            Field field = RechercherChamp(nom);
            if (field == null) Console.WriteLine($"Aucun Champ porte le nom '{nom}'");
            else
            {
                field.Type = (TypeField)System.Enum.Parse(typeof(TypeField), typefield);
            }
        }

        public void ChangerChamp()
        {
            try
            {
                Console.Write("Saisir le nom du champ à changer\nNom : ");
                string nom = Console.ReadLine();
                Field field = RechercherChamp(nom);
                if (field == null) Console.WriteLine($"Aucun Champ porte le nom '{nom}'");
                else
                {
                    Console.Write("Saisir le nouveau nom : ");
                    string nom2 = Console.ReadLine();
                    if (RechercherChamp(nom2) == null) field.Nom = nom2;
                    else throw new Exception($"Un Chammp deja existant porte le nom '{nom2}'");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ChangerChamp(string nom1, string nom2)
        {
            Field field = RechercherChamp(nom1);
            if (field != null)
            {
                if (RechercherChamp(nom2) == null)
                {
                    field.Nom = nom2;
                }
                else { throw new Exception($"Un champ porte deja le nom '{nom2}'"); }
            }
            else { throw new Exception($"Aucun champ porte le nom '{nom1}'"); }
        }

        public void SupprimerChamp()
        {
            Console.Write("Saisir le nom du champ à supprimer : ");
            string nom = Console.ReadLine();
            Field field = RechercherChamp(nom);
            if (field == null) Console.WriteLine($"Aucun Champ porte le nom '{nom}'");
            else Console.WriteLine($"Remove(f) : {fieldList.Remove(field)}");
        }

        public void SupprimerChamp(string nom)
        {
            Field field = RechercherChamp(nom);
            if (field == null) throw new Exception($"Aucun Champ porte le nom '{nom}'");
            else fieldList.Remove(field);
        }

        public void DescribeTable()
        {
            Console.WriteLine(ToString());
        }

        public string TypeVar(string nomField)
        {
            foreach (Field field in fieldList)
            {
                if (field.Nom.Equals(nomField)) return "" + field.Type;
            }
            return null;
        }

        public string ContrainteVar(string nom)
        {
            foreach (Field field in fieldList)
            {
                if (field.Nom.Equals(nom)) return "" + field.Contrainte;
            }
            return null;
        }

        public int IndexOf(string nomChamp)
        {
            int index = 0;
            foreach (Field item in fieldList)
            {
                if (item.Nom.Equals(nomChamp)) return index;
                index++;
            }
            return -1;
        }

        public override string ToString()
        {
            string s = $"Table [{nomTable}]\n";

            foreach (Field item in FieldList)
            {
                s += "\t" + item;
            }
            return s;
        }
        public string NomTable { get => nomTable; set => nomTable = value; }
        public List<Field> FieldList { get => fieldList; set => fieldList = value; }
    }
}


