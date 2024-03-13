using DG.Tweening;
using ElephantSDK;
using SincappStudio;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStates
    {
        Idle,
        Run,
        Dead,
        OnFinishWall,
        Finish
    }

    public static PlayerStates PlayerState;
    private PlayerMovementController _playerMovementController;
    private PlayerAttackController _playerAttackController;
    
    private void Awake()
    {
        _playerAttackController = GetComponent<PlayerAttackController>();
        _playerMovementController = GetComponent<PlayerMovementController>();
    }

    private void OnEnable()
    {
        EventManager.OnGameStart += StartGame;
        EventManager.OnGateCollect += TakeSkills;
    }

    private void OnDisable()
    {
        EventManager.OnGameStart -= StartGame;
        EventManager.OnGateCollect -= TakeSkills;
    }

    private void Start()
    {
        PlayerState = PlayerStates.Idle;
    }

    private void StartGame()
    {
        PlayerState = PlayerStates.Run;
        _playerAttackController.StartShooting();
        
        var persistData = PersistData.Instance;
        persistData.Save();
        var level = persistData.CurrentLevel;
        Elephant.LevelStarted(level);
    }

    private void TakeSkills(GateGroupController.SkillTypes skillType, float skillAmount)
    {
        _playerAttackController.CalculateSkills(skillType, skillAmount);
    }
    
    public void EnterFinish()
    {
        PlayerState = PlayerStates.OnFinishWall;
        EventManager.OnFinishWall?.Invoke();
    }

    public void CollectMoney(float moneyAmount)
    {
        PersistData persistData = PersistData.Instance;
        persistData.Money += moneyAmount;
        EventManager.OnMoneyChange?.Invoke();
    }

    public void Die(float delay)
    {
        transform.DOKill();
        PlayerState = PlayerStates.Finish;
        StartCoroutine(Sincapp.WaitAndAction(delay, () => { EventManager.OnGameWin?.Invoke(); }));
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IInteractable>()?.InteractPlayer(transform);
        
        if (other.gameObject.layer == LayerHandler.EndGameWall)
        {
            other.gameObject.layer = 0;
            Die(0);
        }
    }
}