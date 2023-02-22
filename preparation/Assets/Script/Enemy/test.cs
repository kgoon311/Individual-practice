using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private GameObject moveObject;
    [Range(0f, 1f)] public float pos;
    [SerializeField] private Transform[] transformGroup;
    [SerializeField] private Vector2[] vector2s = new Vector2[4];
    void Start()
    {
        for(int i = 0;i<4;i++)
        {
            vector2s[i] = transformGroup[i].position;
        }
    }
    private void Update()
    {
        transform.position = BezierCurve(vector2s[0], vector2s[1], vector2s[2], vector2s[3],pos);
    }

    Vector2 BezierCurve(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
    {
        Vector2 ab = Vector2.Lerp(a, b, t);
        Vector2 bc = Vector2.Lerp(b, c, t);
        Vector2 cd = Vector2.Lerp(c , d, t);

        Vector2 abbc = Vector2.Lerp(ab, bc, t);
        Vector2 bccd = Vector2.Lerp(bc, cd, t);

        return Vector2.Lerp(abbc, bccd, t);
    }
}
