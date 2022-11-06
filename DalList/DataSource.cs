using DO;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using static DO.Enums;

namespace Dal;
internal static class DataSource
{
    static public readonly Random RandomNumber = new Random(DateTime.Now.Millisecond); ///initialization random object.

    internal static Product[] Products = new Product[50];        ///initialization three arrays for the three entities.
    internal static Order[] Orders = new Order[100];
    internal static OrderItem[] OrderItems = new OrderItem[200];

    private static void s_Initialize() ///function call to any function that holding the data of the entities.
    {
        AddProductToStore();
        AddOrderToStore();
        AddOrderItemsToStore();
    }
    static DataSource() ///call of the default constructor we have inn the class.
    {
        s_Initialize();
    }

    static void AddProductToStore() ///put products in the store.
    {
        ///arrays for all the product ant array to one category etc.
        string[] CoffeMachines = new string[] { "PIXIE", "CITIZE", "ESSENZA", "ESSENZA_PLUSE", "ATELIER" };
        string[] Capsules = new string[] { "ARPEGGIO", "ROMA", "VOLLUTO", "RISTRETO", "SCURO" };
        string[] Accessories = new string[] { "COFFE_MUG", "VARSILO", "NOMAD_SMALL", "NOMAD_LARGE", "SHAKER" };
        string[] Forthers = new string[] { "BARISTA", "AROCHINO", "AROCHINO_2", "AROCHINO_3", "AROCHINO_4" };
        string[] Sweets = new string[] { "AMARETTI_COOKIES", "MILK_CHOOCOLATE", "MINI_COOKIES", "ORANGE_COOKIES", "DARK_CHOOCOLATE" };

        int FivePrecentProduct = (int)(25 * 0.05);///calculation the five precent of products. 
        int CounterIdProducts = 100000; /// the first run number for the id product.

        for (int i = 0; i < 5; i++)///run on the five category
        {
            Product NewProduct = new Product();

            NewProduct.Categoryname = (CoffeeShop)i; ///run on the enum of the five category.

            for (int j = 0; j < 5; j++)              /// run on the 5 product from one category.
            {
                NewProduct.Id = CounterIdProducts++; ///the next run number for the id.

            ///if the five precent number until bigger from zero that mean that not all the five precent
            ///is allready zero in the stock so keep put zero in the stock of the product for now but if 
            ///is allready zero so lest start to put number of stock to the products. 
                NewProduct.Instock = FivePrecentProduct > 0 ? 0 : RandomNumber.Next(20, 50); 

                NewProduct.Name = NewProduct.Categoryname switch
                {
                    ///put the product name in the order of the category. 
                    CoffeeShop.COFFE_MACHINES => CoffeMachines[j],
                    CoffeeShop.CAPSULES => Capsules[j],
                    CoffeeShop.ACCESSORIES => Accessories[j],
                    CoffeeShop.FROTHERS => Forthers[j],
                    CoffeeShop.SWEETS => Sweets[j],
                    _ => throw new ArgumentNullException("You didnt send right name")
                };
                NewProduct.Price = NewProduct.Categoryname switch
                {
                    ///put the product price in the order of the category. 
                    CoffeeShop.COFFE_MACHINES => RandomNumber.Next(500, 1500),
                    CoffeeShop.CAPSULES => RandomNumber.Next(30, 70),
                    CoffeeShop.ACCESSORIES => RandomNumber.Next(70, 90),
                    CoffeeShop.FROTHERS => RandomNumber.Next(60, 80),
                    CoffeeShop.SWEETS => RandomNumber.Next(20, 30),
                    _ => throw new ArgumentNullException("You didnt send right name")
                };

                FivePrecentProduct--;///the five precent from product reduce one after we make one product to put in the store. 
                Products[NextProduct++] = NewProduct; ///put the new product in the store.
            }
        }
    }   
    static void AddOrderToStore()  ///put orders in the store.
    {
        ///arrays for all the names and emails and address of the people are make order.
        string[] costomername = new string[]
        { "Laurent Conklin", "Ariana Mohring", "Ilsa Humphrey", "Breanne Bursnell", "Alexandros Popping",
          "Orel Gerritsma", "Desiri Spreull", "Lewiss Duffer", "Frederique Crow", "Selena Forster",
          "Eolanda Wadmore", "Leah Newis", "Haze Shurmore", "Doralin Bamsey", "Dion Massy", "Kristian Alty",
          "Marshal McGlone", "Clarette Sharrocks", "Rubia Dundridge", "Stuart Caskey", "Jeramie Kassel", "Sileas Schruurs",
          "Pren Blunsden", "Junette Tipling", "Cart Senten", "Paddie Dobkin", "Crissy Lightbody",
          "Dillon Hulles", "Ramsey Klaes", "Ario Di Nisco", "Amelita Chase", "Mortie Pache", "Emelia Juara", "Keefe Goalby",
          "Caitlin Giovani", "Candi Beardshaw", "Denver Mosco", "Estel Habens","L;urette Heisham", "Birgitta Kearns",
        };
        string[] costomeraddress = new string[]
        { "1 Farwell Road", "55098 Pleasure Trail","710 Armistice Drive","49917 Marcy Pass","98242 Fairfield Trail",
          "65798 Brown Court", "56 Old Gate Center", "68368 Dryden Plaza", "8 Moland Circle", "50 Glacier Hill Circle",
          "51267 Commercial Street", "7 Fallview Street", "737 Warbler Hill", "441 Delladonna Terrace","1 Crownhardt Pass",
          "36 Shelley Terrace","95615 Ilene Circle","42 Donald Parkway","084 West Road", "50 Oakridge Street",
          "0 Kipling Pass","28 Sundown Crossing","12422 Anderson Plaza","139 Gateway Parkway","99020 Warrior Place",
          "90481 Northfield Pass","41 Kenwood Avenue","7 Pawling Center","4 Derek Plaza","44514 Rieder Crossing",
          "3 Chive Way","4 Sachtjen Trail", "03 Ridge Oak Junction","45 Shelley Junction","98 Calypso Plaza","16826 Vera Street",
          "7446 Ludington Point","539 Erie Road","7277 Di Loreto Circle","6 Service Park" 
        };
        string[] costomeremail = new string[] 
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
            DateTime RandomTime = new DateTime(DateTime.Now.Year, RandomNumber.Next(1, DateTime.Now.Month),
            RandomNumber.Next(1, DateTime.Now.Day), RandomNumber.Next(1, 13), RandomNumber.Next(1, 61), RandomNumber.Next(1, 61));
            RandomTime.AddMonths(-1); ///just to not make a problem with future time.

            Order newOrder = new Order(); 

            newOrder.Id = GetOrder; ///id for the orders with the run number that we have in the function config.
            newOrder.CustomerName = costomername[RandomNumber.Next(0, 40)];
            newOrder.CustomerAdress = costomeraddress[RandomNumber.Next(0, 40)];
            newOrder.CustomerEmail = costomeremail[RandomNumber.Next(0, 40)];
            newOrder.OrderDate = RandomTime;
            if (i < 16)
                ///random number just we make him to be one ot two days after the random date we have.
                newOrder.ShipDate = newOrder.OrderDate.Add(new TimeSpan(RandomNumber.Next(2), 0, 0, 0)); 
            else
                newOrder.ShipDate = DateTime.MinValue;
            if(i < 12)
                ///random number just we make him to be one ot two days after the random date we have.
                newOrder.DeliveryrDate = newOrder.ShipDate.Add(new TimeSpan(RandomNumber.Next(2), 0, 0, 0));
            else
                newOrder.DeliveryrDate = DateTime.MinValue;
            Orders[NextOrder++] = newOrder; ///put the new order in the store.
        }
    }
    static void AddOrderItemsToStore() ///put orderitems in the store.
    {
        for (int i = 0; i < 20; i++)  ///until 40 item product (minimum amount of item for one order is two).
        {
            int RandomProduct = RandomNumber.Next(0, 21); 
            for (int j = 0; j < RandomNumber.Next(2,5); j++) //ranum amount of product between 2 and 5. 
            {
                OrderItem newOrderItem = new OrderItem();

                newOrderItem.Id = GetOrderItem; //the run number that we have to the orderitem we put in the id of the order item. 
                newOrderItem.ProductID = Products[RandomProduct + j].Id; //put id product from the array product in the id.
                newOrderItem.OredrID = Orders[i].Id; //put id order from the array order in the id.
                newOrderItem.Price = Products[RandomProduct + j].Price; //put price to the itemorder from the array product in the price.
                newOrderItem.Amount = RandomNumber.Next(1, 7); //we can order from one product just between 1 and 6.
                OrderItems[NextOrderItem++] = newOrderItem; //up the run number for the order item.
            }
        }
    }
    // we make class config for all the run number we have here in the data source. 
    //internal static class Config 
    //{
    internal static int NextOrder = 0;
        internal static int NextOrderItem = 0;
        internal static int NextProduct = 0;

        //run number that start from number with 6 digits for the id number.
        private static int IdOrder = 100000;
        internal static int GetOrder => IdOrder++; 
        private static int IdOrderItem = 100000;
        internal static int GetOrderItem => IdOrderItem++;
    //}
}
