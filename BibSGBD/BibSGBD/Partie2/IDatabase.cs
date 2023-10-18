namespace BibSGBD.Partie2
{
    public interface IDatabase
    {
        Table RechercherTable(string nom);

        void AjouterTable();

        void SupprimerTable(string nom);

        void ModifierTable();

        void DELETE(List<string> SplitsList);

        void SELECT(List<string> ColNames, List<string> SubSplitsList);

        List<string> getColsNames(string s, Table table);

        List<string> ExtractValues(List<string> ColNamesList, string s, Table table);

        void ExecuteQuery(string query);

        void TryParse(List<string> Types, ref List<string> Values);

        void TryParse(string Type, ref string Value);

        void Lister(List<string> colNames, List<Row> rows);

        string ShowTables();
    }
}
