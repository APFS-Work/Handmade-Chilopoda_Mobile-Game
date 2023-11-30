using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> EnemyPrefabs = new List<GameObject>();
    public List<GameObject> Enemys = new List<GameObject>();

    public static int EnemyNumber;

    int randomTypesOfEnemy;

    public int wave;

    public Transform RangeTopLeft, RangeBottomRight;

    Vector3 spawnVector;

    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        spawnEnemys();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyNumber <= 0)
        {
            Enemys.Add(Instantiate(EnemyPrefabs[0], new Vector3(-1000, EnemyPrefabs[0].transform.position.y, -1000), EnemyPrefabs[0].transform.rotation));
            EnemyNumber++;
            foreach (GameObject enemy in Enemys)
            { 
                enemy.transform.position = new Vector3(Random.Range(RangeTopLeft.position.x, RangeBottomRight.position.x), 0.0f, Random.Range(RangeBottomRight.position.z, RangeTopLeft.position.z));
                enemy.SetActive(true);
                Debug.Log("One Back");
                EnemyNumber++;
            }
        }
    }

    

    void spawnEnemys()
    {
        wave = 1;

        if (EnemyPrefabs.Count > 1)
        {
            for (int x = 0; x < PlayerStatus.PlayerLength * 3 + wave; x++)
            {
                randomTypesOfEnemy = Random.Range(0, EnemyPrefabs.Count - 1);
                spawnVector = new Vector3(Random.Range(RangeTopLeft.position.x, RangeBottomRight.position.x), 0.0f, Random.Range(RangeBottomRight.position.z, RangeTopLeft.position.z));
                Enemys.Add(Instantiate(EnemyPrefabs[randomTypesOfEnemy], spawnVector, EnemyPrefabs[randomTypesOfEnemy].transform.rotation));
                EnemyNumber++;
            }
        }
        else if (EnemyPrefabs.Count == 1)
        {
            for (int x = 0; x < PlayerStatus.PlayerLength * 3 + wave; x++)
            {
                spawnVector = new Vector3(Random.Range(RangeTopLeft.position.x, RangeBottomRight.position.x), EnemyPrefabs[0].transform.position.y, Random.Range(RangeBottomRight.position.z, RangeTopLeft.position.z));
                Enemys.Add(Instantiate(EnemyPrefabs[0], spawnVector, EnemyPrefabs[0].transform.rotation));
                EnemyNumber++;
            }
        }
    }
}
