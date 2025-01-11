using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    CarController carController;
    MoneyManager moneyManager;
    LifeManager lifeManager;


    public event Action<int> onCollision; // evento para dar la propina al taxi

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

        onCollision?.Invoke(obstacle.lifeLoss);
        moneyManager.Deposit(obstacle.money);
        carController.ModifyVelocity(obstacle.velocityMult);

    }

    // para colision con ConstructionFence
    void OnCollisionEnter(Collision other)
    {
        Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {

            CollisionObstacle(obstacle);
        }

        else
        {
            // si se choca con otra cosa decrece la vida 5 
            lifeManager.DecreaseLife(5);
        }
    }

    // para colisión con monedas
    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {

            CollisionObstacle(obstacle);
            if (!obstacle.solidObject)

            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
