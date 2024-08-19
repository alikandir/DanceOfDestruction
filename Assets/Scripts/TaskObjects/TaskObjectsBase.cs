using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObjectsBase : MonoBehaviour
{
    public float targetSize;
    [TextAreaAttribute]
    public string flavorText;
    [TextAreaAttribute]
    public string taskText;

    public virtual void WhenEaten() { }
}
