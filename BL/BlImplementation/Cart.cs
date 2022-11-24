﻿using System;
using BlApi;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;
using BO;
using System.ComponentModel.DataAnnotations;

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
                            cart.Items.Add(new OrderItem()
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
                            foreach (OrderItem orderItem in cart.Items)
                            {
                                if (orderItem.ProductID == id)
                                {
                                    orderItem.Amount++;
                                    orderItem.TotalPrice += prodact.Price;
                                    break;
                                }
                            }
                        }
                        cart.TotalPrice += prodact.Price;
                        return cart;
                    }
                    throw new IncorrectAmountException("not enough amount in stock");
                }
            }
            throw new EntityNotFoundException("product not found");
        }

        public void OrderConfirmation(BO.Cart cart)
        {
            try
            {
                foreach (OrderItem orderItem in cart.Items)
                {

                    DO.Product product = Dal.Product.Get(orderItem.ProductID);

                    if (orderItem.Amount > product.Instock || orderItem.Amount <= 0)
                    {
                        throw new IncorrectAmountException("not enough amount in stock");
                    }
                }
                if (cart.CustomerEmail != null && cart.CustomerAddress != null && cart.CustomerName != null)
                {
                    throw new EntityDetailsWrongException("missing Customer details");
                }
                if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
                    throw new EntityDetailsWrongException("Invalid email");
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
                throw new EntityNotFoundException(ex.Message);
            }
        }
        public BO.Cart UpdateProductAmount(BO.Cart cart, int id, int newAmount)
        {
            foreach (OrderItem item in cart.Items)
            {
                if(item.ProductID == id)
                {
                    if(newAmount > item.Amount)
                    {
                        item.TotalPrice = item.Price * newAmount;
                        cart.TotalPrice += item.Price * (newAmount - item.Amount);
                        item.Amount = newAmount;
                    }
                    else if(newAmount < item.Amount)
                    {
                        item.TotalPrice = item.Price * newAmount;
                        cart.TotalPrice -= item.Price * (item.Amount - newAmount);
                        item.Amount = newAmount;
                    }
                    else if(item.Amount == newAmount)
                    {
                        cart.Items.Remove(item);
                        cart.TotalPrice -= item.Price * item.Amount;
                    }
                    return cart;
                }
            }
            throw new EntityNotFoundException("the product is not exist in the cart");
        }
    }
}