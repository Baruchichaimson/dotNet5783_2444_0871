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
        public List<ProductForList> GetList();
        public Product GetData(int id);
        public Product GetData(int id, Cart cart);
        public void Add(Product product);
        public Product Delete(int id);
        public void Update(Product product);
    }
}
