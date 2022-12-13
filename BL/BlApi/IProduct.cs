using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    /// <summary>
    /// product entity interface
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// return a list of all products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductForList?>? GetList(Func<ProductForList?, bool>? myFunc = null);
        /// <summary>
        /// return the data of the products
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Product GetData(int id);
        /// <summary>
        /// return the data of the products
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        public BO.ProductItem GetData(int id, Cart cart);
        /// <summary>
        /// gets a product entity and add to the list of products
        /// </summary>
        /// <param name="product"></param>
        public void Add(Product product);
        /// <summary>
        /// gets a product entity and delete product fromn the list.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);
        /// <summary>
        /// gets  a product entity and update him.
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product);
    }
}
