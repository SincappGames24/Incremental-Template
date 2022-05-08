using SincappStudio;
using TMPro;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshPro _highScoreText;
    private Transform _playerTransform;

    private void OnEnable()
    {
        EventManager.GameWin += SetHighScoreOnWin;
    }

    private void OnDisable()
    {
        EventManager.GameWin -= SetHighScoreOnWin;
    }

    private void Start()
    {
        PersistData persistData = PersistData.Instance;

        _playerTransform = FindObjectOfType<PlayerController>().transform;
        transform.SetZ_Pos(persistData.HighScoreSignLastZPosition == 0 ? _playerTransform.position.z : persistData.HighScoreSignLastZPosition);
        _highScoreText.SetText((persistData.HighScore).ToString("0") + "m");
    }

    private void Update()
    {
        if (_playerTransform.position.z < transform.position.z || PlayerController.PlayerState != PlayerController.PlayerStates.Run) return;

        SetHighScore();
        transform.SetZ_Pos(_playerTransform.position.z);
        _highScoreText.SetText((PersistData.Instance.HighScore).ToString("0") + "m");
    }

    private void SetHighScore()
    {
        PersistData persistData = PersistData.Instance;
        persistData.HighScore += (_playerTransform.position.z - transform.position.z);
        persistData.HighScoreSignLastZPosition = _playerTransform.position.z;
    }

    private void SetHighScoreOnWin()
    {
        PersistData persistData = PersistData.Instance;
        persistData.HighScoreSignLastZPosition = 0;
    }
}