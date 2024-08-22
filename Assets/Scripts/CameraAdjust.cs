using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System;


public class CameraAdjust : MonoBehaviour
{
    [SerializeField] ProjectileSpawner spawner;
    private PlayerSizeControl playerSizeControl;
    private float _playerSizeThreshhold;
    public CinemachineVirtualCamera cinemachineCam;
    private Vector3 _followOffset;
    private PlayerMovement playerMovement;
    public float cameraChangeAdjuster = 1.3f;
    public float cameraChangeMultiplier;
    public Vector3[] cameraOffSets;
    public float[] playerSpeeds;
    public float[] projectileSpeeds;
    int currentIndex = -1;
    float currentRate;
    Vector3 targetOffset;
    Vector3 startOffset;
    float desiredDuration = 6f;
    float elapsedTime = 0f;
    private int lastIndex = 0;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerSizeControl = gameObject.GetComponent<PlayerSizeControl>();
        _playerSizeThreshhold = this.gameObject.transform.localScale.x * 1.5f;
        _followOffset = cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        startOffset = new Vector3(0,0,-15);
        targetOffset= cameraOffSets[0];
    }

    private void OnEnable()
    {
        playerSizeControl.OnEat += HandleBlackHoleGrowth;
    }

    private void OnDisable()
    {
        playerSizeControl.OnEat -= HandleBlackHoleGrowth;
    }
    void HandleBlackHoleGrowth(float playerSize, float maxSize)
    {
        currentRate = playerSize / maxSize;
    }
    private void Update()
    {

        int newIndex = GetIndexForRate(currentRate);
        elapsedTime += Time.deltaTime;
        

        if (newIndex != lastIndex)
        {
            // Update only if the index has changed
            currentIndex = newIndex;
            lastIndex = newIndex;
            elapsedTime = 0;
            targetOffset = cameraOffSets[currentIndex];
            spawner.projectileSpeed = projectileSpeeds[currentIndex];
            playerMovement.moveSpeed = playerSpeeds[currentIndex];
            //startOffset = cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
            

        }
        float percentageComplete = elapsedTime / desiredDuration;
        if (elapsedTime <= desiredDuration) 
        { cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, targetOffset, Mathf.SmoothStep(0, 1, percentageComplete)); }
        

    }
    int GetIndexForRate(float rate)
    {
        Debug.Log(rate);
        if (rate >= 0.80f) return 4;
        if (rate > 0.60f) return 3;
        if (rate >= 0.40f) return 2;
        if (rate > 0.20f) return 1;
        return 0;
    }
}
