using UnityEngine;

public class PoliceCarController : MonoBehaviour
{
    public Transform taxi;  // Reference to the taxi object
    public float followSpeed = 15f;  // Speed at which the police car follows
    public float captureDistance = 1f;  // Distance at which the police captures the taxi
    public float speedLimit = 20f;  // Speed limit for the taxi
    public float speedCheckInterval = 0.5f;  // Time between checking speed of the taxi
    private Rigidbody taxiRB;  // Taxi's Rigidbody for speed calculations
    private float timeSinceLastCheck = 0f;

    private void Start()
    {
        // Get the Rigidbody component from the taxi object
        taxiRB = taxi.GetComponent<Rigidbody>();

        if (taxiRB == null)
        {
            Debug.LogError("Taxi Rigidbody not found! Ensure the Taxi has a Rigidbody component.");
        }
    }

    private void Update()
    {
        // Always follow the taxi for now to test movement
        FollowTaxi();
    }

    // Method to calculate taxi speed and check if it's speeding
    private bool IsTaxiSpeeding()
    {
        if (taxiRB == null) return false;

        // Speed is the magnitude of the Rigidbody's velocity
        float taxiSpeed = taxiRB.velocity.magnitude;
        Debug.Log($"Taxi Speed: {taxiSpeed} m/s");

        return taxiSpeed > speedLimit;
    }

    // Method to make the police car follow the taxi
    private void FollowTaxi()
    {
        // Get direction to follow the taxi
        Vector3 direction = (taxi.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, taxi.position);

        Debug.Log($"Direction to taxi: {direction}");
        Debug.Log($"Distance to taxi: {distance}");

        // Keep following the taxi until close enough
        if (distance > captureDistance)
        {
            // Move police car towards the taxi
            transform.position += direction * followSpeed * Time.deltaTime;
        }
        else
        {
            CaptureTaxi();
        }
    }

    // Method to stop the taxi and end the game or journey when captured
    private void CaptureTaxi()
    {
        Debug.Log("Police caught the taxi! Journey stopped.");

        // Logic to stop the taxi
        taxiRB.velocity = Vector3.zero; // Stop the taxi immediately
    }
}
