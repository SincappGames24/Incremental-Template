using UnityEngine;

public class DestructibleBaseSO : ScriptableObject
{
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

    public virtual void Interatact(float currentLockAmount, Vector3 bulletPos) {}

    public virtual void Destroy()
    {
      Destroy(DesctructibleHolder.gameObject);   
    }
}
