using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{


    public bool followingTaxi;
    public bool solidObject;
    public int points;
    public float velocityMult;
    public float time;
    [SerializeField] GameObject obstaclePrefab;
    public Obstacle(bool followingTaxi, bool solidObject, int points, float velocityMult, float time)
    {
        this.followingTaxi = followingTaxi;
        this.solidObject = solidObject;
        this.points = points;
        this.velocityMult = velocityMult;
        this.time = time;


    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("El objeto que ha entrado es: " + other.gameObject.name);
        // Check if the object entering the trigger has the TaxiController component
        //CarController taxi = FindObjectOfType<CarController>();
        CarController carController = other.GetComponent<CarController>();
        if (carController != null) // Si no es null, es del tipo CarController
        {
            Debug.Log("HA DADO AL OBSTACLE");

            // Notifica al DestinationManager destinationManager.HandleTaxiArrival();

            // Desactiva este destino gameObject.SetActive(false);
        }

    }
    // Start is called before the first frame update
    void Start()
        {
        Debug.Log("hola");
        Instantiate(obstaclePrefab, new Vector3(0, -1,0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
