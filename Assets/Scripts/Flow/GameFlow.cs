using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow 
{
    #region Singleton
    public static GameFlow Instance
    {
        get
        {
            if (instance == null)
                instance = new GameFlow();
            return instance;
        }
    }

    private static GameFlow instance;

    private GameFlow() { }
    #endregion

    public void Initialize()
    {
        PlayerManager.Instance.Initialize();
        InputManager.Instance.Initialize();
        TileManager.Instance.Initialize();

    }

    public void PostInitialize()
    {
        PlayerManager.Instance.PostInitialize();
        InputManager.Instance.PostInitialize();
        TileManager.Instance.PostInitialize();

    }

    public void Refresh()
    {
        PlayerManager.Instance.Refresh();
        InputManager.Instance.Refresh();
        TileManager.Instance.Refresh();

    }

    public void PhysicsRefresh()
    {
        PlayerManager.Instance.PhysicsRefresh();
        InputManager.Instance.PhysicsRefresh();
        TileManager.Instance.PhysicsRefresh();

    }
}
