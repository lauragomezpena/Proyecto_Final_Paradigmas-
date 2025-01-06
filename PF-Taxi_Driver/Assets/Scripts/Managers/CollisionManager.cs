using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    CarController carController;
    MoneyManager moneyManager;
    LifeManager lifeManager;
    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
        moneyManager = FindObjectOfType<MoneyManager>();
        lifeManager = FindObjectOfType<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void CollisionObstacle(Obstacle obstacle)
    {

        Debug.Log("HA DADO AL OBSTACLE");
        lifeManager.DecreaseLife(obstacle.lifeLoss);
        moneyManager.Deposit(obstacle.money);
        carController.ModifyVelocity(obstacle.velocityMult);



    }
    void OnCollisionEnter(Collision other)
    {
        // Check if the object has a ConstructionFence component
        Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {

            CollisionObstacle(obstacle);
        }

        else
        {// si se choca con otra cosa decrece la vida 5 
            lifeManager.DecreaseLife(5);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {

            CollisionObstacle(obstacle);
            if (!obstacle.solidObject)

            {
                Debug.Log(" MONEDA");
                other.gameObject.SetActive(false);

            }
        }
    }
}
