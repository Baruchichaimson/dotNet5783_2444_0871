using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, Exception innerexception) : base($"the {entityName} is not exist\n", innerexception) { }
    }
    [Serializable]
    public class NotEnoughInStockException : Exception
    {
        public NotEnoughInStockException(string messege) : base(messege) { }
    }
    [Serializable]
    public class NotExsitException : Exception
    {
        public NotExsitException(string entityName) : base($"the {entityName} is not exist\n") { }
    }
}
