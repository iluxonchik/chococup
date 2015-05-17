using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    interface IVisitable
    {
        List<String> Accept(IVisitor visitor);
    }
}
