using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class UnsupportedVersionException : Exception, ISerializable
    {
        public UnsupportedVersionException()
        {

        }

        public UnsupportedVersionException(string message) : base(message)
        {

        }

        public UnsupportedVersionException(string message, Exception inner) : base(message, inner)
        {

        }

        public UnsupportedVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

    }
}
