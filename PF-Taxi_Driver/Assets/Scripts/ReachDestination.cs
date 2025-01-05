using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachDestination : MonoBehaviour
{
    MoneyManager moneyManager;
    // Start is called before the first frame update
    void Start()
    {        moneyManager = FindObjectOfType<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("El objeto que ha entrado es: " + other.gameObject.name);
        // Check if the object entering the trigger has the TaxiController component
        //CarController taxi = FindObjectOfType<CarController>();
        CarController carController = other.GetComponent<CarController>();
        if (carController != null) // Si no es null, es del tipo CarController
        {
            Debug.Log("Taxi reached the destination!");

            moneyManager.Deposit(100);

            // Notifica al DestinationManager destinationManager.HandleTaxiArrival();

            // Desactiva este destino gameObject.SetActive(false);
        }

    }
}
