using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    [SerializeField] private UnitScript unitBase;
    private UnitScript[,] unitArray;

    private List<UnitScript> openNode;

    private UnitScript curNode;
    private UnitScript startNode;
    private UnitScript endNode;

    //�ֺ� ��带 Ž���� �� ���˴ϴ�.
    private Vector2[] pos = {new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1),
                             new Vector2(-1, 0),                     new Vector2(1, 0),
                             new Vector2(-1, -1),new Vector2(0, -1), new Vector2(1, -1) };

    private int[] cost = { 14, 10, 14, 10, 10, 14, 10, 14 };//�Ϲ� �̵� : 10 , �밢 �̵� : 14
    [SerializeField] private int arrayCount_x;
    [SerializeField] private int arrayCount_y;

    private int setIdx;//��带 �����Ҷ� ���˴ϴ�
    private bool isStart;
    private bool isEnd;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        unitArray = new UnitScript[arrayCount_x, arrayCount_y];
        openNode = new List<UnitScript>();
        for (int i = 0; i < arrayCount_x; i++)
        {
            for (int j = 0; j < arrayCount_y; j++)
            {
                unitArray[i, j] = Instantiate(unitBase, new Vector3(i - arrayCount_x / 2, j - arrayCount_y / 2, 0), transform.rotation);
                unitArray[i, j].nodeIdx = new Vector2(i, j);
            }
        }
    }
    void Update()
    {
        InputUpdate();
    }
    void InputUpdate()
    {
        if (isStart == true)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && startNode != null && endNode != null)
        {
            isStart = true;
            openNode.Clear();
            openNode.Add(startNode);
            curNode = startNode;
            StartAstar();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 ��ġ�� �������� ���� ����
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // ����ĳ��Ʈ�� ���� �浹�� ������Ʈ ����
            {
                UnitScript targetUnit = GetNode(hit.transform.position + new Vector3(arrayCount_x / 2, arrayCount_y / 2));
                targetUnit.type = (UnitType)setIdx;
                if (setIdx == 2)
                {
                    if (startNode != null)
                        startNode.type = 0;

                    startNode = targetUnit;
                }
                if (setIdx == 3)
                {
                    if (endNode != null)
                        endNode.type = 0;

                    endNode = targetUnit;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad0)) setIdx = 0;
        else if (Input.GetKeyDown(KeyCode.Keypad1)) setIdx = 1;
        else if (Input.GetKeyDown(KeyCode.Keypad2)) setIdx = 2;
        else if (Input.GetKeyDown(KeyCode.Keypad3)) setIdx = 3;
    }

    private void StartAstar()
    {
        for (int i = 0; i < 8; i++)
        {
            //���� F�� ���� ������ �ֺ� ��� Ž��
            Vector2 vec = curNode.nodeIdx + pos[i]; 

            //������ �Ѿ���� ����ó��
            if (vec.x < 0 || vec.x >= arrayCount_x || vec.y < 0 || vec.y >= arrayCount_y)
                continue;

            //�ֺ� ��� ����
            OpenNode(GetNode(vec), i);
        }
        openNode.Remove(curNode);

        //�ֺ� ��忡 End��尡 ���� �� ���� �� ���������� ����
        if (isEnd)
        {
            endNode.beforeNode = curNode;
            LinkNode();
            return;
        }

        //����� F�� ���� ���Ž��
        UnitScript lowUnit = openNode[0];
        foreach (UnitScript unit in openNode)
            if (lowUnit.f_f > unit.f_f)
                lowUnit = unit;

        curNode = lowUnit;
        curNode.type = UnitType.CloseNode;
        Invoke("StartAstar", 0.3f);
    }
    private void OpenNode(UnitScript node , int idx)
    {
        if (node == null)
            return;
        else if (node.type == UnitType.End) //���� ��尡 End Node�� ��� ����
        {
            isEnd = true;
            return;
        }
        else if (node.type != UnitType.Unit)//�Ϲ� Unit�� �ƴҽ� ����
            return;

        openNode.Add(node);
        node.beforeNode = curNode;
        //�̵����� �ɸ��� �ڽ�Ʈ ���ϱ�
        node.f_g = node.beforeNode.f_g + cost[idx];
        //END�������� ���� ���� �뷫���� �Ÿ�
        node.f_h = Mathf.Abs(endNode.nodeIdx.x - node.nodeIdx.x)*10 + 
                   Mathf.Abs(endNode.nodeIdx.y - node.nodeIdx.y)*10;

        node.f_f = node.f_g + node.f_h;
        node.type = UnitType.OpenNode;
    }
    private void LinkNode()
    {
        if (endNode.beforeNode == startNode)
            return;

        endNode = endNode.beforeNode;
        endNode.type = UnitType.Road;
        Invoke("LinkNode", 0.5f);
    }
    UnitScript GetNode(Vector2 pos)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;

        return unitArray[x, y];
    }

}
