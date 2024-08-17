using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edibles : MonoBehaviour
{
    public bool isMakingBig;
    public int size=10;
    private void Start()
    {
        transform.localScale = Mathf.Log10(size)*Vector3.one;
    }
}
