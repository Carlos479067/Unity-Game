using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{

    // Start is called before the first frame update
    public void startGame()
    {
        SceneManager.LoadScene("Drive & Deliver");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            // If in the editor, stop playing the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If in a build, quit the application
            Application.Quit();
        #endif
    }
}
