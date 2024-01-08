using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loligochi_classlib
{
    public class SerializationErrorException : Exception
    {
        public SerializationErrorException():base ("Error Code 3: There was an error in the process of serialization which could happened because of file corruption or data loss. The game cant start with this state. ")
        {
            
        }
    }
}
