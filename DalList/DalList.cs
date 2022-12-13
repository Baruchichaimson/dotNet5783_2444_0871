using Dal;
using DalApi;

namespace Dal
{
    internal sealed class DalList : IDal
    {
        public static IDal Instance { get; } = new DalList();
        private DalList()
        {
            this.Product = new DalProduct();
            this.Order = new DalOrder();
            this.OrderItem = new DalOrederItem();
        }
        public IProduct Product { get; }
        public IOrder Order { get; }
        public IOrderItem OrderItem { get; }
    }
}
