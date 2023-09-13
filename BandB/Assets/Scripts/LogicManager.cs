using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class LogicManager : MonoBehaviour
{
    public Tilemap map;
    public TileBase tile;
    public TileBase emptyTile;
    public GameObject keyboardM;
    public GameObject controlerM;

    public List<ScriptableObject> traps;

    private Vector2 keyboardMLocOld;
    private Vector2 controlerMLocOld;

    private TileBase oldTileKey;
    private TileBase oldTileCont;

    void Awake()
    {
        Cursor.visible = false;
        oldTileKey = emptyTile;
        oldTileCont = emptyTile;
        keyboardMLocOld = keyboardM.transform.position;
        controlerMLocOld = controlerM.transform.position;
    }

    private void Update()
    {
        // mPos = Mouse.current.position.ReadValue();
        // mWorldPos = Camera.main.ScreenToWorldPoint(mPos);

        Vector2 keyboardMLoc = keyboardM.transform.position;
        Vector2 controlerMLoc = controlerM.transform.position;

        SetTiles(keyboardMLoc, ref keyboardMLocOld, ref oldTileKey);
        SetTiles(controlerMLoc, ref controlerMLocOld, ref oldTileCont);
    }

    private void SetTiles(Vector2 mLoc, ref Vector2 oldLoc, ref TileBase oldTile)
    {
        // transforms the location of mouse into a cell location
        Vector3Int tempMLoc = map.WorldToCell(mLoc);
        Vector3Int tempMLocOld = map.WorldToCell(oldLoc);

        if (tempMLocOld != tempMLoc)
        {
            // get the tile that is currently at the location
            TileBase tempTile = map.GetTile(tempMLoc);

            // set the tiles white when a mouse hovers over it
            TileCreation(tile, tempMLoc);

            // remove the previous tile
            TileCreation(oldTile, tempMLocOld);

            oldTile = tempTile;
            oldLoc = mLoc;
        }
    }

    private void TileCreation(TileBase t, Vector3Int pos)
    {
        // sets the tile in the position given
        map.SetTile(pos, t);
    }
}
