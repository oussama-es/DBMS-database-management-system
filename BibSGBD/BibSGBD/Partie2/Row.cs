namespace BibSGBD.Partie2
{
    public class Row : List<string>
    {
        string[] data;

        public Row()
        {
            data = new string[0];
        }
        public Row(string[] data)
        {
            this.data = new string[data.Length];
            this.data = data;
        }
        public Row(Row row)
        {
            this.data = new string[row.Count()];
            this.data = row.data;
        }

        public int Count()
        {
            return data.Length;
        }

        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= data.Length) throw new Exception("Index out of range");
                return data[index];
            }
            set
            {
                if (index < 0 || index >= data.Length) throw new Exception("Index out of range");
                data[index] = value;
            }
        }

        public Row(int nbr)
        {
            data = new string[nbr];
        }

        public override string ToString()
        {
            string s = "";
            foreach (var item in data)
            {
                s += item + " | ";
            }
            return s;
        }

        public string[] Data { get => data; set => data = value; }
    }
}

