using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class VersionParserVisitor : IVisitor
    {
        public List<string> Visit(ChocoOutputFetcher cc)
        {
            List<String> version = cc.GetProcessOutput();
            List<string> versionList = new List<string>(1);
            versionList.Add(version[version.Count - 1]);
            return versionList;
        }
    }
}
