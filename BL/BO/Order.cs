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
        public DateTime OrderDate = DateTime.Now;
        public DateTime PaymentDate = DateTime.Now;
        public DateTime ShipDate = DateTime.Now;
        public DateTime DeliveryrDate = DateTime.MinValue;
        public OrderItem Items { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        ID = {ID}
        CustomerName: {CustomerName}
        CustomerEmail: {CustomerEmail}
        CustomerAdress: {CustomerAdress}
        status: {Status}
        PaymentDate: {PaymentDate}
        OrderDate: {OrderDate}
        ShipDate: {ShipDate}
        DeliveryrDate: {DeliveryrDate}";
    }
}
