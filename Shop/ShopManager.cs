using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public int coins;
    public Text coinText;

    public int grenades;
    public int max_grenades;
    public int price_grenades;
    public Text text_number_grenade;

    public int rocketLauncher;
    public int max_rocketLauncher;
    public int price_rocketLauncher;
    public Text text_number_1xRocket;
    public Text text_number_5xRocket;

    public int phantomSword;
    public int max_phantomSword;
    public int price_phantomSword;

    [Header("Chest System")]
    public Chest commonChest;
    public Chest rareChest;
    public Chest epicChest;
    void Start()
    {
        //Load Data from Load
        coins = SaveSystem.Load().coins;
        coinText.text = coins.ToString();
        grenades = SaveSystem.Load().grenades;
        rocketLauncher = SaveSystem.Load().rocketLauncher;

        //Grenade Stuff
        text_number_grenade.text = grenades + "/" + max_grenades;
        text_number_1xRocket.text = rocketLauncher + "/" + max_rocketLauncher;
        text_number_5xRocket.text = rocketLauncher + "/" + max_rocketLauncher;

    }
    public void BuyGrenade()
    {
        //To be called in buy grenade button
        if(grenades < max_grenades && coins >= price_grenades)
        {
            DataToSave ds = new DataToSave();
            ds = SaveSystem.Load();
            ds.coins -= price_grenades;
            ds.grenades += 1;
            SaveSystem.SavePlayer(ds);

            coins = ds.coins;
            coinText.text = coins.ToString();

            grenades = ds.grenades;
        }
        text_number_grenade.text = grenades + "/" + max_grenades;
    }
    public void Buy1xRocket()
    {
        //To be called in buy grenade button
        if (rocketLauncher < max_rocketLauncher && coins >= price_rocketLauncher)
        {
            DataToSave ds = new DataToSave();
            ds = SaveSystem.Load();
            ds.coins -= price_rocketLauncher;
            ds.rocketLauncher += 1;
            SaveSystem.SavePlayer(ds);

            coins = ds.coins;
            coinText.text = coins.ToString();

            rocketLauncher = ds.rocketLauncher;
        }
        text_number_1xRocket.text = rocketLauncher + "/" + max_rocketLauncher;
        text_number_5xRocket.text = rocketLauncher + "/" + max_rocketLauncher;
    }
    public void Buy5xRocket()
    {
        //To be called in buy grenade button
        if (rocketLauncher < max_rocketLauncher && coins >= price_rocketLauncher)
        {
            DataToSave ds = new DataToSave();
            ds = SaveSystem.Load();
            ds.coins -= price_rocketLauncher;
            ds.rocketLauncher += 5;
            SaveSystem.SavePlayer(ds);
            
            coins = ds.coins;
            coinText.text = coins.ToString();

            rocketLauncher = ds.rocketLauncher;
        }
        text_number_1xRocket.text = rocketLauncher + "/" + max_rocketLauncher;
        text_number_5xRocket.text = rocketLauncher + "/" + max_rocketLauncher;
    }
    public void BuyCommonChest()
    {
        if (coins >= price_rocketLauncher)
        {
            DataToSave ds = new DataToSave();
            ds = SaveSystem.Load();
            ds.coins -= commonChest.cost;
            SaveSystem.SavePlayer(ds);

            coins = ds.coins;
            coinText.text = coins.ToString();

            GlobalChestManager.Instance.inventoryChests.Add(commonChest);
        }
        
    }
    public void BuyRareChest()
    {
        if (coins >= price_rocketLauncher)
        {
            DataToSave ds = new DataToSave();
            ds = SaveSystem.Load();
            ds.coins -= rareChest.cost;
            SaveSystem.SavePlayer(ds);

            coins = ds.coins;
            coinText.text = coins.ToString();

            GlobalChestManager.Instance.inventoryChests.Add(rareChest);
        }

    }
    public void BuyEpicChest()
    {
        if (coins >= price_rocketLauncher)
        {
            DataToSave ds = new DataToSave();
            ds = SaveSystem.Load();
            ds.coins -= epicChest.cost;
            SaveSystem.SavePlayer(ds);

            coins = ds.coins;
            coinText.text = coins.ToString();

            GlobalChestManager.Instance.inventoryChests.Add(epicChest);
        }

    }
    public void HomeButton()
    {
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(0);
    }
}
