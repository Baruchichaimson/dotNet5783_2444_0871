using DalApi;
using DO;
using Google.Api.Ads.AdWords.v201809;
using System;
using EntityNotFound = DO.EntityNotFound;

namespace Dal;

/// class for Manage The product database
public class DalProduct
{
    /// Function to add a new product
    public int Add(Product newProduct)
    {
        foreach (Product myproduct in DataSource.Products)
        {
            if (newProduct.Id == myproduct.Id)
                throw new AllreadyExist("id");
        }
        if (DataSource.Products.Count >= 50)
            throw new StorgeIsFull("proudct");
        else
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
                if (DataSource.Products.Count == 0)
                    throw new StorgeIsEmpty("proudct");
                else
                {
                    DataSource.Products.Remove(myproduct);
                }
                break;
            }
        }
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
        throw new EntityNotFound("id");
    }
    /// A function that returns a product by id
    public Product Get(int idToGet)
    {
        foreach (Product myproduct in DataSource.Products)
        {
            if (idToGet == myproduct.Id)
                return myproduct;
        }
        throw new EntityNotFound("product");
    }
    /// <summary>
    /// A function that returns an array of the products in the database
    /// <returns> the array with all the products.
    public IEnumerable<Product> List()
    {
        var productToPrint = new List<Product>();
        foreach (Product myproduct in DataSource.Products)
        {
            productToPrint.Add(myproduct);
        }
        return productToPrint;
    }
}
