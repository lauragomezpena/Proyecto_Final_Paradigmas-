using UnityEngine;

public class Obstacle : MonoBehaviour
{
    //public bool followingTaxi;
    //public bool solidObject;
    [SerializeField] public int lifeLoss;
    [SerializeField] public int money;
    [SerializeField] public float velocityMult;
    [SerializeField] public float time;

    MoneyManager moneyManager;
    LifeManager lifeManager;

    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        lifeManager = FindObjectOfType<LifeManager>();

    }

     void OnTriggerEnter(Collider other)
    {
        Debug.Log("El objeto que ha entrado es: " + other.gameObject.name);

        CarController carController = other.GetComponent<CarController>();
        if (carController != null)
        {
            Debug.Log("HA DADO AL OBSTACLE");
            lifeManager.DecreaseLife(lifeLoss);
            moneyManager.Deposit(money);
        }
    }

}
