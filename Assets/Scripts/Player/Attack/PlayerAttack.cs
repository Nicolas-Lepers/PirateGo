using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private MeleeArea _area;

    [SerializeField] float _delayAttackMeleeHit = 1;
    [SerializeField] float _delayAttackRangeShoot = 1;

    [SerializeField] AudioClip _pistol;
    [SerializeField] ParticleSystem _shot;
    [SerializeField] Transform _shotOrigin;
    [SerializeField] AudioClip _sword;
    private void Start()
    {
        _area = GetComponentInChildren<MeleeArea>();
    }
    public void OnAttackMelee()
    {
        GameManager.Instance.Player.AnimatorRef.SetTrigger("attack_sword");
        GameManager.Instance.ManagerUIRef.WeaponMelee.interactable = false;

        StartCoroutine(this.gameObject.AddComponent<Timer>().Execute(_delayAttackMeleeHit, AttackMelee));
    }
    private void AttackMelee()
    {
        AudioManager.Instance.PlaySFXSound(_sword);
        _area.Target.Hit();
        _area.CanInteract(false);
    }

    public void OnAttackRange()
    {
        GameManager.Instance.Player.AnimatorRef.SetTrigger("attack_gun");
        GameManager.Instance.ManagerUIRef.WeaponRange.interactable = false;

        StartCoroutine(this.gameObject.AddComponent<Timer>().Execute(_delayAttackRangeShoot, AttackRange));
    }
    private void AttackRange()
    {
        _shot.transform.position = _shotOrigin.transform.position;
        _shot.transform.rotation = _shotOrigin.transform.rotation;
        _shot.Play();
        AudioManager.Instance.PlaySFXSound(_pistol);
        GameManager.Instance.BulletObject.Init(transform.position, GameManager.Instance.PlayerTempRef.transform.forward);
    }
}
