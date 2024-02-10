using System.Globalization;
using Sirenix.OdinInspector;
using UnityEngine;

public class GateGroupController : MonoBehaviour, IDataCollectable, IDamageable, IInteractable
{
    public enum SkillTypes
    {
        FireRate,
        Range,
    }
    
    #region Gate 1 Attributes
    
    [TabGroup("Gate 1")]
    [LabelText("Skill Type"),GUIColor(.5f,1f,.5f)]
    [SerializeField] private SkillTypes _firstGateSkillType;

    [TabGroup("Gate 1")]
    [LabelText("Skill Amount")]
    [SerializeField] private float _firstGateSkillAmount;

    [TabGroup("Gate 1")]
    [LabelText("Power Amount")]
    [SerializeField] private float _firstGatePowerAmount;
    
    [TabGroup("Gate 1")]
    [LabelText("Destructible Type")]
    [SerializeField] private DestructibleBaseSO _firstGateDestructibleType;
    
    [TabGroup("Gate 1")]
    [LabelText("Lock Amount")]
    [ShowIf("_firstGateDestructibleType", null)]
    [SerializeField] private float _firstGateLockAmount;
    
    private GateController _firstGateController;
    
    #endregion

    #region Gate 2 Attributes
    
    [TabGroup("Gate 2")]
    [LabelText("Skill Type"),GUIColor(.4f,1f,.4f)]
    [SerializeField] private SkillTypes _secondGateSkillType;

    [TabGroup("Gate 2")]
    [LabelText("Skill Amount")]
    [SerializeField] private float _secondGateSkillAmount;

    [TabGroup("Gate 2")]
    [LabelText("Power Amount")]
    [SerializeField] private float _secondGatePowerAmount;
    
    [TabGroup("Gate 2")]
    [LabelText("Destructible Type")]
    [SerializeField] private DestructibleBaseSO _secondGateDestructibleType;
    
    [TabGroup("Gate 2")]
    [LabelText("Lock Amount")]
    [ShowIf("_secondGateDestructibleType", null)]
    [SerializeField] private float _secondGateLockAmount;
    
    private GateController _secondGateController;
    
    #endregion

    private bool _isSingleGate;
    private InteractableMovementController _interactableMovementController;

    private void Start()
    {
        _interactableMovementController = GetComponent<InteractableMovementController>();
        GateController[] gateControllers = GetComponentsInChildren<GateController>();
        _firstGateController = gateControllers[0];
        _secondGateController = gateControllers.Length < 2 ? null : gateControllers[1];
        
        if (gateControllers.Length == 1)
        {
            _isSingleGate = true;
        }

        if (_firstGateController != null)
        {
            _firstGateController.InitGate(_firstGateSkillType, _firstGateSkillAmount, _firstGatePowerAmount,_firstGateDestructibleType, _firstGateLockAmount);
        }

        if (_secondGateController != null)
        {
            _secondGateController.InitGate(_secondGateSkillType, _secondGateSkillAmount, _secondGatePowerAmount,_secondGateDestructibleType, _secondGateLockAmount);
        }
    }

    private GateController FindInteractedGate(float bulletXPos)
    {
        if (!_isSingleGate)
        {
            if (bulletXPos < 0)
            {
                return _firstGateController;
            }
           
            return _secondGateController;
        }

        return _firstGateController;
    }
    
    public void TakeBulletDamage(float damageAmount, BulletController bullet)
    {
        GateController hittedGate = FindInteractedGate(bullet.transform.position.x);
        hittedGate.IncreaseSkillAmountOnBulletHit(bullet.transform.position);
        _interactableMovementController.Move();
    }
    
    public void InteractPlayer(Transform playerTransform)
    {
        GateController hittedGate = FindInteractedGate(playerTransform.position.x);
        hittedGate.UseSkill();
        Destroy(gameObject);
    }

    public InteractableData GetInteractableData()
    {
        InteractableData data = new InteractableData();

        LevelDataHandler.AddProperty(data, ("_firstGateSkillType", _firstGateSkillType.ToString()),
            ("_firstGateSkillAmount", _firstGateSkillAmount.ToString(CultureInfo.InvariantCulture)),
            ("_firstGatePowerAmount", _firstGatePowerAmount.ToString(CultureInfo.InvariantCulture)),
            ("_secondGateSkillType", _secondGateSkillType.ToString()),
            ("_secondGateSkillAmount", _secondGateSkillAmount.ToString(CultureInfo.InvariantCulture)),
            ("_secondGatePowerAmount", _secondGatePowerAmount.ToString(CultureInfo.InvariantCulture)),
            ("_firstGateDestructibleType", _firstGateDestructibleType == null ? "" : JsonUtility.ToJson(_firstGateDestructibleType)),
            ("_secondGateDestructibleType", _secondGateDestructibleType == null ? "" : JsonUtility.ToJson(_secondGateDestructibleType)),
            ("_firstGateLockAmount", _firstGateLockAmount.ToString(CultureInfo.InvariantCulture)),
            ("_secondGateLockAmount", _secondGateLockAmount.ToString(CultureInfo.InvariantCulture)));

        LevelDataHandler.AddTransformValues(data, transform.position,transform.rotation);
        return data;
    }

    public GameObject GetGameObjectReference()
    {
        return gameObject;
    }
}