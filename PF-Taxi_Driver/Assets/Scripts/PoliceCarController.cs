using UnityEngine;
using UnityEngine.AI;

public class PoliceCarController : MonoBehaviour
{
    public Transform taxi;  // Referencia al taxi
    private NavMeshAgent agent;  // Componente NavMeshAgent

    private void Start()
    {
        // Obtener el componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("El componente NavMeshAgent no se encuentra en el coche de polic�a.");
        }
    }

    private void Update()
    {
        // Asegurarse de que el taxi est� asignado
        if (taxi != null && agent != null)
        {
            // Hacer que el coche de polic�a siga al taxi
            agent.SetDestination(taxi.position);
        }
    }
}



//using UnityEngine;

//public class PoliceCarController : CarController 
//{
//    public Transform taxi; // Referencia al taxi
//    public float followDistance = 5f; // Distancia m�nima antes de reducir velocidad
//    public float stopDistance = 1.5f; // Distancia para capturar el taxi
//    public float maxSpeed = 20f; // Velocidad m�xima del coche de polic�a

//    private CarController carController;

//    private void Start()
//    {
//        carController = GetComponent<CarController>();
//        if (carController == null)
//        {
//            Debug.LogError("No se encontr� el componente CarController en el coche de polic�a.");
//        }
//    }

//    private void Update()
//    {
//        FollowTaxi();
//    }

//    private void FollowTaxi()
//    {
//        if (taxi == null || carController == null) return;

//        // Calcular direcci�n hacia el taxi
//        Vector3 direction = (taxi.position - transform.position).normalized;

//        // Calcular la distancia al taxi
//        float distance = Vector3.Distance(transform.position, taxi.position);

//        // Si el coche est� dentro de la distancia m�nima, captura el taxi
//        if (distance <= stopDistance)
//        {
//            CaptureTaxi();
//            return;
//        }

//        // Ajustar velocidad y direcci�n seg�n la distancia
//        carController.gasInput = distance > followDistance ? 1f : 0.5f; // Acelerar m�s lejos
//        carController.steeringInput = CalculateSteeringInput(direction);
//        carController.brakeInput = 0f; // No frenar mientras sigue al taxi
//    }

//    private float CalculateSteeringInput(Vector3 direction)
//    {
//        // Convertir la direcci�n al espacio local del coche
//        Vector3 localDirection = transform.InverseTransformDirection(direction);

//        // Calcular el �ngulo para girar hacia el taxi
//        float steering = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;

//        // Normalizar el �ngulo para un rango entre -1 y 1
//        steering = Mathf.Clamp(steering / 45f, -1f, 1f);

//        Debug.Log($"Steering Input: {steering}");
//        return steering;
//    }

//    private void CaptureTaxi()
//    {
//        Debug.Log("�La polic�a captur� al taxi!");
//        carController.gasInput = 0f; // Detener el coche
//        carController.brakeInput = 1f;

//        // Opcional: l�gica adicional para detener el taxi
//        Rigidbody taxiRB = taxi.GetComponent<Rigidbody>();
//        if (taxiRB != null)
//        {
//            taxiRB.velocity = Vector3.zero;
//        }
//    }
//}
