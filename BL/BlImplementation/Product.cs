using BlApi;
using BO;
using Google.Api.Ads.AdWords.v201809;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation
{
    /// <summary>
    /// 
    /// </summary>
    internal class Product : IProduct
    {
        private DalApi.IDal Dal = new DO.DalList();
        /// <summary>
        /// A function that converts a list of products from the data 
        /// layer to a list of products of the logical layer
        /// </summary>
        /// <returns></returns>
        public List<ProductForList> GetList()
        {
            List<ProductForList> newList = new();
            foreach (DO.Product item in Dal.Product.List())
            {
                ProductForList productForList = new ProductForList();
                productForList.ID = item.Id;
                productForList.Name = item.Name;
                productForList.Price = item.Price;
                productForList.Category = (BO.CoffeeShop)item.Categoryname;
                newList.Add(productForList);
            }
            return newList;
        }
        /// <summary>
        /// A function that returns a product by ID number
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"> throw an Exception when the product was not found in the database</exception>
        /// <exception cref="IdNotExsitException"> throw an Exception when the id is invalid</exception>
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
                    throw new BO.EntityNotFoundException(ex);
                }
            };
            throw new BO.IdNotExsitException("the id is not valid");
        }
        /// <summary>
        ///  A function that returns a product by ID number and checks whether and how much is in the cart
        /// </summary>
        /// <param name="id">product id</param>
        /// <param name="cart"> costumer cart</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"> throw an Exception when the product was not found in the database</exception>
        /// <exception cref="IdNotExsitException"> throw an Exception when the id is invalid</exception>
        public ProductItem GetData(int id, BO.Cart cart)
        {
            try
            {
                if (id > 0)
                {
                    DO.Product product = Dal.Product.Get(id);
                    ProductItem newProductItem = new()
                    {
                        ID = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        InStock = product.Instock > 0,
                        Category = (BO.CoffeeShop)product.Categoryname!
                    };
                    if (cart.Items is not null)
                    {
                        OrderItem orderItem = cart.Items.Find(orderItem => orderItem.ProductID == id)!;
                        if (orderItem is not null)
                        {
                            newProductItem.Amount = orderItem.Amount;
                        } 
                    }
                    return newProductItem;
                }
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new BO.EntityNotFoundException(ex);
            }
            throw new IdNotExsitException("the id is negtive");
        }
        /// <summary>
        /// A function to add a product to the database
        /// </summary>
        /// <param name="product">A logical entity of a product</param>
        /// <exception cref="EntityDetailsWrongException">Incorrect product details</exception>
        /// <exception cref="AllreadyExistException">Product id already exists</exception>
        public void Add(BO.Product product)
        {   
            if (product.ID >= 100000 && product.ID < 1000000 && product.Name is not null && product.Price > 0 && product.InStock > 0)
            {
                DO.Product newProduct = new()
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Instock = product.InStock,
                    Categoryname = (DO.CoffeeShop)product.Category
                };
                try
                {
                    Dal.Product.Add(newProduct);
                }
                catch(DO.AllreadyExistException ex)
                {
                    throw new BO.AllreadyExistException(ex);
                }
                catch (DO.EntityNotFoundException ex)
                {
                    throw new BO.EntityNotFoundException(ex);
                }
            }
            else
                throw new EntityDetailsWrongException("The product data is incorrect");
        }
        /// <summary>
        ///  A function to deleting a product from the database
        /// </summary>
        /// <param name="id">product id</param>
        /// <exception cref="EntityNotFoundException"> throw an Exception when the product was not found in the database</exception>
        /// <exception cref="ProductIsOnOrderException">It is not possible to delete a product that exists in one of the orders</exception>
        public void Delete(int id)
        {
            bool exsit = Dal.OrderItem.List().Any(x => x!.Value.ProductID == id);
            if (!exsit)
                try
                {
                    Dal.Product.Delete(id);
                }
                 catch (DO.EntityNotFoundException ex)
                {
                    throw new BO.EntityNotFoundException(ex);
                }
            else
                throw new ProductIsOnOrderException("product exsit in order");
        }
        /// <summary>
        /// Function to update product details
        /// </summary>
        /// <param name="product">Receives a logic layer product</param>
        /// <exception cref="EntityNotFoundException"> throw an Exception when the product was not found in the database</exception>
        /// <exception cref="AllreadyExistException">Product id already exists</exception>
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
                try
                {
                    Dal.Product.Update(newProduct);
                }
                catch (DO.EntityNotFoundException ex)
                {
                    throw new BO.EntityNotFoundException(ex);
                }
                catch (DO.AllreadyExistException ex)
                {
                    throw new BO.AllreadyExistException(ex);
                }
            }
            else
                throw new BO.EntityDetailsWrongException("product not exsit");
        }
    }
}

