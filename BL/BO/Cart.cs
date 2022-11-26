using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// 
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// 
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderItem> Items { get; set; }
        /// <summary>
        /// 
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
