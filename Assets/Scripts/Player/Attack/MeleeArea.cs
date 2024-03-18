using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeArea : MonoBehaviour
{
    public IHitable Target;
    private bool _canInteract = true;
    private void OnTriggerEnter(Collider other)
    {
        IHitable hitable = other.GetComponent<IHitable>();
        GameManager game = GameManager.Instance;
        if (hitable != null && _canInteract == true)
        {
            Debug.Log("enter");
            Target = hitable;
            game.ManagerUIRef.WeaponMelee.interactable = true;
        }
    }

    public void CanInteract(bool value)
    {
        _canInteract = value;
    }
    

    private void OnTriggerExit(Collider other)
    {
        IHitable hitable = other.GetComponent<IHitable>();
        GameManager game = GameManager.Instance;
        if (hitable != null && _canInteract == true)
        {
            Debug.Log("exit");
            Target = null;
            game.ManagerUIRef.WeaponMelee.interactable = false;
        }
    }
}
