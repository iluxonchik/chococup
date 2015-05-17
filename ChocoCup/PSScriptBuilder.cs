using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChocoCup
{
    class PSScriptBuilder<T>
    {
        private ICollection<T> packages;
        private string tempFile = null;
        public string TempFile
        {
            get { return tempFile; }
        }
        public PSScriptBuilder(ICollection<T> packages)
        {
            this.packages = packages;
        }

        private void WritePSArrayToSW(ICollection<T> col, StreamWriter sw)
        {
            const string PS_ARRAY_BEGIN = "$a = @(";
            const string PS_ARRAY_END = ");";
            sw.Write(PS_ARRAY_BEGIN);
            int numPack = packages.Count;

            int i = 0;
            for (i = 0; i < packages.Count - 1; i++)
            {
                sw.Write("\"" + packages.ElementAt(i) + "\",");
            }

            if (numPack > 0)
            {
                sw.Write("\"" + packages.ElementAt(i) + "\"" + PS_ARRAY_END);
            }
            else
            {
                sw.WriteLine(PS_ARRAY_END);
            }
        }

        private void WritePSForToSW(ICollection<T> col, StreamWriter sw)
        {
            /* Writes a PS for loop to an array */
            const string CHOCO_COMMAND = "choco install ";
            const string PS_IEX = "iex ";
            int numPack = col.Count;
            string FOR_BEGIN = "for($i=0; $i -le " + numPack + "; $i++) {";
            sw.WriteLine(FOR_BEGIN);
            for (int i = 0; i < numPack; i++)
            {
                sw.WriteLine(PS_IEX + CHOCO_COMMAND + col.ElementAt(i));
            }
            sw.WriteLine("}");
        }

        public string BuildScript() 
        {
            // TODO: Delete temp file
            tempFile = Path.GetTempFileName();
            StreamWriter sw = new StreamWriter(tempFile);
            WritePSArrayToSW(packages, sw);
            WritePSForToSW(packages, sw);
            sw.Close();

            return tempFile;
        }
     }
}
