using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager 
{
    #region Singleton
    public static EnemyManager Instance
    {
        get
        {
            return instance ?? (instance = new EnemyManager());            
        }
    }

    private static EnemyManager instance;

    private EnemyManager() { }
    #endregion

    public List<Enemy> units { get; private set; }
    GameObject enemyPrefab;
    Transform enemyParent;
    int currentBatchIndex;
    int batchSpacing = 5;
    public void Initialize()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        enemyParent = new GameObject("EnemyParent").transform;
        units = new List<Enemy>();
        units.AddRange(GameObject.FindObjectsOfType<Enemy>());
        foreach (Enemy u in units)
            u.Initialize();
    }

    public void PhysicsRefresh()
    {
        foreach (Enemy u in units)
            u.PhysicsRefresh();
    }

    public void PostInitialize()
    {
        foreach (Enemy u in units)
            u.PostInitialize();
    }

    public void Refresh()
    {
        foreach (Enemy u in units.ToList())
            u.Refresh();
    }

    public Enemy CreateEnemy(Vector2 location)
    {
        Enemy toRet = GameObject.Instantiate(enemyPrefab, enemyParent).GetComponent<Enemy>();
        toRet.transform.position = location;
        toRet.Initialize();
        toRet.PostInitialize();
        units.Add(toRet);
        return toRet;
    }

    public void EnemyDied(Enemy e)
    {
        units.Remove(e);
    }
}
