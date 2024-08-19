using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public class CameraAdjust : MonoBehaviour
{
    [SerializeField] ProjectileSpawner spawner;
    private PlayerSizeControl playerSizeControl;
    private float _playerSizeThreshhold;
    public CinemachineVirtualCamera cinemachineCam;
    private Vector3 _followOffset;
    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerSizeControl=gameObject.GetComponent<PlayerSizeControl>();
        _playerSizeThreshhold = this.gameObject.transform.localScale.x*1.5f;
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
        if ( this.gameObject.transform.localScale.x> _playerSizeThreshhold * 2)
        {
            spawner.projectileSpeed *= 2; //i am speed
            playerMovement.moveSpeed *= 2; //we are speed
            DOTween.To(() => cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, x => cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = x,_followOffset*2, 3);
            _followOffset *= 2;
            _playerSizeThreshhold = this.gameObject.transform.localScale.x;
        }
        else if (this.gameObject.transform.localScale.x < _playerSizeThreshhold / 2)
        {
            spawner.projectileSpeed /= 2; //i am sped
            playerMovement.moveSpeed /= 2; //we are sped
            DOTween.To(() => cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, x => cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = x, _followOffset/2, 3);
            _followOffset /= 2;
            _playerSizeThreshhold = this.gameObject.transform.localScale.x;

        }
    }
    
}
