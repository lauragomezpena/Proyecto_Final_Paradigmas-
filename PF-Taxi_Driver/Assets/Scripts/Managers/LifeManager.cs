using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField] int startingLife =100;
    [SerializeField] int currentLife;
    [SerializeField] Slider healthSlider;
    public int CurrentLife { get { return currentLife; } }

    //[SerializeField] TextMeshProUGUI displayBalance;

    void Awake()
    {
        healthSlider.maxValue = startingLife;
        healthSlider.value = currentLife;
        currentLife = startingLife;
        UpdateDisplay();
    }

    public void IncreaseLife(int amount)
    {
        currentLife += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void DecreaseLife(int amount)
    {
        currentLife -= Mathf.Abs(amount);
        UpdateDisplay();

        if (currentLife < 0)
        {
            Debug.Log("Has muerto");
            //Lose the game;
            ReloadScene();
        }
    }

    void UpdateDisplay()
    {
        Debug.Log("Life "+currentLife);
        healthSlider.value = currentLife;
        //displayBalance.text = "Gold: " + currentBalance;
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}


