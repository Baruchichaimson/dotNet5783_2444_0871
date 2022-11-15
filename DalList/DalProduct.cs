using DalApi;
using DO;
using System;
namespace Dal;

/// class for Manage The product database
public class DalProduct
{
    /// Function to add a new product
    public int Add(Product newProduct)
    {
        for (int i = 0; i < DataSource.NextProduct; i++)
        {  
            if (newProduct.Id == DataSource.Products[i].Id)
            throw new AllreadyExist("id");
        }
        if (DataSource.NextProduct == 50)
            throw new StorgeIsFull("proudct");
        else
            DataSource.Products[DataSource.NextProduct++] = newProduct;

        return newProduct.Id;
    }

    ///Function to delete a product
    public void Delete(int idToDelete)
    {
        for (int i = 0; i < DataSource.NextProduct; i++)
        {
            if (idToDelete == DataSource.Products[i].Id)
            {
                if (DataSource.NextProduct == 0)
                    throw new StorgeIsEmpty("proudct");
                else
                {
                    // Replaces with the last one and lowers the size of the array
                    Product temp = DataSource.Products[i];
                    DataSource.Products[i] = DataSource.Products[DataSource.NextProduct - 1];
                    DataSource.Products[DataSource.NextProduct - 1] = temp;
                    DataSource.NextProduct--;
                }
                break;
            }
        }
    }
    /// Function to update a product
    public void Update(Product newProduct)
    {
        bool exist = false;
        for (int i = 0; i < DataSource.NextProduct; i++)
        {
            if (newProduct.Id == DataSource.Products[i].Id)
            {
                DataSource.Products[i] = newProduct;
                exist = true;
                break;
                
            }
        }
        if(!exist)
            throw new EntityNotFound("id");
    }  
    /// A function that returns a product by id
    public Product Get(int idToGet)
    {
        for (int i = 0; i < DataSource.NextProduct; i++)
        {
            if (idToGet == DataSource.Products[i].Id)
                return DataSource.Products[i];
        }
        throw new EntityNotFound("product");
    }
    /// <summary>
    /// A function that returns an array of the products in the database
    /// <returns> the array with all the products.
    public Product[] List()
    {
        Product[] productsList = new Product[DataSource.NextProduct];
        for(int i = 0; i < DataSource.NextProduct; i++)
        {
            productsList[i] = DataSource.Products[i];
        }
        return productsList;
    }
}
