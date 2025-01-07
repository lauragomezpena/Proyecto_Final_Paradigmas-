using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionFence : Obstacle
{
    void Awake()
    {
        solidObject = true;
        lifeLoss = 10;
        money = 0;
        velocityMult = 0.9f;
        time = 0;

    }

}
