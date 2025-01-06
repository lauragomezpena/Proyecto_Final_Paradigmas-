//using UnityEngine;

//public class PoliceCarController : MonoBehaviour
//{
//    public Transform taxi; // Referencia al taxi
//    public float followDistance = 5f; // Distancia m�nima antes de reducir velocidad
//    public float stopDistance = 1.5f; // Distancia para capturar el taxi
//    public float maxSpeed = 20f; // Velocidad m�xima del police car

//    private CarController carController;

//    private void Start()
//    {
//        carController = GetComponent<CarController>();
//        if (carController == null)
//        {
//            Debug.LogError("No se encontr� el componente CarController en el PoliceCar.");
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

//        // Ajustar velocidad y direcci�n seg�n la distancia
//        if (distance > stopDistance)
//        {
//            // Avanza hacia el taxi
//            carController.gasInput = distance > followDistance ? 1f : 0.5f; // Acelera m�s lejos
//            carController.steeringInput = CalculateSteeringInput(direction);
//        }
//        else
//        {
//            // Captura el taxi
//            CaptureTaxi();
//        }
//    }

//    private float CalculateSteeringInput(Vector3 direction)
//    {
//        // Convertir la direcci�n al espacio local del coche
//        Vector3 localDirection = transform.InverseTransformDirection(direction);

//        // Determinar el �ngulo de giro seg�n la direcci�n local
//        float steering = Mathf.Clamp(localDirection.x, -1f, 1f);
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
