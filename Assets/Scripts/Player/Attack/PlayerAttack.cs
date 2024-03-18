using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private MeleeArea _area;
    private void Start()
    {
        _area = GetComponentInChildren<MeleeArea>();
    }
    public void OnAttackMelee()
    {
        GameManager game = GameManager.Instance;
        _area.Target.Hit();
        _area.CanInteract(false);
        game.ManagerUIRef.WeaponMelee.interactable = false;
    }


    public void OnAttackRange()
    {
        GameManager game = GameManager.Instance;
        game.BulletObject.Init(transform.position, game.PlayerTempRef.transform.forward);
        game.ManagerUIRef.WeaponRange.interactable = false;
    }
}
