using DalApi;
using DO;
namespace Dal;
internal class DalProduct : IProduct
{
    const string s_product = @"Products";
    /// <summary>
    /// Add a new product to the XML file and return its Id
    /// </summary>
    /// <param name="product">The product object to be added.</param>
    /// <returns>The Id of the added product.</returns>
    /// <exception cref="AllreadyExistException">Thrown when a product with the same Id already exists in the XML file.</exception>
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
    /// <summary>
    /// Deletes a product from the XML file by its Id.
    /// </summary>
    /// <param name="idToDelete"></param>
    public void Delete(int idToDelete)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
        Product? product = GetElement(element => element?.Id == idToDelete);
        products.Remove(product);
        XMLTools.SaveListToXMLSerializer(products, s_product);
    }
    /// <summary>
    /// Retrieves a product from the XML file by its Id.
    /// </summary>
    /// <param name="idToGet"></param>
    /// <returns></returns>
    public Product Get(int idToGet)
    {
        Product product = GetElement(product => product?.Id == idToGet);
        return product;
    }
    /// <summary>
    /// This method is used to get a single product from an XML file based on a specified condition.
    /// The condition is passed as an optional parameter 'myFunc' of type 'Func<Product?, bool>'.
    /// If 'myFunc' is null, it throws a new 'NullException' with the message "condition".
    /// Otherwise, it uses the 'FirstOrDefault' extension method on the 
    /// 'products' list to get the first product that matches the condition specified in the 'myFunc' parameter.
    /// If no product is found that matches the condition it throws a new 'NullException' with the message "product".
    /// </summary>
    /// <param name="myFunc">The condition to filter the products by. It is passed as an optional parameter of type 'Func<Product?, bool>'</param>
    /// <returns>The product that matches the specified condition</returns>
    /// <exception cref="NullExeption">Thrown when 'myFunc' is null or if no product is found that matches the condition</exception>
    public Product GetElement(Func<Product?, bool>? myFunc = null)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);

        if (myFunc is null)
        {
            throw new NullExeption("condition");
        }
        Product product = products.FirstOrDefault(myFunc) ?? throw new NullExeption("product");
        return product;
    }
    /// <summary>
    /// Returns a list of products from the XML file based on a given condition. If no condition is given, returns all products.
    /// </summary>
    /// <param name="myFunc">A function to filter the list of products. If not provided, all products will be returned.</param>
    /// <returns></returns>
    public IEnumerable<Product?>? List(Func<Product?, bool>? myFunc = null)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);

        if (myFunc is null)
            return products.Select(Product => Product);
        else
            return products.Where(myFunc!);
    }
    /// <summary>
    /// This method updates an existing product in the XML file by replacing 
    /// the old product with the new product with updated information.
    /// </summary>
    /// <param name="newEntity">The new product object with updated information</param>
    public void Update(Product newEntity)
    {
        List<Product?> products = XMLTools.LoadListFromXMLSerializer<Product>(s_product);
        Product? product = GetElement(element => element?.Id == newEntity.Id);
        products.Remove(product);
        products.Add(newEntity);
        XMLTools.SaveListToXMLSerializer(products, s_product);

    }
}
