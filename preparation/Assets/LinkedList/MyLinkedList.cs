using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;

public class Node<T>
{
    public T Data;
    public Node<T> next;
    public Node<T> before;
    
    public Node(T data)
    {
        Data = data;
        next = null;
    }
}
public class MyLinkedList<T>
{
    public Node<T> headNode;
    public Node<T> curNode;

    private int count = 0;
    public void AddList(T data)
    {
        Node<T> node = new Node<T>(data); 
        count++;

        if(headNode == null)
            headNode = node;
        else
        {
            curNode.next = node;
            curNode.before = curNode;
        }

        curNode = node;
    }
    public void RemoveAt(int idx)
    {
        count--;
        if (headNode == null)
        {
            Debug.Log("����ִ� ����Ʈ�Դϴ�.");
        }
        else
        {
            Node<T> deleteNode = FindNode(idx);
            
            if(deleteNode.next != null)
            {
                deleteNode.next.before = deleteNode.before;//���� ��带 ���� ���� ����
                deleteNode.before = deleteNode.next;//���� ��忡 ���� ��带 ����
            }
            else
            {
                Debug.Log("������ ����Դϴ�");
                deleteNode.before = null;
            }

            NodeFree(deleteNode);
        }
    }
    public void RemoveAt(Node<T> idx)
    {
        count--;
        if (headNode == null)
        {
            Debug.Log("����ִ� ����Ʈ�Դϴ�.");
        }
        else
        {
            Node<T> deleteNode = idx;

            if (deleteNode.next != null)
            {
                deleteNode.next.before = deleteNode.before;//���� ��带 ���� ���� ����
                deleteNode.before = deleteNode.next;//���� ��忡 ���� ��带 ����
            }
            else
            {
                Debug.Log("������ ����Դϴ�");
                deleteNode.before = null;
            }

            NodeFree(deleteNode);
        }
    }
    public Node<T> EndNode()
    {
        Node<T> Node = headNode;
        while (Node.next != null)
        {
            Node = Node.next;
        }
        return Node;
    }
    public Node<T> FindNode(int idx)
    {
        Node<T> Node = headNode;
        for(int i = 0; i < idx; i++)
        {
            if (Node.next != null)
            {
                Node = Node.next;
            }
            else
            {
                Debug.Log("����Ʈ�� ��谪�� �ʰ��߽��ϴ�!");
                break;
            }    
        }
        return Node;
    }
    public Node<T> FindNode(Node<T> idx)
    {
        Node<T> Node = headNode;
        while(Node != idx)
        {
            Node = Node.next;
        }
        if(Node.next == null)
        {
            Debug.Log("�����ϴ�.");
            return null;
        }
        else
            return Node;
    }
    private void NodeFree(Node<T> node)
    {
        node.next = null;
        node.before = null;
    }
}
