using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class AllreadyExistException : Exception
    {
        public AllreadyExistException(string entityName) : base($"the {entityName} is allready exist\n") { }
    }
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName) : base($"the {entityName} is not exist\n") { }
    }
    public class NullExeption : Exception
    {
        public NullExeption(string messege) : base(messege + " is null in DO") { }
    }
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }


}
