using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoSingleton<ParticleManager>
{
    [SerializeField] private GameObject _moneyParticle;

    public void MoneyParticle(Vector3 spawnPos)
    {
        Instantiate(_moneyParticle, spawnPos, Quaternion.identity);
    }
}