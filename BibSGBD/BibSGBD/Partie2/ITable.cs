namespace BibSGBD.Partie2
{
    public interface ITable
    {
        void AjouterRow();
        void SupprimerRow();
        void ModifierRow();
        void RechercherRow();
        bool TryParse(string type, string valeur);
        List<Row> SelectAllFromTable();
        int AddColRectif(string nom, string typefield, string contraintefield);
        void DropColRectif(string nom);
        void ModifierChamp(string nomCol, string typeCol);
        void InsertInto(List<string> colNames, List<string> valeurs);
        List<Row> SelectColsFromTableWhere(string nomCol, string operateur, object valeur, List<string> colNames);
        void DeleteFromTableWhere(string nomCol, string operateur, object valeur);
        void Update(string nomCol, string operateur, string valeur, List<int> tabIndices, List<string> tabValeurs);
        List<Row> SelectColsFromTable(List<string> colNames, List<Row> rows);
        List<string> GetColNames();
        List<Row> GetStructTable();
        void DeleteAllFromTable();
    }

}
