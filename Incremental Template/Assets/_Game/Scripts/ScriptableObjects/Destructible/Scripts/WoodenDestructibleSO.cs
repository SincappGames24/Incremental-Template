using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/DestructibleTypes/WoodenDestructible", fileName = "WoodenDestructible",
    order = 0)]
public class WoodenDestructibleSO : DestructibleBaseSO
{
    [SerializeField] private Transform _woodenPrefab;
    private Rigidbody[] _woodenRigi;

    public override void InitDestructible(float startLockAmount,Transform parent)
    {
        base.InitDestructible(startLockAmount,parent);
        DesctructibleHolder = Instantiate(_woodenPrefab, parent);
        _woodenRigi = DesctructibleHolder.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigi in _woodenRigi)
        {
            rigi.transform.SetParent(DesctructibleHolder);
        }
    }

    public override void Interatact(float lockAmount,Transform bulletTransform)
    {
        var closedWoodenCountLerp = (Mathf.InverseLerp(StartLockAmount, 0, lockAmount));
        var closedWoodenCount = Mathf.FloorToInt(Mathf.Lerp(0, _woodenRigi.Length, closedWoodenCountLerp));

        ParticleManager.Instance.InstantiateParticle(DestructionParticle,
            bulletTransform.position + Vector3.down * .1f + Vector3.back * .2f, Quaternion.identity, DesctructibleParent);
        
        for (var i = 0; i < closedWoodenCount; i++)
        {
            Rigidbody rigi = _woodenRigi[i];

            if (rigi.isKinematic)
            {
                rigi.isKinematic = false;
                Vector3 targetPos = Vector3.zero;
                targetPos.y = Random.Range(1, 3);
                targetPos.x = Random.Range(-1, 1);
                targetPos.z = Random.Range(-4, -1);
                rigi.transform.SetParent(null);
                rigi.AddForce(targetPos, ForceMode.Impulse);
                rigi.AddTorque(targetPos * 3, ForceMode.Impulse);
            }
        }
    }
}