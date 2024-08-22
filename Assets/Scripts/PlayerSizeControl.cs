using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerSizeControl : MonoBehaviour
{
    public float size;
    public float maxSize;
    
    public float baseGrowthFactor = 0.1f;

    public event Action<float, float> OnEat; //passes the player size, maxSize
    public Image sizOmeterImage;
    public event Action GameOver;
    Color startColor;
    bool isPlayingCriticAnim=false;
    public GameObject sizeBar;
    public GameObject sizeBarFrame;
    private void Awake()
    {
        UpdateImageSize();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Edibles>() != null) 
        {
            Edibles edible = collision.gameObject.GetComponent<Edibles>();
            if (edible.size <= this.size)
            {
                TunePlayerSize(edible.size,edible.isMakingBig);
                if (edible.gameObject.GetComponent<TaskObjectsBase>() != null)
                { edible.gameObject.GetComponent<TaskObjectsBase>().CompleteTask();}
                Destroy(edible.gameObject);
                OnEat?.Invoke(size,maxSize);
            }
            if (edible.gameObject.CompareTag("Earth"))
            {
                GameOverEmit();
            }
        }
    }
    private void Start()
    {
        startColor = sizOmeterImage.color;
    }
    public void TunePlayerSize(float objectSize, bool isMakingBig)
    {
        if (isMakingBig)
        {/*
            transform.localScale += Vector3.one * baseGrowthFactor * objectSize; 
            size += objectSize;
            */
            
            float sizeMultiplier = (objectSize / size)*1.5f;
            if (sizeMultiplier==0) sizeMultiplier = 0.2f;
            size += objectSize*sizeMultiplier;
            transform.localScale = Vector3.one * Mathf.Pow(size, 1f / 3f);
        }
        else if (!isMakingBig)
        {/*
            transform.localScale -= Vector3.one *baseGrowthFactor * objectSize;
            size -= objectSize;
            */
            float sizeMultiplier = 50*(objectSize / maxSize);
            size -= objectSize*sizeMultiplier;
            if (size <= 10) { size = 10; }
            transform.localScale = Vector3.one * Mathf.Pow(size,1f/3f);
            
        }
        UpdateImageSize();
    }
    public void UpdateImageSize()
    {
        sizOmeterImage.fillAmount = size / maxSize;
        if (sizOmeterImage.fillAmount >= 0.70)
        {
            sizOmeterImage.color = Color.red;
            CriticLevelAnimationPlayer();
        }
        else if (sizOmeterImage.fillAmount < 0.70)
        { 
            sizOmeterImage.color = Color.cyan;
            sizeBar.GetComponent<Animator>().Play("SizeBarNormal");
            isPlayingCriticAnim = false;        
        }
        if (sizOmeterImage.fillAmount >= 1)
        {
            GameOverEmit();
        }
    }


    public void GameOverEmit()
    {
        GameOver?.Invoke();
        SceneManager.LoadScene("GameOver");
    }
    void CriticLevelAnimationPlayer()
    {
        if (isPlayingCriticAnim) { return; }
        isPlayingCriticAnim = true;
        sizeBar.GetComponent<Animator>().Play("Critic");
        
    }
}
