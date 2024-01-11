using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public void Shoot(float range, Transform playerPos)
    {
        transform.DOMoveZ(playerPos.position.z + range, 20).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
