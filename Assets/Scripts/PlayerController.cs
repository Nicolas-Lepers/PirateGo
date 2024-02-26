using UnityEngine;
using Movement;

public class PlayerController : MonoBehaviour
{
    public Tile InitialTile;

    private void Start()
    {
        transform.position = InitialTile.transform.position;
    }
}
