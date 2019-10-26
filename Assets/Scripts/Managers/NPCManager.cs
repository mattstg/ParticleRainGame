using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager 
{
    #region Singleton
    public static NPCManager Instance
    {
        get
        {
            return instance ?? (instance = new NPCManager());            
        }
    }

    private static NPCManager instance;

    private NPCManager() { }
    #endregion

    GameObject npcPrefab;
    Transform npcParent;

    public List<NPC> units { get; private set; }

    public void Initialize()
    {
        npcPrefab = Resources.Load<GameObject>("Prefabs/NPC");
        npcParent = new GameObject("NPCParent").transform;
        units = new List<NPC>();
        units.AddRange(GameObject.FindObjectsOfType<NPC>());
        foreach (NPC u in units)
            u.Initialize();
    }

    public void PhysicsRefresh()
    {
        foreach (NPC u in units)
            u.PhysicsRefresh();
    }

    public void PostInitialize()
    {
        foreach (NPC u in units)
            u.PostInitialize();
    }

    public void Refresh()
    {
        foreach (NPC u in units)
            u.Refresh();
    }

    public void HasDied(NPC npcWhoDied)
    {
        units.Remove(npcWhoDied);
    }

    public NPC CreateNPC(Vector2 location)
    {
        NPC toRet = GameObject.Instantiate(npcPrefab, npcParent).GetComponent<NPC>();
        toRet.transform.position = location;
        toRet.Initialize();
        toRet.PostInitialize();
        units.Add(toRet);
        return toRet;
    }
}
