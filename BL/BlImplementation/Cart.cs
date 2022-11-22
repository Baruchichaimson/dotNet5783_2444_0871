using System;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Text.RegularExpressions;


namespace BlImplementation
{
    internal class Cart : ICart
    {
        private IDal Dal = new DO.DalList();

        public BO.Cart AddProduct(BO.Cart cart, int id)
        {
            bool prodactExistInCart = cart.Items.Any(x => x.ProductID == id);
       
            foreach (DO.Product prodact in Dal.Product.List())
            {
                if (prodact.Id == id)
                {
                    if (prodact.Instock > 0)
                    {
                        if (!prodactExistInCart)
                        {
                            cart.Items.Add(new BO.OrderItem()
                            {
                                ProductID = prodact.Id,
                                Name = prodact.Name,
                                Price = prodact.Price,
                                Amount = 1,
                                TotalPrice = prodact.Price
                            });
                        }
                        else
                        {
                            foreach (BO.OrderItem orderItem in cart.Items)
                            {
                                if (orderItem.ProductID == id)
                                {
                                    orderItem.Amount++;
                                    orderItem.Price += prodact.Price;
                                    break;
                                }
                            }
                        }
                        cart.TotalPrice += prodact.Price;
                        return cart;
                    }
                    throw new Exception("the id is negtive");
                }
            }
            throw new Exception("the id is negtive");
        }

        public void OrderConfirmation(BO.Cart cart, string name, string mail, string addres)
        {
            foreach (BO.OrderItem orderItem in cart.Items)
            {
                DO.Product prodact = Dal.Product.Get(orderItem.ProductID);
                if (orderItem.Amount > prodact.Instock || orderItem.Amount <= 0)
                {
                    throw new Exception("not have in the storge");
                }
            }
            if(cart.CustomerEmail != null && cart.CustomerAddress != null && cart.CustomerName != null)
            {

            }
            


        }

        public BO.Cart UpdateProductAmount(BO.Cart cart, int id, int newAmount)
        {
            
        }
    }
}
