using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneButtons : MonoBehaviour
{   
    bool isGamePaused = false;
    public void Restart()
    {
        
        SceneManager.LoadScene("MainScene");
    }

    public void Pause()
    {
        
        if (isGamePaused)
        {
            isGamePaused = false;
            Time.timeScale = 0;
        }
        else
        {
            isGamePaused = true;
            Time.timeScale = 1;
        }


    }


    public void ExitButton()
    {
       
        SceneManager.LoadScene("MainMenu");
    }
}
