using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entty
{
    [SerializeField] protected float itmeSpawnPercentage;
    protected override void Update()
    {
        base.Update();
        Move();
        Shoot();
    }
    protected virtual void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < -10)
            Destroy(gameObject);
    }
    protected virtual void Shoot()
    {
        attackDelay += Time.deltaTime * attackSpeed;
        if (attackDelay > 1)
        {
            StartCoroutine(Attack());
            attackDelay = 0;
        }
    }
    protected virtual IEnumerator Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180));
        yield return null;
    }
    protected override void Dead()
    {
        if (Random.Range(0, 101) < itmeSpawnPercentage)
            Manager.instance.ItemSpawn(transform);
        Destroy(gameObject);
    }
}
