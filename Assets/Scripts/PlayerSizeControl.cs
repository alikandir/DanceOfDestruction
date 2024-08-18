using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeControl : MonoBehaviour
{
    public float size;
    public float baseGrowthFactor = 0.1f; 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Edibles>() != null) 
        {
            Edibles edible = collision.gameObject.GetComponent<Edibles>();
            if (edible.size <= this.size)
            {
                TunePlayerSize(edible.size,edible.isMakingBig);
                Destroy(edible.gameObject);
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
        
    }
}
