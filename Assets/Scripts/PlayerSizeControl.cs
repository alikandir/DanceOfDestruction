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
    }
}
