using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class LogicManager : MonoBehaviour
{
    public Tilemap map;
    public TileBase tile;

    private Vector2 mWorldPos;
    private Vector2 mPos;

    void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        mPos = Mouse.current.position.ReadValue();
        mWorldPos = Camera.main.ScreenToWorldPoint(mPos);

        Vector3Int temp = map.WorldToCell(mWorldPos);

        map.SetTile(temp, tile);
    }
}
