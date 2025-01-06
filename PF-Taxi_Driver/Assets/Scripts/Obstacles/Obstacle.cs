using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool followingTaxi;
    public bool solidObject;
    [SerializeField] public int lifeLoss;
    [SerializeField] public int money;
    [SerializeField] public float velocityMult=1.0f;
    [SerializeField] public float time;
}
//MoneyManager moneyManager;
//LifeManager lifeManager;

//void Start()
//{
//    moneyManager = FindObjectOfType<MoneyManager>();
//    lifeManager = FindObjectOfType<LifeManager>();

//}

// void OnTriggerEnter(Collider other)
//{

//    CarController carController = other.GetComponent<CarController>();
//    if (carController != null)
//    {
//        Debug.Log("HA DADO AL OBSTACLE");
//        lifeManager.DecreaseLife(lifeLoss);
//        moneyManager.Deposit(money);
//        if (!solidObject){
//            gameObject.SetActive(false);
//        }
//}



