using DalApi;
using BlImplementation;
using Google.Api.Ads.AdWords.v201809;
using BlApi;
using BO;
using System.Threading.Channels;


namespace BlTest;
/// <summary>
/// program test
/// </summary>
internal class Program
{
    /// <summary>
    /// An arttitube of the interface that contains all the logical entities
    /// </summary>
    private static IBl bl = new Bl();
    /// <summary>
    /// cart for user
    /// </summary>
    private static Cart cart = new Cart();
    /// <summary>
    /// to check if the number is correct
    /// </summary>
    /// <returns></returns>
    private static int tryParseInt()
    {
        int num;
        while (!int.TryParse(Console.ReadLine(), out num))
        {
            Console.WriteLine("enter a number again");
        }
        return num;
    }
    /// <summary>
    /// The function allows actions on the products that exist in the store.
    /// </summary>
    private static void ProductOptions()
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nProduct list request press: 1 \nProduct details request for admin press: 2");
            Console.WriteLine("Product details request for customer press: 3 \nAdding a product press: 4 \nProduct deletion press: 5 \nUpdate product data press: 6");
            int userChoice = tryParseInt();
            switch (userChoice)
            {
                case (int)UserProduct.EXIT:
                    return;
                case (int)UserProduct.LIST_REQUEST:
                    {
                        IEnumerable<ProductForList> list = bl.Product.GetList();
                        foreach (ProductForList item in list)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case (int)UserProduct.DETAILS_REQUEST_ADMIN:
                    {
                        Console.WriteLine("enter product id:");
                        Console.WriteLine(bl.Product.GetData(tryParseInt()));
                        break;
                    }
                case (int)UserProduct.DETAILS_REQUEST_CUSTOMER:
                    {
                        Console.WriteLine("enter product id:");
                        Console.WriteLine(bl.Product.GetData(tryParseInt(), cart));
                        break;
                    }
                case (int)UserProduct.ADDING_PRODUCT:
                    {
                        Product newProduct = new();
                        Console.WriteLine("enter product id:");
                        int id = tryParseInt();
                        newProduct.ID = id;
                        Console.WriteLine("Enter the name of product:");
                        newProduct.Name = Console.ReadLine();
                        Console.WriteLine("Enter category number:");
                        int numCategory;
                        do
                        {
                            Console.WriteLine("for COFFE_MACHINES press  0 \nfor CAPSULES press 1 \nfor ACCESSORIES press 2 \nfor FROTHERS press 3 \nfor SWEETS press 4");
                            numCategory = tryParseInt();
                        } while (numCategory < 0 || numCategory > 4);
                        newProduct.Category = numCategory switch
                        {
                            0 => CoffeeShop.COFFE_MACHINES,
                            1 => CoffeeShop.CAPSULES,
                            2 => CoffeeShop.ACCESSORIES,
                            3 => CoffeeShop.FROTHERS,
                            4 => CoffeeShop.SWEETS,
                        };
                        Console.WriteLine("Enter the price:");
                        double price = tryParseInt();
                        newProduct.Price = price;
                        Console.WriteLine("Enter amount:");
                        int inStock = tryParseInt();
                        newProduct.InStock = inStock;
                        bl.Product.Add(newProduct);
                        Console.WriteLine("the product has been succsefully added");
                        break;
                    }
                case (int)UserProduct.PRODUCT_DELETION:
                    {
                        Console.WriteLine("enter product id:");
                        bl.Product.Delete(tryParseInt());
                        Console.WriteLine("the product has been succsefully deleted ");
                        break;
                    }
                case (int)UserProduct.UPDATE_DATA:
                    {
                        Product newProduct = new();
                        Console.WriteLine("enter product id:");
                        int id = tryParseInt();
                        newProduct.ID = id;
                        Console.WriteLine("Enter the name of product:");
                        newProduct.Name = Console.ReadLine();
                        Console.WriteLine("Enter category number:");
                        int numCategory;
                        do
                        {
                            Console.WriteLine("for COFFE_MACHINES press  0 \nfor CAPSULES press 1 \nfor ACCESSORIES press 2 \nfor FROTHERS press 3 \nfor SWEETS press 4");
                            numCategory = tryParseInt();
                        } while (numCategory < 0 || numCategory > 4);
                        newProduct.Category = numCategory switch
                        {
                            0 => CoffeeShop.COFFE_MACHINES,
                            1 => CoffeeShop.CAPSULES,
                            2 => CoffeeShop.ACCESSORIES,
                            3 => CoffeeShop.FROTHERS,
                            4 => CoffeeShop.SWEETS,
                        };
                        Console.WriteLine("Enter the price:");
                        double price = tryParseInt();
                        newProduct.Price = price;
                        Console.WriteLine("Enter amount:");
                        int inStock = tryParseInt();
                        newProduct.InStock = inStock;
                        bl.Product.Update(newProduct);
                        Console.WriteLine("the product has been succsefully update");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("not in the option");
                        break;
                    }
            }
        }
    }
    /// <summary>
    ///  The function allows actions on the orders that we going to get.
    /// </summary>
    private static void OrderOptions()
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nExit press: 0 \nOrder list request press: 1 \nUpdate order details for admin press: 2");
            Console.WriteLine("Order details request press: 3 \nUpdate shipping order press: 4 \nUpdate delivery order press: 5 \nOrder tracking press: 6");
            int userChoice = tryParseInt();
            switch (userChoice)
            {
                case (int)UserOrder.EXIT:
                    return;
                case (int)UserOrder.LIST_REQUEST:
                    {
                        IEnumerable<OrderForList> list = bl.Order.GetList();
                        foreach (OrderForList item in list)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    }
                case (int)UserOrder.ORDER_UPDATE_ADMIN:
                    {
                        Console.WriteLine("enter order id:");
                        int orderId = tryParseInt();
                        Console.WriteLine("enter product id:");
                        int productId = tryParseInt();
                        Console.WriteLine("enter amount");
                        int amount = tryParseInt();
                        bl.Order.UpdateAdmin(orderId, productId, amount);
                        Console.WriteLine("the order has been succsefully update");
                        break;
                    }
                case (int)UserOrder.DETAILS_REQUEST:
                    {
                        Console.WriteLine("enter order id:");
                        Console.WriteLine(bl.Order.GetData(tryParseInt()));
                        break;
                    }
                case (int)UserOrder.UPDATE_SHIPPING:
                    {
                        Console.WriteLine("enter order id:");
                        Console.WriteLine(bl.Order.UpdateShippingDate(tryParseInt()));
                        break;
                    }
                case (int)UserOrder.UPDATE_DELIVERY:
                    {
                        Console.WriteLine("enter order id:");
                        Console.WriteLine(bl.Order.DeliveryUpdate(tryParseInt()));
                        break;
                    }
                case (int)UserOrder.ORDER_TRACKING:
                    {
                        Console.WriteLine("enter order id:");
                        Console.WriteLine(bl.Order.OrderTracking(tryParseInt()));
                        break;
                    }
                default:
                    {
                        Console.WriteLine("not in the option");
                        break;
                    }
            }
        }
    }
    /// <summary>
    ///  The function allows actions on the cart of the customer have.
    /// </summary>
    private static void CartOptions()
    {
        while (true)
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nAdd product press: 1 \nUpdate amount press: 2 \nConfirmation order press: 3 ");
            int userChoice = tryParseInt();
            switch (userChoice)
            {
                case (int)UserCart.EXIT:
                    return;
                case (int)UserCart.ADDING_PRODUCT:
                    {
                        Console.WriteLine("enter product id:");
                        int id = tryParseInt();
                        Console.WriteLine(bl.Cart.AddProduct(cart, id));
                        break;
                    }
                case (int)UserCart.UPDATE_AMOUNT:
                    {
                        Console.WriteLine("enter product id:");
                        int id = tryParseInt();
                        Console.WriteLine("enter new amount:");
                        int newAmount = tryParseInt();
                        Console.WriteLine(bl.Cart.UpdateProductAmount(cart, id, newAmount));
                        break;
                    }
                case (int)UserCart.ORDER_CONFIRMATION:
                    {
                        Console.WriteLine("enter name:");
                        string input = Console.ReadLine();
                        cart.CustomerName = input;
                        Console.WriteLine("enter address:");
                        input = Console.ReadLine();
                        cart.CustomerAddress = input;
                        Console.WriteLine("enter email:");
                        input = Console.ReadLine();
                        cart.CustomerEmail = input;
                        bl.Cart.OrderConfirmation(cart);
                        cart = new();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("not in the option");
                        break;
                    }
            }
        }
    }
    /// <summary>
    /// the main program to test all the part of the logic and the data layers togther.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        while (true) 
        {
            Console.WriteLine("Press your choice \nexit press: 0 \nfor product menu press: 1 \nfor order menu press: 2 \nfor cart menu press: 3");

            int.TryParse(Console.ReadLine(), out int userChoice);
            try
            {
                switch (userChoice)
                {
                    case (int)UserForMainBO.EXIT:
                        return;
                    case (int)UserForMainBO.PRODUCT:
                        ProductOptions();
                        break;
                    case (int)UserForMainBO.ORDER:
                        OrderOptions();
                        break;
                    case (int)UserForMainBO.CART:
                        CartOptions();
                        break;
                }
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            catch (AllreadyExistException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            catch (NotEnoughInStockException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IdNotExsitException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (IncorrectAmountException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (EntityDetailsWrongException ex)
            {
                Console.WriteLine(ex.Message);
            }   
            catch(CartException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ProductIsOnOrderException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
