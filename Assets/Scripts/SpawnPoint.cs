using UnityEngine;
using System.Collections;
using EnemyList = System.Collections.Generic.List<Enemy>;

public class SpawnPoint : MonoBehaviour {

    [SerializeField]
    private EnemyList enemyPrefab;
    
    [SerializeField]
    private float spawnDelaySec = 10.0f;
    
    private Transform playerObject;

	void Start ()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(SpawnCo());
	}

    private IEnumerator SpawnCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelaySec);

            if (GameController.Instance.EnemiesCount < GameController.Instance.MaxEnemiesCount)
            {
                Spawn();
            }
        }
    }

    public void Spawn()
    {
        Enemy newEnemy =
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], transform.position, Quaternion.identity) as
                Enemy;
        newEnemy.Target = playerObject;

        GameController.Instance.EnemiesCount++;
    }
}
