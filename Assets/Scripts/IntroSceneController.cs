using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneController : MonoBehaviour
{
    private Animator anim;
    public GameObject startButton;
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(PlayAnimation());
        startButton.SetActive(false);
    }
    IEnumerator PlayAnimation()
    {
        anim.Play("IntroScene1");
        
        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("LeftUp"));
        anim.Play("IntroScene2");
        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("RightUp"));
        anim.Play("IntroScene3");
        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("LeftDown"));
        anim.Play("IntroScene4");
        yield return new WaitForSeconds(5);
        Destroy(GameObject.Find("RightDown"));
        startButton.SetActive(true);
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
