using DalApi;
using DO;
using Google.Api.Ads.AdWords.v201809;
using System;
using System.Linq;

namespace Dal;

/// <summary>
/// class for Manage The product database
/// </summary>
internal class DalProduct : IProduct
{
    /// <summary>
    /// Function to add a new product
    /// </summary>
    /// <param name="newProduct"> prodact fo adding</param>
    /// <returns>the new product id</returns>
    /// <exception cref="AllreadyExistException"> when teh id exist</exception>
    public int Add(Product newProduct)
    {
        if (DataSource.Products.Exists(element => element?.Id == newProduct.Id))
        {
            throw new AllreadyExistException($"product with id {newProduct.Id}");
        }
        DataSource.Products.Add(newProduct);
        return newProduct.Id;
    }
    /// <summary>
    /// Function to delete a product
    /// </summary>
    /// <param name="idToDelete">product id for deleting</param>
    public void Delete(int idToDelete)
    {
        Product? product = GetElement(element => element?.Id == idToDelete);
        DataSource.Products.Remove(product);
    }
    /// <summary>
    /// Function to update a product
    /// </summary>
    /// <param name="newProduct"> product for update</param>
    public void Update(Product newProduct)
    {
        Product? product = GetElement(element => element?.Id == newProduct.Id);
        DataSource.Products.Remove(product);
        DataSource.Products.Add(newProduct);
    }
    /// <summary>
    /// A function that returns a product by id
    /// </summary>
    /// <param name="idToGet"> its id we got from the user </param>
    /// <returns> return my product with this id </returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public Product Get(int idToGet)
    {
        Product product = GetElement(product => product?.Id == idToGet);
        return product;
    }
    /// <summary>
    /// A function for getting a IEnumerable of the database list
    /// </summary>
    /// <param name="myFunc"> a condition delegate for filtering the list</param>
    /// <returns>IEnumerable</returns>
    public IEnumerable<Product?>? List(Func<Product?, bool>? myFunc = null)
    {
        if (myFunc is null)
            return DataSource.Products.Select(Product => Product);
        else
            return DataSource.Products.Where(myFunc!);
    }
    /// <summary>
    /// A function for getting a element from the database
    /// </summary>
    /// <param name="myFunc">a condition delegate for a certain element</param>
    /// <returns> one item </returns>
    /// <exception cref="NullExeption"> if the item is null</exception>

    public Product GetElement(Func<Product?, bool>? myFunc)
    {
        if (myFunc is null)
        {
            throw new NullExeption("condition");
        }
        Product product =  DataSource.Products.FirstOrDefault(myFunc) ?? throw new NullExeption("product") ;
        return product;
    }
}
