//using UnityEngine;

//public class PoliceCarController : MonoBehaviour
//{
//    public Transform taxi; // Referencia al taxi
//    public float followDistance = 5f; // Distancia mínima antes de reducir velocidad
//    public float stopDistance = 1.5f; // Distancia para capturar el taxi
//    public float maxSpeed = 20f; // Velocidad máxima del police car

//    private CarController carController;

//    private void Start()
//    {
//        carController = GetComponent<CarController>();
//        if (carController == null)
//        {
//            Debug.LogError("No se encontró el componente CarController en el PoliceCar.");
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

//        // Ajustar velocidad y dirección según la distancia
//        if (distance > stopDistance)
//        {
//            // Avanza hacia el taxi
//            carController.gasInput = distance > followDistance ? 1f : 0.5f; // Acelera más lejos
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
//        // Convertir la dirección al espacio local del coche
//        Vector3 localDirection = transform.InverseTransformDirection(direction);

//        // Determinar el ángulo de giro según la dirección local
//        float steering = Mathf.Clamp(localDirection.x, -1f, 1f);
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
