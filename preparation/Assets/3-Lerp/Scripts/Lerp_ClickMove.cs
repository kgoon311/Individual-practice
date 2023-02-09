using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp_ClickMove : MonoBehaviour
{
    Coroutine coroutine;
    [SerializeField] private float speed;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(C_Move());
        }
    }
    IEnumerator C_Move()
    {
        Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 beforePos = transform.position;

        float dis = Vector2.Distance(beforePos, targetPos);
        float timer = 0;
        while(timer < 1)
        {
            timer += Time.deltaTime / dis * speed;
            transform.position = Vector2.Lerp(beforePos, targetPos, timer);
            yield return null;
        }
            yield return null;
    }
}
