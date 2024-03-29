﻿using BlApi;

namespace BlImplementation;

/// <summary>
/// Implementation of the interface IBl
/// </summary>
sealed internal class Bl : IBl
{
    /// <summary>
    /// The attribute that can receive from the logic function product its implementations in the display layer
    /// </summary>
    public IProduct Product { get; } = new Product();
    /// <summary>
    /// The attribute that can receive from the logic function order its implementations in the display layer
    /// </summary>
    public IOrder Order { get; } = new Order();
    /// <summary>
    /// The attribute that can receive from the logic function cart its implementations in the display layer
    /// </summary>
    public ICart Cart { get; } = new Cart();

}
