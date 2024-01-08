using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loligochi_classlib
{
    public class FileMissingException : Exception
    {
        public FileMissingException() : base("Error code 2: The given FileSource was null. A file is could be corrupt or missing! ")
        {
            
        }
    }
}
