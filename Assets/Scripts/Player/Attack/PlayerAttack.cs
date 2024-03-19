using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private MeleeArea _area;

    [SerializeField] float DelayAttackMeleeHit = 0.5f;
    [SerializeField] float DelayAttackRangeShoot = 0.5f;
    private void Start()
    {
        _area = GetComponentInChildren<MeleeArea>();
    }
    public void OnAttackMelee()
    {
        GameManager.Instance.Player.AnimatorRef.SetTrigger("attack_sword");
        GameManager.Instance.ManagerUIRef.WeaponMelee.interactable = false;

        StartCoroutine(this.gameObject.AddComponent<Timer>().Execute(DelayAttackMeleeHit, AttackMelee));
    }
    private void AttackMelee()
    {
        _area.Target.Hit();
        _area.CanInteract(false);
    }

    public void OnAttackRange()
    {
        GameManager.Instance.Player.AnimatorRef.SetTrigger("attack_gun");
        GameManager.Instance.ManagerUIRef.WeaponRange.interactable = false;

        StartCoroutine(this.gameObject.AddComponent<Timer>().Execute(DelayAttackRangeShoot, AttackRange));
    }
    private void AttackRange()
    {
        GameManager.Instance.BulletObject.Init(transform.position, GameManager.Instance.PlayerTempRef.transform.forward);
    }
}
