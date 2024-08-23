using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    GameObject current;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject howtoplay;
    public GameObject title;
    public void PlayButton()
    {
        SceneManager.LoadScene("IntroScene");
    }


    public void BackButton()
    {
        title.gameObject.SetActive(true);
        current.SetActive(false);
        mainMenu.SetActive(true);
        current = null;
    }

    public void Credits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false );
        current = credits;
        
    }

    public void HowToPlay()
    {
        title.gameObject.SetActive(false);
        howtoplay.SetActive(true);
        mainMenu.SetActive(false);
        current = howtoplay;
    }

    public void Exit()
    {
        Application.Quit();
        
    }
}
