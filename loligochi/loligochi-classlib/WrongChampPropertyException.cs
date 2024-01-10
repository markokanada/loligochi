using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loligochi_classlib
{
	public class WrongChampPropertyException: Exception
	{
        public WrongChampPropertyException():base("Error code 4: Wrong value given to specific property")
        {
            
        }
    }
}
