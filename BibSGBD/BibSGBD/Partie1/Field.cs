namespace BibSGBD.Partie1
{

    public class Field
    {
        string nom;
        TypeField type;
        Constraint contrainte;

        public Field()
        {
            Nom = "vide";
            Type = TypeField.Text;
            contrainte = Constraint.NotNulle;
        }

        public override string ToString()
        {
            return $"{Nom} ({Type}) ({Contrainte})\n";
        }
        public string Nom { get => nom; set => nom = value; }
        public TypeField Type { get => type; set => type = value; }
        internal Constraint Contrainte { get => contrainte; set => contrainte = value; }

    }
}
