using System;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BO;

namespace BlImplementation
{
    /// <summary>
    /// the class cart have the all function logic the we need in the basket shopping in the shop.
    /// </summary>
    internal class Cart : ICart
    {
        private IDal Dal = new DO.DalList();
        /// <summary>
        /// the function add product from the store to the basket shopping .
        /// </summary>
        /// <param name="cart"> We receive a cart  initialized shopping basket entity </param>
        /// <param name="id"> the id is for the user that want to add id specific </param>
        /// <returns> return the basket shopping with the new product we put in him </returns>
        /// <exception cref="BO.IncorrectAmountException"> the exception say we dont have ehough in the stock for put this amount of product in the basket shopping</exception>
        /// <exception cref="BO.EntityNotFoundException"> the id product is not correct and we can't search this product in the stock </exception>
        public BO.Cart AddProduct(BO.Cart cart, int id)
        {
            bool prodactExistInCart = false;
            if (cart.Items is null)
            {
                cart.Items = new();
            }
            prodactExistInCart = cart.Items.Any(x => x.ProductID == id);

            foreach (DO.Product product in Dal.Product.List())
            {
                if (product.Id == id)
                {
                    if (product.Instock > 0)
                    {
                        if (!prodactExistInCart || cart.Items is null)
                        {
                            cart.Items.Add(new BO.OrderItem()
                            {
                                ProductID = product.Id,
                                Name = product.Name,
                                Price = product.Price,
                                Amount = 1,
                                TotalPrice = product.Price
                            });
                        }
                        else
                        {
                            foreach (BO.OrderItem orderItem in cart.Items)
                            {
                                if (orderItem.ProductID == id)
                                {
                                    orderItem.Amount++;
                                    orderItem.TotalPrice += product.Price;
                                    break;
                                }
                            }
                        }
                        if (cart.TotalPrice > 0)
                            cart.TotalPrice += product.Price;
                        else
                            cart.TotalPrice = product.Price;
                        return cart;
                    }
                    throw new BO.IncorrectAmountException("not enough amount in stock");
                }
            }
            throw new BO.EntityNotFoundException("product not found");
        }
        /// <summary>
        /// the function check that all the details on the order is 
        /// correct and confirmation the order after that and put him in the list order and update the stock if we by someting.
        /// </summary>
        /// <param name="cart"> We receive a cart  initialized shopping basket entity </param>
        /// <exception cref="BO.IncorrectAmountException"> the exception say we dont have ehough in the stock for put this amount of product in the basket shopping</exception>
        /// <exception cref="BO.EntityDetailsWrongException"> the exception say that we miss details in the order or myabe the email is not valid</exception>
        /// <exception cref="BO.EntityNotFoundException">is take the exception fron the data layer and say that </exception>
        /// <exception cref="BO.AllreadyExistException">throw exception when the id is all ready exsit </exception>
        public void OrderConfirmation(BO.Cart cart)
        {
            if (cart.Items is null || cart.Items.Count == 0)
            {
                throw new CartEmptyException("the cart is empty");
            }
            try
            {
                foreach (BO.OrderItem orderItem in cart.Items)
                {

                    DO.Product product = Dal.Product.Get(orderItem.ProductID);

                    if (orderItem.Amount > product.Instock || orderItem.Amount <= 0)
                    {
                        throw new BO.IncorrectAmountException("not enough amount in stock");
                    }
                }

                if (cart.CustomerEmail == null || cart.CustomerAddress == null || cart.CustomerName == null)
                {
                    throw new BO.EntityDetailsWrongException("missing Customer details");
                }
                if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
                    throw new BO.EntityDetailsWrongException("Invalid email");
                DO.Order order = new()
                {
                    CustomerName = cart.CustomerName,
                    CustomerAdress = cart.CustomerAddress,
                    CustomerEmail = cart.CustomerEmail,
                    OrderDate = DateTime.Now,
                    ShipDate = DateTime.MinValue,
                    DeliveryrDate = DateTime.MinValue
                };
                int idOrder = Dal.Order.Add(order);
                foreach (var item in cart.Items)
                {
                    DO.OrderItem orderItem = new()
                    {
                        ProductID = item.ProductID,
                        Price = item.Price,
                        Amount = item.Amount,
                        OredrID = idOrder
                    };
                    Dal.OrderItem.Add(orderItem);
                    DO.Product product = Dal.Product.Get(item.ProductID);
                    product.Instock -= item.Amount;
                    Dal.Product.Update(product);
                }
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new BO.EntityNotFoundException(ex.Message);
            }
            catch (DO.AllreadyExistException ex)
            {
                throw new BO.AllreadyExistException(ex.Message);
            }
        }
        /// <summary>
        /// the function is check if the order item is correct and if wh have it we can update is amount.
        /// </summary>
        /// <param name="cart"> We receive a cart  initialized shopping basket entity </param>
        /// <param name="id"> the id is for the user that want to add id specific </param>
        /// <param name="newAmount"> the user put amount that he want to update in the order that we have in the basket shopping</param>
        /// <returns> return the basket shopping with new oder </returns>
        /// <exception cref="BO.EntityNotFoundException"> the exception say that the prouct we wanted to change is not exist in the basket shopping</exception>
        public BO.Cart UpdateProductAmount(BO.Cart cart, int id, int newAmount)
        {
            if(cart.Items is null)
            {
                throw new CartEmptyException("the cart is empty");
            }
            foreach (BO.OrderItem item in cart.Items)
            {
                if (item.ProductID == id)
                {
                    if (newAmount == 0)
                    {
                        cart.Items.Remove(item);
                        cart.TotalPrice -= item.Price * item.Amount;
                    }
                    else if (newAmount > item.Amount)
                    {
                        item.TotalPrice = item.Price * newAmount;
                        cart.TotalPrice += item.Price * (newAmount - item.Amount);
                        item.Amount = newAmount;
                    }
                    else if (newAmount < item.Amount)
                    {
                        if (newAmount < 0)
                        {
                            throw new IncorrectAmountException("the amount can't be negative number");
                        }
                        item.TotalPrice = item.Price * newAmount;
                        cart.TotalPrice -= item.Price * (item.Amount - newAmount);
                        item.Amount = newAmount;
                    }
                    return cart;
                }
            }
            throw new BO.EntityNotFoundException("the product is not exist in the cart");
        }
    }
}
