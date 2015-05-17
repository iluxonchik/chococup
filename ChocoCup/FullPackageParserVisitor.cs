using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class FullPackageParserVisitor : IVisitor
    {
        /* 
         * Includes package name and package version 
         */
        private const string CHOCOLATEY_NAME = "Chocolatey";

        public List<string> Visit(ChocoCup cc)
        {
            const char SEPARATOR = ' ';
            const string VERSION_OPT = "-version ";
            List<String> packages = cc.GetProcessOutput();
            int numPackages = packages.Count;
            int index = 0;
            Queue<string> itemsToRemove = new Queue<String>();

            // enqueue items to remove (items which are not package names)
            while (index < numPackages && !packages[index].StartsWith(CHOCOLATEY_NAME))
            {
                itemsToRemove.Enqueue(packages[index]);
                index++;
            }

            itemsToRemove.Enqueue(packages[index]); // remove chocolatey string version
            itemsToRemove.Enqueue(packages[numPackages-1]); // remove number of packages installed message

            packages = packages.Except(itemsToRemove).ToList();

            numPackages = packages.Count;

            for (int i = 0; i < numPackages; i++)
            {
                int indexOfSeparator = packages[i].IndexOf(SEPARATOR);
                packages[i] = packages[i].Substring(0, indexOfSeparator) + " " + VERSION_OPT + packages[i].Substring(indexOfSeparator + 1);
            }

            return packages;
        }
    }
}
