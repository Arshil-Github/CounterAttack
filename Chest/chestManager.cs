using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;


public class chestManager : MonoBehaviour
{
    /*[Header("Weapons")]
    public Weapon[] commonWeapon;
    public Weapon[] rareWeapon;
    public Weapon[] epicWeapon;
    public Weapon[] legendaryWeapon;*/

    [Header("Objects")]
    public GameObject ChestParentObject;
    public GameObject pf_Chest;

    public ChestOpenUI chestOpenUI;
    public void Start()
    {
        List<Chest> inventoryChest = GlobalChestManager.Instance.inventoryChests;
        foreach(Chest c in GlobalChestManager.Instance.inventoryChests)
        {
            GameObject a = Instantiate(pf_Chest, ChestParentObject.transform);
            a.GetComponent<Image>().sprite = c.sprite;
            a.GetComponent<ChestBlock>().chest = c;
            a.GetComponent<Button>().onClick.AddListener(delegate { OpenChest(a.GetComponent<ChestBlock>().chest); });
        }
    }

    void OpenChest(Chest c)
    {
        chestOpenUI.gameObject.SetActive(true);
        chestOpenUI.SetUp();
        chestOpenUI.chestToLoad = c;
    }
    /* public void weaponRewarded()
     {
         anim.SetTrigger("openChest");

         int m = Random.Range(1, 100 * multiplier);

         if (IsWithin(m, 0, w.commonPercentage * multiplier))
         {
             Transform reward = Instantiate(pfslot, SloatUI.position, Quaternion.identity);
             reward.parent = SloatUI;

             *//*int i = Random.Range(0, commonWeapon.Length);
             reward.GetComponent<Image>().sprite = commonWeapon[i].weaponImage;

             Debug.Log(commonWeapon[i].name);

             if (commonWeapon[i].isPurchased == true)
             {
                 commonWeapon[i].attack += attackAdder;
                 newAttack.gameObject.SetActive(true);
                 newAttack.text = "New Attack - " + commonWeapon[i].attack;
             }
             else
             {
                 commonWeapon[i].isPurchased = true;
             }
 *//*
             return;
         }
         else if (IsWithin(m, w.commonPercentage * multiplier, w.rarePercentage * multiplier))
         {
             Transform reward = Instantiate(pfslot, SloatUI.position, Quaternion.identity);
             reward.parent = SloatUI;

            *//* int i = Random.Range(0, rareWeapon.Length);
             reward.GetComponent<Image>().sprite = rareWeapon[i].weaponImage;

             if (rareWeapon[i].isPurchased == true)
             {
                 rareWeapon[i].attack += attackAdder;
                 newAttack.gameObject.SetActive(true);
                 newAttack.text = "New Attack - " + rareWeapon[i].attack;
             }
             else
             {
                 rareWeapon[i].isPurchased = true;
             }*//*

             return;
         }
         else if (IsWithin(m, w.rarePercentage * multiplier, w.epicPercentage * multiplier))
         {
             Transform reward = Instantiate(pfslot, SloatUI.position, Quaternion.identity);
             reward.parent = SloatUI;

             *//*int i = Random.Range(0, epicWeapon.Length);
             reward.GetComponent<Image>().sprite = epicWeapon[i].weaponImage;

             if (epicWeapon[i].isPurchased == true)
             {
                 epicWeapon[i].attack += attackAdder;
                 newAttack.gameObject.SetActive(true);
                 newAttack.text = "New Attack - " + epicWeapon[i].attack;
             }
             else
             {
                 epicWeapon[i].isPurchased = true;
             }*//*

             return;
         }
         else
         {
             Transform reward = Instantiate(pfslot, SloatUI.position, Quaternion.identity);
             reward.parent = SloatUI;

             *//*int i = Random.Range(0, legendaryWeapon.Length);
             reward.GetComponent<Image>().sprite = legendaryWeapon[i].weaponImage;

             if (legendaryWeapon[i].isPurchased == true)
             {
                 legendaryWeapon[i].attack += attackAdder;
                 newAttack.gameObject.SetActive(true);
                 newAttack.text = "New Attack - " + legendaryWeapon[i].attack;
             }
             else
             {
                 legendaryWeapon[i].isPurchased = true;
             }*//*

             return;
         }
     }
     public bool IsWithin(int value, int minimum, int maximum)
     {
         return value >= minimum && value <= maximum;
     }
     public void DestroyReward()
     {
         foreach (Transform child in SloatUI.transform)
         {
             GameObject.Destroy(child.gameObject);
         }
     }*/


}