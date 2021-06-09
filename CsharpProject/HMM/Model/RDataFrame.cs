using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RDataFrame
    {
        List<string>[] data;
        int nCol;
        int size;
        string[] colNames;
        int pointer;

        public int NumCol { get => nCol; }
        public int Size { get => size; }
        public string[] Title { get => colNames; }

        #region Constructor
        public RDataFrame(int numColumns)
        {
            nCol = numColumns;
            size = 0;
            pointer = 0;
            data = new List<string>[nCol];
            colNames = new string[nCol];
            for (int i = 0; i < nCol; i++)
            {
                data[i] = new List<string>();
                colNames[i] = i.ToString();
            }
        }

        public RDataFrame(params string[] columnNames)
        {
            size = 0;
            if (checkNames(columnNames))
            {
                colNames = columnNames;
                nCol = columnNames.Length;
                data = new List<string>[nCol];
                for (int i = 0; i < nCol; i++)
                {
                    data[i] = new List<string>();
                }
                pointer = -1;
            }
            else
            {
                nCol = 0;
                colNames = new string[0];
                Console.WriteLine("Fail to Create the Data Frame");
            }
        }

        bool checkNames(string[] names)
        {
            int length = names.Length;
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (names[i].Equals(names[j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region POINTOR
        public void moveToForstLine()
        {
            pointer = 0;
        }

        public void moveToTitle()
        {
            pointer = -1;
        }

        public void moveTo(int index)
        {
            if (pointer < -1 || pointer >= size)
            {
                Console.WriteLine("Fail to move the pointer");
            }
            else
            {
                pointer = index;
            }
        }

        public bool hasNextLine()
        {
            return pointer < size;
        }

        #endregion

        #region GETTER methods

        public string[] getColumn(string colName)
        {
            int i = -1;
            do
            {
                i++;
                if (i >= nCol)
                {
                    return new string[0];
                }
            } while (!colName.Equals(colNames[i]));
            return data[i].ToArray();
        }

        public string[] getColumn(int colIndex)
        {
            return data[colIndex].ToArray();
        }


        public string[] getLine()
        {
            if (-1 == pointer)
            {
                pointer++;
                return colNames;
            }
            else
            {
                return getLine(pointer);
            }
        }

        public string[] getLine(int lineIndex)
        {
            string[] result = new string[nCol];
            for (int i = 0; i < nCol; i++)
            {
                result[i] = data[i][lineIndex];
            }
            pointer = lineIndex + 1;
            return result;
        }

        public double[,] toMatrix()
        {
            double[,] matrix = new double[size, nCol];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    string temp = (data[j])[i];
                    if (!double.TryParse(temp, out matrix[i, j]))
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
            return matrix;
        }

        public double[] toDoubleVector()
        {
            int length;
            double[] vector;
            string[] temp;
            if (1 == size)
            {// one line
                length = nCol;
                moveToForstLine();
                temp = getLine();
            }
            else
            {
                length = size;
                temp = getColumn(0);
            }
            vector = new double[length];
            for (int i = 0; i < length; i++)
            {
                if (!double.TryParse(temp[i], out vector[i]))
                {
                    vector[i] = 0;
                }
            }
            return vector;
        }

        #endregion

        public void addLine(params string[] args)
        {
            if (args.Length != nCol)
            {
                Console.WriteLine("Fail to add the line");
            }
            else
            {
                for (int i = 0; i < nCol; i++)
                {
                    data[i].Add(args[i]);
                }
                size++;
            }
        }

        #region ToString
        public string ToStringByLine()
        {
            return ToStringByLine(size);
        }

        public string ToStringByLine(int first)
        {
            string result = "";
            for (int i = 0; i < nCol; i++)
            {
                result += colNames[i] + ":\n";
                int loopLength = Math.Min(data[i].Count, first);
                for (int j = 0; j < loopLength; j++)
                {
                    result += data[i][j] + " ";
                }
                result += "\n";
            }
            return result;
        }

        public string ToStringByColumn()
        {
            return ToStringByColumn(size);
        }

        public string ToStringByColumn(int first)
        {
            string result = "";
            int loopLength = Math.Min(size, first);
            for (int i = 0; i < nCol; i++)
            {
                result += colNames[i] + "\t";
            }
            result += "\n";
            for (int i = 0; i < loopLength; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    result += data[j][i] + "\t";
                }
                result += "\n";
            }
            return result;
        }

        public override string ToString()
        {
            return ToStringByColumn();
        }
        #endregion
    }
}
