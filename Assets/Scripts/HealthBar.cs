using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;

    [SerializeField]
    private Destructable owner;

    void Start ()
    {
        healthBar = gameObject.GetComponent<Image>();
    }
	
	void Update ()
	{
        transform.forward = Camera.main.transform.position - transform.position;

        healthBar.fillAmount = Mathf.InverseLerp(0.0f, owner.HitPointsMax, owner.HitPoints);
	}
}
 