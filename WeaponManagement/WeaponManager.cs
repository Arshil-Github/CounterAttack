using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public Image currentWeaponImage;
    public GameObject ammoSlot;
    public GameObject ammoUI;
    public GameObject player;
    public Image nextWeapon_Image;
    public Text nextWeapon_Ammo;

    List<GameObject> ammoList = new List<GameObject>();

    private void Start()
    {
    }
    public void equipWeapon(Weapon current, Weapon nextWeapon)
    {
        //Show which weapon is equippied; w to be taken from InventoryManager cuurent weapon
        currentWeaponImage.sprite = current.gunSprite;
        //Show Info about next Weapon
        nextWeapon_Image.sprite = nextWeapon.gunSprite;
        nextWeapon_Ammo.text = nextWeapon.ammo.ToString();
    }
    public void ChangeAmmo(int i , Color c)
    {
        //Instantiate ammo UI Dots 
        for(int j = 0; j < i; j++)
        {
            GameObject e = Instantiate(ammoUI, ammoSlot.gameObject.transform);

            //Change Color of the ammo by getting current weapon by index from PLayerMovement
            e.GetComponent<Image>().color = c;

            ammoList.Add(e);
        }
    }
    public void Update()
    {
        //Just for Testing
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ReduceAmmo();
        }
    }
    public void ReduceAmmo()
    {
        //Remove the last GameObject from ammolist
        if(ammoList.Count - 1 != 0)// to prevent IndexOutOfExpectation
        {
            Destroy(ammoList[ammoList.Count - 1]);
            ammoList.RemoveAt(ammoList.Count - 1);
        }
        else
        {
            //Change Weapon in Player Script when out of ammo
            Destroy(ammoList[ammoList.Count - 1]);
            ammoList.RemoveAt(ammoList.Count - 1);
            player.GetComponent<PlayerMovement>().ChangeCurrentWeapon();
        }
    }
}
