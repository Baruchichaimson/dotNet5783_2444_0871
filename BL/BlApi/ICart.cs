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
        public Cart AddProduct(Cart cart, int id);
        public Cart UpdateProductAmount(Cart cart, int id, int newAmount);
        public void OrderConfirmation(Cart cart, string name, string mail, string addres);
    }
}
