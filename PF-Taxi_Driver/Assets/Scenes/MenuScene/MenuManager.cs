using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void ExitGame()
    {

        Debug.Log("Exiting Game...");

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the editor.
        #else
                        Application.Quit(); // Exits the game in a build.
        #endif

    }

    public void StartGame()
    {

        SceneManager.LoadScene("GameScene");

    }


}
