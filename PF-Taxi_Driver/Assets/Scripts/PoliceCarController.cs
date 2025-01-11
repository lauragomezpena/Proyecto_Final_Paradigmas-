using UnityEngine;
using UnityEngine.AI;

public class PoliceCarController : MonoBehaviour
{
    private Rigidbody policeRB;

    public Transform taxi;  // Reference to the taxi
    private CarController taxiController;

    [SerializeField] Radar radar;
    private NavMeshAgent agent;  // NavMeshAgent component

    private bool isChasing = false;  // Flag to determine if the police car has started chasing the taxi
    public float captureDistance = 5f; // Distance at which the police captures the taxi
    GameManager gameManager;
    

    private void Start()
    {
        policeRB = gameObject.GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();

        gameManager.onFinishRide += StopPoliceCar;


        if (taxi != null)
        {
            taxiController = taxi.GetComponent<CarController>();
        }
      
    }

    private void Update()
    {
        if (taxi != null && agent != null && taxiController != null)
        {

            if (radar.TriggerRadar(taxiController) && !isChasing)
            {
                //  start chasing
                isChasing = true;
                Debug.Log("Police car starts chasing the taxi!");
            }

            //  keep following the taxi
            if (isChasing)
            {
                agent.SetDestination(taxi.position);

                float distance = Vector3.Distance(transform.position, taxi.position);

                if (distance <= captureDistance)
                {
                    CaptureTaxi();
                }
            }
        }
    }

    public void CaptureTaxi()
    {
        gameManager.HandlePoliceCapture();
    }

    public void StopPoliceCar()
     {
        agent.isStopped = true;

        policeRB.velocity = Vector3.zero;

        agent.ResetPath();               
        isChasing = false;

    }
}

