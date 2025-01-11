using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] int startingBalance = 0;
    [SerializeField] public int currentBalance=0 ;

    GameManager gameManager;
    public int CurrentBalance { get { return currentBalance; } }

    [SerializeField] TextMeshProUGUI displayBalance; // in win panel
    [SerializeField] TextMeshProUGUI displayBalance2; // in fail panel 

    void Awake()
    {

        gameManager = FindObjectOfType<GameManager>();

        gameManager.onVictory += Deposit;
        gameManager.onLoose += UpdateDisplay2;

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
        displayBalance.text = ""+ currentBalance;
    }
    void UpdateDisplay2()
    {
        displayBalance2.text = "" + currentBalance;
    }
}

