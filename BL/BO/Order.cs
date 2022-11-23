using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAdress { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime DeliveryrDate { get; set; }
        public List<OrderItem> Items { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        ID = {ID}
        CustomerName: {CustomerName}
        CustomerEmail: {CustomerEmail}
        CustomerAdress: {CustomerAdress}
        status: {Status}
        OrderDate: {OrderDate}
        ShipDate: {ShipDate}
        DeliveryrDate: {DeliveryrDate}";
    }
}
