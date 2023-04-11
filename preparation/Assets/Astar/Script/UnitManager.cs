using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    [SerializeField] private UnitScript unitBase;
    [SerializeField] private UnitScript[,] unitArray;

    [SerializeField] private List<UnitScript> openNode;
    [SerializeField] private List<UnitScript> closeNode;

    private UnitScript curNode;
    [SerializeField] private UnitScript startNode;
    [SerializeField] private UnitScript endNode;

    private Vector2[] pos = {new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1),
                             new Vector2(-1, 0),                     new Vector2(1, 0),
                             new Vector2(-1, -1),new Vector2(0, -1), new Vector2(1, -1) };
    private int[] cost = { 14, 10, 14, 10, 10, 14, 10, 14 };
    [SerializeField] private int arrayCount_x;
    [SerializeField] private int arrayCount_y;
    private int setIdx;
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
        closeNode = new List<UnitScript>();
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 위치를 기준으로 레이 생성
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // 레이캐스트를 통해 충돌된 오브젝트 검출
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
            Vector2 vec = curNode.nodeIdx + pos[i]; //가장 작은 노드부터 주변 노드 탐색

            //범위에 넘어가는지 예외처리
            if (vec.x > 0 || vec.x < arrayCount_x || vec.y > 0 || vec.y < arrayCount_y)
                OpenNode(GetNode(vec), i);
        }
        openNode.Remove(curNode);

        UnitScript lowUnit = openNode[0];
        foreach (UnitScript unit in openNode)
            if (lowUnit.f_f > unit.f_f)
                lowUnit = unit;

        curNode = lowUnit;
        if (curNode.type == UnitType.End)
        {
            curNode.type = UnitType.CloseNode;
            endNode.beforeNode = curNode;
            LinkNode();
            return;
        }

        curNode.type = UnitType.CloseNode;
        Invoke("StartAstar", 0.3f);
    }
    private void OpenNode(UnitScript node , int idx)
    {
        if (node == null)
            return;
        else if (node.type != UnitType.Unit)//일반 Unit이 아닐시 리턴
            return;

        openNode.Add(node);
        node.beforeNode = curNode;
        node.f_g = node.beforeNode.f_g + cost[idx];
        node.f_h = Mathf.Abs(endNode.nodeIdx.x - node.nodeIdx.x) + Mathf.Abs(endNode.nodeIdx.y - node.nodeIdx.y);
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
