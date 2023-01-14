using Dal;
using DalApi;

namespace Dal
{
    /// <summary>
    /// The DalXml class is an implementation of the IDal interface that uses XML files to store data.
    /// It contains instances of the DalProduct, DalOrder, and DalOrderItem classes for managing products, orders, and order items respectively
    /// </summary>
    internal sealed class DalXml : IDal
    {
        public static IDal Instance { get; } = new DalXml();
        private DalXml() { }
        public IProduct Product { get; } = new DalProduct();
        public IOrder Order { get; } = new DalOrder();
        public IOrderItem OrderItem { get; } = new DalOrederItem();
    }
}
