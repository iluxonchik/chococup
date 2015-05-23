using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class ChocoCup
    {
        internal delegate void ScriptAction(string fileName);

        public IVisitor Visitor { get; set; }

        IVisitor visitor = null;
        readonly Type DEFAULT_VISITOR = typeof(NameOnlyPackageParserVisitor);
        private string outputFilePath = null;
        private ChocoOutputFetcher chocoOutput = null;
        private PSScriptBuilder<string> psScriptBuilder = null;
        ScriptAction scriptAction;

        public ChocoCup(string args = null)
        {
            // For testing purposes
            scriptAction += PrintScriptToConsole;
        }

        public List<string> GetPacakgeNames(IVisitor visitor, string chocoPath = null, string args = null)
        {
            chocoOutput = new ChocoOutputFetcher(chocoPath, args);
            return chocoOutput.Accept(visitor);
        }

        public string BuildScript(List<string> packages)
        {
            psScriptBuilder = new PSScriptBuilder<string>(packages);
            psScriptBuilder.BuildScript();
            return psScriptBuilder.TempFile;
        }

        public void Run()
        {
            // TODO: parse arguments

            if (Visitor == null)
            {
                Visitor = (IVisitor)Activator.CreateInstance(DEFAULT_VISITOR);
            }
            ChocoOutputFetcher cof = new ChocoOutputFetcher();
            psScriptBuilder = new PSScriptBuilder<string>(cof.Accept(Visitor));
            psScriptBuilder.BuildScript();

            if (scriptAction != null)
            {
                scriptAction(psScriptBuilder.TempFile);
            }
            // TODO
        }

        private void PrintScriptToConsole(string filePath)
        {
            string dataRead;

            StreamReader sr = new StreamReader(filePath);
            dataRead = sr.ReadLine();
            while(dataRead != null)
            {
                Console.WriteLine(dataRead);
                dataRead = sr.ReadLine();
            }
        }

        private void SaveScriptToFile(string filePath)
        {
            string destFileName = Console.ReadLine();
            string destDirectory = Path.GetDirectoryName(destFileName);

            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            File.Copy(filePath, destFileName);
        }

        private void GenerateScriptToConsole(PSScriptBuilder<string> psScriptBuilder)
        {
            /* Generate and write scipt directly to console */
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            sw.AutoFlush = true;
            Console.SetOut(sw);
            psScriptBuilder.BuildScript(sw);
            Console.SetOut(Console.Out);
        }
    }
}
