using UnityEngine;

public class Coin : MonoBehaviour
{
    //public bool followingTaxi;
    //public bool solidObject;
    [SerializeField] public int points;
    [SerializeField] public float velocityMult;
    [SerializeField] public float time;

    MoneyManager moneyManager;
    LifeManager lifeManager;

    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        lifeManager = FindObjectOfType<LifeManager>();
        Debug.Log("OBSTACULO");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("El objeto que ha entrado es: " + other.gameObject.name);

        CarController carController = other.GetComponent<CarController>();
        if (carController != null)
        {
            Debug.Log("HA DADO AL OBSTACLE");
            lifeManager.DecreaseLife(points);
        }
    }

}
