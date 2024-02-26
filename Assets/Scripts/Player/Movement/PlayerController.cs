using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Tile InitialTile;

    private void Start()
    {
        transform.position = InitialTile.transform.position;
    }
}
