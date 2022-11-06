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
    enum User { EXIT, ADD, DELETE, UPDATE, GET, PRINTALL };
    public static void ProductOptions()
    {
        Console.WriteLine("Press your choice \n exit press: 0 \n add product press: 1 \n delete product press: 2 \n");
        Console.WriteLine("update product press: 3 \n get product by id press: 4 \n print all products press: 5 \n");
        int user_choice = Convert.ToInt32(Console.ReadLine());
         switch(user_choice)
        {
            case (int)User.EXIT: return;
                    break;
            case (int)User.ADD:
                {
                    Product newproduct = new Product(); 
                    Console.WriteLine("Enter ID: ");
                    newproduct.Id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the name of product: ");
                    newproduct.Name = Console.ReadLine();
                    Console.WriteLine("Enter category number: ");
                    int numcategory = Convert.ToInt32(Console.ReadLine());
                    newproduct.Categoryname = numcategory switch
                    {
                        0 => CoffeeShop.COFFE_MACHINES,
                        1 => CoffeeShop.CAPSULES,
                        2 => CoffeeShop.ACCESSORIES,
                        3 => CoffeeShop.FROTHERS,
                        4 => CoffeeShop.SWEETS,
                    };
                    Console.WriteLine("Enter the price: ");
                    newproduct.Price = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter amount: ");
                    newproduct.Instock = Convert.ToInt32(Console.ReadLine());
                    DalProduct.AddProduct(newproduct);
                }
                    break;
            case (int)User.DELETE:
                {
                    Console.WriteLine("Enter ID: ");
                    int productId = Convert.ToInt32(Console.ReadLine());
                    DalProduct.DeleteProduct(productId);
                };
                    break;
            case (int)User.UPDATE:
                {
                    Product newproduct = new Product();
                    Console.WriteLine("Enter ID to updae: ");
                    newproduct.Id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the name of product: ");
                    newproduct.Name = Console.ReadLine();
                    Console.WriteLine("Enter category number: ");
                    int numcategory = Convert.ToInt32(Console.ReadLine());
                    newproduct.Categoryname = numcategory switch
                    {
                        0 => CoffeeShop.COFFE_MACHINES,
                        1 => CoffeeShop.CAPSULES,
                        2 => CoffeeShop.ACCESSORIES,
                        3 => CoffeeShop.FROTHERS,
                        4 => CoffeeShop.SWEETS,
                    };
                    Console.WriteLine("Enter the price: ");
                    newproduct.Price = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter amount: ");
                    newproduct.Instock = Convert.ToInt32(Console.ReadLine());
                    DalProduct.UpdateProduct(newproduct);
                }
                    break;
            case (int)User.GET:
                {
                    Console.WriteLine("Enter ID: ");
                    int productId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(DalProduct.GetProduct(productId));
                }
                    break;
            case (int)User.PRINTALL:
                {
                    Product[] newarray = DalProduct.ProductList();
                    for(int i = 0; i < newarray.Length; i++)
                    {
                        Console.WriteLine(newarray[i]);
                    }
                }
                    break;
        };
    }
    static void Main(string[] args)
    {
        Console.WriteLine("working");
    }
}
   
  
   



