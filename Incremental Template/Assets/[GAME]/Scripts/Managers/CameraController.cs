using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _idleCam;
    [SerializeField] private GameObject _paintCam;
    [SerializeField] private GameObject _endGameCam;
    [SerializeField] private GameObject _simCam;
    [SerializeField] private CinemachineVirtualCamera _runnerCam;
    private void OnEnable()
    {
    EventManager.OnGameStart += StartGame;

    }

    private void OnDisable()
    {
    
        EventManager.OnGameStart -= StartGame;
     
    }

    private void Awake()
    {
        _idleCam.SetActive(false);
    }

    private void OpenSimCam()
    {
        _simCam.SetActive(true);
        float a =  _simCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z;
        DOTween.To(() => a, x => a = x, 0, 2f).OnUpdate(() =>
        {
            _simCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 9.2f, a);
        });
    }

    private void EndSimulation()
    {
        float a =  _simCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z;
        DOTween.To(() => a, x => a = x, -4, 1f).OnUpdate(() =>
        {
            _simCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0, 9.2f, a);
        });
        _simCam.transform.DOLocalRotate(new Vector3(12.7f, 0, 0), .7f);
    }

    private void StartGame()
    {
        _idleCam.SetActive(false);
    }

    private void OpenEndGameCam()
    {
        _endGameCam.SetActive(true);
    }

    private void ChangeCameraToPaintArea(float a)
    {
        _paintCam.SetActive(true);
    } 
    private void ChangeCameraToRun()
    {
        _paintCam.SetActive(false);
    }
    #region EditorPanelCameraSettings

    public void CameraXPos(float amount)
    {
        var pos = _runnerCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        _runnerCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            new Vector3(amount, pos.y, pos.z);
    }

    public void CameraYPos(float amount)
    {
        var pos = _runnerCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        _runnerCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            new Vector3(pos.x, -amount, pos.z);
    }

    public void CameraZPos(float amount)
    {
        var pos = _runnerCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        _runnerCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            new Vector3(pos.x, pos.y, -amount);
    }

    public void CameraRotateX(float amount)
    {
        _runnerCam.transform.rotation = Quaternion.Euler(amount, _runnerCam.transform.localEulerAngles.y,
            _runnerCam.transform.localEulerAngles.z);
    }

    public void CameraRotateY(float amount)
    {
        _runnerCam.transform.rotation = Quaternion.Euler(_runnerCam.transform.localEulerAngles.x, amount,
            _runnerCam.transform.localEulerAngles.z);
    }

    public void Fov(float amount)
    {
        _runnerCam.m_Lens.FieldOfView = amount;
    }

    #endregion
}