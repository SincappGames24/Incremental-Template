using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DestructibleTypes/ConcreteDestructible", fileName = "ConcreteDestructible", order = 1)]
public class ConcreteDestructibleSO : DestructibleBaseSO
{
    [SerializeField] private Transform _concretePrefab;
    private List<Rigidbody> _concretePieceRigis;
    private int _startDestructableCount;
    private int _explodedDestructableCount;

    public override void InitDestructible(float startLockAmount, Transform parent)
    {
        DesctructibleHolder = Instantiate(_concretePrefab, parent);
        _concretePieceRigis = DesctructibleHolder.GetComponentsInChildren<Rigidbody>(true).ToList();
        _startDestructableCount = _concretePieceRigis.Count;
    }

    public override void Interatact(float currentLockAmount,Transform bulletTransform)
    {
        var closedWoodenCountLerp = (Mathf.InverseLerp(StartLockAmount, 0, currentLockAmount));
        var closedWoodenCount = Mathf.CeilToInt(Mathf.Lerp(0, _startDestructableCount, closedWoodenCountLerp));
        DesctructibleHolder.GetChild(1).gameObject.SetActive(false);
        _concretePieceRigis[0].transform.parent.gameObject.SetActive(true);

        for (var i = 0; i < closedWoodenCount - _explodedDestructableCount; i++)
        {
            Rigidbody rigi = _concretePieceRigis
                .OrderBy(obj => Vector3.Distance(obj.transform.position, bulletTransform.position))?
                .FirstOrDefault(x => x.isKinematic);

            if (rigi != null)
            {
                rigi.isKinematic = false;
                Vector3 targetPos = Vector3.zero;
                targetPos.y = Random.Range(1, 3);
                targetPos.x = Random.Range(-1, 1);
                targetPos.z = Random.Range(-4, -1);
                rigi.transform.SetParent(null);
                rigi.AddForce(targetPos, ForceMode.Impulse);
                rigi.AddTorque(targetPos * 3, ForceMode.Impulse);
                
                if (i == 0)
                {
                    ParticleManager.Instance.InstantiateParticle(DestructionParticle, rigi.transform.position
                        , Quaternion.identity);
                }
            }
        }
        
        _explodedDestructableCount += closedWoodenCount - _explodedDestructableCount;
    }
}