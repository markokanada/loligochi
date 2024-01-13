using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loligochi_classlib
{
    public class EnvirovmentIsNullException: Exception
    {
        public EnvirovmentIsNullException() : base("Error code 5: The given Envirovment was null.")
        { }
    }
}
