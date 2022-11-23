using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        public BO.Cart AddProduct(Cart cart, int id);
        public BO.Cart UpdateProductAmount(Cart cart, int id, int newAmount);
        public void OrderConfirmation(Cart cart);
    }
}
