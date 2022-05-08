using System;
using System.Collections;
using System.Collections.Generic;
using SincappStudio;
using TMPro;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _incomeParticles = new List<GameObject>();
    private float _incomeAmount;
    private Transform _incomePos;

    private void OnEnable()
    {
        EventManager.GameStart += SetInComeAmount;
        EventManager.OnGetIncome += ShowIncome;
    }

    private void OnDisable()
    {
        EventManager.GameStart -= SetInComeAmount;
        EventManager.OnGetIncome -= ShowIncome;
    }

    private void Start()
    {
        _incomePos = FindObjectOfType<PlayerController>().transform;
    }

    private void SetInComeAmount()
    {
        _incomeAmount = PersistData.Instance.Income;
     
        for (int i = 0; i < _incomeParticles.Count; i++)
        {
            _incomeParticles[i].GetComponentInChildren<TextMeshPro>().SetText(_incomeAmount.ToString("f1")+"$");
        }
    }

    private void ShowIncome()
    {
        _incomeParticles[0].transform.position = _incomePos.position + new Vector3(1,.5f,0);
        _incomeParticles[0].SetActive(true);
        var holderObj = _incomeParticles[0];
        _incomeParticles.RemoveAt(0);

        StartCoroutine(Sincapp.WaitAndAction(.75f, () =>
        {
            _incomeParticles.Add(holderObj);
            holderObj.SetActive(false);
        }));
    }
}
