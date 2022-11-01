using static DO.Enums;
namespace DO;
public struct Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CoffeeShop Categoryname { get; set; }
    public double Price { get; set; }
    public int Instock { get; set; }
    public override string ToString() => $@"
    Product ID = {Id}: {Name},
    Category - {Categoryname}
    Price: {Price}
    Instock: {Instock}";
}
