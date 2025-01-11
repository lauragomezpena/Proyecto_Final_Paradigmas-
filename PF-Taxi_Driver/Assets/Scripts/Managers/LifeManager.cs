using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    int startingLife =100;
    int currentLife;
    [SerializeField] Slider healthSlider;
    GameManager gameManager;
    public int CurrentLife { get { return currentLife; } }



    void Awake()
    {
        healthSlider.maxValue = startingLife;
        healthSlider.value = currentLife;
        currentLife = startingLife;
        UpdateDisplay();
    }

    public void IncreaseLife(int amount)
    {
        currentLife += (amount);
        UpdateDisplay();
    }

    public void DecreaseLife(int amount)
    {
        currentLife -= (amount);
        UpdateDisplay();

        if (currentLife < 0)
        {
            gameManager.HandleDeath();
        }
    }

    void UpdateDisplay()
    {
        Debug.Log("Life "+currentLife);
        healthSlider.value = currentLife;

    }

}


