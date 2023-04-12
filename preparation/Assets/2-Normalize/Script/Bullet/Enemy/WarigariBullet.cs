using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarigariBullet : EnemyBullet
{
    private bool turnOver;
    private const float turnTime = 0.3f;
    private const float turnRotate = 60f;
    private void Start()
    {
        StartCoroutine(C_TurnOver());
    }
    private IEnumerator C_TurnOver()
    {
        transform.Rotate((turnOver == false)
            ? new Vector3(0, 0, turnRotate)
            : new Vector3(0, 0, -turnRotate));
            
        yield return new WaitForSeconds(turnTime);

        turnOver = !turnOver;

        yield return StartCoroutine(C_TurnOver());   
    }
}
