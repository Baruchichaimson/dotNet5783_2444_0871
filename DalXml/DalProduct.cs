using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace Dal
{
    internal class DalProduct : IProduct
    {
        const string s_product = @"Product";
        public int Add(Product product)
        {
            List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
            if (products.Exists(element => element?.Id == product.Id))
            {
                throw new AllreadyExistException($"product with id {product.Id}");
            }
            products.Add(product);
            XMLTools.SaveListToXMLSerializer(products,s_product);
            return product.Id;
        }

        public void Delete(int idToDelete)
        {
            List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
            Product? product = GetElement(element => element?.Id == idToDelete);
            products.Remove(product);
            XMLTools.SaveListToXMLSerializer(products, s_product);
        }

        public Product Get(int idToGet)
        {
            Product product = GetElement(product => product?.Id == idToGet);
            return product;
        }

        public Product GetElement(Func<Product?, bool>? myFunc)
        {
            List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);

            if (myFunc is null)
            {
                throw new NullExeption("condition");
            }
            Product product = products.FirstOrDefault(myFunc) ?? throw new NullExeption("product");
            return product;
        }

        public IEnumerable<Product?>? List(Func<Product?, bool>? myFunc = null)
        {
            List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);

            if (myFunc is null)
                return products.Select(Product => Product);
            else
                return products.Where(myFunc!);
        }

        public void Update(Product newEntity)
        {
            List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
            Product? product = GetElement(element => element?.Id == newEntity.Id);
            products.Remove(product);
            products.Add(newEntity);
            XMLTools.SaveListToXMLSerializer(products, s_product);

        }
    }
}
