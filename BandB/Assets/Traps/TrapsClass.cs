using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// [CreateAssetMenu] // allows you to create the asset in unity itself
public class TrapsClass : ScriptableObject
{
    public TileBase[] tiles;

    protected int width = 1;
    protected int height = 1;

    public virtual void Enable()
    {

    }
}
