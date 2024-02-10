using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : MonoBehaviour
{
    public float BulletPower { get; set; } = 1;
    private ObjectPool<BulletController> _currentPool;
    
    public void SetPool(ObjectPool<BulletController> pool)
    {
        _currentPool = pool;
    }
    
    public void Shoot(float range, Transform playerPos)
    {
        transform.DOMoveZ(playerPos.position.z + range, 20).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
        {
            _currentPool.Release(this);
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        var damagable = other.GetComponent<IDamageable>();

        if (damagable != null)
        {
            damagable.TakeBulletDamage(BulletPower, this);
            _currentPool.Release(this);
        }
    }
}
