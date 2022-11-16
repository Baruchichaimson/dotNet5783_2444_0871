using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class AllreadyExist : Exception
    {
        public AllreadyExist(string entityName) : base($"the {entityName} is allready exist\n") { }
    }
    public class EntityNotFound : Exception
    {
        public EntityNotFound(string entityName) : base($"the {entityName} is not exist\n") { }
    }
    public class StorgeIsFull : Exception
    {
        public StorgeIsFull(string entityName) : base($"the storge of {entityName} is full\n") { }
    }
    public class StorgeIsEmpty : Exception
    {
        public StorgeIsEmpty(string entityName) : base($"the storge of {entityName} is empty\n") { }
    }
}
