using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int startingBalance = 0;
    [SerializeField] int currentBalance;

    GameManager gameManager;
    public int CurrentBalance { get { return currentBalance; } }

    //[SerializeField] TextMeshProUGUI displayBalance;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentBalance = startingBalance;

        gameManager.onVictory += Deposit;
        UpdateDisplay();
    }


    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();

    }

    void UpdateDisplay()
    {
        Debug.Log("Money: "+ currentBalance);
        //displayBalance.text = "Gold: " + currentBalance;
    }

}

