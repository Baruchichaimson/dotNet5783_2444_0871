using DO;
using System;
namespace Dal;

public class DalProduct
{
   
    public static int AddProduct(Product NewProduct)
    {
       
        for (int i = 0; i < DataSource.NextProduct; i++)
        {
           
            if (NewProduct.Id == DataSource.Products[i].Id)
            throw new Exception("the id is allready exist");
        }
        if (DataSource.NextProduct == 50)
            throw new Exception("the storge of proudct is full");
        else
            DataSource.Products[DataSource.NextProduct++] = NewProduct;

        return NewProduct.Id;
    }
    public static void DeleteProduct(int IDToDelete)
    {
        for (int i = 0; i < DataSource.NextProduct; i++)
        {
            if (IDToDelete == DataSource.Products[i].Id)
            {
                if (DataSource.NextProduct == 0)
                    throw new Exception("the storge of proudct is empty");
                else
                {
                    Product Temp = DataSource.Products[i];
                    DataSource.Products[i] = DataSource.Products[DataSource.NextProduct - 1];
                    DataSource.Products[DataSource.NextProduct - 1] = Temp;
                    DataSource.NextProduct--;
                }
                break;
            }
        }
    }
    public static void UpdateProduct(Product newproduct)
    {
        for (int i = 0; i < DataSource.NextProduct; i++)
        {
            if (newproduct.Id == DataSource.Products[i].Id)
            {
                DataSource.Products[i] = newproduct;
                break;
            }
        }
        throw new Exception("the id is not exist");
    }
    public static Product GetProduct(int IDToGet)
    {
        for (int i = 0; i < DataSource.NextProduct; i++)
        {
            if (IDToGet == DataSource.Products[i].Id)
            {
                return DataSource.Products[i];
            }
        }
        throw new Exception("the product is not exist");
    }
    public static Product[] ProductList()
    {
        Product[] productsList = new Product[DataSource.NextProduct];
        for(int i = 0; i < DataSource.NextProduct; i++)
        {
            productsList[i] = DataSource.Products[i];
        }
        return productsList;
    }
}
