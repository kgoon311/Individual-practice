using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAttackEnemy : Enemy
{
    [SerializeField] 
        private float shootCount;
    [HideInInspector]
        public int movePattern;

    private float timer = 0;
    [SerializeField]
    private float deadTime = 2;
    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if(timer > deadTime)
        {
            Destroy(gameObject);
        }
    }
    protected override void Move()
    {
        switch(movePattern)
        {
            case 0:
                transform.position += Vector3.down * speed * Time.deltaTime;
                transform.Rotate(new Vector3(0, 0, 80 * Time.deltaTime));
                break;
            case 1:
                transform.position += Vector3.right * speed * Time.deltaTime;
                transform.Rotate((new Vector3(0, 0, 100 * Time.deltaTime)));
                break;
            case 2:
                transform.position += Vector3.left * speed * Time.deltaTime;
                transform.Rotate((new Vector3(0, 0, 100 * Time.deltaTime)));
                break;
        }
    }
    protected override IEnumerator Attack()
    {
        for(int i = 1; i <= shootCount; i++)
        { 
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, (360 / shootCount) * i));
        }

        yield return null;
    }
}
