using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrollMap : MonoBehaviour
{
    [SerializeField] private GameObject[] maps = new GameObject[2];

    [SerializeField] private float bgScale;
    [SerializeField] private float scrollSpeed;
    void Update()
    {
        ScrollMap();
    }
    void ScrollMap()
    {
        for(int i = 0; i < 3; i++)
        {
            maps[i].transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
            if(maps[i].transform.position.y <= -bgScale-5) maps[i].transform.position += new Vector3(0, bgScale*3, 0);
        }
    }
}
