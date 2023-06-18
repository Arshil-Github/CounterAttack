using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coins;
    public int xp;

    public int grenades;
    public int max_grenades;

    public int rocketLauncher;
    public int max_rocketLauncher;

    public int phantomSword;
    public int max_phantomSword;

    public PlayerData(DataToSave ds)
    {
        coins = ds.coins;
        xp = ds.xp;

        grenades = ds.grenades;
        max_grenades = ds.max_grenades;

        rocketLauncher = ds.rocketLauncher;
        max_rocketLauncher = ds.max_rocketLauncher;

        phantomSword = ds.phantomSword;
        max_phantomSword = ds.max_phantomSword;
    }
}
