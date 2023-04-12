using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class StartLinkedList : MonoBehaviour
{
    [SerializeField] private GameObject NodeObject;
    [SerializeField] private TextMesh[] NodeText = new TextMesh[5];
    [SerializeField] private MyLinkedList<int> list = new MyLinkedList<int>();

    private int count;
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            NodeText[i] = Instantiate(NodeObject, new Vector3(-4 + 2 * i, 0, 0), transform.rotation).GetComponent<TextMesh>();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) list.AddList(count++);
        if (Input.GetKeyDown(KeyCode.Alpha2)) list.RemoveAt(list.EndNode());
        if (Input.GetKeyDown(KeyCode.Alpha2)) list.RemoveAt(list.EndNode());
    }
}