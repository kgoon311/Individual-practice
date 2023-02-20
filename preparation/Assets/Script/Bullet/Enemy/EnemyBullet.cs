using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Entty>().OnHit(damage);
            //Instantiate(hitEffect, transform.position, transform.rotation);
            //Destroy(gameObject);
        }
    }
}
