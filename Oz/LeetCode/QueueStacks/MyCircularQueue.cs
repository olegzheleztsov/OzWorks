namespace Oz.LeetCode.QueueStacks;

public class MyCircularQueue
{
    private readonly int[] _array;
    private int _count;
    private int _head = -1;
    private int _tail = -1;

    public MyCircularQueue(int k) =>
        _array = new int[k];

    public bool EnQueue(int value)
    {
        if (IsFull())
        {
            return false;
        }

        if (IsEmpty() && _head == -1 && _tail == -1)
        {
            _head = _tail = 0;
        }
        else
        {
            _tail = (_tail + 1) % _array.Length;
        }

        _array[_tail] = value;
        _count++;
        return true;
    }

    public bool DeQueue()
    {
        if (IsEmpty())
        {
            return false;
        }

        _head = (_head + 1) % _array.Length;
        _count--;
        return true;
    }

    public int Front()
    {
        if (IsEmpty())
        {
            return -1;
        }

        return _array[_head];
    }

    public int Rear()
    {
        if (IsEmpty())
        {
            return -1;
        }

        return _array[_tail];
    }

    private bool IsEmpty() =>
        _count == 0;

    private bool IsFull() =>
        _count == _array.Length;
}