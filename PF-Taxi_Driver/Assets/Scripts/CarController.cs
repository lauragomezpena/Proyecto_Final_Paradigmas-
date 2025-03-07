using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody playerRB;
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;
    public float motorPower;
    public float brakePower;
    public float slipAngle;
    private float speed;
    public AnimationCurve steeringCurve;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        gameManager.onFinishRide += StopCarController;
        playerRB.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        speed = playerRB.velocity.magnitude;
        CheckInput();
        ApplyMotor();
        ApplySteering();
        ApplyBrake();
        ApplyWheelPositions();
    }

    void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");

        steeringInput = Input.GetAxis("Horizontal");

        slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);

        //fixed code to brake even after going on reverse by Andrew Alex 
        float movingDirection = Vector3.Dot(transform.forward, playerRB.velocity);
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }


    }
    void ApplyBrake()
    {
        colliders.wheelFR.brakeTorque = brakeInput * brakePower * 0.7f;
        colliders.wheelFL.brakeTorque = brakeInput * brakePower * 0.7f;

        colliders.wheelRR.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.wheelRL.brakeTorque = brakeInput * brakePower * 0.3f;


    }
    void ApplyMotor()
    {

        colliders.wheelRR.motorTorque = motorPower * gasInput;
        colliders.wheelRL.motorTorque = motorPower * gasInput;

    }
    void ApplySteering()
    {

        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        if (slipAngle < 120f)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, playerRB.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.wheelFR.steerAngle = steeringAngle;
        colliders.wheelFL.steerAngle = steeringAngle;
    }

    void ApplyWheelPositions()
    {
        UpdateWheel(colliders.wheelFR, wheelMeshes.wheelFR);
        UpdateWheel(colliders.wheelFL, wheelMeshes.wheelFL);
        UpdateWheel(colliders.wheelRR, wheelMeshes.wheelRR);
        UpdateWheel(colliders.wheelRL, wheelMeshes.wheelRL);
    }

    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }

    public void ModifyVelocity(float mult)
    {
        playerRB.velocity *= mult;


    }

    public float GetSpeed()
    {

        return speed;
    }

    public void StopCarController()
    {


        playerRB.velocity = Vector3.zero;

        gasInput = 0;
        brakeInput = 0;
    }

}
[System.Serializable]
public class WheelColliders
{
    public WheelCollider wheelFR;
    public WheelCollider wheelFL;
    public WheelCollider wheelRR;
    public WheelCollider wheelRL;
}
[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer wheelFR;
    public MeshRenderer wheelFL;
    public MeshRenderer wheelRR;
    public MeshRenderer wheelRL;
}
