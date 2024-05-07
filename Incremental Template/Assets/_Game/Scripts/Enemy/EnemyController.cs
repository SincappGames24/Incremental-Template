using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseInteractableController
{
    public override void TakeBulletDamage(float damageAmount, BulletController bullet)
    {
        base.TakeBulletDamage(damageAmount, bullet);
    }

    public override void InteractPlayer(Transform playerTransform)
    {
        base.InteractPlayer(playerTransform);
    }
}
