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
        private string outputFilePath = null;
        private ChocoOutputFetcher chocoOutput = null;
        private PSScriptBuilder<string> psScriptBuilder = null;
        private const string CHOCO_VERSION_OPT = "--version";
        private const string CHOCO_SUPPORTED_VERSION = "0.9.9.6";
        private string[] args;

        public ChocoCup(string[] args = null)
        {
            this.args = args;
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

        public void CheckCurrentChocoVersion(ChocoCupOptions copt)
        {
            ChocoOutputFetcher cof = new ChocoOutputFetcher(copt.ChocoPath, CHOCO_VERSION_OPT);
            string currVer = cof.Accept(new VersionParserVisitor())[0];

            if (currVer != CHOCO_SUPPORTED_VERSION)
            {
                /* This doesn't really work if multiple versions are to be supported
                   A better approach would probably be to return a version-specific
                    visitor.
                */
                bool userAnswered = false;
                Console.WriteLine("You are using Chocolatey version {0}. This tool is designed to work with {1}.", currVer, CHOCO_SUPPORTED_VERSION);
                Console.WriteLine("Do you want to run the tool anyway? (y/n)");
                while (!userAnswered)
                {
                    string userAnswer = Console.ReadLine();
                    switch (userAnswer)
                    {
                        case "yes":
                        case "y":
                            userAnswered = true;
                            break;
                        case "no":
                        case "n":
                            userAnswered = true;
                            throw new UnsupportedVersionException("This tool is designed to work with Chocolater v." + CHOCO_SUPPORTED_VERSION + 
                                " You are running Chocolatey v." + currVer);
                        default:
                            Console.WriteLine("Please type 'y' or 'n'");
                            break;
                    }
                }

            }
        }

        public void Run()
        {
            ChocoCupOptions copt = ArgumentParser.Parse(args);

            if (!copt.IgnoreChocoVersion) { 
                CheckCurrentChocoVersion(copt);
            }

           IVisitor visitor = (IVisitor)Activator.CreateInstance(copt.Visitor);
           ChocoOutputFetcher cof = new ChocoOutputFetcher(copt.ChocoPath);

            psScriptBuilder = new PSScriptBuilder<string>(cof.Accept(visitor));
            psScriptBuilder.BuildScript();
            outputFilePath = copt.OutFilePath;

            if(copt.PrintScript)
            {
                PrintScriptToConsole(psScriptBuilder.TempFile);
            }

            bool fileSaved = false;
            while(!fileSaved) {
                try
                {
                    if (outputFilePath == null)
                        PrintFileNameRequestMsg();

                    SaveScriptToFile(outputFilePath);
                    fileSaved = true;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("You don't have permission to create files/directories at {0}", outputFilePath);
                    Console.WriteLine("Tip: Try running the program as an administrator or select a different path.");
                    PrintFileNameRequestMsg();
                    outputFilePath = null;

                }
                catch (IOException)
                {
                    Console.WriteLine("An error ocurred while copying the file. Please try a different name. (Maybe a file with that name already exists?)");
                    PrintFileNameRequestMsg();
                    outputFilePath = null;
                }
                catch (ArgumentNullException)
                {
                    Console.Write("Script file output path(including name and extension):");
                    outputFilePath = null;
                }
                catch (Exception)
                {
                    Console.WriteLine("An error ocurred while copying the file. Please try a different name.");
                    PrintFileNameRequestMsg();
                    outputFilePath = null;
                }
            }
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

        private void SaveScriptToFile(string filePath, bool replaceFile = false)
        {
            if (filePath == null)
                filePath = Console.ReadLine();

            string destDirectory = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            File.Copy(psScriptBuilder.TempFile, filePath, replaceFile);
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

        private void PrintFileNameRequestMsg() { Console.Write("Script file output path (including name and extension):"); }
    }
}
