using System.Diagnostics;

public class Program 
{
    public static void Main()
    {
        #region list
        /*MyList myList = new MyList();

        for(int i = 0; i<100;i++)
        {
            myList.Add(i);
        }
        myList.PrintData();
        Console.WriteLine();
        Console.WriteLine($"capacity : {myList.capacity} , count : {myList.count}");
        Console.WriteLine();
        myList.Clear();
        myList.Add(0);
        myList.Add(2);
        myList.Add(8);
        myList.Add(5);
        myList.PrintData();
        Console.WriteLine();

        myList.RemoveAt(3);
        myList.PrintData();
        Console.WriteLine();

        myList.Clear();
        myList.PrintData();
*/
        #endregion

        #region stack
        /*MyStack stack = new MyStack();

        stack.Push(0);
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.PrintData();
        Console.WriteLine();

        stack.Pop();
        stack.PrintData();

        Debug.Assert(stack.top == 3);
        stack.Pop();
        Debug.Assert(stack.top == 2);
        stack.Clear();
        stack.PrintData();
        Console.WriteLine(stack.Capacity);
        Console.WriteLine(stack.count);*/
        #endregion

        #region Queue
        /*MyQueue queue = new MyQueue();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(5);
        queue.Enqueue(6);
        queue.Enqueue(30);

        queue.Print();
        Console.WriteLine();

        Console.WriteLine("Dequeue : " +queue.Dequeue());
        Console.WriteLine();
        Console.WriteLine("Dequeue : " +queue.Dequeue());
        Console.WriteLine();

        queue.Enqueue(1);
        queue.Print();
        Console.WriteLine();

        queue.Clear();
        queue.Print();
        Console.WriteLine();

        queue.Enqueue(1);
        queue.Print();
        Console.WriteLine();*/
        #endregion

        #region CircleQueue
        /*  MyCircleQueue queue = new MyCircleQueue(4);
          queue.Enqueue(1);
          queue.Enqueue(2);
          queue.Enqueue(3);

          Debug.Assert(queue.Count == 3);
          Debug.Assert(queue.Capacity == 4);

          Debug.Assert(queue.Dequeue() == 1);
          Debug.Assert(queue.Count == 2);

          queue.Clear();
          Debug.Assert(queue.Count == 0);

          queue.Enqueue(4);
          Debug.Assert(queue.Dequeue() == 4);

          queue.Enqueue(5);
          queue.Enqueue(6);
          queue.Enqueue(7);
          Debug.Assert(queue.Count == 3);

          Debug.Assert(queue.Dequeue() == 5);
          queue.Enqueue(8);
          queue.Enqueue(9);

          Debug.Assert(queue.Dequeue() == 6);
          Debug.Assert(queue.Dequeue() == 7);
          Debug.Assert(queue.Dequeue() == 8);
          Debug.Assert(queue.Dequeue() == 9);

          queue.Enqueue(10);
          queue.Enqueue(11);

          Debug.Assert(queue.Dequeue() == 10);
          Debug.Assert(queue.Dequeue() == 11);

          Debug.Assert(queue.Count == 0);*/
        #endregion

        #region LinkedList
        MyLinkedList list = new MyLinkedList(); 
        list.Add(1);
        list.Add(2);
        list.PrintAll();

        list.Add(list.Find(2),4);
        list.PrintAll();

        list.RemoveAt(2);
        list.PrintAll();

        list.Clear();
        list.PrintAll();

        list.Add(2);
        list.Add(3);
        list.PrintAll();
        Console.WriteLine(list.Find(2).data);
        Console.WriteLine(list.FindAt(1).data);


        #endregion
    }
}
