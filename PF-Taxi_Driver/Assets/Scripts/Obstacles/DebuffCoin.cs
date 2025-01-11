using UnityEngine;

public class DebuffCoin : Obstacle
{
    void Awake()
    {
        solidObject = false;
        lifeLoss = 5;
        money = 0;
        velocityMult = 0.8f;
        time = 0;

    }
}
