using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// the class is make the entity of the order with all is details.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// the id order
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// customer name in the order
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// customer email in the order
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// customer address in the order
        /// </summary>
        public string? CustomerAdress { get; set; }
        /// <summary>
        /// bring the status of the order.
        /// </summary>
        public OrderStatus? Status { get; set; }
        /// <summary>
        /// show when the order is created
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// show when the order is shipped
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// show when the order is dilverd
        /// </summary>
        public DateTime? DeliveryrDate { get; set; }
        /// <summary>
        /// to put in list the all order items.
        /// </summary>
        public List<OrderItem?> Items { get; set; }
        /// <summary>
        /// bring the total price od the order with all the order item he have.
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        Order ID: {ID}
        Customer Name: {CustomerName}
        Customer Email: {CustomerEmail}
        Customer Adress: {CustomerAdress}
        status: {Status}
        Order Date: {OrderDate}
        Ship Date: {ShipDate}
        Deliveryr Date: {DeliveryrDate}
        Items: {string.Join(Environment.NewLine, Items)}
        Total price order: {TotalPrice}";
    }
}
