using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] GameObject[] taskObjectList;
    [SerializeField] TextMeshProUGUI taskTextUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int SupremeTaskSelector()
    {
        if (taskObjectList.Length > 0)
        {
            return Random.Range(0, taskObjectList.Length);
        }
        else
            return -9;
    }
}
