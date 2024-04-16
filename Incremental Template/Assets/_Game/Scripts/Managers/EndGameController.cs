using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndGameController : MonoBehaviour
{
    public int TotalObstacleRowCount = 13;

    [Header("References")] [SerializeField]
    private Transform _finishGroundHolder;
    [SerializeField] private Transform _spawnFinishGround;
    [SerializeField] private GameObject _finishObstacle;
    [SerializeField] private GameObject _endOfFinishTargetObj;
    [SerializeField] private Transform _highScoreSign;
    private List<Color> _groundColors = new List<Color>();
    private const int _playerLayer = 8;

    private void OnEnable()
    {
        EventManager.OnGameWin += SetHighScorePos;
    }

    private void OnDisable()
    {
        EventManager.OnGameWin -= SetHighScorePos;
    }

    private void Start()
    {
        _highScoreSign.transform.localPosition = new Vector3(_highScoreSign.transform.localPosition.x,
            _highScoreSign.transform.localPosition.y, PersistData.Instance.HighScoreSignPositionZ);
        SpawnGround();
    }

    private void SpawnGround()
    {
        GetColorToGround();
        float spawnZPosForGrounds = 0;

        for (int i = 0; i < TotalObstacleRowCount + 7; i++)
        {
            #region GroundSpawn

            var spawnedGround = Instantiate(_spawnFinishGround, transform.position, Quaternion.Euler(0, 0, 0),
                _finishGroundHolder);
            spawnedGround.localPosition = new Vector3(0, 0, spawnZPosForGrounds);
            spawnedGround.GetComponentInChildren<Renderer>().material.color = _groundColors[i];
            spawnZPosForGrounds += 7.916f;

            #endregion
        }

        StartCoroutine(CreateEndGameObstacle());
    }

    private void GetColorToGround()
    {
        int count = 0;

        for (int i = 0; i < TotalObstacleRowCount + 7; i++)
        {
            if (count < 5)
            {
                var a = Mathf.InverseLerp(0, 5, count);
                _groundColors.Add(Color.Lerp(new Color(0.31f, 0.61f, 1f), new Color(1f, 0.47f, 0.95f), a));
            }
            else if (count < 10)
            {
                var a = Mathf.InverseLerp(5, 10, count);
                _groundColors.Add(Color.Lerp(new Color(1f, 0.47f, 0.95f), new Color(1f, 0.48f, 0.19f), a));
            }
            else if (count < 15)
            {
                var a = Mathf.InverseLerp(10, 15, count);
                _groundColors.Add(Color.Lerp(new Color(1f, 0.48f, 0.19f), new Color(1f, 0.33f, 0.33f), a));
            }
            else
            {
                var a = Mathf.InverseLerp(15, 20, count);
                _groundColors.Add(Color.Lerp(new Color(1f, 0.33f, 0.33f), new Color(0.36f, 0.7f, 0.34f), a));
            }

            count++;
        }
    }

    private IEnumerator CreateEndGameObstacle()
    {
        float spawnZPosForWall = 14;
        int endGameObstacleWatt = 2;
        RemoteController remoteController = RemoteController.Instance;

        for (int i = 0; i < TotalObstacleRowCount; i++)
        {
            var willSpawnObj = _finishObstacle;

            if (i == TotalObstacleRowCount - 1)
            {
                spawnZPosForWall += 20;
                willSpawnObj = _endOfFinishTargetObj;
            }

            GameObject spawnedObstacleRow = null;

            if (i != TotalObstacleRowCount - 1)
            {
                spawnedObstacleRow = Instantiate(willSpawnObj, transform.position, Quaternion.Euler(0, 180, 0), transform);
                
                PersistData persistData = PersistData.Instance;
                
                foreach (var obstacles in spawnedObstacleRow.GetComponentsInChildren<EndGameObstacle>())
                {
                    var endGameReachedCount = persistData.EndGameReachCount;
                    int diffReachedCount = Mathf.Abs(remoteController.EndGameObstacleNumbers.Length - endGameReachedCount);
                    float additiveWhenOver = 0f;
                    
                    if (endGameReachedCount >= remoteController.EndGameObstacleNumbers.Length)
                    {
                        additiveWhenOver =
                            remoteController.EndGameObstacleNumbersAddWhenReached[remoteController.EndGameObstacleNumbersAddWhenReached.Length - 1]
                            + (diffReachedCount * 2);
                    }

                    obstacles.EndGameObstacleNumber = remoteController.EndGameObstacleNumbers[i] + remoteController.EndGameObstacleNumbersAddWhenReached[endGameReachedCount] + additiveWhenOver;
                }
            }
            else
            {
                spawnedObstacleRow = _endOfFinishTargetObj;
                spawnedObstacleRow.SetActive(true);
            }

            spawnedObstacleRow.transform.localPosition = new Vector3(0, 0, spawnZPosForWall);
            spawnZPosForWall += 11;
            yield return new WaitForSeconds(.1f);
        }
    }

    private void SetHighScorePos()
    {
        PersistData persistData = PersistData.Instance;

        if (_highScoreSign.position.z < LevelManager.Instance.PlayerController.transform.position.z)
        {
            var cachedPlayerTransformZ = LevelManager.Instance.PlayerController.transform.position.z;
            _highScoreSign.DOMoveZ(cachedPlayerTransformZ + 2, 1f).OnComplete(() =>
            {
                persistData.HighScoreSignPositionZ = _highScoreSign.localPosition.z - 2;
            });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _playerLayer)
        {
            gameObject.layer = 0;
            other.GetComponentInParent<PlayerController>().EnterFinish();
        }
    }
}