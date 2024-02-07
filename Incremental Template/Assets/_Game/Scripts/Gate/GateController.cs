using System.Globalization;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using Sirenix.OdinInspector;
using TMPro;

public class GateController : MonoBehaviour
{
    private DestructibleBaseSO _destructibleBase;
    private GateGroupController.SkillTypes _skillType;
    private float _skillAmount;
    private float _powerAmount;
    private float _lockAmount;
    private bool _isGateLock;

    #region References

    public bool ShowReferences;
    [Header("References")] 
    [ShowIf("ShowReferences")] [SerializeField] private TextMeshPro _skillAmountText;
    [ShowIf("ShowReferences")] [SerializeField] private TextMeshPro _skillNameText;
    [ShowIf("ShowReferences")] [SerializeField] private TextMeshPro _powerAmountText;
    [ShowIf("ShowReferences")] [SerializeField] private TextMeshPro _lockAmountText;
    [ShowIf("ShowReferences")] [SerializeField] private MeshRenderer _gateMesh;

    #endregion

    public void InitGate(GateGroupController.SkillTypes skillType, float skillAmount, float powerAmount, DestructibleBaseSO destructibleBase,float lockAmount)
    {
        _skillType = skillType;
        _skillAmount = skillAmount;
        _powerAmount = powerAmount;
        _destructibleBase = destructibleBase;
        _lockAmount = lockAmount;

        if (destructibleBase != null && lockAmount > 0)
        {
            _destructibleBase.InitDestructible(lockAmount,transform);
            _isGateLock = true;
            _lockAmountText.gameObject.SetActive(true);
        }

        SetGateTexts();
    }
    
    public void UseSkill()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        EventManager.OnGateCollect?.Invoke(_skillType, _skillAmount);
        transform.DOKill();
        Destroy(transform.parent.gameObject);
    }
    
    private void SetGateTexts()
    {
        _skillNameText.SetText(_skillType.ToString());

        string mathSign = "+";

        if (_powerAmount < 0)
        {
            mathSign = "";
        }

        _powerAmountText.SetText($"{mathSign}{_powerAmount}");

        SetLockAmountText();
        SetSkillAmountText();
    }

    private void HitDestructible(Transform bulletTransform)
    {
        _lockAmount = Mathf.Clamp(_lockAmount - 1, 0, int.MaxValue);
        _destructibleBase.Interatact(_lockAmount, bulletTransform);
        SetLockAmountText();

        if (_lockAmount == 0)
        {
            _destructibleBase.Destroy();
            _isGateLock = false;
            _lockAmountText.gameObject.SetActive(false);
        }
    }

    public void IncreaseSkillAmountOnBulletHit(Transform bulletTransform)
    {
        if (_isGateLock)
        {
            HitDestructible(bulletTransform);
            return;
        }
            
        _skillAmount += _powerAmount;
        MMVibrationManager.Haptic(HapticTypes.SoftImpact);

        _skillAmountText.transform.DOScale(Vector3.one * 1.15f, .075f).OnComplete(() =>
        {
            _skillAmountText.transform.DOScale(Vector3.one, .075f);
        });

        SetSkillAmountText();
    }
    
    private void SetSkillAmountText()
    {
        string mathSign = "+";

        if (_skillAmount < 0)
        {
            mathSign = "";
        }

        _skillAmountText.SetText($"{mathSign}{_skillAmount:0}");
        CheckGateColor();
    }
    
    private void SetLockAmountText()
    {
        _lockAmountText.SetText($"-{_lockAmount}");
    }

    private void CheckGateColor()
    {
        if (_skillAmount < 0)
        {
            _gateMesh.materials[0].color = new Color(0.74f, 0.06f, 0.03f, 0.68f);
            _gateMesh.materials[1].color = new Color(0.76f, 0.29f, 0.28f, 0.68f);
        }
        else
        {
            _gateMesh.materials[0].color = new Color(0f, 0.59f, 0.14f, 0.68f);
            _gateMesh.materials[1].color = new Color(0.39f, 0.69f, 0.38f, 0.68f);
        }
    }
}