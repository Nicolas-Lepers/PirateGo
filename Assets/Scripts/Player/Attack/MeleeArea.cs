using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeArea : MonoBehaviour
{
    public IHitable Target;
    private void OnTriggerEnter(Collider other)
    {
        IHitable hitable = other.GetComponent<IHitable>();
        GameManager game = GameManager.Instance;
        if (hitable != null)
        {
            Debug.Log("enter");
            Target = hitable;
            game.ManagerUIRef.WeaponMelee.interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IHitable hitable = other.GetComponent<IHitable>();
        GameManager game = GameManager.Instance;
        if (hitable != null)
        {
            Debug.Log("exit");
            Target = null;
            game.ManagerUIRef.WeaponMelee.interactable = false;
        }
    }
}
