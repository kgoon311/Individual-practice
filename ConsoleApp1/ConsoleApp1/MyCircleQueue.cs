using System.Reflection.Emit;
using System.Security.AccessControl;

public class MyCircleQueue
{
    public int[] array;
    public int Capacity;
    public int Count;
    public int front;
    public int rear;

    public MyCircleQueue(int size)
    {
        array = new int[size];
        Capacity = size;
        Clear();
    }

    public void Enqueue(int num)
    {
        if(Count >= Capacity)
        {
            Console.WriteLine("최대 사이즈입니다.");
            return;
        }
        Count++;
        array[rear] = num;
        rear = (rear + 1) % Capacity;
    }
    public int Dequeue()
    {
        if(Count == 0)
        {
            Console.WriteLine("비어있는 큐 입니다.");
            return -1;
        }
        int num = array[front];
        front = (front + 1) % Capacity;
        Count--;

        return num;
    }
    
    public void Clear()
    {
        front = 0;
        rear = 0;
        Count = 0;
    }
    public void Print()
    {
        for(int i = 0;i<Count;i++)
        {
            Console.WriteLine(array[i]);
        }
    }
}

