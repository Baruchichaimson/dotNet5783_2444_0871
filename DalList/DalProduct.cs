using DO;
using System;
namespace Dal;

/// class for Manage The product database
public class DalProduct
{

    /// Function to add a new product
    public int AddProduct(Product newProduct)
    {
        for (int i = 0; i < DataSource.NextProduct; i++)
        {  
            if (newProduct.Id == DataSource.Products[i].Id)
            throw new Exception("the id is allready exist\n");
        }
        if (DataSource.NextProduct == 50)
            throw new Exception("the storge of proudct is full\n");
        else
            DataSource.Products[DataSource.NextProduct++] = newProduct;

        return newProduct.Id;
    }

    ///Function to delete a product
    public void DeleteProduct(int idToDelete)
    {
        for (int i = 0; i < DataSource.NextProduct; i++)
        {
            if (idToDelete == DataSource.Products[i].Id)
            {
                if (DataSource.NextProduct == 0)
                    throw new Exception("the storge of proudct is empty\n");
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
    public void UpdateProduct(Product newProduct)
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
            throw new Exception("the id is not exist\n");
    }
    /// A function that returns a product by id
    public Product GetProduct(int idToGet)
    {
        for (int i = 0; i < DataSource.NextProduct; i++)
        {
            if (idToGet == DataSource.Products[i].Id)
                return DataSource.Products[i];
        }
        throw new Exception("the product is not exist\n");
    }
    /// A function that returns an array of the products in the database
    public Product[] ProductList()
    {
        Product[] productsList = new Product[DataSource.NextProduct];
        for(int i = 0; i < DataSource.NextProduct; i++)
        {
            productsList[i] = DataSource.Products[i];
        }
        return productsList;
    }
}
