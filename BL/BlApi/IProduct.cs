using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BO.ProductForList> GetList();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Product GetData(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        public BO.ProductItem GetData(int id, Cart cart);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void Add(Product product);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public void Update(Product product);
    }
}
