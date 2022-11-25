using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int ID { get; set; }
        public OrderStatus Status { get; set; }
        public List<string>? orderDetails { get; set; }
        public override string ToString() => $@"
        ID: {ID}
        Status: {Status}
        order Details:
        {string.Join(Environment.NewLine, orderDetails)}";
    }
}
