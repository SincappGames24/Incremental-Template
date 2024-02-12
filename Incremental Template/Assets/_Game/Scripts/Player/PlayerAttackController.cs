using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Transform _shootPosTransform;
    [SerializeField] private BulletController _bullet;
    private ObjectPool<BulletController> _bulletPool;
    private float _range;
    private float _fireRate;

    private void Awake()
    {
        _bulletPool = new ObjectPool<BulletController>(InstantiateBullet, OnTakeBulletFromPool, OnReturnBulletToPool,
            OnDestroyBullet, true, 20, 100);
    }

    public void StartShooting()
    {
        _range = PersistData.Instance.Range;
        _fireRate = PersistData.Instance.FireRate;
        StartCoroutine(StartShoot());
    }

    public void CalculateSkills(GateGroupController.SkillTypes skillType, float skillAmount)
    {
        if (skillType == GateGroupController.SkillTypes.Range)
        {
            _range += skillAmount / 5;
            _range = Mathf.Clamp(_range, 13, 50);
        }
        else if (skillType == GateGroupController.SkillTypes.FireRate)
        {
            _fireRate -= skillAmount / 150;
            _fireRate = Mathf.Clamp(_fireRate, .075f, int.MaxValue);
        }
    }

    private IEnumerator StartShoot()
    {
        while (PlayerController.PlayerState == PlayerController.PlayerStates.Run
               || PlayerController.PlayerState == PlayerController.PlayerStates.OnFinishWall)
        {
            // _rightHand.DOLocalRotate(new Vector3(-10,0,0), .1f).SetEase(Ease.OutSine).OnComplete(() =>
            // {
            //     _rightHand.DOLocalRotate(Vector3.zero, _fireRate);
            // });

            _bulletPool.Get().Shoot(_range, transform);
            yield return new WaitForSeconds(_fireRate);
        }
    }

    private BulletController InstantiateBullet()
    {
        var bullet = Instantiate(_bullet, _shootPosTransform.position, Quaternion.identity);
        bullet.SetPool(_bulletPool);
        return bullet;
    }

    private void OnTakeBulletFromPool(BulletController bullet)
    {
        bullet.transform.position = _shootPosTransform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(BulletController bullet)
    {
        bullet.transform.DOKill();
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(BulletController bullet)
    {
        Destroy(bullet.gameObject);
    }
}