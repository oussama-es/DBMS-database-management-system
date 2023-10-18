using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsSGBD
{
    class Analyseur
    {
        Database database;
        StructTable tableStructure;

        RichTextBox textBoxError, textBoxQuery;
        DataGridView dataGridview;
        TreeView treeView;

        private static int CIRCLE = 0, DATABASE = 1, PRIMARYKEY = 2, TABLE = 3;

        public Analyseur(Database database)
        {
            this.database = database;
        }

        public Analyseur(RichTextBox textBoxError, RichTextBox textBoxQuery, DataGridView dataGridview, TreeView treeView)
        {
            this.textBoxError = textBoxError;
            this.textBoxQuery = textBoxQuery;
            this.dataGridview = dataGridview;
            this.treeView = treeView;
        }

        public void DELETE(List<string> SplitsList)
        {
            if (SplitsList[1].ToUpper().Trim().Equals("FROM"))
            {
                string nomTable = SplitsList[2];
                Table table = database.RechercherTable(nomTable);
                if (table == null) throw new Exception($"Aucune table sous le nom '{nomTable}' trouvée");
                if (SplitsList.Count == 3)
                {
                    DELETE_FROM(table);
                }
                else if (SplitsList[3].ToUpper().Trim().Equals("WHERE"))
                {
                    DELETE_FROM_WHERE(SplitsList, table);
                }
                else { throw new Exception("'WHERE' attendue!"); }
            }
        }

        public void DELETE_FROM(Table table)
        {
            table.DeleteAllFromTable();
        }

        public void DELETE_FROM_WHERE(List<string> SplitsList, Table table)
        {
            string nomColonne = SplitsList[4];
            if (TableStructure.TypeVar(nomColonne) == null) throw new Exception($"Aucune colonne existante sous le nom '{nomColonne}'");

            string operateur = SplitsList[5];
            string valeur = SplitsList[6];
            TryParse(TableStructure.TypeVar(nomColonne), ref valeur);

            table.DeleteFromTableWhere(nomColonne, operateur, valeur);
        }

        public void SELECT(List<string> ColNames, List<string> SubSplitsList)
        {
            if (SubSplitsList[0].ToUpper().Trim().Equals("FROM"))
            {
                string nomTable = SubSplitsList[1];
                Table table = database.RechercherTable(nomTable);
                if (table == null) throw new Exception($"Aucune table sous le nom '{nomTable}' trouvée");
                TableStructure = table.StructTable;
                if (SubSplitsList.Count == 2)
                {
                    SELECT_FROM(ColNames, table);
                }
                else if (SubSplitsList[2].ToUpper().Trim().Equals("WHERE"))
                {
                    SELECT_FROM_WHERE(ColNames, SubSplitsList, table);
                }
                else { throw new Exception("'WHERE' attendue!"); }
            }
            else { throw new Exception("'FROM' attendue!"); }
        }

        public void SELECT_FROM(List<string> ColNames, Table table)
        {
            PrintTab(ColNames, table.SelectColsFromTable(ColNames, table.Rows));
        }

        public void SELECT_FROM_WHERE(List<string> ColNames, List<string> SubSplitsList, Table table)
        {
            string nomColonne = SubSplitsList[3];
            if (TableStructure.TypeVar(nomColonne) == null) throw new Exception($"Aucune colonne existante sous le nom '{nomColonne}'");
            
            string operateur = SubSplitsList[4];
            string valeur = SubSplitsList[5];
            TryParse(TableStructure.TypeVar(nomColonne), ref valeur);

            PrintTab(ColNames, table.SelectColsFromTableWhere(nomColonne, operateur, valeur, ColNames));
        }

        public void UPDATE(Table table, List<string> SubSplitList, string where, string nomColonne, string operateur, string valeur)
        {
            List<int> tabIndices = new List<int>();
            List<string> tabValeurs = new List<string>();
            string modifications = "";
            foreach (string item in SubSplitList)
            {
                modifications += item + " ";
            }
            modifications = modifications.Trim();
            string[] modifSplits = modifications.Split(',');
            List<string> colNames = new List<string>();
            List<string> colTypes = new List<string>();
            foreach (string item in modifSplits)
            {
                Print(0, item);
                if (item.Equals("")) throw new Exception("Requete non valide!");
                string[] tmp = item.Split(' ');
                if (tmp.Length != 3) throw new Exception("Requete non valide! (count 3)");
                colNames.Add(tmp[0]);
                if (!tmp[1].Equals("=")) throw new Exception("'=' attendu pour l'assignation");
                tabValeurs.Add(tmp[2]);
            }
            foreach (string item in colNames)
            {
                int index = TableStructure.IndexOf(item);
                if (index == -1) throw new Exception($"Aucune colonne sous le nom '{item}'");
                tabIndices.Add(index);
                colTypes.Add(TableStructure.TypeVar(item));
            }
            TryParse(colTypes, ref tabValeurs);
            if (where.ToUpper().Equals("WHERE"))
            {
                if (TableStructure.TypeVar(nomColonne) == null) throw new Exception($"Aucune colonne existante sous le nom '{nomColonne}'");
                TryParse(TableStructure.TypeVar(nomColonne), ref valeur);
                if (tabIndices.Count != tabValeurs.Count) throw new Exception("tabIndices.Count != tabValeurs.Count");
                Print(0, "UPDATE");
                table.Update(nomColonne, operateur, valeur, tabIndices, tabValeurs);
            }
            else throw new Exception("'WHERE' attendu");
        }

        public List<string> ExtractColNames(string s)
        {
            string[] colNames = s.Split(',');
            List<string> ColNamesList = new List<string>();

            for (int i = 0; i < colNames.Length; i++)
            {
                if (colNames[i].Equals("")) throw new Exception("Requete non valide!");// syntax err (starts with ',' or ends with ',' or ',,')
                if (TableStructure.TypeVar(colNames[i]) == null) throw new Exception($"Aucune colonne existante sous le nom '{colNames[i]}'");
                ColNamesList.Add(colNames[i]);
            }
            return ColNamesList;
        }

        public List<string> ExtractValues(List<string> ColNamesList, string s)
        {
            string[] colValues = s.Split(',');
            List<string> ColValuesList = new List<string>();

            for (int i = 0; i < colValues.Length; i++)
            {
                if (colValues[i].Equals("")) throw new Exception("Requete non valide!");// syntax err (starts with ',' or ends with ',' or ',,')
                ColValuesList.Add(colValues[i]);
            }

            if (ColNamesList.Count != ColValuesList.Count) throw new Exception("Le nombre de valeurs ne correspond pas au nombre de colonnes");

            List<string> ColTypesList = new List<string>();
            foreach (string item in ColNamesList)
            {
                string res = TableStructure.TypeVar(item);
                ColTypesList.Add(res);
            }
            TryParse(ColTypesList, ref ColValuesList);
            return ColValuesList;
        }

        public void ExecuteQuery(string query)
        {
            ClearGridView();
            try
            {
                //ClearGridViewColumns();
                // Test sur ';'
                int pos = query.IndexOf(';');
                if (pos.Equals(-1)) throw new Exception("';' attendu");
                else if (!pos.Equals(query.Length - 1)) throw new Exception("La requete doit finir avec ';'");
                else if (pos.Equals(query.Length - 1)) { query = query.Substring(0, query.Length - 1); }
                // Split
                string[] splits = query.Trim().Split(' ');
                List<string> SplitsList = new List<string>();

                for (int i = 0; i < splits.Length; i++)
                {
                    if (splits[i].Equals("")) throw new Exception("La requete contient un ' ' de plus"); //soit la requete commence par ' ' soit fin soit 2' ' suivis
                    SplitsList.Add(splits[i]);
                }

                if (database == null)
                {
                    if (SplitsList[0].ToUpper().Equals("CREATE"))
                    {
                        if (SplitsList.Count == 3)
                        {
                            if (SplitsList[1].ToUpper().Equals("DATABASE"))
                            {
                                database = new Database { NomDataBase = SplitsList[2] };
                                TreeNode treeNode = new TreeNode(SplitsList[2]);
                                treeNode.ImageIndex = treeNode.SelectedImageIndex = Analyseur.DATABASE;
                                AddNode(treeView.Nodes[0], treeNode);
                                textBoxError.ForeColor = Color.Green;
                                textBoxError.Text += "Requete valide" + '\n';
                            }
                            else { throw new Exception($"Creez une DATABASE d'abord"); }
                        }
                        else { throw new Exception($"Creez une DATABASE d'abord"); }
                    }
                    else { throw new Exception($"Creez une DATABASE d'abord"); }
                }
                else // database != null
                {
                    if ((SplitsList[0]).ToUpper().Equals("SELECT"))
                    {
                        // SubSplitsList
                        List<string> SubSplitsList = new List<string>();
                        for (int i = 2; i < SplitsList.Count; i++)
                        {
                            SubSplitsList.Add(SplitsList[i]);
                        }

                        if (SplitsList.Count >= 2 && SubSplitsList.Count >= 2)// out of range test
                        {
                            string nomTable = SubSplitsList[1];
                            Table table = database.RechercherTable(nomTable);
                            if (table == null) throw new Exception($"Aucune table sous le nom '{nomTable}' trouvée");
                            TableStructure = table.StructTable;
                            if ((SplitsList[1]).Equals("*"))
                            {
                                SELECT(table.GetColNames(),SubSplitsList);
                                Print(0, "Requete Validée");
                            }
                            else
                            {
                                List<string> ColNames = ExtractColNames(SplitsList[1]);
                                SELECT(ColNames, SubSplitsList);
                                Print(0, "Requete Validée");
                            }

                        }
                        else { throw new Exception("Requete non valide"); }
                    }
                    else if (SplitsList[0].ToUpper().Equals("DELETE"))
                    {
                        DELETE(SplitsList);
                        Print(0, "Requete Validée");
                    }
                    else if ((SplitsList[0]).ToUpper().Equals("UPDATE"))
                    {
                        if (SplitsList.Count >= 10)
                        {
                            string nomTable = SplitsList[1];
                            Table table = database.RechercherTable(nomTable);
                            if (table == null) throw new Exception($"Aucune table sous le nom '{nomTable}' trouvée");
                            TableStructure = table.StructTable;
                            if (SplitsList[2].ToUpper().Equals("SET"))
                            {
                                int count = SplitsList.Count;
                                string valeur = SplitsList[count - 1];
                                string operateur = SplitsList[count - 2];
                                string nomColonne = SplitsList[count - 3];
                                string where = SplitsList[count - 4];
                                List<string> SubSplitList = new List<string>();

                                for (int i = 3; i < SplitsList.Count - 4; i++)
                                {
                                    SubSplitList.Add(SplitsList[i]);
                                }
                                UPDATE(table, SubSplitList, where, nomColonne, operateur, valeur);
                            }
                            else { throw new Exception("'SET' attendu!"); }
                        }
                        else { throw new Exception("Requete non Validée"); }
                    }
                    else if (SplitsList[0].ToUpper().Equals("INSERT"))
                    {
                        if (SplitsList[1].ToUpper().Equals("INTO"))
                        {
                            string nomTable = SplitsList[2];
                            Table table = database.RechercherTable(nomTable);
                            if (table == null) throw new Exception($"Aucune table sous le nom '{nomTable}' trouvée");
                            TableStructure = table.StructTable;
                            // SubSplitsList
                            List<string> SubSplitsList = new List<string>();
                            for (int i = 3; i < SplitsList.Count; i++)
                            {
                                SubSplitsList.Add(SplitsList[i]);
                            }

                            if (!SubSplitsList[0][0].Equals('(')) throw new Exception("'(' attendu!");
                            else if (!SubSplitsList[0][SubSplitsList[0].Length - 1].Equals(')')) throw new Exception("')' attendu!");
                            SubSplitsList[0] = SubSplitsList[0].Substring(1, SubSplitsList[0].Length - 2);

                            List<string> ColNames = ExtractColNames(SubSplitsList[0]);

                            if (SubSplitsList[1].ToUpper().Equals("VALUES"))
                            {
                                if (!SubSplitsList[2][0].Equals('(')) throw new Exception("'(' attendu!");
                                else if (!SubSplitsList[2][SubSplitsList[2].Length - 1].Equals(')')) throw new Exception("')' attendu!");
                                SubSplitsList[2] = SubSplitsList[2].Substring(1, SubSplitsList[2].Length - 2);

                                List<string> ColValues = ExtractValues(ColNames, SubSplitsList[2]);
                                
                                table.InsertInto(ColNames, ColValues);
                                Print(0, "Requete Validée");
                            }
                            else { throw new Exception("'VALUES' attendu!"); }
                        }
                        else { throw new Exception("'INTO' attendu!"); }
                    }
                    else if (SplitsList[0].ToUpper().Equals("CREATE"))
                    {
                        if (SplitsList.Count == 3)// out of range test
                        {
                            if (SplitsList[1].ToUpper().Equals("TABLE"))
                            {
                                Table table = database.RechercherTable(SplitsList[2]);
                                if (table == null)
                                {
                                    StructTable structtable = new StructTable { NomTable = SplitsList[2] };
                                    table = new Table(structtable);
                                    database.Tables.Add(table);
                                    TreeNode treeNode = new TreeNode(SplitsList[2]);
                                    treeNode.Name = SplitsList[2];
                                    treeNode.ImageIndex = treeNode.SelectedImageIndex = Analyseur.TABLE;
                                    AddNode(treeView.Nodes[0].Nodes[0], treeNode);
                                    Print(0, "Requete Valide");
                                }
                                else { throw new Exception($"Une table deja existante porte le nom '{SplitsList[2]}'"); }
                            }
                            else { throw new Exception("'TABLE' attendu"); }
                        }
                        else { throw new Exception("Requete non valide"); }
                    }
                    else if (SplitsList[0].ToUpper().Equals("SHOW"))
                    {
                        if (SplitsList.Count == 2)
                        {
                            if (SplitsList[1].ToUpper().Equals("TABLES"))
                            {
                                PrintTab(3, database.ShowTables_Grid());
                            }
                        }
                        else { throw new Exception("Requete non valide"); }
                    }
                    else if (SplitsList[0].ToUpper().Equals("SEARCH"))
                    {
                        if (SplitsList.Count == 3)
                        {
                            if (SplitsList[1].ToUpper().Equals("TABLE"))
                            {
                                Table table = database.RechercherTable(SplitsList[2]);
                                if (table != null)
                                {
                                    List<string> gridCols = new List<string> { "Nom", "Type", "Contrainte" };
                                    PrintTab(gridCols, table.GetStructTable());
                                }
                                else { throw new Exception($"Aucune table sous le nom '{SplitsList[2]}'"); }
                            }
                        }
                        else { throw new Exception("Requete non valide"); }
                    }
                    else if (SplitsList[0].ToUpper().Equals("DROP"))
                    {
                        if (SplitsList.Count == 3)
                        {
                            if (SplitsList[1].ToUpper().Equals("TABLE"))
                            {
                                Table table = database.RechercherTable(SplitsList[2]);
                                if (table != null) //validee
                                {
                                    database.SupprimerTable(table);
                                    //remove node
                                    treeView.Nodes[0].Nodes[0].Nodes.RemoveByKey(table.StructTable.NomTable);
                                    Print(0, "Requete Valide");
                                }
                                else { throw new Exception($"Aucune table sous le nom '{SplitsList[2]}'"); }
                            }
                        }
                        else { throw new Exception("Requete non valide"); }
                    }
                    else if (SplitsList[0].ToUpper().Equals("ALTER"))
                    {
                        if (SplitsList.Count >= 6)
                        {
                            if (SplitsList[1].ToUpper().Equals("TABLE"))
                            {
                                string nomTable = SplitsList[2];
                                Table table = database.RechercherTable(nomTable);
                                if (table != null)
                                {
                                    string instruction = SplitsList[3];
                                    TableStructure = table.StructTable;
                                    if (instruction.ToUpper().Equals("ADD"))
                                    {
                                        if (SplitsList.Count == 7)
                                        {
                                            string nomCol = SplitsList[4];
                                            if (TableStructure.RechercherChamp(nomCol) == null)
                                            {
                                                string typeCol = SplitsList[5];
                                                string[] typeNoms = System.Enum.GetNames(typeof(TypeField));
                                                if (typeNoms.Contains(typeCol))
                                                {
                                                    string contrainteCol = SplitsList[6];
                                                    string[] contrainteNoms = System.Enum.GetNames(typeof(Constraint));
                                                    if (contrainteNoms.Contains(contrainteCol))
                                                    {
                                                        if (TableStructure.ExistantPrimaryKey() && SplitsList[6].Equals(System.Enum.GetName(typeof(Constraint), Constraint.PrimaryKey))) throw new Exception("Impossible d'avoir plus d'une Clé Primaire dans une table");
                                                        //table.
                                                        table.AddColRectif(nomCol, typeCol, contrainteCol);
                                                        TreeNode treeNode = new TreeNode(nomCol);
                                                        treeNode.Name = nomCol;
                                                        if (contrainteCol.Equals("PrimaryKey")) treeNode.ImageIndex = treeNode.SelectedImageIndex = Analyseur.PRIMARYKEY;
                                                        else treeNode.ImageIndex = treeNode.SelectedImageIndex = Analyseur.CIRCLE;
                                                        AddNode(treeView.Nodes[0].Nodes[0].Nodes.Find(nomTable, false)[0], treeNode);
                                                        Print(0, "Requete valide");
                                                    }
                                                    else throw new Exception($"La valeur demandée '{contrainteCol}' est introuvable.");
                                                }
                                                else throw new Exception($"La valeur demandée '{typeCol}' est introuvable.");
                                            }
                                            else { throw new Exception($"Champ deja existant sous le nom '{nomCol}'"); }
                                        }
                                        else { throw new Exception("Requete non valide"); }
                                    }
                                    else if (instruction.ToUpper().Equals("MODIFY"))
                                    {
                                        string nomCol = SplitsList[4];
                                        if (TableStructure.RechercherChamp(nomCol) != null)
                                        {
                                            string typeCol = SplitsList[5];
                                            string[] typeNoms = System.Enum.GetNames(typeof(TypeField));
                                            if (typeNoms.Contains(typeCol))// test type
                                            {
                                                table.ModifierChamp(nomCol, typeCol);
                                                Print(0, "Requete Validée");
                                            }
                                            else throw new Exception($"La valeur demandée '{typeCol}' est introuvable.");
                                        }
                                        else { throw new Exception($"Aucun champ existant sous le nom '{nomCol}'"); }
                                    }
                                    else if (instruction.ToUpper().Equals("CHANGE"))
                                    {
                                        string ancienNom = SplitsList[4];
                                        string nouvNom = SplitsList[5];
                                        if (TableStructure.RechercherChamp(ancienNom) != null)
                                        {
                                            if (TableStructure.RechercherChamp(nouvNom) == null)
                                            {
                                                treeView.Nodes[0].Nodes[0].Nodes[nomTable].Nodes[ancienNom].Text = nouvNom;
                                                treeView.Nodes[0].Nodes[0].Nodes[nomTable].Nodes[ancienNom].Name = nouvNom;
                                                TableStructure.ChangerChamp(ancienNom, nouvNom);
                                                Print(0, "Requete Validée");
                                            }
                                            else { throw new Exception($"la table '{nomTable}' contient deja un champ sous le nom '{nouvNom}'"); }
                                        }
                                        else { throw new Exception($"la table '{nomTable}' contient aucun champ sous le nom '{ancienNom}'"); }
                                    }
                                    else if (instruction.ToUpper().Equals("DROP") && SplitsList[4].ToUpper().Equals("COLUMN"))
                                    {
                                        string nomCol = SplitsList[5];
                                        if (TableStructure.RechercherChamp(nomCol) != null)
                                        {
                                            treeView.Nodes[0].Nodes[0].Nodes[nomTable].Nodes[nomCol].Remove();
                                            table.DropColRectif(nomCol);
                                        }
                                        else { throw new Exception($"Aucun champ existant sous le nom '{nomCol}'"); }
                                    }
                                    else { throw new Exception($"'{instruction}' est non reconnu comme mot cle"); }
                                }
                                else { throw new Exception($"Aucune table sous le nom '{nomTable}'"); }
                            }
                            else { throw new Exception("'TABLE' attendu!"); }
                        }
                        else { throw new Exception("Requete non valide"); }
                    }
                    else { throw new Exception($"'{SplitsList[0]}' est non reconnu comme mot cle"); }
                }
            }
            catch (Exception ex)
            {
                Print(1, ex.Message);
            }
        }

        public void TryParse(List<string> Types, ref List<string> Values)
        {
            char cote = (char)39;
            for (int i = 0; i < Values.Count; i++)
            {
                if (Types[i].Equals("Integer"))
                {
                    if (!int.TryParse(Values[i], out _))
                    {
                        throw new Exception($"Impossible de convertir '{Values[i]}' en 'Integer'");
                    }
                }
                else if (Types[i].Equals("Real"))
                {
                    if (!float.TryParse(Values[i], out _))
                    {
                        throw new Exception($"Impossible de convertir '{Values[i]}' en 'Real'");
                    }
                }
                else if (Types[i].Equals("Date"))
                {
                    if (!DateTime.TryParse(Values[i], out _))
                    {
                        throw new Exception($"Impossible de convertir '{Values[i]}' en 'Date'");
                    }
                }
                else if (Types[i].Equals("Text"))
                {
                    if (!Values[i][0].Equals(cote) || !Values[i][Values[i].Length - 1].Equals(cote)) throw new Exception(" ' manquante");
                    Values[i] = Values[i].Substring(1, Values[i].Length - 2);
                }
                else { throw new Exception($"Le type demande '{Types[i]}' non trouve"); }
            }
        }

        public void TryParse(string Type, ref string Value)
        {
            char cote = (char)39;
            if (Type.Equals("Integer"))
            {
                if (!int.TryParse(Value, out _))
                {
                    throw new Exception($"Impossible de convertir '{Value}' en 'Integer'");
                }
            }
            else if (Type.Equals("Real"))
            {
                if (!float.TryParse(Value, out _))
                {
                    throw new Exception($"Impossible de convertir '{Value}' en 'Real'");
                }
            }
            else if (Type.Equals("Date"))
            {
                if (!DateTime.TryParse(Value, out _))
                {
                    throw new Exception($"Impossible de convertir '{Value}' en 'Date'");
                }
            }
            else if (Type.Equals("Text"))
            {
                if (!Value[0].Equals(cote) || !Value[Value.Length - 1].Equals(cote)) throw new Exception(" ' manquante");
                Value = Value.Substring(1, Value.Length - 2);
            }
            else { throw new Exception($"Le type demande '{Type}' non trouve"); }
        }

        public void Print(int i, object s)
        {
            if (i == 0) textBoxError.ForeColor = Color.Green;
            else if (i == 1) textBoxError.ForeColor = Color.Red;
            else if (i == 2) textBoxError.ForeColor = Color.Black;
            textBoxError.Text += s + "\n";
        }

        public void PrintTab(List<string> colNames, List<Row> rows)
        {
            ClearGridView();
            Print(2, dataGridview.ColumnCount);
            foreach (string item in colNames)
            {
                DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
                Column.HeaderText = Column.Name = item;
                Column.ReadOnly = true;
                dataGridview.Columns.Add(Column);
            }

            foreach (Row item in rows)
            {
                Print(2, item);
                dataGridview.Rows.Add(item.Data);
            }
        }

        public void PrintTab(int colsCount, List<Row> rows)
        {
            ClearGridView();
            //Print(2, dataGridview.ColumnCount);
            for (int i = 0; i < colsCount; i++)
            {
                dataGridview.Columns.Add(new DataGridViewTextBoxColumn { ReadOnly = true} );
            }
            dataGridview.ColumnHeadersVisible = false;
            foreach (Row item in rows)
            {
                Print(2, item);
                dataGridview.Rows.Add(item.Data);
            }
        }

        public void ClearGridView()
        {
            dataGridview.Rows.Clear();
            dataGridview.Columns.Clear();
            dataGridview.ColumnHeadersVisible = true;
        }

        /// 
        public void AddNode(TreeNode parent, TreeNode child)
        {
            parent.Nodes.Add(child);
        }

        public void RemoveNode(TreeNode parent, TreeNode child)
        {
            parent.Nodes.Remove(child);
        }

        public void ModifyNode(TreeNode node, string nouvNom)
        {
            node.Text = nouvNom;
        }
        /// 

        internal StructTable TableStructure { get => tableStructure; set => tableStructure = value; }
        internal Database Database { get => database; set => database = value; }
    }
}
