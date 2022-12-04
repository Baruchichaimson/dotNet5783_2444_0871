using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// the class is make the entity of the order item with all is details.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// the id oreder item
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// the product name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// product id in the order
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// the price on the order item
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// the amount of the order item
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// the total price of the order item with is all amount
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        ID: {ID}
        Name: {Name}
        Product ID: {ProductID}
        Price: {Price}
        Amount: {Amount}
        Total Price: {TotalPrice}";
    }
}
