using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject hitEffect;
    [SerializeField] protected float speed;
    public float damage;
    public float destroyTime;
    private float timer;

    protected virtual void Update()
    {
        Move();
        timer += Time.deltaTime;
        if(timer > destroyTime)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void Move()
    {
        transform.Translate(Vector3.up *speed * Time.deltaTime);
    }
}
