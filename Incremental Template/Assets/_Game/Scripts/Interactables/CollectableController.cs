using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour,IDamageable,IInteractable
{
    public void TakeBulletDamage(float damageAmount, BulletController bullet)
    {
    }

    public void InteractPlayer(Transform playerTransform)
    {
    }
}