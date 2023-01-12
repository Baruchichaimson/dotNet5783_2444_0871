namespace DO;
public struct Order
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }

    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryrDate { get; set; }
    public override string ToString() => $@"
    Order ID = {Id}
    CustomerName: {CustomerName}
    CustomerEmail: {CustomerEmail}
    CustomerAdress: {CustomerAdress}
    OrderDate: {OrderDate}
    ShipDate: {ShipDate}
    DeliveryrDate: {DeliveryrDate}";
}
