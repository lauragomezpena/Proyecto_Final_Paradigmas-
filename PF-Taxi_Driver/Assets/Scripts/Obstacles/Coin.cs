using UnityEngine;

public class Coin : Obstacle
{


    void Awake()
    {
        solidObject = false;
        lifeLoss = 0;
        money = 5;
        time = 0;

    }


}
