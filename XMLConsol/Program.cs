using DalApi;
using DO;


namespace Dal;

internal class Program
{
    private static readonly IDal dallist = Factory.Get()!;
    const string s_orderItem = @"OrderItems";
    const string s_product = @"Products";
    const string s_order = @"Orders";

    private static void LoadOrders()
    {
        List<Order?> orders = dallist.Order.List()!.ToList();
        XMLTools.SaveListToXMLSerializer(orders, s_order);
    }
    private static void LoadOrderItems()
    {
        List<OrderItem?> orderItems = dallist.OrderItem.List()!.ToList();
        XMLTools.SaveListToXMLSerializer(orderItems, s_orderItem);

    }
    private static void LoadProducts()
    {
        List<Product?> products = dallist.Product.List()!.ToList();
        XMLTools.SaveListToXMLSerializer(products, s_product);

    }
    static void Main()
    {
        while (true) ///ask the user until he will put zero.
        {
            int userChoice;
            do
                Console.WriteLine("Press your choice \nexit press: 0 \nfor load xml products press: 1 \nfor load xml orders press: 2 \nfor load xml order items press: 3");
            while (int.TryParse(Console.ReadLine(), out userChoice)); ///the user must select which entity wants to perform actions
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
        }
    }
}