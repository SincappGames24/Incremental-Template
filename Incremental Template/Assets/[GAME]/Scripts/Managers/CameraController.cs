using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _idleCam;
    [SerializeField] private GameObject _endGameCam;
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


    private void StartGame()
    {
        _idleCam.SetActive(false);
    }

    private void OpenEndGameCam()
    {
        _endGameCam.SetActive(true);
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