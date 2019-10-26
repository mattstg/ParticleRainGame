using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldLinks : MonoBehaviour
{
    private static WorldLinks instance;
    public static WorldLinks Instance { get { return instance ?? (instance = GameObject.FindObjectOfType<WorldLinks>()); } }

    public Collider2D worldBounds;
    public Transform playerStartLocation;
    public TileBase woodTile;
    public TileBase fireTile;
    public Tilemap woodTileMap;
    public Tilemap fireTileMap;
    public AnimationCurve flamesPerPercent;

}
