namespace SiteTask.SrtucturData;

public interface IHeap<T> where T : IComparable<T>
{
    public void Put();
    public bool Contains();
}

public class Heap<T> : IHeap<T> where T : IComparable<T>
{
    public void Put()
    {
        throw new NotImplementedException();
    }

    public bool Contains()
    {
        throw new NotImplementedException();
    }
}