using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Product
    {
        /// <summary>
        /// product id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// prodact name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// priduct price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// enum of product category
        /// </summary>
        public CoffeeShop? Category { get; set; }
        /// <summary>
        /// How many in stock
        /// </summary>
        public int InStock { get; set; }
        public override string ToString() => $@"
        Product ID : {ID}
        Name: {Name}
        Price: {Price}
        Category: {Category}
        InStock: {InStock}";
    }
}
