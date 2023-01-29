using BlImplementation;

namespace BlApi;

public class Factory
{
    public static IBl? Get() => new Bl();
}
