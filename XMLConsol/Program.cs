using DalApi;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class Program
{
    private static IDal dallist = Factory.Get();
    const string s_orderItem = @"OrderItems";
    const string s_product = @"Products";
    const string s_order = @"Orders";

    private static void LoadOrders()
    {
        List<Order?> orders = dallist.Order.List().ToList();
        XMLTools.SaveListToXMLSerializer(orders, s_order);
    }
    private static void LoadOrderItems()
    {
        List<OrderItem?> orderItems = dallist.OrderItem.List().ToList();
        XMLTools.SaveListToXMLSerializer(orderItems, s_orderItem);

    }
    private static void LoadProducts()
    {
        List<Product?> products = dallist.Product.List().ToList();
        XMLTools.SaveListToXMLSerializer(products, s_product);

    }
    static void Main(string[] args)
    {
        while (true) ///ask the user until he will put zero.
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nfor load xml products press: 1 \nfor load xml orders press: 2 \nfor load xml order items press: 3");

            int.TryParse(Console.ReadLine(), out int userChoice); ///the user must select which entity wants to perform actions
            //try
            //{
                switch (userChoice)
                {
                    case (int)UserForMain.EXIT:
                        return;
                    case (int)UserForMain.PRODUCT:
                        LoadProducts();
                        break;
                    case (int)UserForMain.ORDER:
                        LoadOrders();
                        break;
                    case (int)UserForMain.ORDER_ITEM:
                        LoadOrderItems();
                        break;
                }
            //}
            //catch (Exception ex) ///make catch to the all throw we have in the program.
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }
    }
}