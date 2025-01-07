using UnityEngine;
using UnityEngine.AI;

public class PoliceCarController : MonoBehaviour
{
    public Transform taxi;  // Reference to the taxi
    private NavMeshAgent agent;  // NavMeshAgent component
    public float speedLimit = 15f;  // Speed limit that the taxi must exceed for the police car to start following it
    private CarController taxiController;  // Taxi's Rigidbody to check its speed
    private bool isChasing = false;  // Flag to determine if the police car has started chasing the taxi
    public float captureDistance = 5f; // Distance at which the police captures the taxi
    GameManager gameManager;
    [SerializeField] Radar radar;
    private void Start()
    {
        // Get the NavMeshAgent component
        gameManager = FindObjectOfType<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on Police Car.");
        }

        // Get the taxi's Rigidbody component to track its speed
        if (taxi != null)
        {
            taxiController = taxi.GetComponent<CarController>();
        }
        else
        {
            Debug.LogError("Taxi not assigned.");
        }
    }

    private void Update()
    {
        // Ensure that the taxi and the NavMeshAgent are assigned
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

    private void CaptureTaxi()
    {
        Debug.Log("¡La policía capturó al taxi!");

        // Stop the police car
        agent.isStopped = true;

        // Optionally, stop the taxi as well (you can add your own logic here)
        Rigidbody taxiRB = taxi.GetComponent<Rigidbody>();
        if (taxiRB != null)
        {
            taxiRB.velocity = Vector3.zero;
        }

        // End the chase
        isChasing = false;
        gameManager.HandlePoliceCapture();
    }
}

