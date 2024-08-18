using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edibles : MonoBehaviour
{
    public bool isMakingBig;
    public float size=10;
    public float baseGrowthFactor = 0.1f;
    private void Start()
    {
        transform.localScale = Vector3.one * Mathf.Pow(size, 1f / 3f);
    }
}
