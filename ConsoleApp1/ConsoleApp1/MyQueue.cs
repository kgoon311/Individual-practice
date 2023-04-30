public class MyQueue
{
    public MyList list = new MyList();
    public int Capacity { get { return list.capacity; } }
    public int Count { get { return list.count; } set { list.count = value; } }
    public MyQueue()
    {
        Count = 0;
        list.capacity = 4;
    }
    public void Enqueue(int num)
    {
        list.Add(num);
    }
    public int Dequeue() 
    {
        int num = list[0];
        for(int i = 0;i< Count - 1;i++) 
            list[i] = list[i+1];

        Count--;
        return num;
    }
    public void Clear()
    {
        list.Clear();
        Count = 0;
    }
    public void Print()
    {
        for (int i = 0; i < Count; i++)
            Console.WriteLine(list[i]);
    }
}

