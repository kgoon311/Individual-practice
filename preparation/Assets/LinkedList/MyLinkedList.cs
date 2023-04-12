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
            Debug.Log("비어있는 리스트입니다.");
        }
        else
        {
            Node<T> deleteNode = FindNode(idx);
            
            if(deleteNode.next != null)
            {
                deleteNode.next.before = deleteNode.before;//다음 노드를 이전 노드와 연결
                deleteNode.before = deleteNode.next;//이전 노드에 다음 노드를 연결
            }
            else
            {
                Debug.Log("마지막 노드입니다");
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
            Debug.Log("비어있는 리스트입니다.");
        }
        else
        {
            Node<T> deleteNode = idx;

            if (deleteNode.next != null)
            {
                deleteNode.next.before = deleteNode.before;//다음 노드를 이전 노드와 연결
                deleteNode.before = deleteNode.next;//이전 노드에 다음 노드를 연결
            }
            else
            {
                Debug.Log("마지막 노드입니다");
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
                Debug.Log("리스트의 경계값을 초과했습니다!");
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
            Debug.Log("없습니다.");
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
