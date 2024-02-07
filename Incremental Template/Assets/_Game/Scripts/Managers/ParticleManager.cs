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
    
    public void InstantiateParticle(ParticleSystem particleSystem, Vector3 spawnPos, Quaternion rotation, Transform parent = null)
    {
        Instantiate(particleSystem.gameObject, spawnPos, rotation, parent);
    }
}