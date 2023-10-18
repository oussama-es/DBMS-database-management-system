namespace BibSGBD.Partie1
{
    public interface IStructDatabase
    {
        StructTable RechercherTable(string nom);
        StructTable GetStructTableByName(string name);
        void AjouterTable();
        void SupprimerTable();
        void ModifierTable();
        void ShowTables();
    }
}
