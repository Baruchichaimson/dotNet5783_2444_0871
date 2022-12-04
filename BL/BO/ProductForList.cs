using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    /// <summary>
    /// class for Displaying relevant product information
    /// </summary>
    public class ProductForList
    {
        /// <summary>
        /// product id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        ///  product name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        ///  product price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// enum of product category
        /// </summary>
        public CoffeeShop? Category { get; set; }
        public override string ToString() => $@"
        ID : {ID}
        Name: {Name}
        Price: {Price}
        Category: {Category}";
    }
}
