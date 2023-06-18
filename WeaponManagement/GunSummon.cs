using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSummon : MonoBehaviour
{
    public Weapon weapon;
    public Vector2 detectionRange;

    SpriteRenderer sp;
    GameObject player;
    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        sp.sprite = weapon.gunSprite;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CheckForPlayer()
    {
        //Find Distance between player and gameObject
        Vector2 distanceBtw = player.transform.position - transform.position;

        //Absolute value of distanceBtw
        distanceBtw.x = Mathf.Abs(distanceBtw.x);
        distanceBtw.y = Mathf.Abs(distanceBtw.y);

        if (distanceBtw.y <= detectionRange.y && distanceBtw.x <= detectionRange.x)
        {
            //Add a weapon to player Inventory
            player.GetComponent<PlayerMovement>().inventoryWeapon.Add(weapon);
            Destroy(gameObject);
        }
    }
}
