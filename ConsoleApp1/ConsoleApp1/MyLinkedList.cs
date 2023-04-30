using System.Text.RegularExpressions;

public class Node
{
    public int data;
    public Node next;
    public Node(int num)
    {
        data = num;
    }
}
public class MyLinkedList
{

    private int count;
    private Node head;
    private Node taill;

    public MyLinkedList()
    {
        Clear();
    }
    public void Add(int data)
    {
        if (count++ == 0)
        {
            head = new Node(data);
            taill = head;
            return;
        }
        Node newNode = new Node(data);
        taill.next = newNode;
        taill = newNode;
    }
    public void Add(Node node, int data)
    {
        Node newNode = new Node(data);
        Node wayNode = head;
        for (int i = 0; i < count; i++)
        {
            if (wayNode.next.data == node.data)
                break;
            wayNode = wayNode.next;
        }
        wayNode.next = newNode;
        newNode.next = node;
        count++;
    }
    public void RemoveAt(int idx)
    {
        if (idx == 0)
        {
            head = head.next;
            return;
        }

        count--;
        Node targetNode = FindAt(idx - 1);
        targetNode.next = targetNode.next.next;
    }
    public void Clear()
    {
        count = 0;
        head = null;
        taill = null;
    }
    public Node Find(int data)
    {
        Node node = head;
        for (int i = 0; i < count; i++)
        {
            if (node.data == data)
                return node;
            node = node.next;
        }
        return node;
    }
    public Node FindAt(int idx)
    {
        Node node = head;
        for (int i = 0; i < idx; i++)
            node = node.next;

        return node;
    }
    public void PrintAll()
    {
        Node node = head;
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(node.data);
            node = node.next;
        }
        Console.WriteLine();
    }
}
