using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class InvalidArgumentsException : Exception, ISerializable
    {
        public InvalidArgumentsException()
        {

        }

        public InvalidArgumentsException(string message) : base(message)
        {
            
        }

        public InvalidArgumentsException(string message, Exception inner) : base(message, inner)
        {

        }

        public InvalidArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
