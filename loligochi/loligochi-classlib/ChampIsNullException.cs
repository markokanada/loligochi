using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loligochi_classlib
{
    public class ChampIsNullException : Exception
    {
        public ChampIsNullException() : base("Error code 1: The given champ was null. ")
        {
            
        }
    }
}
