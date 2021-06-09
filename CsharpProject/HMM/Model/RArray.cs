using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RArray
    {
        int nRow;
        int nCol;
        int dimension;
        String[] rowNames;
        String[] colNames;
        double[] values1;
        double[,] values2;

        public int NumCol { get => nCol; }
        public string[] RowNames { get => rowNames; set => rowNames = value; }
        public string[] ColNames { get => colNames; set => colNames = value; }

        #region SubScript
        public double this[int index]
        {
            get
            {
                if (2 == dimension)
                {
                    return -1;
                }
                return values1[index];
            }
        }

        public double this[string colName]
        {
            get
            {
                if (2 == dimension)
                {
                    return -1;
                }
                int i = -1;
                do
                {
                    i++;
                    if (i >= nCol)
                    {
                        return 0;
                    }
                } while (!colName.Equals(colNames[i]));
                return values1[i];
            }
        }

        public double this[int indexRow, int indexCol]
        {
            get
            {
                if (1 == dimension)
                {
                    return -1;
                }
                return values2[indexRow, indexCol];
            }
        }

        #endregion

        #region Constructor
        // Constructor for diemnsion 1
        public RArray(double[] values, String[] colNames)
        {
            dimension = 1;
            this.values1 = values;
            this.colNames = colNames;
            nCol = colNames.Length;
        }

        // Constructor for diemnsion 2
        public RArray(double[,] values, String[] rowNames, String[] colNames)
        {
            dimension = 2;
            this.values2 = values;
            this.rowNames = rowNames;
            this.colNames = colNames;
            nRow = rowNames.Length;
            nCol = colNames.Length;
        }

        #endregion

        /*
        #region GET Methods

        // For dimension 1
        public double get(int i)
        {
            if (2 == dimension)
            {
                return -1;
            }
            return values1[i];
        }

        public double get(string colName)
        {
            int i = -1;
            do
            {
                i++;
                if (i >= nCol)
                {
                    return 0;
                }
            } while (!colName.Equals(colNames[i]));
            return values1[i];
        }

        // For dimension 2
        public double get(int i, int j)
        {
            if (1 == dimension)
            {
                return -1;
            }
            return values2[i, j];
        }

        #endregion
        */
        #region ToString Methods

        public override String ToString()
        {
            switch (dimension)
            {
                case 1:
                    return dim1ToString();
                case 2:
                    return dim2ToString();
                default:
                    return "Nothing";
            }
        }

        String dim1ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < nCol; i++)
            {
                result.Append(colNames[i] + "\t|");
            }
            result.Append("\n");
            for (int i = 0; i < nCol; i++)
            {
                result.Append(String.Format("{0:0.0#####}\t|", values1[i]));
            }
            result.Append("\n");
            return result.ToString();
        }

        String dim2ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("\t|");
            for (int i = 0; i < nCol; i++)
            {
                result.Append(colNames[i] + "\t|");
            }
            result.Append("\n");
            for (int i = 0; i < nRow; i++)
            {
                result.Append(rowNames[i] + "\t|");
                for (int j = 0; j < nCol; j++)
                {
                    result.Append(String.Format("{0:0.0#####}\t|", values2[i, j]));
                }
                result.Append("\n");
            }
            return result.ToString();
        }

        public String rowNamesToString()
        {
            if (dimension == 1) return "";
            StringBuilder result = new StringBuilder();
            foreach (string item in rowNames)
            {
                result.Append(item + "| ");
            }
            return result.ToString();
        }

        public String colNamesToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (string item in colNames)
            {
                result.Append("\"" + item + "\" ");
            }
            return result.ToString();
        }
        #endregion
    }
}
