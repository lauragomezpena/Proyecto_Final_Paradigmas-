using System;
using TMPro;
using UnityEngine;
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

    private void Start()
    {
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
        // Lógica cuando el taxi llega al destino
        

        Debug.Log("Taxi reached the destination!");
        Debug.Log(timer + " seconds left");


        int tipAmount = (Mathf.RoundToInt(timer) * 10);

        onVictory?.Invoke(tipAmount);
        Debug.Log($"Tip Received: ${tipAmount}");

        HandleWinState();
    }

    public void HandlePoliceCapture()
    {
        // Lógica cuando el taxi llega al destino
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
        gameState = GameState.Fail;
        gameInProgress = false;
        changeFailText();
   

    }

    public void PlayAgain()
    {
        // Reinicia el juego: vuelve a cargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void changeFailText()

    {
        TextMeshProUGUI[] texts = failPanel.GetComponentsInChildren<TextMeshProUGUI>();
        failSituationText = texts[1]; // el segundo texto va a ser el modificado

        failSituationText.text = failSituation;
    }

    public void ExitGame()
    {
 
        Debug.Log("Exiting Game...");

        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the editor.
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

