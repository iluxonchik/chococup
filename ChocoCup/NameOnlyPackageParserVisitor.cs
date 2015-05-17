using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class NameOnlyPackageParserVisitor : IVisitor
    {
        /* 
         * Includes package names only 
         */
        public List<string> Visit(ChocoCup cc)
        {
            List<String> packages = cc.Accept(new FullPackageParserVisitor());
            int numPackages = packages.Count;

            for (int i = 0; i < numPackages; i++)
            {
                packages[i] = packages[i].Substring(0, packages[i].IndexOf(' '));
            }

            return packages;
        }
    }
}
