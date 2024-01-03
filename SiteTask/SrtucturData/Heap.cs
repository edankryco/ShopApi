namespace SiteTask.SrtucturData;

public interface IHeap<T> where T : IComparable<T>
{
    public int Size();
    public void Put(T item);
    public T? GetMax();
}

public class Heap<T> : IHeap<T> where T : IComparable<T>
{
    private List<T> _heap = new();

    public int Count => _heap.Count;

    public int Size()
    {
        return Count;
    }

    public void Put(T item)
    {
       _heap.Add(item);

       var i = Count - 1;
       var parentIndex = GetParentIndex(i);

       while (i > 0 && _heap[parentIndex].CompareTo(_heap[i]) < 0)
       {
           Swap(i, parentIndex);
           i = parentIndex;
           parentIndex = GetParentIndex(i);
       }
    }

    public T? GetMax()
    {
        if (Count <= 0) return default;
        var result = _heap[0];
        _heap[0] = _heap[Count - 1];
        _heap.RemoveAt(Count - 1);
        Sort(0);
        
        return result;
    }
    
    private void Sort(int curenteIndex)
    {
        var maxIndex = curenteIndex;
        int leftIndex;
        int rightIndex;

        while (curenteIndex < Count)
        {
            leftIndex = 2 * curenteIndex + 1;
            rightIndex = 2 * curenteIndex + 2;

            if (leftIndex < Count && _heap[leftIndex].CompareTo(_heap[maxIndex]) > 0)
                maxIndex = leftIndex;
            if (rightIndex < Count && _heap[rightIndex].CompareTo(_heap[maxIndex]) > 0)
                maxIndex = rightIndex;
            if (maxIndex == curenteIndex)
                break;
            
            Swap(curenteIndex, maxIndex);
            curenteIndex = maxIndex;
        }
    }

    private void Swap(int currentIndex, int parentIndex)
    {
        (_heap[currentIndex], _heap[parentIndex]) = (_heap[parentIndex], _heap[currentIndex]);
    }

    private static int GetParentIndex(int i)
    {
        return (i - 1) / 2;
    }
}