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
    [Serializable]
    public class StorgeIsFullException : Exception
    {
        public StorgeIsFullException(string entityName) : base($"the storge of {entityName} is full\n") { }
    }
    [Serializable]
    public class StorgeIsEmptyException: Exception
    {
        public StorgeIsEmptyException(string entityName) : base($"the storge of {entityName} is empty\n") { }
    }
}
