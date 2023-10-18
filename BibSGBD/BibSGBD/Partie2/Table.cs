using BibSGBD.Partie1;

namespace BibSGBD.Partie2
{
    public class Table : ITable
    {
        StructTable structTable;
        List<Row> rows;

        public Table()
        {
            structTable = new StructTable();
            rows = new List<Row>();
        }

        public Table(StructTable structTable)
        {
            this.structTable = structTable;
            rows = new List<Row>();
        }

        public void AjouterRow()
        {
            string[] data = new string[structTable.FieldList.Count];
            for (int i = 0; i < structTable.FieldList.Count; i++)
            {
                Console.Write(structTable.FieldList[i].Nom + " : ");
                string valeur = Console.ReadLine();
                if (valeur.Equals(""))
                {
                    if (!structTable.ContrainteVar(structTable.FieldList[i].Nom).Equals("Nulle"))
                    {
                        Console.WriteLine($"remplissage obligatoire du champ '{structTable.FieldList[i].Nom}'");
                        i--;
                    }
                    else { data[i] = valeur; }
                }
                else
                {
                    string type = structTable.TypeVar(structTable.FieldList[i].Nom);
                    if (!TryParse(type, valeur))
                    {
                        Console.WriteLine($"Impossible de convertir '{valeur}' en '{type}'");
                        i--;
                    }
                    else
                    {
                        string contrainte = structTable.ContrainteVar(structTable.FieldList[i].Nom);
                        if (contrainte.Equals("PrimaryKey") || contrainte.Equals("Unique"))
                        {
                            if (unicite(structTable.FieldList[i].Nom, valeur))
                            {
                                data[i] = valeur;
                            }
                            else { Console.WriteLine($"Les valeurs de ce champ doivent etre uniques"); i--; }
                        }
                        else { data[i] = valeur; }
                    }
                }
            }
            Row row = new Row(data);
            rows.Add(row);
            //Insert into ... (ID, FIRST_NAME, LAST_NAME) Values (C101, nom1, prenom1)
        }

        public bool unicite(string nomChamp, string valeur)
        {
            int index = structTable.IndexOf(nomChamp);

            foreach (Row row in rows)
            {
                if (row[index].Equals(valeur)) return false;
            }
            return true;
        }

        public void SupprimerRow()
        {

        }

        public void ModifierRow()
        {

        }

        public void RechercherRow()
        {
            Console.WriteLine(structTable.NomTable + rows.Count);
            foreach (Row row in Rows)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine("***********");
        }

        public bool TryParse(string type, string valeur)
        {
            if (type.Equals("Integer"))
            {
                return int.TryParse(valeur, out _);
            }
            else if (type.Equals("Real"))
            {
                return float.TryParse(valeur, out _);
            }
            else if (type.Equals("Date"))
            {
                return DateTime.TryParse(valeur, out _);
            }
            //else if (type.Equals("Text")) { return true; }
            return true;
        }

        public override string ToString()
        {
            return structTable.ToString();
        }

        //SQL Querries
        /*
        public string SelectAllFromTable()
        {
            string s = "";
            foreach (Row row in Rows)
            {
                s += row + "\n";
            }
            return s;
        }
        */
        public List<Row> SelectAllFromTable()
        {
            return rows;
        }
        /*
        public string SelectColsFromTable(params string[] colNames)
        {
            string s = "";
            int[] colPos = new int[colNames.Length];

            for (int i = 0; i < colPos.Length; i++)
            {
                colPos[i] = structTable.IndexOf(colNames[i]);
            }

            foreach (Row row in Rows)
            {
                foreach (var index in colPos)
                {
                    s += $"{row[index]} | ";
                }
                s += '\n';
            }
            return s;
        }
        */


        /*
        public void SelectColsFromTableWhere(params string[] colNames)
        {
            int[] colPos = new int[colNames.Length];

            for (int i = 0; i < colPos.Length; i++)
            {
                colPos[i] = structTable.IndexOf(colNames[i]);
            }

            foreach (Row row in Rows)
            {
                foreach (var index in colPos)
                {
                    Console.Write($"{row[index]} | ");
                }
                Console.WriteLine();
            }
        }*/

        public void init(ref string[] data)
        {
            for (int i = 0; i < data.Length; i++) data[i] = "null";
        }

        public int AddColRectif(string nom, string typefield, string contraintefield)
        {
            structTable.AjouterChamp(nom, typefield, contraintefield);
            List<Row> rows = new List<Row>();
            foreach (Row item in Rows)
            {
                rows.Add(item);
            }
            DeleteAllFromTable();
            for (int i = 0; i < rows.Count; i++)
            {
                string[] data = new string[structTable.FieldList.Count];
                init(ref data);
                for (int j = 0; j < rows[i].Count(); j++)
                {
                    data[j] = rows[i][j];
                }
                Row r = new Row(data);
                Rows.Add(r);
            }
            return rows.Count;
        }

        public void DropColRectif(string nom)
        {
            int index = structTable.IndexOf(nom);
            //deplacement
            for (int i = 0; i < Rows.Count; i++)
            {
                for (int j = index; j < Rows[i].Count() - 1; j++)
                {
                    Rows[i][j] = Rows[i][j + 1];
                }
            }
            structTable.SupprimerChamp(nom);
            List<Row> rows = new List<Row>();
            foreach (Row item in Rows)
            {
                rows.Add(item);
            }
            DeleteAllFromTable();
            for (int i = 0; i < rows.Count; i++)
            {
                string[] data = new string[structTable.FieldList.Count];
                init(ref data);
                for (int j = 0; j < data.Length; j++)
                {
                    data[j] = rows[i][j];
                }
                Row r = new Row(data);
                Rows.Add(r);
            }
        }

        public void ModifierChamp(string nomCol, string typeCol)
        {
            int index = StructTable.IndexOf(nomCol);
            foreach (Row item in rows)
            {
                if (!TryParse(typeCol, item[index])) throw new Exception($"Impossible de convertir tous les elements de '{nomCol}' en '{typeCol}'");
            }
            StructTable.ModifierChamp(nomCol, typeCol);
        }

        /*
        public void DeleteAllFromTableWhere(string[] colNames, string[] valeurs)
        {
            rows.Clear();
        }
        */

        /*
        public void UpdateSetWhere()
        {

        }
        */

        public void InsertInto(List<string> colNames, List<string> valeurs)
        {
            List<string> NNull_and_PK_Cols = new List<string>();
            foreach (Field field in structTable.FieldList)
            {
                if (field.Contrainte.Equals(Constraint.NotNulle) || field.Contrainte.Equals(Constraint.PrimaryKey)) NNull_and_PK_Cols.Add(field.Nom);
            }
            //tester si toutes les cols avec une contrainte NOTNULLE ou PK sont remplies
            foreach (string item in NNull_and_PK_Cols)
            {
                if (!colNames.Contains(item)) throw new Exception($"Remplissage obligatoire de la colonne '{item}'");
            }
            int[] colPos = new int[colNames.Count];
            for (int i = 0; i < colNames.Count; i++)
            {
                colPos[i] = structTable.IndexOf(colNames[i]);
            }

            //unicite pk et unique
            List<string> UNIQUE_and_PK_Cols = new List<string>();
            foreach (Field field in structTable.FieldList)
            {
                if (field.Contrainte.Equals(Constraint.Unique) || field.Contrainte.Equals(Constraint.PrimaryKey)) UNIQUE_and_PK_Cols.Add(field.Nom);
            }
            int[] colPos2 = new int[UNIQUE_and_PK_Cols.Count];
            for (int i = 0; i < UNIQUE_and_PK_Cols.Count; i++)
            {
                colPos2[i] = structTable.IndexOf(UNIQUE_and_PK_Cols[i]);
            }

            for (int i = 0; i < colPos2.Length; i++)
            {
                foreach (Row item in rows)
                {
                    if (item[i].Equals(valeurs[i])) throw new Exception("Unicite non respectee");
                }
            }

            string[] data = new string[structTable.FieldList.Count];
            init(ref data);
            for (int i = 0; i < colPos.Length; i++)
            {
                data[colPos[i]] = valeurs[i];
            }
            Row row = new Row(data);
            rows.Add(row);
        }

        public List<Row> SelectColsFromTableWhere(string nomCol, string operateur, object valeur, List<string> colNames)
        {
            int index = structTable.IndexOf(nomCol);
            List<Row> list = new List<Row>();
            for (int i = 0; i < rows.Count; i++)
            {
                if (operateur.Equals("="))
                {
                    if (rows.ElementAt(i)[index].Equals(valeur)) list.Add(rows.ElementAt(i));
                }
                else if (operateur.Equals("<>") || operateur.Equals("!="))
                {
                    if (!rows.ElementAt(i)[index].Equals(valeur)) list.Add(rows.ElementAt(i));
                }
                else { throw new Exception($"'{operateur}' est non reconnu comme operateur"); }
            }
            return SelectColsFromTable(colNames, list);
        }

        public void DeleteFromTableWhere(string nomCol, string operateur, object valeur)
        {
            int index = structTable.IndexOf(nomCol);
            for (int i = 0; i < rows.Count; i++)
            {
                if (operateur.Equals("="))
                {
                    if (rows.ElementAt(i)[index].Equals(valeur)) rows.RemoveAt(i);
                }
                else if (operateur.Equals("<>") || operateur.Equals("!="))
                {
                    if (!rows.ElementAt(i)[index].Equals(valeur)) rows.RemoveAt(i);
                }
                else { throw new Exception($"'{operateur}' est non reconnu comme operateur"); }
            }
        }

        public void Update(string nomCol, string operateur, string valeur, List<int> tabIndices, List<string> tabValeurs)
        {
            int index = structTable.IndexOf(nomCol);
            for (int i = 0; i < rows.Count; i++)
            {
                if (operateur.Equals("="))
                {
                    if (rows.ElementAt(i)[index].Equals(valeur))
                    {
                        for (int j = 0; j < tabIndices.Count; j++)
                        {
                            rows.ElementAt(i)[tabIndices[j]] = tabValeurs[j];
                        }
                    }
                }
                else if (operateur.Equals("<>") || operateur.Equals("!="))
                {
                    if (!rows.ElementAt(i)[index].Equals(valeur))
                    {
                        for (int j = 0; j < tabIndices.Count; j++)
                        {
                            rows.ElementAt(i)[tabIndices[j]] = tabValeurs[j];
                        }
                    }
                }
                else { throw new Exception($"'{operateur}' est non reconnu comme operateur"); }
            }
        }

        public List<Row> SelectColsFromTable(List<string> colNames, List<Row> rows)
        {
            List<Row> list = new List<Row>();
            int[] colsIndexes = new int[colNames.Count];
            for (int i = 0; i < colNames.Count; i++)
            {
                colsIndexes[i] = structTable.IndexOf(colNames[i]);
            }
            foreach (Row item in rows)
            {
                Row row = new Row(colNames.Count);
                for (int i = 0; i < colsIndexes.Length; i++)
                {
                    row[i] = item[colsIndexes[i]];
                }
                list.Add(row);
            }
            return list;
        }

        public List<string> GetColNames()
        {
            List<string> list = new List<string>();
            foreach (Field item in structTable.FieldList)
            {
                list.Add(item.Nom);
            }
            return list;
        }

        public List<Row> GetStructTable()
        {
            List<Row> tab = new List<Row>();
            foreach (Field item in structTable.FieldList)
            {
                string[] data = new string[3];
                data[0] = item.Nom;
                data[1] = item.Type + "";
                data[2] = item.Contrainte + "";
                Row row = new Row(data);
                tab.Add(row);
            }
            return tab;
        }

        public void DeleteAllFromTable()
        {
            rows.Clear();
        }

        public StructTable StructTable { get => structTable; set => structTable = value; }
        public List<Row> Rows { get => rows; set => rows = value; }
    }
}
