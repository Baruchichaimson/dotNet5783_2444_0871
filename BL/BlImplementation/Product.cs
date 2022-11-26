using BlApi;
using BO;
using Google.Api.Ads.AdWords.v201809;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation
{
    internal class Product : IProduct
    {
        private DalApi.IDal Dal = new DO.DalList();
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
        /// <exception cref="IdNotExsitException"></exception>
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
                    throw new EntityNotFoundException(ex.Message);
                }
                catch (DO.AllreadyExistException ex)
                {
                    throw new AllreadyExistException(ex.Message);
                }
            };
            throw new IdNotExsitException("the id is not valid");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
        /// <exception cref="IdNotExsitException"></exception>
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
                        Category = (BO.CoffeeShop)product.Categoryname
                    };

                    OrderItem orderItem = cart.Items.Find(orderItem => orderItem.ProductID == id);
                    if (orderItem is not null)
                    {
                        newProductItem.Amount = orderItem.Amount;
                    }
                    return newProductItem;
                }
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new EntityNotFoundException(ex.Message);
            }
            catch (DO.AllreadyExistException ex)
            {
                throw new AllreadyExistException(ex.Message);
            }
            throw new IdNotExsitException("the id is negtive");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="EntityDetailsWrongException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
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
                    throw new EntityDetailsWrongException(ex.Message);
                }
                catch (DO.EntityNotFoundException ex)
                {
                    throw new AllreadyExistException(ex.Message);
                }
            }
            else
                throw new EntityDetailsWrongException("The product data is incorrect");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
        /// <exception cref="Exception"></exception>
        public void Delete(int id)
        {
            bool exsit = Dal.OrderItem.List().Any(x => x.ProductID == id);
            if (!exsit)
                try
                {
                    Dal.Product.Delete(id);
                }
                 catch (DO.EntityNotFoundException ex)
                {
                    throw new EntityNotFoundException(ex.Message);
                }
                catch (DO.AllreadyExistException ex)
                {
                    throw new AllreadyExistException(ex.Message);
                }

            else
                throw new Exception("product not exsit");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="AllreadyExistException"></exception>
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
                    throw new EntityNotFoundException(ex.Message);
                }
                catch (DO.AllreadyExistException ex)
                {
                    throw new AllreadyExistException(ex.Message);
                }
            }
            else
                throw new EntityNotFoundException("product not exsit");
        }
    }
}

