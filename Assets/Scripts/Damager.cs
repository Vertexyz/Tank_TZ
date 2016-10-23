using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour {

    private GameObject owner;

    [SerializeField]
    public float Damage;

    [SerializeField]
    public float Radius;

    [SerializeField]
    private GameObject explosionPrefab;
    
    [HideInInspector]
    public GameObject Owner;

    private void OnCollisionEnter(Collision collision)
    {
        Destructable collisionTarget = collision.gameObject.GetComponent<Destructable>();

        if (collisionTarget && !GameObject.Equals(collision.gameObject, Owner))
        {
            if (Radius > 0)
            {
                CauseExplosionDamage();
            }
            else
            {
                collisionTarget.Hit(Damage);
            }
        }

        if (explosionPrefab != null)
            Explosion.Create(transform.position, explosionPrefab);

        ParticleSystem trail = gameObject.GetComponentInChildren<ParticleSystem>();
        if (trail != null)
        {
            Destroy(trail.gameObject, trail.startLifetime);
            trail.Stop();
            trail.transform.SetParent(null);
        }

        Destroy(gameObject);
    }

    private void CauseExplosionDamage()
    {
        Collider[] explosionVictims = Physics.OverlapSphere(transform.position, Radius);

        for (int i = 0; i < explosionVictims.Length; i++)
        {
            Vector3 vectorToVictim = explosionVictims[i].transform.position - transform.position;
            float decay = 1 - (vectorToVictim.magnitude / Radius);
            Destructable currentVictim = explosionVictims[i].gameObject.GetComponent<Destructable>();
            if (currentVictim != null && !GameObject.Equals(explosionVictims[i].gameObject, Owner))
            {
                currentVictim.Hit(Damage * decay);
            }
            Rigidbody victimRigidbody = explosionVictims[i].gameObject.GetComponent<Rigidbody>();
            if (victimRigidbody != null)
            {
                victimRigidbody.AddForce(vectorToVictim.normalized * decay * 50000);
            }
        }
    }
}
