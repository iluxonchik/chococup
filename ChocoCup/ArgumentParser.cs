using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    static class ArgumentParser
    {
        private const string IGONORE_CHOCO_VER_OPT = "-i";
        private const string INC_VERSION_OPT = "-v";
        private const string CHOCO_PATH_OPT = "-c";
        private const string OUT_FILE_OPT = "-f";
        private const string PRINT_OPT = "-p";
        private const string INVALID_ARGUMENTS_MESSAGE = "There was an error parsing the command line arguments." + 
                                                            "Please make sure you followed the correct format.";

        public static ChocoCupOptions Parse(string[] args)
        {
            if (args == null)
                return null;

            ChocoCupOptions copt = new ChocoCupOptions();

            int numArgs = args.Length;

            for (int i = 0; i < numArgs; i++)
            {
                switch (args[i])
                {
                    case OUT_FILE_OPT:
                        copt.OutFilePath = args[++i];
                        break;

                    case CHOCO_PATH_OPT:
                        copt.ChocoPath = args[++i];
                        break;

                    case PRINT_OPT:
                        copt.PrintScript = true;
                        break;

                    case INC_VERSION_OPT:
                        copt.Visitor = copt.FULL_NAME_VISITOR;
                        break;

                    case IGONORE_CHOCO_VER_OPT:
                        copt.IgnoreChocoVersion = true;
                        break;

                    default:
                        throw new InvalidArgumentsException(INVALID_ARGUMENTS_MESSAGE);
                }
            }

            return copt;
        }
    }
}
