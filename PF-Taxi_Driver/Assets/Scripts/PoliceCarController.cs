using UnityEngine;
using UnityEngine.AI;

public class PoliceCarController : MonoBehaviour
{
    public Transform taxi;  // Reference to the taxi
    private NavMeshAgent agent;  // NavMeshAgent component
    public float speedLimit = 15f;  // Speed limit that the taxi must exceed for the police car to start following it
    private Rigidbody taxiRigidbody;  // Taxi's Rigidbody to check its speed
    private bool isChasing = false;  // Flag to determine if the police car has started chasing the taxi
    public float captureDistance = 5f; // Distance at which the police captures the taxi
    GameManager gameManager;
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
            taxiRigidbody = taxi.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Taxi not assigned.");
        }
    }

    private void Update()
    {
        // Ensure that the taxi and the NavMeshAgent are assigned
        if (taxi != null && agent != null && taxiRigidbody != null)
        {
            float taxiSpeed = taxiRigidbody.velocity.magnitude;  // Speed in meters per second
            // Debugging the taxi's speed
            // Debug.Log("Taxi Speed: " + taxiSpeed.ToString("F2") + " m/s");

            // Check if the taxi's speed exceeds the speed limit
            if (taxiSpeed > speedLimit && !isChasing)
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
                Debug.Log(distance);
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




//using UnityEngine;

//public class PoliceCarController : CarController 
//{
//    public Transform taxi; // Referencia al taxi
//    public float followDistance = 5f; // Distancia mínima antes de reducir velocidad
//    public float stopDistance = 1.5f; // Distancia para capturar el taxi
//    public float maxSpeed = 20f; // Velocidad máxima del coche de policía

//    private CarController carController;

//    private void Start()
//    {
//        carController = GetComponent<CarController>();
//        if (carController == null)
//        {
//            Debug.LogError("No se encontró el componente CarController en el coche de policía.");
//        }
//    }

//    private void Update()
//    {
//        FollowTaxi();
//    }

//    private void FollowTaxi()
//    {
//        if (taxi == null || carController == null) return;

//        // Calcular dirección hacia el taxi
//        Vector3 direction = (taxi.position - transform.position).normalized;

//        // Calcular la distancia al taxi
//        float distance = Vector3.Distance(transform.position, taxi.position);

//        // Si el coche está dentro de la distancia mínima, captura el taxi
//        if (distance <= stopDistance)
//        {
//            CaptureTaxi();
//            return;
//        }

//        // Ajustar velocidad y dirección según la distancia
//        carController.gasInput = distance > followDistance ? 1f : 0.5f; // Acelerar más lejos
//        carController.steeringInput = CalculateSteeringInput(direction);
//        carController.brakeInput = 0f; // No frenar mientras sigue al taxi
//    }

//    private float CalculateSteeringInput(Vector3 direction)
//    {
//        // Convertir la dirección al espacio local del coche
//        Vector3 localDirection = transform.InverseTransformDirection(direction);

//        // Calcular el ángulo para girar hacia el taxi
//        float steering = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;

//        // Normalizar el ángulo para un rango entre -1 y 1
//        steering = Mathf.Clamp(steering / 45f, -1f, 1f);

//        Debug.Log($"Steering Input: {steering}");
//        return steering;
//    }

//    private void CaptureTaxi()
//    {
//        Debug.Log("¡La policía capturó al taxi!");
//        carController.gasInput = 0f; // Detener el coche
//        carController.brakeInput = 1f;

//        // Opcional: lógica adicional para detener el taxi
//        Rigidbody taxiRB = taxi.GetComponent<Rigidbody>();
//        if (taxiRB != null)
//        {
//            taxiRB.velocity = Vector3.zero;
//        }
//    }
//}
