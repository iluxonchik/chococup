using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class ProcessOutputFetcherException: Exception, ISerializable
    {

        public ProcessOutputFetcherException()
        {

        }

        public ProcessOutputFetcherException(string message) : base(message)
        {

        }

        public ProcessOutputFetcherException(string message, Exception inner) : base(message, inner)
        {

        }

        public ProcessOutputFetcherException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
