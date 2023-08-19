using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private Transform _shootPosTransform;
    [SerializeField] private BulletController _bullet;
    private float _range;
    private float _fireRate;

    public void StartShooting()
    {
        _range = PersistData.Instance.Range;
        _fireRate = PersistData.Instance.FireRate;
        StartCoroutine(StartShoot());
    }
    
    public void CalculateSkills(GateController.SkillTypes skillType, float skillAmount)
    {
        if (skillType == GateController.SkillTypes.Range)
        {
            _range += skillAmount / 5;
            _range = Mathf.Clamp(_range, 13, 50);
        }
        else if (skillType == GateController.SkillTypes.FireRate)
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

            Instantiate(_bullet, _shootPosTransform.position,Quaternion.identity).Shoot(_range,transform);
            yield return new WaitForSeconds(_fireRate);
        }
    }
}
