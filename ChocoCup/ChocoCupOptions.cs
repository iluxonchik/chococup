using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class ChocoCupOptions
    {
        public readonly Type DEFAULT_VISITOR = typeof(NameOnlyPackageParserVisitor);
        public readonly Type FULL_NAME_VISITOR = typeof(FullPackageParserVisitor);
        public string ChocoPath { get; set; }
        public string OutFilePath { get; set; }
        public bool PrintScript { get; set; }
        public bool IgnoreChocoVersion { get; set; }
        public Type Visitor { get; set; }

        public ChocoCupOptions()
        {
            ChocoPath = null;
            OutFilePath = null;
            PrintScript = false;
            Visitor = DEFAULT_VISITOR;
        }
    }
}
