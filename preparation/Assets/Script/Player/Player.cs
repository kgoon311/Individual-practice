using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entty
{
    [SerializeField] private Vector2[] clampPos;

    [SerializeField] private int attackIdx = 0;

    [Header("Drone")]
    [SerializeField] private GameObject targetingObject;

    [SerializeField] private GameObject[] droneGroups = new GameObject[5];
    [SerializeField] private GameObject droneObject;
    [SerializeField] private GameObject droneBullet;
    [SerializeField] private int droneCount = 0;

    [SerializeField] private GameObject gameOverPanel;
    protected override void Update()
    {
        base.Update();
        Move();
        Shoot();
        Targetting();
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(x, y) * speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampPos[0].x, clampPos[1].x),
                                         Mathf.Clamp(transform.position.y, clampPos[0].y, clampPos[1].y), 0);
    }
    private void Shoot()
    {
        attackDelay += Time.deltaTime * attackSpeed;
        if (attackDelay > 1)
        {
            Pattern(attackIdx);
            for (int droneIdx = 0; droneIdx < droneCount; droneIdx++)
            {
                Instantiate(droneBullet, droneGroups[droneIdx].transform.position, droneGroups[droneIdx].transform.localRotation);
            }
            attackDelay = 0;
        }

    }
    private void Targetting()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            targetingObject = Physics2D.Raycast(transform.position, Vector2.up, 100, LayerMask.GetMask("Enemy")).transform.gameObject;
        }

        if (targetingObject == null)
        {
            for (int droneIdx = 0; droneIdx < droneCount; droneIdx++)
            {
                droneGroups[droneIdx].transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            for (int i = 0; i < droneCount; i++)
            {
                Vector2 dir = targetingObject.transform.position - droneGroups[i].transform.position;
                float deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                droneGroups[i].transform.rotation = Quaternion.Euler(0, 0, deg - 90f);
            }
            if (targetingObject.transform.position.y < -4) targetingObject = null;
        }

    }
    private void Pattern(int idx)
    {
        Bullet bulletObject;
        switch (idx)
        {
            case 0:
                bulletObject = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Bullet>();
                bulletObject.damage = this.damage;
                break;
            case 1:
                for (int i = -1; i < 2; i += 2)
                {
                    bulletObject = Instantiate(bullet, transform.position + Vector3.right * i * 0.5f, Quaternion.Euler(0, 0, 0)).GetComponent<Bullet>();
                    bulletObject.damage = this.damage;
                }
                break;
            case 2:
                for (int i = -1; i < 2; i++)
                {
                    bulletObject = Instantiate(bullet, transform.position + Vector3.right * i * 0.5f, Quaternion.Euler(0, 0, 0)).GetComponent<Bullet>();
                    bulletObject.damage = this.damage;
                    if (i == 0)
                    {
                        bulletObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
                        bulletObject.transform.localScale += Vector3.up / 2;
                        bulletObject.transform.position += Vector3.up / 2;
                        bulletObject.damage = this.damage * 2;
                    }
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Drone"))
        {
            GetDrone();
            Destroy(collision.gameObject);
        }
    }
    private void GetDrone()
    {
        if (droneCount < 4)
        {
            droneGroups[droneCount] = Instantiate(droneObject, transform.position, transform.rotation, transform);
            droneCount += 1;
            StartCoroutine(DroneMove());
        }
    }
    private IEnumerator DroneMove()
    {
        float moveTimer = 0;
        switch (droneCount)
        {
            case 1:
                while (moveTimer < 1)
                {
                    moveTimer += Time.deltaTime;
                    droneGroups[0].transform.position = Vector3.Lerp(droneGroups[0].transform.position, transform.position + Vector3.left, moveTimer);
                    yield return null;
                }
                break;
            case 2:
                while (moveTimer < 1)
                {
                    moveTimer += Time.deltaTime;
                    droneGroups[1].transform.position = Vector3.Lerp(droneGroups[1].transform.position, transform.position + Vector3.right, moveTimer);
                    yield return null;
                }
                break;
            case 3:
                while (moveTimer < 1)
                {
                    moveTimer += Time.deltaTime;
                    droneGroups[2].transform.position = Vector3.Lerp(droneGroups[2].transform.position, transform.position + Vector3.left * 2, moveTimer);
                    yield return null;
                }
                break;
            case 4:
                while (moveTimer < 1)
                {
                    moveTimer += Time.deltaTime;
                    droneGroups[3].transform.position = Vector3.Lerp(droneGroups[3].transform.position, transform.position + Vector3.right * 2, moveTimer);
                    yield return null;
                }
                break;
        }

        yield return null;
    }

    protected override void Dead()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
