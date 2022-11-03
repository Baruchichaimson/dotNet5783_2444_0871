using DO;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using static DO.Enums;

namespace Dal;
internal static class DataSource
{
    static public readonly Random RandomNumber = new Random(DateTime.Now.Millisecond);
    internal static Product[] Products = new Product[50];
    internal static Order[] Orders = new Order[100];
    internal static OrderItem[] OrderItems = new OrderItem[200];

    private static void s_Initialize()
    {
        AddProductToStore();
        AddOrderToStore();
        AddOrderItemsToStore();
    }
    static DataSource()
    {
        s_Initialize();
    }
   
    static void AddProductToStore()
    {
        string[] CoffeMachines = new string[] { "PIXIE", "CITIZE", "ESSENZA", "ESSENZA_PLUSE" , "ATELIER"};
        string[] Capsules = new string[] { "ARPEGGIO", "ROMA", "VOLLUTO", "RISTRETO", "SCURO" };
        string[] Accessories = new string[] { "COFFE_MUG", "VARSILO", "NOMAD_SMALL", "NOMAD_LARGE", "SHAKER" };
        string[] Forthers = new string[] { "BARISTA", "AROCHINO", "AROCHINO_2", "AROCHINO_3" , "AROCHINO_4" };
        string[] Sweets = new string[] { "AMARETTI_COOKIES", "MILK_CHOOCOLATE", "MINI_COOKIES", "ORANGE_COOKIES", "DARK_CHOOCOLATE"};

        int FivePrecentProduct = (int)(25 * 0.05);
        int CounterIdProducts = 100000;
 
        for (int i = 0; i < 5; i++)
        {
            Product NewProduct = new Product();
            NewProduct.Categoryname = (CoffeeShop)i;
            for(int j = 0; j < 5; j++)
            { 
                NewProduct.Id = CounterIdProducts++;
                NewProduct.Instock = FivePrecentProduct > 0 ? 0 : RandomNumber.Next(20, 50);
                NewProduct.Name = NewProduct.Categoryname switch
                {
                  CoffeeShop.COFFE_MACHINES => CoffeMachines[j],
                  CoffeeShop.CAPSULES => Capsules[j],
                  CoffeeShop.ACCESSORIES => Accessories[j],
                  CoffeeShop.FROTHERS => Forthers[j],
                  CoffeeShop.SWEETS => Sweets[j],
                _ => throw new ArgumentNullException("You didnt send right name")
                };
            NewProduct.Price = NewProduct.Categoryname switch
            {
                CoffeeShop.COFFE_MACHINES => RandomNumber.Next(500, 1500),
                CoffeeShop.CAPSULES => RandomNumber.Next(30, 70),
                CoffeeShop.ACCESSORIES => RandomNumber.Next(70, 90),
                CoffeeShop.FROTHERS => RandomNumber.Next(60, 80),
                CoffeeShop.SWEETS => RandomNumber.Next(20, 30),
                _ => throw new ArgumentNullException("You didnt send right name")
            };

            FivePrecentProduct--;
            Products[Config.NextProduct++] = NewProduct;
        }
    }
    static void AddOrderToStore()
    {
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

        for (int i = 0; i < 20; i++)
        {
            DateTime RandomTime = new DateTime(DateTime.Now.Year, RandomNumber.Next(1, DateTime.Now.Month),
            RandomNumber.Next(1, DateTime.Now.Day), RandomNumber.Next(1, 13), RandomNumber.Next(1, 61), RandomNumber.Next(1, 61));
            RandomTime.AddMonths(-1);

            Order newOrder = new Order();

            newOrder.Id = Config.GetOrder;
            newOrder.CustomerName = costomername[RandomNumber.Next(0, 40)];
            newOrder.CustomerAdress = costomeraddress[RandomNumber.Next(0, 40)];
            newOrder.CustomerEmail = costomeremail[RandomNumber.Next(0, 40)];
            newOrder.OrderDate = RandomTime;
            if (i < 16)
                newOrder.ShipDate = newOrder.OrderDate.Add(new TimeSpan(RandomNumber.Next(2), 0, 0, 0));
            else
                newOrder.ShipDate = DateTime.MinValue;
            if(i < 12)
                newOrder.DeliveryrDate = newOrder.ShipDate.Add(new TimeSpan(RandomNumber.Next(2), 0, 0, 0));
            else
                newOrder.DeliveryrDate = DateTime.MinValue;
            Orders[Config.NextOrder++] = newOrder;
        }
    }
    static void AddOrderItemsToStore()
    {
        for (int i = 0; i < 20; i++)
        {
            int RandomProduct = RandomNumber.Next(0, 10);
            for (int j = 0; j < RandomNumber.Next(2,5); j++)
            {
                OrderItem newOrderItem = new OrderItem();

                newOrderItem.Id = Config.GetOrderItem;
                newOrderItem.ProductID = Products[RandomProduct + j].Id;
                newOrderItem.OredrID = Orders[i].Id;
                newOrderItem.Price = Products[RandomProduct + j].Price;
                newOrderItem.Amount = RandomNumber.Next(1, 7);
                OrderItems[Config.NextOrderItem++] = newOrderItem;
            }
        }
    }
    internal static class Config
    {
        internal static int NextOrder = 0;
        internal static int NextOrderItem = 0;
        internal static int NextProduct = 0;

        private static int IdOrder = 100000;
        internal static int GetOrder => IdOrder++;
        private static int IdOrderItem = 100000;
        internal static int GetOrderItem => IdOrderItem++;
    }
}
