using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float Damage;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    public float Radius;

    [SerializeField]
    private Destructable destructable;

    [HideInInspector]
    public Transform Target;

    private NavMeshAgent navMeshAgent;

    private Boolean seeTarget;

    void Awake()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        destructable = gameObject.GetComponent<Destructable>();
    }

    void Start()
    {
        destructable.OnDestroyed += Destroyed;
    }

    void Update()
    {
        if (Target)
        {
            navMeshAgent.SetDestination(Target.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Target != null && !GameObject.Equals(collision.gameObject, Target.gameObject))
            return;

        Destructable collisionTarget = collision.gameObject.GetComponent<Destructable>();

        if (collisionTarget)
            collisionTarget.Hit(Damage);

        if (explosionPrefab != null)
            Explosion.Create(transform.position, explosionPrefab);

        Die();
    }

    private void Destroyed()
    {
        ScoreLabel.score += 1;
        Die();
    }

    private void Die()
    {
        GameController.Instance.EnemiesCount--;

        destructable.OnDestroyed -= Destroyed;

        Destroy(gameObject);
    }
}
