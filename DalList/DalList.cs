using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public sealed class DalList : IDal
    {
        public IProduct Product => new DalProduct();
        public IOrder Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrederItem();
    }
}
