using UnityEngine;
using UnityEngine.AI;

public class PoliceCarController : MonoBehaviour
{
    private Rigidbody policeRB;

    public Transform taxi;  // Reference to the taxi
    private CarController taxiController;

    [SerializeField] Radar radar;
    private NavMeshAgent agent;  // NavMeshAgent component
    public float speedLimit = 15f;  // Speed limit that the taxi must exceed for the police car to start following it
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


            // Check if the taxi's speed exceeds the speed limit
            if (radar.TriggerRadar(taxiController) && !isChasing)
            {
                // If taxi exceeds speed limit, start chasing
                isChasing = true;
                Debug.Log("Police car starts chasing the taxi!");
            }

            // If the police car has started chasing, keep following the taxi
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
        Debug.Log("¡La policía capturó al taxi!");


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

