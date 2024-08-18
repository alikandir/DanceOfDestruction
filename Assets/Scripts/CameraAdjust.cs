using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public class CameraAdjust : MonoBehaviour
{
    private PlayerSizeControl playerSizeControl;
    private float _playerSizeThreshhold;
    public CinemachineVirtualCamera cinemachineCam;
    private Vector3 _followOffset;
    private void Awake()
    {
        playerSizeControl=gameObject.GetComponent<PlayerSizeControl>();
        _playerSizeThreshhold = playerSizeControl.size;
        _followOffset = cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void OnEnable()
    {
        playerSizeControl.OnEat += HandleBlackHoleGrowth;
    }

    private void OnDisable()
    {
        playerSizeControl.OnEat -= HandleBlackHoleGrowth;
    }
    void HandleBlackHoleGrowth(float playerSize)
    {
        if (playerSize > _playerSizeThreshhold * 2)
        {
            
            DOTween.To(() => cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, x => cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = x,_followOffset*2, 3);
            _followOffset *= 2;
            _playerSizeThreshhold = playerSize;
        }
        else if (playerSize < _playerSizeThreshhold / 2)
        {
            DOTween.To(() => cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, x => cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = x, _followOffset/2, 3);
            _followOffset /= 2;
            _playerSizeThreshhold = playerSize;
        }
    }
    private void Update()
    {
        
    }
}
