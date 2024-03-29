﻿namespace BO;

[Serializable]
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Exception inner, string messege = "") : base(messege , inner) { }
}
[Serializable]
public class AllreadyExistException : Exception
{
    public AllreadyExistException(Exception inner, string messege = "") : base(messege, inner) { }
}
[Serializable]
public class NotEnoughInStockException : Exception
{
    public NotEnoughInStockException(string messege) : base(messege) { }
}
[Serializable]
public class IdNotExsitException : Exception
{
    public IdNotExsitException(string messege) : base(messege) { }
}
[Serializable]
public class EntityDetailsWrongException : Exception
{
    public EntityDetailsWrongException(string messege) : base(messege) { }
}
[Serializable]
public class IncorrectAmountException : Exception
{
    public IncorrectAmountException(string messege) : base(messege) { }
}
[Serializable]
public class CartException : Exception
{
    public CartException(string messege) : base(messege) { }
}

[Serializable]
public class ProductIsOnOrderException : Exception
{
    public ProductIsOnOrderException(string messege) : base(messege) { }
}
[Serializable]
public class NullExeption : Exception
{
    public NullExeption(string messege) : base(messege + " is null in BO") { }
   
}
[Serializable]
public class NotOldeOrderExcepton : Exception
{
    public NotOldeOrderExcepton(string messege) : base(messege) { }

}
[Serializable]
public class NullExeptionForDO : Exception
{
    public NullExeptionForDO(Exception inner, string messege = "") : base(messege, inner) { }
}

[Serializable]
public class BLConfigException : Exception
{
    public BLConfigException(string msg) : base(msg) { }
    public BLConfigException(Exception inner, string messege = "") : base(messege, inner) { }
}



