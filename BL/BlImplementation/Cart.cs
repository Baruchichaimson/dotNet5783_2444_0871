using System;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        private IDal Dal = new DalList();

        public BO.Cart AddProduct(BO.Cart cart, int id)
        {
            
        }

        public void OrderConfirmation(BO.Cart cart, string name, string mail, string addres)
        {
            
        }

        public BO.Cart UpdateProductAmount(BO.Cart cart, int id, int newAmount)
        {
            
        }
    }
}
