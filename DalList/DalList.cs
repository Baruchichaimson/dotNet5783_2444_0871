﻿using DalApi;

namespace Dal;

internal sealed class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }
    public IProduct Product { get; } = new DalProduct();
    public IOrder Order { get; } = new DalOrder();
    public IOrderItem OrderItem { get; } = new DalOrederItem();
}
