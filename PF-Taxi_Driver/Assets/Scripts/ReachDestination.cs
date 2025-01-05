using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachDestination : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("El objeto que ha entrado es: " + other.gameObject.name);
        // Check if the object entering the trigger has the TaxiController component
        //CarController taxi = FindObjectOfType<CarController>();
        CarController carController = other.GetComponent<CarController>();
        if (carController != null) // Si no es null, es del tipo CarController
        {
            Debug.Log("Taxi reached the destination!");

            // Notifica al DestinationManager destinationManager.HandleTaxiArrival();

            // Desactiva este destino gameObject.SetActive(false);
        }

    }
}
