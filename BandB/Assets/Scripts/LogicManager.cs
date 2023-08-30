using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class LogicManager : MonoBehaviour
{
    public Tilemap map;
    public TileBase tile;
    public GameObject keyboardM;
    public GameObject controlerM;

    private Vector2 keyboardMLoc;
    private Vector2 controlerMLoc;

    void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        // mPos = Mouse.current.position.ReadValue();
        // mWorldPos = Camera.main.ScreenToWorldPoint(mPos);

        keyboardMLoc = keyboardM.transform.position;
        controlerMLoc = controlerM.transform.position;

        // set the tiles white when the mouse hovers over it
        Vector3Int temp = map.WorldToCell(keyboardMLoc);

        map.SetTile(temp, tile);

        // set the tiles white when the controler hoevers over it
        temp = map.WorldToCell(controlerMLoc);

        map.SetTile(temp, tile);
    }
}
