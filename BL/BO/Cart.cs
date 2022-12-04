using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// the class is make the entity of the basket shopping.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// we can put customer name in the order that he make for himself.
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// we can put customer email in the order that we have in the cart that he make for himself.
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// we can put customer address in the order that we have in the cart  that he make for himself.
        /// </summary>
        public string? CustomerAddress { get; set; }
        /// <summary>
        /// we need the list of the order item we have in the order that we have in the cart  that the customer build for himself in the cart.
        /// </summary>
        public List<OrderItem?>? Items { get; set; }
        /// <summary>
        /// we need the total price for the all basket shopping.
        /// </summary>
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
        CustomerName: {CustomerName}
        CustomerEmail: {CustomerEmail}
        CustomerAdress: {CustomerAddress}
        Items: {string.Join(Environment.NewLine, Items)}
        TotalPrice cart: {TotalPrice}";
    }
}
