using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ManagerUI : MonoBehaviour
{
    public Button WeaponMelee;
    public Button WeaponRange;
    public GameObject Congratulation;


    private void Start()
    {
        Congratulation.SetActive(false);
        Congratulation.transform.DOScale(0, 0);
    }

    public void EndStage()
    {
        Congratulation.SetActive(true);
        Congratulation.transform.DOScale(1, 2);
        GameManager.Instance.SceneManagerRef.OnEndLevel(1);
    }

  
}
