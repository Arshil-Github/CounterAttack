using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalChestManager : MonoBehaviour
{
    #region GlobalCrap
    //For making this a global Script
    public static GlobalChestManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public List<Chest> allChests;
    public List<Chest> inventoryChests;

    public void AddChestInInven(Chest c)
    {
        inventoryChests.Add(c);
    }
}
