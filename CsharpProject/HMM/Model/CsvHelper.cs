using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CsvHelper
    {
        String path;

        public CsvHelper(String path)
        {
            this.path = path;
        }

        #region Read CSV

        public RDataFrame getDataByStreamReader(char splitChar)
        {
            // create a stream
            var reader = new StreamReader(File.OpenRead(this.path));
             // get the titles
            var titles = reader.ReadLine().Replace("\"", "").Split(splitChar);
            titles = trimArrayString(titles);
            RDataFrame result = new RDataFrame(titles);
            // get the content
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(splitChar);
                values = trimArrayString(values);
                result.addLine(values);
            }
            // close the stream
            reader.Close();

            return result;
        }

        private string[] trimArrayString(string[] str)
        {
            string[] result = str;
            int index = str.Length;
            while (0 != (index--))
            {
                result[index] = str[index].Trim();
            }
            return result;
        }

        public static RDataFrame getDataFrame(string path, char splitChar)
        {
            CsvHelper helper = new CsvHelper(path);
            return helper.getDataByStreamReader(splitChar);
        }

        public static RDataFrame getDataFrame(string path)
        {
            return getDataFrame(path, ',');
        }

        /*

        public List<List<string>> getDataByTextfieldParser(char splitChar)
        {
            List<string> listStates = new List<string>();
            List<string> listSymbols = new List<string>();
            using (TextFieldParser csvReader = new TextFieldParser(path))
            {
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { splitChar.ToString() });
                csvReader.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvReader.ReadLine();

                while (!csvReader.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvReader.ReadFields();
                    listStates.Add(fields[0]);
                    listSymbols.Add(fields[1]);
                }

                List<List<string>> result = new List<List<string>>();
                result.Add(listStates);
                result.Add(listSymbols);

                return result;
            }
        }
        */
        #endregion


        #region Write CSV

        public void writeDataByStreamWriter(RDataFrame data, char splitChar)
        {
            FileInfo fi = new FileInfo(path);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

            // About Title or NOT ?
            //data.moveToTitle(); with title (default)
            //data.moveToForstLine(); without title

            while (data.hasNextLine())
            {
                string line = prepareOneLine(data.getLine(), splitChar);
                sw.WriteLine(line);
            }

            sw.Close();
            fs.Close();
        }

        string prepareOneLine(string[] oneLine, char splitChar)
        {
            string line = "";
            int numColumn = oneLine.Length;
            for (int i = 0; i < numColumn; i++)
            {
                // double the double quotation marks | 给英文的双引号加倍
                string temp = oneLine[i].Replace("\"", "\"\"");
                if (temp.Contains(',') || temp.Contains('"')
                    || temp.Contains('\r') || temp.Contains('\n'))
                {// those who contains comma, quotation marks or line break,
                 // needs to be surrounded in double quotation marks
                 // | 含逗号、引号、换行符的，需要放到引号中
                    temp = string.Format("\"{0}\"", temp);
                }
                line += temp;
                if (i < numColumn - 1)
                {
                    line += splitChar.ToString();
                }
            }
            return line;
        }


        public void writeDataByWriteAllText(RDataFrame data, char splitChar)
        {
            StringBuilder strBuilder = new StringBuilder();
            while (data.hasNextLine())
            {
                strBuilder.AppendLine(string.Join(splitChar.ToString(), data.getLine()));
            }
            // Create and write the csv file
            File.WriteAllText(path, strBuilder.ToString());

            // To append more lines to the csv file
            //File.WriteAllText(path, strBuilder.ToString());
        }

        #endregion


    }
}
