using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField]
    public float MoveSpeed = 12f;
    [SerializeField]
    public float TurnSpeed = 180f;

    [SerializeField]
    private List<GameObject> rocketPrefabs;
    
    [SerializeField]
    private float shootPower = 10000.0f;

    [SerializeField]
    private Transform gun;

    [SerializeField]
    private Destructable destructable;

    private Rigidbody playerRigidbody;

    private float movementInputValue;
    private float turnInputValue;

    private int CurrentWearpon;

    private void Awake ()
    {
        playerRigidbody = GetComponent<Rigidbody> ();
        destructable = GetComponent<Destructable>();
    }

    private void Start()
    {
        destructable.OnDestroyed += Destroyed;
    }

    private void Update ()
    {
        movementInputValue = Input.GetAxis ("Vertical");
        turnInputValue = Input.GetAxis ("Horizontal");
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            CurrentWearpon++;
            if (CurrentWearpon >= rocketPrefabs.Count)
                CurrentWearpon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CurrentWearpon--;
            if (CurrentWearpon < 0)
                CurrentWearpon = rocketPrefabs.Count-1;
        }
    }
        
    private void FixedUpdate ()
    {
        Move ();
        Turn ();
    }


    private void Move ()
    {
        Vector3 movement = transform.forward * movementInputValue * MoveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }
        
    private void Turn ()
    {
        float turn = turnInputValue * TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

        playerRigidbody.MoveRotation (playerRigidbody.rotation * turnRotation);
    }

    private void Shoot()
    {
        GameObject newRocket = Instantiate(rocketPrefabs[CurrentWearpon], gun.position, gun.rotation) as GameObject;
        newRocket.GetComponent<ConstantForce>().force = gun.forward*shootPower;

        Damager bulletBehaviour = newRocket.GetComponent<Damager>();
        bulletBehaviour.Owner = gameObject;

        Destroy(newRocket, 3f);
    }
    
    private void Destroyed()
    {
        destructable.OnDestroyed -= Destroyed;

        GameController.Instance.RestartGame();
    }
}