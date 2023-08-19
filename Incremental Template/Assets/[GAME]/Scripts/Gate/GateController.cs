using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using SincappStudio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GateController : MonoBehaviour
{
    public enum SkillTypes
    {
        Range,
        FireRate,
    }

    [SerializeField] private SkillTypes _skillType;
    [SerializeField] float _skillAmount;
    [SerializeField] float _powerAmount;
    [Header("References")]
    [SerializeField] private TextMeshPro _skillAmountText;
    [SerializeField] private TextMeshPro _skillNameText;
    [SerializeField] private TextMeshPro _powerAmountText;
    [SerializeField] private MeshRenderer _gateMesh;

    private void Awake()
    {
        _skillNameText.SetText(_skillType.ToString());
        
        string mathSign = "+";

        if (_powerAmount < 0)
        {
            mathSign = "";
        }

        _powerAmountText.SetText($"{mathSign}{_powerAmount}");
        
        SetSkillAmountText();
    }

    public void IncreaseSkillAmountOnBulletHit()
    {
        _skillAmount += _powerAmount;
        MMVibrationManager.Haptic(HapticTypes.SoftImpact);

        _skillAmountText.transform.DOScale(Vector3.one * 1.15f, .075f).OnComplete(() =>
        {
            _skillAmountText.transform.DOScale(Vector3.one, .075f);
        });

        SetSkillAmountText();
    }
    
    public void UseSkill()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        EventManager.OnGateCollect?.Invoke(_skillType, _skillAmount);
        transform.DOKill();
        Destroy(transform.parent.gameObject);
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