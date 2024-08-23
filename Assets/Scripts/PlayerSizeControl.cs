using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerSizeControl : MonoBehaviour
{
    public TextMeshProUGUI sizeText;
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
        sizeText.color = Color.cyan;
        sizeText.text = "Size: " + size.ToString("0.0");
    }
    public void TunePlayerSize(float objectSize, bool isMakingBig)
    {
        if (isMakingBig)
        {/*
            transform.localScale += Vector3.one * baseGrowthFactor * objectSize; 
            size += objectSize;
            */
            
            float sizeMultiplier = 1.0f - (objectSize / maxSize);
            if (sizeMultiplier == 0) { sizeMultiplier = 0.1f; }
            size += objectSize*sizeMultiplier;
            Debug.Log("size Multiplier " + sizeMultiplier);
            Debug.Log("size increase:" + objectSize * sizeMultiplier + "object size: " + objectSize);
            transform.localScale = Vector3.one * Mathf.Pow(size, 1f / 3f);
        }
        else if (!isMakingBig)
        {/*
            transform.localScale -= Vector3.one *baseGrowthFactor * objectSize;
            size -= objectSize;
            */
            float sizeMultiplier = (objectSize / maxSize);
            Debug.Log("size Multiplier " + sizeMultiplier);
            Debug.Log("size decrease:" + objectSize * sizeMultiplier+ "object size: " + objectSize);
            size -= objectSize * sizeMultiplier;
            transform.localScale = Vector3.one * Mathf.Pow(size,1f/3f);
            
        }
        UpdateImageSize();
        sizeText.text="Size: " + size.ToString("0.0");
    }
    public void UpdateImageSize()
    {
        sizOmeterImage.fillAmount = size / maxSize;
        if (sizOmeterImage.fillAmount >= 0.70)
        {
            
            CriticLevelAnimationPlayer();
        }
        else if (sizOmeterImage.fillAmount < 0.70)
        { 
            
            sizeBar.GetComponent<Animator>().Play("SizeBarNormal");
            isPlayingCriticAnim = false;
            sizeText.color = Color.cyan;
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
        sizeText.color = Color.red;
        
    }
}
