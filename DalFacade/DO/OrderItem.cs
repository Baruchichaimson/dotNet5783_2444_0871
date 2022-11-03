
namespace DO;
public struct OrderItem
{
    public int Id { get; set; }
    public int ProductID { get; set; }
    public int OredrID  { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
    Product ID = {ProductID}
    OredrID: {OredrID}
    Price: {Price}
    Amount: {Amount}";
}

