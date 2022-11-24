using DalApi;
using DO;
using System;

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
    public Product Get(int idToGet)
    {
        foreach (Product myproduct in DataSource.Products)
        {
            if (idToGet == myproduct.Id)
                return myproduct;
        }
        throw new EntityNotFoundException("product");
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
