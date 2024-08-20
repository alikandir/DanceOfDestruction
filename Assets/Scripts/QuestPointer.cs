using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestPointer : MonoBehaviour
{
    public Camera uiCamera;
    private Vector3 targetLocation;
    private RectTransform pointerTransform;
    public Sprite arrowSprite;
    public Sprite crossSprite;
    private Image pointerImage;
    private TaskManager taskManager;
    bool isTarget;
    private GameObject target;
    // Start is called before the first frame update
    void Awake()
    {
        
        pointerTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        pointerImage = transform.Find("Pointer").GetComponent<Image>();
        taskManager = FindFirstObjectByType<TaskManager>();
    }
    private void OnEnable()
    {
        taskManager.SpawnedTask += PositionFinder;
        taskManager.OnTaskFinished += OnTaskFinished;
    }

    private void OnTaskFinished()
    {
        isTarget = false;
    }

    private void OnDisable()
    {
        taskManager.SpawnedTask -= PositionFinder;
    }

    // Update is called once per frame
    void Update()
    {
        
        

        float borderSize = 100f;
        Vector3 targetPosScreenPoint = Camera.main.WorldToScreenPoint(targetLocation);
        bool isOffScreen = targetPosScreenPoint.x <= borderSize || targetPosScreenPoint.x > Screen.width - borderSize || targetPosScreenPoint.y <= borderSize || targetPosScreenPoint.y > Screen.height - borderSize;
        //if (isOffScreen)
        {
            RotatePointerTowardsTarget();
           
            pointerImage.sprite = arrowSprite;
            Vector3 cappedTargetScreenPosition = targetPosScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = 0f;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = 0f;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerTransform.position = pointerWorldPosition;
            pointerTransform.localPosition = new Vector3(pointerTransform.localPosition.x, pointerTransform.localPosition.y, 0f);

        }
        //else 
        //{
        //    pointerTransform.localEulerAngles = new Vector3(0, 0, 0);
        //    pointerImage.sprite = crossSprite;
        //    Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPosScreenPoint);
        //    pointerTransform.position = pointerWorldPosition;
        //    pointerTransform.localPosition = new Vector3(pointerTransform.localPosition.x, pointerTransform.localPosition.y, 0f);
        //}
        if (isTarget&&target!=null)
        {
            targetLocation = target.transform.position;
        }
    }

    private void RotatePointerTowardsTarget()
    {
        Vector3 toPosition = targetLocation;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        pointerTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    void PositionFinder(GameObject task)
    {
        isTarget = true;
        target=task;
        targetLocation=task.transform.position;
    }
}
