using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsSGBD
{
    class StructDatabase
    {
        string nomDataBase;
        List<StructTable> tables;

        public StructDatabase()
        {
            nomDataBase = "vide";
            Tables = new List<StructTable>();
        }

        public StructTable RechercherTable(string nom)
        {
            foreach (StructTable item in Tables)
            {
                if (item.NomTable.Equals(nom)) return item;
            }
            return null;
        }

        public StructTable GetStructTableByName(string name)
        {
            foreach (StructTable item in tables)
            {
                if (item.NomTable.Equals(name)) return item;
            }
            return null;
        }

        public void AjouterTable()
        {
            try
            {
                Console.WriteLine("Entrez le nom de la table : ");
                string nom = Console.ReadLine();
                StructTable table = RechercherTable(nom);
                if (table != null) throw new Exception($"Table deja existante sous le nom '{nom}'");
                else
                {
                    table = new StructTable();
                    table.NomTable = nom;
                    Console.Write("Saisir le nombre de champs souhaité : ");
                    int nbr = int.Parse(Console.ReadLine());
                    for (int i = 0; i < nbr; i++)
                    {
                        table.AjouterChamp();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SupprimerTable()
        {
            Console.Write("Saisir le nom de la table à supprimer : ");
            string nom = Console.ReadLine();
            StructTable table = RechercherTable(nom);
            if (table == null) Console.WriteLine($"Aucune Table porte le nom '{nom}'");
            else Tables.Remove(table);
        }

        public void ModifierTable()
        {
            Console.Write("Saisir le nom de la table à modifier : ");
            string nom = Console.ReadLine();
            StructTable table = RechercherTable(nom);
            if (table == null) Console.WriteLine($"Aucune Table porte le nom '{nom}'");
            else
            {
                Console.WriteLine("1. Ajouter Champ\n2. Modifier Champ\n3. Renommer Champ\n4. Supprimer Champ\n\n0. Retour\n");
                string choix;
                do
                {
                    choix = Console.ReadLine();
                    switch (choix)
                    {
                        case "1":
                            table.AjouterChamp();
                            break;
                        case "2":
                            table.ModifierChamp();
                            break;
                        case "3":
                            table.ChangerChamp();
                            break;
                        case "4":
                            table.SupprimerChamp();
                            break;
                    }
                } while (!choix.Equals("0"));
            }
        }

        public void ShowTables()
        {
            Console.WriteLine("\t+++ " + NomDataBase + " +++\n");
            foreach (StructTable item in Tables)
            {
                Console.WriteLine(item);
            }
        }

        public string NomDataBase { get => nomDataBase; set => nomDataBase = value; }
        public List<StructTable> Tables { get => tables; set => tables = value; }
    }
}
