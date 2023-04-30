using System.Reflection.Emit;

public class MyStack
{
    MyList list = new MyList();
    public int count;
    private int capacity;
    public int Capacity { get { return list.capacity; } }
    public int top { get { return list[count - 1]; }}
    public MyStack()
    {

    }

    public void Push(int number)
    {
        list.Add(number);
        count++;
    }

    public void Pop()
    {
        list.RemoveAt(count--);
    }
    public void Clear()
    {
        list.Clear();
        count = 0;
    }
    public void PrintData()
    {
        list.PrintData();
    }
}