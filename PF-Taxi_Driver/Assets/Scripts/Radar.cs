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

        bool result;

        if (speed > legalSpeed)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return (result);
    }
    
}

