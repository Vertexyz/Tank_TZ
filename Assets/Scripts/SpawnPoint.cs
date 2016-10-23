using UnityEngine;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour {

    [SerializeField]
    private List<Enemy> enemyPrefab;
    
    [SerializeField]
    private float spawnDelaySec = 10.0f;
    
    private Transform playerObject;

	void Start ()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("Spawn", 0.0f, spawnDelaySec);
	}

    public void Spawn()
    {
        if (GameController.Instance.EnemiesCount < GameController.Instance.MaxEnemiesCount)
        {
            Enemy newEnemy =
                Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], transform.position, Quaternion.identity) as
                    Enemy;
            newEnemy.Target = playerObject;

            GameController.Instance.EnemiesCount++;
        }
    }
}
