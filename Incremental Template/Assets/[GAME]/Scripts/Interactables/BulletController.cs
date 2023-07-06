using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public void Shoot(float range)
    {
        transform.DOMoveZ(transform.position.z + range, 30).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
