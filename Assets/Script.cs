using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{


    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) { return; }
        Touch touch = Input.GetTouch(0);
        //touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            
        }

    }
}
