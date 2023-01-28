namespace BlApi;

/// <summary>
/// The interface is used as a wrapper for our other
/// interfaces and it will be more comfortable
/// to work through it later
/// </summary>
public interface IBl
{
    /// <summary>
    /// Creating an instance of an interface for a logical 
    /// entity that we will work with in the lower layer
    /// </summary>
    public IProduct Product { get; }
    /// <summary>
    /// Creating an instance of an interface for a logical 
    /// entity that we will work with in the lower layer
    /// </summary>
    public IOrder Order { get; }
    /// <summary>
    ///Creating an instance of an interface for a logical 
    /// entity that we will work with in the lower layer
    /// </summary>
    public ICart Cart { get; }


}
