using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        public OrderItem GetOrderItemByOrderAndProductId(int orderId, int productId);
        public IEnumerable<OrderItem> OrderItemsListByOrder(int orderId);
    }
}
