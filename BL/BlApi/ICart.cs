
namespace BlApi;

/// <summary>
/// The interface wraps the cart class that handles 
/// operations on the shopping basket and serves as 
/// the route for transferring the logic to the display layer
/// </summary>
public interface ICart
{
    /// <summary>
    /// this function is to add product to the basket shopping if he is exist in the stock.
    /// </summary>
    /// <param name="cart"> We receive a cart  initialized shopping basket entity </param>
    /// <param name="id">the id is for the user that want to add id specific </param>
    /// <returns></returns>
    public BO.Cart AddProduct(BO.Cart cart, int id);
    /// <summary>
    /// this function is for update which order item we have in the basket shopping
    /// </summary>
    /// <param name="cart"> We receive a cart  initialized shopping basket entity </param>
    /// <param name="id"> the id is for the user that want to add id specific </param>
    /// <param name="newAmount"> the user put the amount that is want to update in the basket shopping</param>
    /// <returns></returns>
    public BO.Cart UpdateProductAmount(BO.Cart cart, int id, int newAmount);
    /// <summary>
    /// the function is to confirmation the basket shopping after every thing is correct and the the basket not empty or somthing.
    /// </summary>
    /// <param name="cart"> We receive a cart  initialized shopping basket entity </param>
    public void OrderConfirmation(BO.Cart cart);
    event Action? Action;
}
