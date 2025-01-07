using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{


    private float speed;
    private float legalSpeed = 5f;


    public bool TriggerRadar(CarController vehicle)
    {

        speed = vehicle.GetSpeed();
        string message;
        bool result;

        if (speed > legalSpeed)
        {
            result = true;
            message = "Caught above legal speed.";
        }
        else
        {
            result = false;
            message = "Driving legally.";
        }
        //Debug.Log(message); 
        return (result);
    }
    
}

