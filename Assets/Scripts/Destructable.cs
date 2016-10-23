using UnityEngine;

public delegate void Destroyed();

public class Destructable : MonoBehaviour {

    [SerializeField]
    public float HitPointsMax = 100;

    [Range(0.0f, 1.0f)]
    public float Defence = 0;

    [HideInInspector]
    public float HitPoints;

    public event Destroyed OnDestroyed;
    
    void Start()
    {
        HitPoints = HitPointsMax;
    }
	
    public void Hit(float damage)
    {
        HitPoints -= damage - (damage * Defence);
        
        if (HitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (OnDestroyed != null)
            OnDestroyed();

        Destroy(gameObject);
    }
}
