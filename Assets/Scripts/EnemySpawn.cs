using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] Enemy;
    public GameObject SpawnPosition;
    public GameObject TheCity;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        index = Random.Range(0, Enemy.Length);
        var EnemySpawn = Instantiate(Enemy[index], SpawnPosition.transform.position, Quaternion.identity);
        EnemySpawn.transform.SetParent(TheCity.transform);
        yield return new WaitForSeconds(30f);
        StartCoroutine(SpawnEnemy());
    }
}
