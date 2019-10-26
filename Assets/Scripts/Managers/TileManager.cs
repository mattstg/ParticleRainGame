using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager
{
    #region Singleton
    public static TileManager Instance
    {
        get
        {
            return instance ?? (instance = new TileManager());
        }
    }

    private static TileManager instance;

    private TileManager() { }
    #endregion
    Vector2Int gridSize = new Vector2Int(50, 25); //make them even
    public float[,] gridTiles; //Represents the value of wood hp, 0 is destroyed and gone, 1 is healthy
    public ParticleSystem[,] particleSystems; //Represents the value of wood hp, 0 is destroyed and gone, 1 is healthy

    //Batches
    int curBatchNumber;
    int numOfBatches = 10;

    //Fire particle balances
    int numOfInitialFires = 5;
    int maxFireParticles = 15;
    float HEAT_PER_PARTICLE = .005f;
    GameObject fireParticlePrefab;
    public void Initialize()
    {
        //make the grid tracker
        gridTiles = new float[gridSize.x, gridSize.y];
        particleSystems = new ParticleSystem[gridSize.x, gridSize.y];
        Transform particleParent = new GameObject("ParticlesParent").transform;
        fireParticlePrefab = Resources.Load<GameObject>("FireSystem");

        //Create the grid
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                WorldLinks.Instance.woodTileMap.SetTile(new Vector3Int(i, j, 0), WorldLinks.Instance.woodTile);
                WorldLinks.Instance.fireTileMap.SetTile(new Vector3Int(i, j, 0), WorldLinks.Instance.fireTile);
                WorldLinks.Instance.fireTileMap.SetTileFlags(new Vector3Int(i, j, 0), UnityEngine.Tilemaps.TileFlags.None); //I hate unity for this
                WorldLinks.Instance.fireTileMap.SetColor(new Vector3Int(i, j, 0), new Color(0,0,0,0f)); //invisible
                particleSystems[i, j] = GameObject.Instantiate(fireParticlePrefab, particleParent).GetComponent<ParticleSystem>();
                particleSystems[i, j].transform.position = new Vector3(i + .5f, j + .5f, 0);
                var e = particleSystems[i, j].emission;
                e.rateOverTime = 0;
            }
        }

        //Create a spot for player
        Vector3Int randomSpot = new Vector3Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y), 0);
        WorldLinks.Instance.woodTileMap.SetTile(randomSpot, null);
        gridTiles[randomSpot.x, randomSpot.y] = -1;
        PlayerManager.Instance.SpawnPlayer((Vector2Int)randomSpot);

        //Set random grids on fire
        for(int i = 0; i < numOfInitialFires; i++)
        {
            Vector2Int loc;
            do
            {
                loc = new Vector2Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y - 1));
            }
            while (gridTiles[loc.x, loc.y] != 0);
            gridTiles[loc.x, loc.y + 1] = -1;
            WorldLinks.Instance.woodTileMap.SetTile(new Vector3Int(loc.x, loc.y + 1, 0), null);
            SetTileFireAmt(loc, .5f); //Fire at 50%
        }

        
    }

    private void ModTileFireAmt(Vector2Int gridLoc, float modFireAmt)
    {
        SetTileFireAmt(gridLoc, modFireAmt + gridTiles[gridLoc.x, gridLoc.y]);
    }
    private void SetTileFireAmt(Vector2Int gridLoc, float fireAmt)
    {
        gridTiles[gridLoc.x, gridLoc.y] = fireAmt; 
        WorldLinks.Instance.fireTileMap.SetColor(new Vector3Int(gridLoc.x, gridLoc.y, 0), new Color(1, 1, 1, fireAmt));
        int amountOfParticles = (int)(WorldLinks.Instance.flamesPerPercent.Evaluate(fireAmt) * maxFireParticles);
        var e = particleSystems[gridLoc.x, gridLoc.y].emission;
        e.rateOverTime = amountOfParticles;
        if (gridTiles[gridLoc.x, gridLoc.y] >= .75f)
        {
            WorldLinks.Instance.woodTileMap.SetTile(new Vector3Int(gridLoc.x, gridLoc.y, 0), null);            
        }
    }

    public void FireParticleHitLocation(Vector2 loc)
    {
        //Debug.Log("Hit loc: " + loc);
        Vector2Int gridLoc = new Vector2Int((int)loc.x, (int)loc.y);
        ModTileFireAmt(gridLoc, HEAT_PER_PARTICLE);
    }

    public void PhysicsRefresh()
    {

    }

    public void PostInitialize()
    {

    }

    public void Refresh()
    {

    }
}

