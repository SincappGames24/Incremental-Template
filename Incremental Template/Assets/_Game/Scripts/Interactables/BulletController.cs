using DG.Tweening;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float BulletPower { get; set; }
    
    public void Shoot(float range, Transform playerPos)
    {
        transform.DOMoveZ(playerPos.position.z + range, 20).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var damagable = other.GetComponent<IDamageable>();
        
        if (damagable != null)
        {
            damagable.TakeBulletDamage(BulletPower,this);
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}
