using DalApi;
using DO;
using Google.Api.Ads.AdWords.v201809;
using System;
using System.Linq;

namespace Dal;

/// class for Manage The product database
internal class DalProduct : IProduct
{
    /// Function to add a new product
    public int Add(Product newProduct)
    {
        foreach (Product myproduct in DataSource.Products)
        {
            if (newProduct.Id == myproduct.Id)
                throw new AllreadyExistException("Product");
        }
            DataSource.Products.Add(newProduct);
        return newProduct.Id;
    }

    ///Function to delete a product
    public void Delete(int idToDelete)
    {
        foreach (Product myproduct in DataSource.Products)
        {
            if (idToDelete == myproduct.Id)
            {
                DataSource.Products.Remove(myproduct);
                return; 
            }
        }
        throw new EntityNotFoundException("product");
    }
    /// Function to update a product
    public void Update(Product newProduct)
    {
        foreach (Product myproduct in DataSource.Products)
        {
            if (newProduct.Id == myproduct.Id)
            {
                DataSource.Products.Remove(myproduct);
                DataSource.Products.Add(newProduct);
                return;
            }
        }
        throw new EntityNotFoundException("product");
    }
    /// <summary>
    /// A function that returns a product by id
    /// </summary>
    /// <param name="idToGet"> its id we got from the user </param>
    /// <returns> return my product with this id </returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public Product? Get(int idToGet)
    {
        Product? product = GetElement(product => product!.Value.Id == idToGet);
        if(product == null)
             throw new EntityNotFoundException("product");
        return product;
    }
    /// <summary>
    /// A function that returns an array of the products in the database
    /// <returns> the array with all the products.
    public IEnumerable<Product?> List(Func<Product?, bool>? myFunc = null)
    {
        bool flag = myFunc is null;
        if (flag)
            return DataSource.Products.Select(Product => Product);
        else
            return DataSource.Products.Where(myFunc);
    }
    public Product? GetElement(Func<Product?, bool>? myFunc )
    {
        Product? product =  DataSource.Products.FirstOrDefault(myFunc);
        if (product == null)
            throw new EntityNotFoundException("product");
        return product;
    }
}
