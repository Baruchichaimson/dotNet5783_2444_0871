using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IProduct
    {
        public List<BO.ProductForList> GetList();
        public BO.Product GetData(int id);
        public BO.ProductItem GetData(int id, Cart cart);
        public void Add(Product product);
        public void Delete(int id);
        public void Update(Product product);
    }
}
