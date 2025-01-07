using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar la escena

// Define los diferentes estados del juego
public enum GameState
{
    Play,  // El juego está en curso
    Win,   // El jugador ha ganado
    Fail   // El jugador ha fallado
}


public class GameManager : MonoBehaviour
{
    public GameState gameState;
    public float timeLimit = 30f; // Puedes adaptarlo a tu juego
    private float timer;
    private bool gameInProgress = false;
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] GameObject winPanel;  // Panel de victoria
    //[SerializeField] GameObject failPanel; // Panel de fallo
    //[SerializeField] GameObject playAgainButton; // Botón para jugar de nuevo

    private void Start()
    {
        // Inicializa el estado del juego
        gameState = GameState.Play;
        timer = timeLimit;
        gameInProgress = true;
        ShowTimer();    

        //// Inicializa la UI para jugar nuevamente
        winPanel.SetActive(false);
        //failPanel.SetActive(false);
        //playAgainButton.SetActive(false);
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
                    gameState = GameState.Fail;
                    HandleFailState();
                }
            }
        }
    }

    public void HandleTaxiArrival()
    {
        // Lógica cuando el taxi llega al destino
        Debug.Log("Taxi reached the destination!");
        Debug.Log(timer + " seconds left");
        Debug.Log((Mathf.RoundToInt(timer)*10) + " coins by clients");
        gameState = GameState.Win;
        HandleWinState();
    }

    public void HandlePoliceCapture()
    {
        // Lógica cuando el taxi llega al destino
        Debug.Log("Taxi has been captured");
        gameState = GameState.Fail;
        HandleFailState();
    }

    private void HandleWinState()
    {
        gameInProgress = false;
        //PlayAgain();
        winPanel.SetActive(true); // Muestra el panel de victoria
        //playAgainButton.SetActive(true); // Muestra el botón de "Jugar de nuevo"
    }

    private void HandleFailState()
    {
        gameInProgress = false;
        PlayAgain();    
        //failPanel.SetActive(true); // Muestra el panel de fallo
        //playAgainButton.SetActive(true); // Muestra el botón de "Jugar de nuevo"
    }

    public void PlayAgain()
    {
        // Reinicia el juego: vuelve a cargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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


    void ShowTimer()
    {
        if (timeText != null)
        {
            timeText.text = "Time: " + timer.ToString("F2")+"s"; //Muestra el tiempo actualizado con 2 decimales
        }
    }
}

