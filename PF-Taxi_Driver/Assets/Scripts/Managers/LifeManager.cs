using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LifeManager : MonoBehaviour
{
    [SerializeField] int startingLife =100;

    [SerializeField] int currentLife;
    public int CurrentLife { get { return currentLife; } }

    //[SerializeField] TextMeshProUGUI displayBalance;

    void Awake()
    {
        currentLife = startingLife;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        currentLife += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentLife -= Mathf.Abs(amount);
        UpdateDisplay();

        if (currentLife < 0)
        {
            //Lose the game;
            ReloadScene();
        }
    }

    void UpdateDisplay()
    {
        Debug.Log(currentLife);
        //displayBalance.text = "Gold: " + currentBalance;
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}


