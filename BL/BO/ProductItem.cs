namespace BO;

/// <summary>
/// class for Displaying relevant product information to the user
/// </summary>
public class ProductItem
{
    /// <summary>
    /// product id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// product price
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// enum of product category
    /// </summary>
    public CoffeeShop? Category { get; set; }
    /// <summary>
    /// How many in user cart
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// if in stock
    /// </summary>
    public bool InStock { get; set; }
    public override string ToString() => $@"
        ID : {ID}
        Name: {Name}
        Price: {Price}
        Category: {Category}
        Amount: {Amount}
        InStock: {InStock}";
}
