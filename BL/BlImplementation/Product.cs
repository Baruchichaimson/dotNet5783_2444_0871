using BlApi;
using BO;
using Google.Api.Ads.AdWords.v201809;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation
{
    internal class Product : IProduct
    {
        private DalApi.IDal Dal = new DO.DalList();
        public List<BO.ProductForList> GetList()
        {
            BO.ProductForList productForList = new BO.ProductForList();
            List<BO.ProductForList> newList = new();
            foreach (DO.Product item in Dal.Product.List())
            {
                productForList.ID = item.Id;
                productForList.Name = item.Name;
                productForList.Price = item.Price;
                productForList.Category = (BO.CoffeeShop)item.Categoryname;
                newList.Add(productForList);
            }
            return newList;

        }
        public BO.Product GetData(int id)
        {
            if (id > 0)
            {
                try
                {
                    DO.Product product = Dal.Product.Get(id);
                    BO.Product newProduct = new()
                    {
                        ID = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        InStock = product.Instock,
                        Category = (BO.CoffeeShop)product.Categoryname
                    };
                    return newProduct;
                }
                catch (DO.EntityNotFoundException ex)
                {
                    throw new BO.EntityNotFoundException(ex.Message);
                };
            };
            throw new IdNotExsitException("the id is negtive");
        }
        public BO.ProductItem GetData(int id, BO.Cart cart)
        {
            DO.Product product = Dal.Product.Get(id);
            
            try
            {
                if (id > 0)
                {
                    BO.ProductItem newProductItem = new()
                    {
                        ID = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        InStock = product.Instock > 0,
                        Category = (BO.CoffeeShop)product.Categoryname
                    };
                    BO.OrderItem orderItem = cart.Items.First(orderItem => orderItem.ID == id);

                    if (orderItem is not null)
                    {
                        newProductItem.Amount = orderItem.Amount;
                    }
                    return newProductItem;
                }
                throw new ("the id is negtive");
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new BO.EntityNotFoundException("product", ex);
            }
        }
        public void Add(BO.Product product)
        {
            
            if (product.ID > 0 && product.Name is not null && product.Price > 0 && product.InStock > 0)
            {
                DO.Product newProduct = new()
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Instock = product.InStock,
                    Categoryname = (DO.CoffeeShop)product.Category
                };
                Dal.Product.Add(newProduct);
            }
            else
                throw new Exception("product not exsit");
        }
        public void Delete(int id)
        {
            bool exsit = Dal.OrderItem.List().Any(x => x.ProductID == id);
            if (!exsit)
                Dal.Product.Delete(id);
            else
                throw new Exception("product not exsit");
        }
        public void Update(BO.Product product)
        {
            if (product.ID > 0 && product.Name is not null && product.Price > 0 && product.InStock > 0)
            {
                DO.Product newProduct = new()
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Instock = product.InStock,
                    Categoryname = (DO.CoffeeShop)product.Category
                };
                Dal.Product.Update(newProduct);
            }
            else
                throw new Exception("product not exsit");
        }
    }
}

