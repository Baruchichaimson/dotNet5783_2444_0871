using BlApi;
using Google.Api.Ads.AdWords.v201809;

namespace BlImplementation
{
    internal class Product : IProduct
    {
        private DalApi.IDal Dal = new DO.DalList();
        private DO.Product products = new();
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
            DO.Product product = new();
            BO.Product newProduct = new();
            try
            {
                if (id > 0)
                {
                    product = Dal.Product.Get(id);

                    newProduct.ID = product.Id;
                    newProduct.Name = product.Name;
                    newProduct.Price = product.Price;
                    newProduct.InStock = product.Instock;
                    newProduct.Category = (BO.CoffeeShop)product.Categoryname;
                    return newProduct;
                }
                throw new Exception("the id is negtive");
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new BO.EntityNotFoundException("product", ex);
            }
        }
        public BO.ProductItem GetData(int id, BO.Cart cart)
        {
            DO.Product product = new();
            BO.ProductItem newProductItem = new();
            try
            {
                if (id > 0)
                {
                    product = Dal.Product.Get(id);

                    newProductItem.ID = product.Id;
                    newProductItem.Name = product.Name;
                    newProductItem.Price = product.Price;
                    newProductItem.InStock = product.Instock > 0;
                    newProductItem.Category = (BO.CoffeeShop)product.Categoryname;

                    BO.OrderItem orderItem = cart.Items.First(orderItem => orderItem.ID == id);

                    if (orderItem is not null)
                    {
                        newProductItem.Amount = orderItem.Amount;
                    }
                    return newProductItem;
                }
                throw new Exception("the id is negtive");
            }
            catch (DO.EntityNotFoundException ex)
            {
                throw new BO.EntityNotFoundException("product", ex);
            }
        }
        public void Add(BO.Product product)
        {

        }
        public BO.Product Delete(int id)
        {

        }
        public void Update(BO.Product product)
        {

        }
    }
}
}
