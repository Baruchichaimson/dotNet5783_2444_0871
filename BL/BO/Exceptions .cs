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
        public EntityNotFoundException(string messege) : base(messege) { }
    }
    [Serializable]
    public class NotEnoughInStockException : Exception
    {
        public NotEnoughInStockException(string messege) : base(messege) { }
    }
    [Serializable]
    public class IdNotExsitException : Exception
    {
        public IdNotExsitException(string messege) : base(messege) { }
    }
    [Serializable]
    public class EntityDetailsWrongException : Exception
    {
        public EntityDetailsWrongException(string messege) : base(messege) { }
    }
    [Serializable]
    public class IncorrectAmountException : Exception
    {
        public IncorrectAmountException(string messege) : base(messege) { }
    }
}
