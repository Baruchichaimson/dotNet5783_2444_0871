using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enums;

namespace Dal;

internal class Program
{
    private static DalProduct S_dalProduct = new DalProduct();
    private static DalOrder S_dalOrder = new DalOrder();
    private static DalOrederItem S_dalOrderItem = new DalOrederItem();
    private static void ProductOptions()/// the user input whice active he want to do on the product.
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nadd product press: 1 \ndelete product press: 2");
            Console.WriteLine("update product press: 3 \nget product by id press: 4 \nprint all products press: 5 \n");
            int.TryParse(Console.ReadLine(), out int userChoice);
            switch (userChoice)
            {
                case (int)User.EXIT:
                    return;
                case (int)User.ADD:///add new product and input the id, name, category, price , amount and add to the store.

                    {
                        Product newProduct = new Product(); 
                        Console.WriteLine("Enter ID:");
                        int.TryParse(Console.ReadLine(), out int id);
                        newProduct.Id = id;
                        Console.WriteLine("Enter the name of product:");
                        newProduct.Name = Console.ReadLine();
                        Console.WriteLine("Enter category number:");
                        Console.WriteLine("for COFFE_MACHINES press  0 \nfor CAPSULES press 1 \nfor ACCESSORIES press 2 \nfor FROTHERS press 3 \nfor SWEETS press 4");
                        int.TryParse(Console.ReadLine(), out int numCategory);
                        newProduct.Categoryname = numCategory switch
                        {
                            0 => CoffeeShop.COFFE_MACHINES,
                            1 => CoffeeShop.CAPSULES,
                            2 => CoffeeShop.ACCESSORIES,
                            3 => CoffeeShop.FROTHERS,
                            4 => CoffeeShop.SWEETS,
                            _ => throw new Exception("No category found")
                        };
                        Console.WriteLine("Enter the price:");
                        double.TryParse(Console.ReadLine(), out double price);
                        newProduct.Price = price;
                        Console.WriteLine("Enter amount:");
                        int.TryParse(Console.ReadLine(), out int inStock);
                        newProduct.Instock = inStock;
                        S_dalProduct.AddProduct(newProduct);/// go to the function that add product to array of products.
                        Console.WriteLine("the product has been succsefully added");
                    }
                    break;
                case (int)User.DELETE: ///input id and go to the function that check if the id is exist and delete from the store.
                    {
                        Console.WriteLine("Enter ID:");
                        int.TryParse(Console.ReadLine(), out int productId);
                        S_dalProduct.DeleteProduct(productId);
                        Console.WriteLine("the product has been succsefully deleted ");
                    };
                    break;
                case (int)User.UPDATE:  ///add new product and input the id, name, category, price , amount.
                    {
                        Product newProduct = new Product();
                        Console.WriteLine("Enter ID to updae:");
                        int.TryParse(Console.ReadLine(), out int id);
                        newProduct.Id = id;
                        Console.WriteLine("Enter the name of product:");
                        newProduct.Name = Console.ReadLine();
                        Console.WriteLine("Enter category number:");
                        Console.WriteLine("for COFFE_MACHINES press  0 \nfor CAPSULES press 1 \nfor ACCESSORIES press 2 \nfor FROTHERS press 3 \nfor SWEETS press 4");
                        int.TryParse(Console.ReadLine(), out int numCategory);
                        newProduct.Categoryname = numCategory switch
                        {
                            0 => CoffeeShop.COFFE_MACHINES,
                            1 => CoffeeShop.CAPSULES,
                            2 => CoffeeShop.ACCESSORIES,
                            3 => CoffeeShop.FROTHERS,
                            4 => CoffeeShop.SWEETS,
                        };
                        Console.WriteLine("Enter the price:");
                        double.TryParse(Console.ReadLine(), out double price);
                        newProduct.Price = price;
                        Console.WriteLine("Enter amount:");
                        int.TryParse(Console.ReadLine(), out int inStock);
                        newProduct.Instock = inStock;
                        S_dalProduct.UpdateProduct(newProduct); ///go to the function that check if the id is exist update the store.
                        Console.WriteLine("the product has been succsefully update");

                    }
                    break;
                case (int)User.GET: ///input product id and go to the function that return the product.
                    {
                        Console.WriteLine("Enter ID:");
                        int.TryParse(Console.ReadLine(), out int productId);
                        Console.WriteLine(S_dalProduct.GetProduct(productId));
                    }
                    break;
                case (int)User.PRINTALL: /// make new array and put in him the array of the products and print the products.
                    {
                        Product[] newArray = S_dalProduct.ProductList();
                        foreach (Product product in newArray)
                        {
                            Console.WriteLine(product);
                        }
                    }
                    break;
            }
        }
    }
    private static void OrderOptions()/// the user input whice active he want to do on the order
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nadd order press: 1 \ndelete order press: 2");
            Console.WriteLine("update order press: 3 \nget order by id press: 4 \nprint all orders press: 5 \n");
            int.TryParse(Console.ReadLine(), out int userChoice);
            switch (userChoice)
            {
                case (int)User.EXIT:
                    return;
                case (int)User.ADD:  ///add new product and input the customer name, customer email, customer adress, order date, ship date, delivery date.
                    {
                        Order newOrder = new Order();
                        Console.WriteLine("Enter the customer name: ");
                        newOrder.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the customer email: ");
                        newOrder.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("Enter the customer adress: ");
                        newOrder.CustomerAdress = Console.ReadLine();
                        newOrder.OrderDate = DateTime.Now;
                        newOrder.ShipDate = DateTime.MinValue;
                        newOrder.DeliveryrDate = DateTime.MinValue;
                        S_dalOrder.AddOrder(newOrder); /// go to the function that add order to array of orders.
                        Console.WriteLine("the order has been succsefully added ");
                    }
                    break;
                case (int)User.DELETE:  ///input id and go to the function that check if the id is exist and delete from the store.
                    {
                        Console.WriteLine("Enter ID: ");
                        int.TryParse(Console.ReadLine(), out int orderId);
                        S_dalOrder.DeleteOrder(orderId);
                        Console.WriteLine("the order has been succsefully deleted ");
                    };
                    break;
                case (int)User.UPDATE:   ///add new product and input the customer name, customer email, customer adress, order date, ship date, delivery date.
                    {
                        Order newOrder = new Order();
                        Console.WriteLine("Enter ID: ");
                        int.TryParse(Console.ReadLine(), out int id);
                        newOrder.Id = id;
                        Console.WriteLine("Enter the customer name: ");
                        newOrder.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the customer email: ");
                        newOrder.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("Enter the customer adress: ");
                        newOrder.CustomerAdress = Console.ReadLine();
                        newOrder.OrderDate = DateTime.Now;
                        newOrder.ShipDate = DateTime.Now.AddDays(5);
                        newOrder.DeliveryrDate = DateTime.Now.AddDays(10);
                        S_dalOrder.UpdateOrder(newOrder); ///go to the function that check if the id is exist update the store.
                        Console.WriteLine("the order has been succsefully update ");
                    }
                    break;
                case (int)User.GET: ///input order id and go to the function that return the order.
                    {
                        Console.WriteLine("Enter ID: ");
                        int.TryParse(Console.ReadLine(), out int orderId);
                        Console.WriteLine(S_dalOrder.GetOrder(orderId));
                    }
                    break;
                case (int)User.PRINTALL: /// make new array and put in him the array of the orders and print the orders.
                    {
                        Order[] newArray = S_dalOrder.OrderList();
                        foreach (Order item in newArray)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
            }
        }
    }
    private static void OrderItemOptions()/// the user input whice active he want to do on the order item
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nadd order item press: 1 \ndelete order item press: 2");
            Console.WriteLine("update order item press: 3 \nget order item by id press: 4 \nprint all order items press: 5");
            Console.WriteLine("get order item by order and product id press: 6 \nget list of items by order id press: 7 \n");

            int.TryParse(Console.ReadLine(), out int userChoice);
            switch (userChoice)
            {
                case (int)User.EXIT: 
                    return;
                case (int)User.ADD: ///add new product and input the product id, order id, amount, price.
                    {
                        OrderItem newOrderItem = new OrderItem();
                        Console.WriteLine("Enter the product id: ");
                        int.TryParse(Console.ReadLine(), out int productId);
                        newOrderItem.ProductID = productId;
                        Console.WriteLine("Enter the Order id: ");
                        int.TryParse(Console.ReadLine(), out int orderId);
                        newOrderItem.OredrID = orderId;
                        Console.WriteLine("Enter the amount: ");
                        int.TryParse(Console.ReadLine(), out int amount);
                        newOrderItem.Amount = amount;
                        newOrderItem.Price = S_dalProduct.GetProduct(newOrderItem.ProductID).Price;
                        S_dalOrderItem.AddOrderItem(newOrderItem); /// go to the function that add order item to array of order items.
                        Console.WriteLine("the orderItem has been succsefully added ");
                    }
                    break;
                case (int)User.DELETE:  ///input id and go to the function that check if the id is exist and delete from the store.
                    {
                        Console.WriteLine("Enter order item ID: ");
                        int.TryParse(Console.ReadLine(), out int orderItemId);
                        S_dalOrderItem.DeleteOrderItem(orderItemId);
                        Console.WriteLine("the orderItem has been succsefully deleted ");
                    };
                    break;
                case (int)User.UPDATE: ///add new product and input the product id, order id, amount, price.
                    {
                        OrderItem newOrderItem = new OrderItem();
                        Console.WriteLine("Enter item ID: ");
                        int.TryParse(Console.ReadLine(), out int id);
                        newOrderItem.Id = id;
                        Console.WriteLine("Enter product ID: ");
                        int.TryParse(Console.ReadLine(), out int productId);
                        newOrderItem.ProductID = productId;
                        Console.WriteLine("Enter the amount: ");
                        int.TryParse(Console.ReadLine(), out  int amount);
                        newOrderItem.Amount= amount;
                        newOrderItem.Price = S_dalProduct.GetProduct(newOrderItem.ProductID).Price;
                        S_dalOrderItem.UpdateOrderItem(newOrderItem); ///go to the function that check if the id is exist update the store.
                        Console.WriteLine("the orderItem has been sccessfully update ");
                    }
                    break;
                case (int)User.GET: ///input order item id and go to the function that return the order item.
                    {
                        Console.WriteLine("Enter item Id: ");
                        int.TryParse(Console.ReadLine(), out int orderItemId);
                        Console.WriteLine(S_dalOrderItem.GetOrderItem(orderItemId));
                    }
                    break;
                case (int)User.PRINTALL: /// make new array and put in him the array of the order items and print the order items.
                    {
                        OrderItem[] newArray = S_dalOrderItem.OrderItemsList();
                        foreach (OrderItem item in newArray)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
                case (int)User.BY_ORDER_AND_PRODUCT: ///give the order item by order number and product
                    {
                        Console.WriteLine("Enter order ID: ");
                        int.TryParse(Console.ReadLine(), out int orderId);
                        Console.WriteLine("Enter product ID: ");
                        int.TryParse(Console.ReadLine(), out int productId);
                        Console.WriteLine(S_dalOrderItem.GetOrderItemByOrderAndProductId(orderId, productId));///go to this function
                    }
                    break;
                case (int)User.ITEM_BY_ORDER_ID: ///give order item by id number of the order item.
                    {
                        Console.WriteLine("Enter order ID: ");
                        int.TryParse(Console.ReadLine(), out int orderID);
                        OrderItem[] newArray = S_dalOrderItem.OrderItemsListByOrder(orderID);
                        foreach (OrderItem item in newArray)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    break;
            }
        }
    }
    static void Main(string[] args)
    {
        while (true) ///ask the user until he will put zero.
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nfor product menu press: 1 \nfor order menu press: 2 \nfor order item menu press: 3");

            int.TryParse(Console.ReadLine(), out int userChoice); ///the user must select which entity wants to perform actions
            try
            {
                switch (userChoice)
                {
                    case (int)UserForMain.EXIT:
                        return;
                    case (int)UserForMain.PRODUCT:
                        ProductOptions();
                        break;
                    case (int)UserForMain.ORDER:
                        OrderOptions();
                        break;
                    case (int)UserForMain.ORDER_ITEM:
                        OrderItemOptions();
                        break;
                }
            }
            catch (Exception ex) ///make catch to the all throw we have in the program.
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}






