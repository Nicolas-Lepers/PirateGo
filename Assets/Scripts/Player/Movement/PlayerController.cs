using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public Tile InitialTile;
    public Tile CurrentTile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is another Player Controller in this scene !");
        }
    }

    private void Start()
    {
        transform.position = InitialTile.transform.position;
        CurrentTile = InitialTile;
    }

    public Tile SwipeDirection(Vector2 direction, float _directionThreshold)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Forward");
            return CurrentTile.ForwardTile;
        }
        else if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            Debug.Log("Swipe BackWard");
            return CurrentTile.BackwardTile;
        }
        else if (Vector2.Dot(Vector2.left, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Left");
            return CurrentTile.LeftTile;
        }
        else if (Vector2.Dot(Vector2.right, direction) > _directionThreshold)
        {
            Debug.Log("Swipe Right");
            return CurrentTile.RightTile;
        }

        return null;
    }

    public void Move(Tile nextTile)
    {
        if (nextTile != null)
        {
            transform.position = nextTile.transform.position;
            CurrentTile = nextTile;
        }
    }
}