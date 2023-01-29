using BlImplementation;

namespace BlApi;

/// <summary>
/// order entity interface
/// </summary>
public interface IOrder
{
    /// <summary>
    /// the function return the list of all ordrs
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?>? GetList();
    /// <summary>
    /// the function recevies an order id and return the order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order GetData(int id);
    /// <summary>
    /// the function recevies an order id and update a shipping date
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order UpdateShippingDate(int id);
    /// <summary>
    /// the function receives an order id and update a date of delivery
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public BO.Order DeliveryUpdate(int id);
    /// <summary>
    /// The function receives an order id and returns the current status of the order.
    /// </summary>
    /// <param name="id">The ID of the order to be tracked.</param>
    /// <returns>An object containing the status of the order.</returns>
    public BO.OrderTracking OrderTracking(int id);
    /// <summary>
    /// The function allows a manager to update the quantity of a specific product in a cart.
    /// </summary>
    /// <param name="orderId">The ID of the order to be updated.</param>
    /// <param name="productId">The ID of the product to be updated.</param>
    /// <param name="amount">The new quantity of the product in the cart.</param>
    public void UpdateAdmin(int orderId, int productId, int amount);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int? getOldOrder();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<StatisticksOrders> GetStatisticksOrdersByMonthsAndYear();
}
