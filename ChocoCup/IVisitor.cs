using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    interface IVisitor
    {
        List<string> visit(ChocoCup cc);
    }
}
