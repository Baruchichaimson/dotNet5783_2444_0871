using DO;
using static DO.Enums;

namespace Dal;
internal static class DataSource
{
    static public readonly Random RandomNumber = new Random();
    static Product[] ArrProducts = new Product[50];
    static Order[] ArrOrders = new Order[100];
    static OrderItem[] ArrOrderItems = new OrderItem[200];
    static void s_Initialize()
    {
        AddProductToStore();
        AddOrderToStore();
        AddOrderItemsToStore();
    }
    static void AddProductToStore()
    {
        string[] CoffeMachines = new string[] { "PIXIE", "CITIZE", "ESSENZA", "ESSENZA_PLUSE" };
        string[] Capsules = new string[] { "ARPEGGIO", "ROMA", "VOLLUTO" };
        string[] Accessories = new string[] { "COFFE_MUG", "VARSILO" };
        string[] Forthers = new string[] { "BARISTA", "AROCHINO" };
        string[] Sweets = new string[] { "AMARETTI_COOKIES", "MILK_CHOOCOLATE" };
        int numberofproducts = 13;
        int fiveprecent = (int)(numberofproducts * 0.05);
        for (int i = 0; i < numberofproducts; i++)
        {

            Product newproduct = new Product();

            newproduct.Id = RandomNumber.Next(100000, 999999);
            newproduct.Name = newproduct.Categoryname switch
            {
                CoffeeShop.COFFE_MACHINES => CoffeMachines[RandomNumber.Next(1, 4)],
                CoffeeShop.CAPSULES => Capsules[RandomNumber.Next(1, 3)],
                CoffeeShop.ACCESSORIES => Accessories[RandomNumber.Next(1, 2)],
                CoffeeShop.FROTHERS => Forthers[RandomNumber.Next(1, 2)],
                CoffeeShop.SWEETS => Sweets[RandomNumber.Next(1, 2)],
                _ => throw new ArgumentNullException("You didnt send right name")

            };
            newproduct.Categoryname = (CoffeeShop)RandomNumber.Next(1, 4);
            newproduct.Price = newproduct.Categoryname switch
            {
                CoffeeShop.COFFE_MACHINES => RandomNumber.Next(500, 1500),
                CoffeeShop.CAPSULES => RandomNumber.Next(30, 70),
                CoffeeShop.ACCESSORIES => RandomNumber.Next(70, 90),
                CoffeeShop.FROTHERS => RandomNumber.Next(60, 80),
                CoffeeShop.SWEETS => RandomNumber.Next(20, 30),
                _ => throw new ArgumentNullException("You didnt send right name")
            };
            newproduct.Instock = fiveprecent > 0 ? 0 : RandomNumber.Next(20, 50);
            fiveprecent--;
        }
    }
    static void AddOrderToStore()
    {

    }
    static void AddOrderItemsToStore()
    {

    }
}
