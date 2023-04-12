using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemyObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> spawnPos = new List<GameObject>();
    [SerializeField]
    private float spawnTime;
    private float timer;
    private void Update()
    {
        Spawn();
    }
    void Spawn()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime)
        {
            SpawnPattern(Random.Range(0 ,5));
            timer = 0;
        }
    }
    void SpawnPattern(int patternIdx)
    {
        GameObject enemy;
        switch (patternIdx)
        {
            case 0:
                for(int idx = 0; idx < 5;idx++)
                    Instantiate(enemyObjects[0], spawnPos[idx].transform.position, Quaternion.Euler(0, 0, 0));
                break;
            case 1:
                for (int idx = 0; idx < 5; idx++)
                    Instantiate(enemyObjects[idx % 2], spawnPos[idx].transform.position, Quaternion.Euler(0, 0, 0));
                break;
            case 2:
                for (int idx = 0; idx < 2; idx++)
                { 
                    enemy = Instantiate(enemyObjects[2], spawnPos[idx + 5].transform.position, Quaternion.Euler(0, 0, 90 - 180 * idx));
                    enemy.GetComponent<SphereAttackEnemy>().movePattern = idx + 1;
                }
                break;
            case 3:
                for(int idx = 0; idx < 2; idx++)
                   Instantiate(enemyObjects[2], spawnPos[idx + 7].transform.position, Quaternion.Euler(0, 0,90 - 180 * idx));
                break;
            case 4:
                for (int idx = 0; idx < 2; idx++)
                {
                    Instantiate(enemyObjects[3], spawnPos[0 + (4*idx)].transform.position, Quaternion.Euler(0, 0, 0));
                }
                break;
        }
    }
}
