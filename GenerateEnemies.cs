using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{

    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;


    void Start()
    {
        enemy.SetActive(true);
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 2)
        {
            xPos = Random.Range(-83, 80);
            zPos = Random.Range(166, 229);
            Instantiate(enemy, new Vector3(xPos, 90, zPos), Quaternion.identity);
            yield return new WaitForSeconds(1);
            enemyCount += 1;
        }
    }

}
