using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager
{
    #region Singleton
    public static PlayerManager Instance
    {
        get
        {
            return instance ?? (instance = new PlayerManager());
        }
    }

    private static PlayerManager instance;

    private PlayerManager() { }
    #endregion

    public Player player { set; get; }
    public void Initialize()
    {
        //GameObject go = GameObject.Find("PlayerSpawn");
        //GameObject.FindObjectOfType<WorldLinks>().playerStartLocation

        
    }

    public void SpawnPlayer(Vector2Int gridLoc)
    {
        GameObject playerObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        player = playerObj.GetComponent<Player>();
        player.transform.position = new Vector3(gridLoc.x, gridLoc.y,0) + new Vector3(.5f,.5f,0);
        player.Initialize();

        CinemachineVirtualCamera cvm = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        cvm.Follow = playerObj.transform;
    }
    public void PostInitialize()
    {
        player.PostInitialize();
    }

    public void PhysicsRefresh()
    {
        player.PhysicsRefresh();
    }


    public void Refresh()
    {
        player.Refresh();
    }
}
