using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] PathRenderer _path;


    [SerializeField] GameObject _currentLevel;
    [SerializeField] GameObject _firstLevel;

    private Vector3 _destination = Vector3.zero;
    [SerializeField, Tooltip("For the time of move animation")] float _moveTime;


    void Start()
    {
        PlayerPrefs.DeleteAll();

        _currentLevel = GameObject.Find(PlayerPrefs.GetString("CurrentLvl")) == null ?
            _firstLevel : GameObject.Find(PlayerPrefs.GetString("CurrentLvl"));

        Player.transform.position = _currentLevel.transform.position;


        Timer timer = this.gameObject.AddComponent<Timer>();

        StartCoroutine(timer.Execute(1.0f, CheckPosition));
    }

    public void CheckPosition()
    {
        var posDestination = GetLastSelection();

        if (posDestination.transform.position != _destination)
        {

            _destination = posDestination.transform.position;
            PlayerPrefs.SetString("CurrentLvl", posDestination.name);

            StartCoroutine(MoveAndRotate(_moveTime, _destination));
        }
    }

    public GameObject GetLastSelection()
    {
        var last = _path.Paths[0];
        foreach (var item in _path.Paths)
        {
            var buttonSelection = item.GetComponent<LevelSelection>().ButtonSelection;
            if (buttonSelection.IsInteractable() == false)
            {
                break;
            }
            last = item;
        }
        return last;
    }
    private IEnumerator MoveAndRotate(float time, Vector3 targetPos)
    {
        Player.transform.DOMove(targetPos, _moveTime);

        yield return new WaitForSeconds(time);


        //transform.LookAt(nextTarget);
        Vector3 eulerAngles = Player.transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        Player.transform.rotation = Quaternion.Euler(eulerAngles);
    }

}
