using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSizeControl : MonoBehaviour
{
    public float size;
    public float maxSize;
    
    public float baseGrowthFactor = 0.1f;

    public event Action<float> OnEat; //passes the player size
    public Image sizOmeterImage;
    public event Action GameOver;
    Color startColor;
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
                OnEat?.Invoke(size);
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
            size += objectSize;
            transform.localScale = Vector3.one * Mathf.Pow(size, 1f / 3f);
        }
        else if (!isMakingBig)
        {/*
            transform.localScale -= Vector3.one *baseGrowthFactor * objectSize;
            size -= objectSize;
            */
            size -= objectSize;
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
        }
        else if (sizOmeterImage.fillAmount < 0.70)
        { sizOmeterImage.color = Color.cyan; }
        if (sizOmeterImage.fillAmount >= 1)
        {
            GameOver();
        }
    }


    public void GameOverEmit()
    {
        GameOver?.Invoke();
    }
}
