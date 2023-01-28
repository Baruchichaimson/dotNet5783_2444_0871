using System;
namespace BO;

/// <summary>
/// a class for Order Tracking
/// </summary>
public class OrderTracking
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// enum of status order (Confirmed ,Sent ,Delivered)
    /// </summary>
    public OrderStatus? Status { get; set; }
    /// <summary>
    /// List of strings with tracking dates by progress
    /// </summary>
    public List<string?>? orderDetails { get; set; }
    public override string ToString() => $@"ID: {ID}{Environment.NewLine}Status: {Status}{Environment.NewLine}order Details:{Environment.NewLine}{string.Join(Environment.NewLine, orderDetails)}";
}
