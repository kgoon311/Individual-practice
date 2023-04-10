using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    [SerializeField] private UnitScript unitBase;
    [SerializeField] private UnitScript[,] unitArray;
    [SerializeField] private int arrayCount_x;
    [SerializeField] private int arrayCount_y;
    
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        unitArray = new UnitScript[arrayCount_x, arrayCount_y];
        for(int i = 0; i <= arrayCount_x;i++)
        {
            for(int j = 0; j <= arrayCount_y;j++)
                unitArray[i, j] = Instantiate(unitBase , new Vector3(i - arrayCount_x/2, j - arrayCount_y / 2, 0) , transform.rotation);
        }
    }
    void Update()
    {
        
    }
}
