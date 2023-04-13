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

    //주변 노드를 탐색할 때 사용됩니다.
    private Vector2[] pos = {new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1),
                             new Vector2(-1, 0),                     new Vector2(1, 0),
                             new Vector2(-1, -1),new Vector2(0, -1), new Vector2(1, -1) };

    private int[] cost = { 14, 10, 14, 10, 10, 14, 10, 14 };//일반 이동 : 10 , 대각 이동 : 14
    [SerializeField] private int arrayCount_x;
    [SerializeField] private int arrayCount_y;

    private int setIdx;//노드를 설정할때 사용됩니다
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
            //가장 F가 작은 노드부터 주변 노드 탐색
            Vector2 vec = curNode.nodeIdx + pos[i]; 

            //범위에 넘어가는지 예외처리
            if (vec.x < 0 || vec.x >= arrayCount_x || vec.y < 0 || vec.y >= arrayCount_y)
                continue;

            //주변 노드 오픈
            OpenNode(GetNode(vec), i);
        }
        openNode.Remove(curNode);

        //주변 노드에 End노드가 있을 시 종료 후 시작지점과 연결
        if (isEnd)
        {
            endNode.beforeNode = curNode;
            LinkNode();
            return;
        }

        //노드중 F가 적은 노드탐색
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
        else if (node.type == UnitType.End) //열린 노드가 End Node일 경우 종료
        {
            isEnd = true;
            return;
        }
        else if (node.type != UnitType.Unit)//일반 Unit이 아닐시 리턴
            return;

        openNode.Add(node);
        node.beforeNode = curNode;
        //이동까지 걸리는 코스트 더하기
        node.f_g = node.beforeNode.f_g + cost[idx];
        //END지점까지 예외 없이 대략적인 거리
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
