using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChocoCup
{
    class PSScriptBuilder<T> : IDisposable
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
                sw.WriteLine("\"" + packages.ElementAt(i) + "\"" + PS_ARRAY_END);
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
                sw.WriteLine(PS_IEX +  "\"" + CHOCO_COMMAND + col.ElementAt(i) + "\"");
            }
            sw.WriteLine("}");
        }

        public void BuildScript(StreamWriter sw = null) 
        {
            if (sw == null)
            {
                // No StreamWriter was passed, create a new temp file
                tempFile = Path.GetTempFileName();
                sw = new StreamWriter(tempFile);
            }

            WritePSArrayToSW(packages, sw);
            WritePSForToSW(packages, sw);
            sw.Close();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed resourses
                }

                // Dispose unmanaged resources

                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }

                disposedValue = true;
            }
        }

        ~PSScriptBuilder() {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            // Flag that the Finalizer doesn't have to be run, so that GC can
            // reclaim the memory right away
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
