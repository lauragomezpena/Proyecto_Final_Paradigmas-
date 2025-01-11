using System;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement; // Para reiniciar la escena

// Los diferentes estados del juego
public enum GameState
{
    Play,  // El juego está en curso
    Win,   // El jugador ha ganado
    Fail   // El jugador ha fallado

}


public class GameManager : MonoBehaviour
{
    public GameState gameState;
    MoneyManager moneyManager;  

    public float timeLimit =45f; 
    private float timer;
    private bool gameInProgress = false;

    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] GameObject winPanel;  // Panel de victoria
    [SerializeField] GameObject failPanel; // Panel de fallo
    private TextMeshProUGUI failSituationText;    
    private string failSituation;   // Mensaje que indice porque has perido

    public event Action<int> onVictory; // evento para dar la propina al taxi
    public event Action onFinishRide; // evento para parar coches cuando termina un viaje
    public event Action onLoose;
    private void Start()
    {

        moneyManager = FindObjectOfType<MoneyManager>();
        // Inicializa el estado del juego
        gameState = GameState.Play;
        timer = timeLimit;
        gameInProgress = true;
        ShowTimer();    

        //// Inicializa la UI para jugar nuevamente
        winPanel.SetActive(false);
        failPanel.SetActive(false);

    }

    private void Update()
    {
        if (gameInProgress)
        {
            ShowTimer();    
            if (gameState == GameState.Play)
            {
                // Resta tiempo
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = 0;

                    HandleTimeEnd();
                }
            }
        }
        else

        {
            onFinishRide?.Invoke();
            if (gameState == GameState.Win)
            {
                winPanel.SetActive(true); // Muestra el panel de victoria
            }
            if (gameState == GameState.Fail)
            {
                failPanel.SetActive(true); // Muestra el panel de fallo
            }

        }


    }

    public void HandleTaxiArrival()
    {
        Debug.Log("Taxi reached the destination!");

        int tipAmount = (Mathf.RoundToInt(timer) * 10);

        onVictory?.Invoke(tipAmount);
        string text = $"You have receive a tip: ${tipAmount}";
        changeMoneytext(text);

        HandleWinState();
    }

    public void HandlePoliceCapture()
    {
        Debug.Log("Taxi has been captured");
        failSituation = "Taxi has been captured by the Police Car!";
        HandleFailState();
    }

    public void HandleTimeEnd()
    {
        failSituation = "Your time has run out!";
        HandleFailState();
    }

    public void HandleDeath()
    {
        failSituation = "Life has run out!";
        HandleFailState();
    }
    public void HandleWinState()
    {
        gameState = GameState.Win;
        gameInProgress = false;
     
    }


    public void HandleFailState()
    {
        onLoose.Invoke();
        gameState = GameState.Fail;
        gameInProgress = false;
        changeFailText();
    }

    public void PlayAgain()
    {
        // Aquí guardas el dinero actual antes de recargar la escena
        int moneyBeforeReload = moneyManager.CurrentBalance;

        // Reinicia la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Después de recargar la escena, restauras el dinero (o cualquier otro dato)
        moneyManager.currentBalance = moneyBeforeReload;
    }


    public void changeFailText()

    {
        TextMeshProUGUI[] texts = failPanel.GetComponentsInChildren<TextMeshProUGUI>();
        failSituationText = texts[1]; // el segundo texto 

        failSituationText.text = failSituation;
    }

    public void changeMoneytext(string text)

    {
        TextMeshProUGUI[] texts = winPanel.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI moneyText = texts[1]; // el segundo

        moneyText.text = text;
    }

    public void ExitGame()
    {
 
        Debug.Log("Exiting Game...");

        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; 
        #else
                Application.Quit(); // Exits the game in a build.
        #endif
                
    }

    public void ShowTimer()
    {
        if (timeText != null)
        {
            timeText.text = "Time: " + timer.ToString("F2")+"s"; 
        }
    }


}

