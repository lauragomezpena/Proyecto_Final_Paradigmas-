using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachDestination : MonoBehaviour
{
    MoneyManager moneyManager;
    GameManager gameManager;    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        moneyManager = FindObjectOfType<MoneyManager>();
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        CarController carController = other.GetComponent<CarController>();
        if (carController != null) // Si no es null, es del tipo CarController
        {
            Debug.Log("Taxi reached the destination!");

            moneyManager.Deposit(100);
            gameManager.HandleTaxiArrival();

        }

    }


}
