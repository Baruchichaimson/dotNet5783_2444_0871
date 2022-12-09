using DO;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
namespace Dal;
internal static class DataSource
{
    ///<summary>
    /// initialization random object.
    /// </summary>
    static public readonly Random randomNumber = new Random(DateTime.Now.Millisecond);

    ///<summary>
    /// initialization three list for the three entities.
    /// </summary>
    internal static List<Product?> Products = new List<Product?>();       
    internal static List<Order?> Orders = new List<Order?>();
    internal static List<OrderItem?> OrderItems = new List<OrderItem?>();

    ///<summary>
    /// function call to any function that holding the data of the entities.
    /// </summary>
    private static void S_Initialize()
    {
        AddProductToStore();
        AddOrderToStore();
        AddOrderItemsToStore();
    }
    ///<summary>
    /// call of the default constructor we have inn the class.
    /// </summary>
    static DataSource() 
    {
        S_Initialize();
    }
    /// <summary>
    /// if the id number that was chooce is allready 
    /// </summary>
    /// <param name="id"> the number that chooce.</param>
    /// <returns> in the database return fallse and if not return true.</returns>
    private static bool check(int id)
    {
        foreach (var item in Products)
        {
            if (item?.Id == id)
            {
                return true;
            };
        };
      return false;
    }
    ///<summary>
    /// put products in the store.
    /// </summary>
    static void AddProductToStore() 
    {
        ///arrays for all the product ant array to one category etc.
        string[] CoffeMachines = new string[] { "PIXIE", "CITIZE", "ESSENZA", "ESSENZA_PLUSE", "ATELIER" };
        string[] Capsules = new string[] { "ARPEGGIO", "ROMA", "VOLLUTO", "RISTRETO", "SCURO" };
        string[] Accessories = new string[] { "COFFE_MUG", "VARSILO", "NOMAD_SMALL", "NOMAD_LARGE", "SHAKER" };
        string[] Forthers = new string[] { "BARISTA", "AROCHINO", "AROCHINO_2", "AROCHINO_3", "AROCHINO_4" };
        string[] Sweets = new string[] { "AMARETTI_COOKIES", "MILK_CHOOCOLATE", "MINI_COOKIES", "ORANGE_COOKIES", "DARK_CHOOCOLATE" };

        int fivePrecentProduct = (int)(25 * 0.05);///calculation the five precent of products. 

        for (int i = 0; i < 5; i++) ///run on the five category
        {
            Product newProduct = new Product();

            newProduct.Categoryname = (CoffeeShop)i; ///run on the enum of the five category.

            for (int j = 0; j < 5; j++)              /// run on the 5 product from one category.
            {
                int randomIdProducts = randomNumber.Next(100000, 1000000);
                while(check(randomIdProducts)) ///if the number is already in the database, then draw a new number.
                {
                    randomIdProducts = randomNumber.Next(100000, 1000000);
                }
                newProduct.Id = randomIdProducts;   ///put the number for the id.

                ///if the five precent number until bigger from zero that mean that not all the five precent
                ///is allready zero in the stock so keep put zero in the stock of the product for now but if 
                ///is allready zero so lest start to put number of stock to the products. 
                newProduct.Instock = fivePrecentProduct > 0 ? 0 : randomNumber.Next(20, 50);

                newProduct.Name = newProduct.Categoryname switch
                {
                    ///put the product name in the order of the category. 
                    CoffeeShop.COFFE_MACHINES => CoffeMachines[j],
                    CoffeeShop.CAPSULES => Capsules[j],
                    CoffeeShop.ACCESSORIES => Accessories[j],
                    CoffeeShop.FROTHERS => Forthers[j],
                    CoffeeShop.SWEETS => Sweets[j],
                };
                newProduct.Price = randomNumber.Next(100, 300);

                fivePrecentProduct--;///the five precent from product reduce one after we make one product to put in the store. 
                Products.Add(newProduct); ///put the new product in the store.
            }
        }
    }
    ///<summary>
    /// put orders in the store.
    /// </summary>
    static void AddOrderToStore()  
    {
        ///arrays for all the names and emails
        /// and address of the people are make order.
        string[] costomerName = new string[]
        { "Laurent Conklin", "Ariana Mohring", "Ilsa Humphrey", "Breanne Bursnell", "Alexandros Popping",
          "Orel Gerritsma", "Desiri Spreull", "Lewiss Duffer", "Frederique Crow", "Selena Forster",
          "Eolanda Wadmore", "Leah Newis", "Haze Shurmore", "Doralin Bamsey", "Dion Massy", "Kristian Alty",
          "Marshal McGlone", "Clarette Sharrocks", "Rubia Dundridge", "Stuart Caskey", "Jeramie Kassel", "Sileas Schruurs",
          "Pren Blunsden", "Junette Tipling", "Cart Senten", "Paddie Dobkin", "Crissy Lightbody",
          "Dillon Hulles", "Ramsey Klaes", "Ario Di Nisco", "Amelita Chase", "Mortie Pache", "Emelia Juara", "Keefe Goalby",
          "Caitlin Giovani", "Candi Beardshaw", "Denver Mosco", "Estel Habens","L;urette Heisham", "Birgitta Kearns",
        };
        string[] costomerAddress = new string[]
        { "1 Farwell Road", "55098 Pleasure Trail","710 Armistice Drive","49917 Marcy Pass","98242 Fairfield Trail",
          "65798 Brown Court", "56 Old Gate Center", "68368 Dryden Plaza", "8 Moland Circle", "50 Glacier Hill Circle",
          "51267 Commercial Street", "7 Fallview Street", "737 Warbler Hill", "441 Delladonna Terrace","1 Crownhardt Pass",
          "36 Shelley Terrace","95615 Ilene Circle","42 Donald Parkway","084 West Road", "50 Oakridge Street",
          "0 Kipling Pass","28 Sundown Crossing","12422 Anderson Plaza","139 Gateway Parkway","99020 Warrior Place",
          "90481 Northfield Pass","41 Kenwood Avenue","7 Pawling Center","4 Derek Plaza","44514 Rieder Crossing",
          "3 Chive Way","4 Sachtjen Trail", "03 Ridge Oak Junction","45 Shelley Junction","98 Calypso Plaza","16826 Vera Street",
          "7446 Ludington Point","539 Erie Road","7277 Di Loreto Circle","6 Service Park"
        };
        string[] costomEremail = new string[]
        {
          "vbrownett1@whitehouse.gov","remanueli2@harvard.edu","rcroley3@ycombinator.com","jfrean4@earthlink.net","fwhewell5@nature.com",
          "mbuffery6@tamu.edu","lnorwood7@i2i.jp","pkiernan8@github.com","mplaster9@scientificamerican.com","bdedney0@google.pl",
          "lfillera@pcworld.com","bheaneyb@cafepress.com","aquilkinc@npr.org","awildishd@timesonline.co.uk","rcarlete@sphinn.com",
          "isansung@quantcast.com","kmerielh@fda.gov","echewtert@ycombinator.com","dmeekinsu@bbb.org","cdecourtney10@simplemachines.org",
          "maickini@unesco.org","ciddisonj@rakuten.co.jp","rsawbridgek@archive.org","ffassl@google.co.uk","medgesonm@wikispaces.com",
          "jdulintyn@soup.io","fmintoffo@reuters.com","mfessionsp@goo.ne.jp","grussq@independent.co.uk","ftrautr@hp.com","egolts@w3.org",
          "lcopsonv@multiply.com","bdolanw@scribd.com","bchaffx@cisco.com","cfoxcrofty@twitter.com","lsimsz@woothemes.com",
          "bkrolman11@weather.com","eboice12@simplemachines.org","nlingner13@whitehouse.gov","dcochranef@sbwire.com"
        };

        for (int i = 0; i < 20; i++) ///until 20 orders.
        {
            ///make new object for the random time for the date of the order we need.
            DateTime randomTime = new DateTime(DateTime.Now.Year, randomNumber.Next(1, DateTime.Now.Month),
            randomNumber.Next(1, DateTime.Now.Day), randomNumber.Next(0, 24), randomNumber.Next(0, 60), randomNumber.Next(0, 60));
            randomTime.AddMonths(-1); ///just to not make a problem with future time.

            Order newOrder = new Order();

            newOrder.Id = GetOrder; ///id for the orders with the run number that we have in the function config.
            newOrder.CustomerName = costomerName[randomNumber.Next(0, 40)];
            newOrder.CustomerAdress = costomerAddress[randomNumber.Next(0, 40)];
            newOrder.CustomerEmail = costomEremail[randomNumber.Next(0, 40)];
            newOrder.OrderDate = randomTime;
            if (i < 16)
                ///random number just we make him to be one ot two days after the random date we have.
                newOrder.ShipDate = newOrder.OrderDate?.Add(new TimeSpan(randomNumber.Next(1, 3), 0, 0, 0));
            else
                newOrder.ShipDate = null;
            if (i < 12)
                ///random number just we make him to be one ot two days after the random date we have.
                newOrder.DeliveryrDate = newOrder.ShipDate?.Add(new TimeSpan(randomNumber.Next(4, 6), 0, 0, 0));
            else
                newOrder.DeliveryrDate = null;
            Orders.Add(newOrder); ///put the new order in the store.
        }
    }
    ///<summary>
    /// put orderitems in the store.
    /// </summary>
    static void AddOrderItemsToStore()
    {
        for (int i = 0; i < 20; i++)  ///until 40 item product (minimum amount of item for one order is two).
        {
            int randomProduct = randomNumber.Next(0, 21);
            for (int j = 0; j < randomNumber.Next(2, 5); j++) ///ranum amount of product between 2 and 5. 
            {
                OrderItem newOrderItem = new OrderItem();

                newOrderItem.Id = GetOrderItem; ///the run number that we have to the orderitem we put in the id of the order item. 
                newOrderItem.ProductID = Products[randomProduct + j]?.Id ?? 0; ///put id product from the array product in the id.
                newOrderItem.OredrID = Orders[i]?.Id ?? -1; ///put id order from the array order in the id.
                newOrderItem.Price = Products[randomProduct + j]?.Price?? 0; ///put price to the itemorder from the array product in the price.
                newOrderItem.Amount = randomNumber.Next(1, 7); ///we can order from one product just between 1 and 6.
                OrderItems.Add(newOrderItem); ///up the run number for the order item.
            }
        }
    }
   
    ///run number that start from number with 6 digits for the id number.
    private static int IdOrder = 1;
    internal static int GetOrder => IdOrder++;
    private static int IdOrderItem = 1;
    internal static int GetOrderItem => IdOrderItem++;
}
