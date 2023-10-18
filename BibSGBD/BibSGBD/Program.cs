using BibSGBD.Partie2;

namespace BibSGBD
{
    public class Program
    {
        public static void Menu()
        {
            Console.WriteLine("1/ Ajouter Table\n2/ Rechercher Table\n3/ Supprimer Table\n4/ Modifier Table\n5/ Show Tables\n6/ ExecuteQuery\n\n");
        }

        static void Main(string[] args)
        {
         /*   Console.Write("Entrer le nom de la base de donnees : ");
            string nombd = Console.ReadLine();
            Database database = new Database { NomDataBase = nombd };
            Console.Clear();

            string choix;

            string nom;
            do
            {
                Menu();
                choix = Console.ReadLine();
                Console.Clear();
                switch (choix)
                {
                    case "1": //Ajouter Table
                        database.AjouterTable();
                        Console.ReadKey();
                        break;
                    case "2": //Rechercher Table
                        Console.Write("Le nom : ");
                        nom = Console.ReadLine();
                        Table t = database.RechercherTable(nom);
                        if (t == null) Console.WriteLine("ERROR");
                        else { Console.WriteLine(t); }
                        Console.ReadKey();
                        break;
                    case "3": //Supprimer Table
                        Console.Write("Saisir le nom de la table à supprimer : ");
                        nom = Console.ReadLine();
                        database.SupprimerTable(nom);
                        Console.ReadKey();
                        break;
                    case "4": //Modifier Table
                        database.ModifierTable();
                        Console.ReadKey();
                        break;
                    case "5": //Show Tables
                        Console.WriteLine(database.ShowTables());
                        Console.ReadKey();
                        break;
                    case "6": //ExecuteQuery
                        string query = Console.ReadLine();
                        database.ExecuteQuery(query);
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            } while (!choix.Equals("exit"));*/
        }
    }
}
