﻿namespace DalApi;

public interface ICrud<T>  where T : struct
{
    public int Add(T newEntity);
    public void Delete(int idToDelete);
    public void Update(T newEntity);
    public T Get(int idToGet);
    public IEnumerable<T?>? List(Func<T?, bool>? myFunc = null);
    public T GetElement(Func<T?, bool>? myFunc);
}
