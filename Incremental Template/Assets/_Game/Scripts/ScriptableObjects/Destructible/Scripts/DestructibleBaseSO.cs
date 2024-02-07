using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructibleBaseSO : ScriptableObject
{
    public enum DestructibleTypes
    {
        Wooden,
        Concrete
    }

    public ParticleSystem DestructionParticle;
    public AudioClip DestructionSound;
    protected float StartLockAmount;
    protected Transform DesctructibleHolder;
    protected Transform DesctructibleParent;

    public virtual void InitDestructible(float startLockAmount, Transform parent)
    {
        StartLockAmount = startLockAmount;
        DesctructibleParent = parent;
    }

    public abstract void Interatact(float currentLockAmount, Transform bulletTransform);

    public virtual void Destroy()
    {
      Destroy(DesctructibleHolder.gameObject);   
    }
}
