using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave : MonoBehaviour
{
    public int coins;
    public int xp;
    public int level;

    public int grenades;
    public int max_grenades;
    public int rocketLauncher;
    public int max_rocketLauncher;
    public int phantomSword;
    public int max_phantomSword;

    private void Start()
    {
        DataToSave ds = SaveSystem.Load();

        coins = ds.coins;
        xp = ds.xp;
    }
    public void Savedata()
    {
        SaveSystem.SavePlayer(this);
    }
    public void SetExperience(int xp)
    {
        GameObject.Find("Experience").GetComponent<ExperienceManager>().increaseXp(xp);
    }
    public void SetCoin(int num)
    {
        //Call this function when coins are gained to Update UI
        coins += num;
        GameObject.Find("GameManager").GetComponent<GameManager>().ChangeCoinText(coins);
    }
}
