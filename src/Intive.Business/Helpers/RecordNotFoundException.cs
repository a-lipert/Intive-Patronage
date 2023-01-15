using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intive.Business.Helpers
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string property, int id) : base($"{property} with {id} not found.") { }
    }
}
