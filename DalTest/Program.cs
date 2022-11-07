using DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enums;

namespace Dal;

internal class Program
{
    private DalProduct DalProduct = new DalProduct();
    private DalOrder DalOrder = new DalOrder();
    private DalOrederItem DalOrederItem = new DalOrederItem();
    enum User { EXIT, ADD, DELETE, UPDATE, GET, PRINTALL, BY_ORDER_AND_PRODUCT, ITEM_BY_ORDER_ID };
    enum UserForMain { EXIT, PRODUCT, ORDER, ORDER_ITEM }
    public static void ProductOptions()
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nadd product press: 1 \ndelete product press: 2");
            Console.WriteLine("update product press: 3 \nget product by id press: 4 \nprint all products press: 5 \n");
            int.TryParse(Console.ReadLine(), out int user_choice);
            switch (user_choice)
            {
                case (int)User.EXIT:
                    return;
                case (int)User.ADD:
                    {
                        Product newproduct = new Product();
                        Console.WriteLine("Enter ID:");
                        int.TryParse(Console.ReadLine(), out int id);
                        newproduct.Id = id;
                        Console.WriteLine("Enter the name of product:");
                        newproduct.Name = Console.ReadLine();
                        Console.WriteLine("Enter category number:");
                        Console.WriteLine("for COFFE_MACHINES press  0 \nfor CAPSULES press 1 \nfor ACCESSORIES press 2 \nfor FROTHERS press 3 \nfor SWEETS press 4");
                        int.TryParse(Console.ReadLine(), out int numcategory);
                        newproduct.Categoryname = numcategory switch
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
                        newproduct.Price = price;
                        Console.WriteLine("Enter amount:");
                        int.TryParse(Console.ReadLine(), out int inStock);
                        newproduct.Instock = inStock;
                        DalProduct.AddProduct(newproduct);
                        Console.WriteLine("the product has been succsefully added");
                    }
                    break;
                case (int)User.DELETE:
                    {
                        Console.WriteLine("Enter ID:");
                        int.TryParse(Console.ReadLine(), out int productId);
                        DalProduct.DeleteProduct(productId);
                        Console.WriteLine("the product has been succsefully deleted ");
                    };
                    break;
                case (int)User.UPDATE:
                    {
                        Product newproduct = new Product();
                        Console.WriteLine("Enter ID to updae:");
                        int.TryParse(Console.ReadLine(), out int id);
                        newproduct.Id = id;
                        Console.WriteLine("Enter the name of product:");
                        newproduct.Name = Console.ReadLine();
                        Console.WriteLine("Enter category number:");
                        Console.WriteLine("for COFFE_MACHINES press  0 \nfor CAPSULES press 1 \nfor ACCESSORIES press 2 \nfor FROTHERS press 3 \nfor SWEETS press 4");
                        int.TryParse(Console.ReadLine(), out int numcategory);
                        newproduct.Categoryname = numcategory switch
                        {
                            0 => CoffeeShop.COFFE_MACHINES,
                            1 => CoffeeShop.CAPSULES,
                            2 => CoffeeShop.ACCESSORIES,
                            3 => CoffeeShop.FROTHERS,
                            4 => CoffeeShop.SWEETS,
                        };
                        Console.WriteLine("Enter the price:");
                        double.TryParse(Console.ReadLine(), out double price);
                        newproduct.Price = price;
                        Console.WriteLine("Enter amount:");
                        int.TryParse(Console.ReadLine(), out int inStock);
                        newproduct.Instock = inStock;
                        DalProduct.UpdateProduct(newproduct);
                        Console.WriteLine("the product has been succsefully update");

                    }
                    break;
                case (int)User.GET:
                    {
                        Console.WriteLine("Enter ID:");
                        int.TryParse(Console.ReadLine(), out int productId);
                        Console.WriteLine(DalProduct.GetProduct(productId));
                    }
                    break;
                case (int)User.PRINTALL:
                    {
                        Product[] newarray = DalProduct.ProductList();
                        for (int i = 0; i < newarray.Length; i++)
                        {
                            Console.WriteLine(newarray[i]);
                        }
                    }
                    break;
            }
        }
    }
    public static void OrderOptions()
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nadd order press: 1 \ndelete order press: 2");
            Console.WriteLine("update order press: 3 \nget order by id press: 4 \nprint all orders press: 5 \n");
            int.TryParse(Console.ReadLine(), out int user_choice);
            switch (user_choice)
            {
                case (int)User.EXIT:
                    return;
                case (int)User.ADD:
                    {
                        Order neworder = new Order();
                        Console.WriteLine("Enter the customer name: ");
                        neworder.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the customer email: ");
                        neworder.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("Enter the customer adress: ");
                        neworder.CustomerAdress = Console.ReadLine();
                        neworder.OrderDate = DateTime.Now;
                        neworder.ShipDate = DateTime.MinValue;
                        neworder.DeliveryrDate = DateTime.MinValue;
                        DalOrder.AddOrder(neworder);
                        Console.WriteLine("the order has been succsefully added ");
                    }
                    break;
                case (int)User.DELETE:
                    {
                        Console.WriteLine("Enter ID: ");
                        int.TryParse(Console.ReadLine(), out int OrderId);
                        DalOrder.DeleteOrder(OrderId);
                        Console.WriteLine("the order has been succsefully deleted ");
                    };
                    break;
                case (int)User.UPDATE:
                    {
                        Order neworder = new Order();
                        Console.WriteLine("Enter ID: ");
                        int.TryParse(Console.ReadLine(), out int id);
                        neworder.Id = id;
                        Console.WriteLine("Enter the customer name: ");
                        neworder.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the customer email: ");
                        neworder.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("Enter the customer adress: ");
                        neworder.CustomerAdress = Console.ReadLine();
                        neworder.OrderDate = DateTime.Now;
                        neworder.ShipDate = DateTime.Now.AddDays(5);
                        neworder.DeliveryrDate = DateTime.Now.AddDays(10);
                        DalOrder.UpdateOrder(neworder);
                        Console.WriteLine("the order has been succsefully update ");
                    }
                    break;
                case (int)User.GET:
                    {
                        Console.WriteLine("Enter ID: ");
                        int.TryParse(Console.ReadLine(), out int OrderId);
                        Console.WriteLine(DalOrder.GetOrder(OrderId));
                    }
                    break;
                case (int)User.PRINTALL:
                    {
                        Order[] newarray = DalOrder.OrderList();
                        for (int i = 0; i < newarray.Length; i++)
                        {
                            Console.WriteLine(newarray[i]);
                        }
                    }
                    break;
            }
        }
    }
    public static void OrderItemOptions()
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nadd order item press: 1 \ndelete order item press: 2");
            Console.WriteLine("update order item press: 3 \nget order item by id press: 4 \nprint all order items press: 5");
            Console.WriteLine("get order item by order and product id press: 6 \nget list of items by order id press: 7 \n");

            int.TryParse(Console.ReadLine(), out int user_choice);
            switch (user_choice)
            {
                case (int)User.EXIT:
                    return;
                case (int)User.ADD:
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
                        newOrderItem.Price = DalProduct.GetProduct(newOrderItem.ProductID).Price;
                        DalOrederItem.AddOrderItem(newOrderItem);
                        Console.WriteLine("the orderItem has been succsefully added ");
                    }
                    break;
                case (int)User.DELETE:
                    {
                        Console.WriteLine("Enter order item ID: ");
                        int.TryParse(Console.ReadLine(), out int orderItemId);
                        DalOrederItem.DeleteOrderItem(orderItemId);
                        Console.WriteLine("the orderItem has been succsefully deleted ");
                    };
                    break;
                case (int)User.UPDATE:
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
                        newOrderItem.Price = DalProduct.GetProduct(newOrderItem.ProductID).Price;
                        DalOrederItem.UpdateOrderItem(newOrderItem);
                        Console.WriteLine("the orderItem has been sccessfully update ");
                    }
                    break;
                case (int)User.GET:
                    {
                        Console.WriteLine("Enter item Id: ");
                        int.TryParse(Console.ReadLine(), out int OrderItemId);
                        Console.WriteLine(DalOrederItem.GetOrderItem(OrderItemId));
                    }
                    break;
                case (int)User.PRINTALL:
                    {
                        OrderItem[] newarray = DalOrederItem.OrderItemsList();
                        for (int i = 0; i < newarray.Length; i++)
                        {
                            Console.WriteLine(newarray[i]);
                        }
                    }
                    break;
                case (int)User.BY_ORDER_AND_PRODUCT:
                    {
                        Console.WriteLine("Enter order ID: ");
                        int.TryParse(Console.ReadLine(), out int OrderID);
                        Console.WriteLine("Enter product ID: ");
                        int.TryParse(Console.ReadLine(), out int ProductID);
                        Console.WriteLine(DalOrederItem.GetOrderItemByOrderAndProductId(OrderID, ProductID));
                    }
                    break;
                case (int)User.ITEM_BY_ORDER_ID:
                    {
                        Console.WriteLine("Enter order ID: ");
                        int.TryParse(Console.ReadLine(), out int OrderID);
                        OrderItem[] newarray = DalOrederItem.OrderItemsListByOrder(OrderID);
                        for (int i = 0; i < newarray.Length; i++)
                        {
                            Console.WriteLine(newarray[i]);
                        }
                    }
                    break;
            }
        }
    }
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nfor product menu press: 1 \nfor order menu press: 2 \nfor order item menu press: 3");

            int.TryParse(Console.ReadLine(), out int user_choice);
            try
            {
                switch (user_choice)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}






