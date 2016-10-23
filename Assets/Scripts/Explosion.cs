using UnityEngine;
using System.Collections;

public class Explosion
{

    public static void Create(Vector3 position, GameObject prefab)
    {
        GameObject newExplosion = MonoBehaviour.Instantiate(prefab, position, Quaternion.identity) as GameObject;
        MonoBehaviour.Destroy(newExplosion, newExplosion.GetComponent<ParticleSystem>().startLifetime);
    }
}
