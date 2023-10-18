namespace BibSGBD.Partie1
{
    public interface IStructTable
    {
        bool ExistantPrimaryKey();
        Field RechercherChamp(string nom);
        void AjouterChamp();
        void AjouterChamp(string nom, string typefield, string contraintefield);
        void ModifierChamp();
        void ModifierChamp(string nom, string typefield);
        void ChangerChamp();
        void ChangerChamp(string nom1, string nom2);
        void SupprimerChamp();
        void SupprimerChamp(string nom);
        void DescribeTable();
        string TypeVar(string nomField);
        string ContrainteVar(string nom);
        int IndexOf(string nomChamp);
    }
}
