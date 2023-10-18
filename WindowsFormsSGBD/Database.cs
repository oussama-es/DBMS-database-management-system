using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsSGBD
{
    class Database
    {
        private string nomDataBase;
        private List<Table> tables;

        public Database()
        {
            nomDataBase = "vide";
            tables = new List<Table>();
        }

        public Table RechercherTable(string nom)
        {
            foreach (Table item in Tables)
            {
                if (item.StructTable.NomTable.Equals(nom)) return item;
            }
            return null;
        }

        public void AjouterTable()
        {
            try
            {
                Console.WriteLine("Entrez le nom de la table : ");
                string nom = Console.ReadLine();
                Table table = RechercherTable(nom);
                if (table != null) throw new Exception($"Table deja existante sous le nom '{nom}'");
                else
                {
                    //creation dela structure de la table
                    StructTable structtable = new StructTable { NomTable = nom };
                    Console.Write("Saisir le nombre de champs souhaité : ");
                    int nbr = int.Parse(Console.ReadLine());
                    for (int i = 0; i < nbr; i++)
                    {
                        structtable.AjouterChamp();
                    }
                    //creation de la table
                    table = new Table(structtable);
                    Tables.Add(table);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SupprimerTable(string nom)
        {
            Table table = RechercherTable(nom);
            if (table == null) Console.WriteLine($"Aucune table trouvée sous le nom '{nom}'");
            else { Tables.Remove(table); }
        }

        public void SupprimerTable(Table table)
        {
            tables.Remove(table);
        }

        public void ModifierTable()
        {
            Console.Write("Saisir le nom de la table à modifier : ");
            string nom = Console.ReadLine();
            Table table = RechercherTable(nom);
            if (table == null) Console.WriteLine($"Aucune table trouvée sous le nom '{nom}'");
            else
            {
                string choix;
                do
                {
                    Console.WriteLine("1. Ajouter Champ\n2. Modifier Champ\n3. Renommer Champ\n4. Supprimer Champ\n\n0. Retour\n");
                    choix = Console.ReadLine();
                    switch (choix)
                    {
                        case "1":
                            table.StructTable.AjouterChamp();
                            break;
                        case "2":
                            table.StructTable.ModifierChamp();
                            break;
                        case "3":
                            table.StructTable.ChangerChamp();
                            break;
                        case "4":
                            table.StructTable.SupprimerChamp();
                            break;
                    }
                } while (!choix.Equals("0"));
            }
        }

        public string ShowTables()
        {
            string s = "";
            foreach (Table item in Tables)
            {
                s += item + " \n";
            }
            return s;
        }

        public List<Row> ShowTables_Grid()
        {
            List<Row> list = new List<Row>();
            foreach (Table item in Tables)
            {
                string[] data = new string[] { "", item.StructTable.NomTable, ""};
                Row row = new Row(data);
                list.Add(row);

                data = new string[] { "Nom", "Type", "Contrainte" };
                row = new Row(data);
                list.Add(row);
                list.AddRange(item.GetStructTable());

                list.Add(new Row(3));
            }
            return list;
        }

        /*
        public void ExecuteQuery(string query)
        {
            Analyseur a = new Analyseur(query);
        }
        */

        public string NomDataBase { get => nomDataBase; set => nomDataBase = value; }
        internal List<Table> Tables { get => tables; set => tables = value; }
    }
}
