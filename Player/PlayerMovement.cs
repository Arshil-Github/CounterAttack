using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public GameObject MovementDots;
    public GameObject PlayerMiddlePoint; //TODO: make this offset list in weapon instead of here so that range can be changed from there
    public GameObject WeaponManager;
    public int equippiedWeaponIndex;
    public List<Weapon> inventoryWeapon;
    public Animator anim;
    public SpriteRenderer CurrentWeapon2D;
    public GameObject Flash;
    private Vector3 spawnLocation;
    public WeaponManager weaponManagerScript;
    public Vector3[] offsets;
    Vector3 desiredPos;

    public GameManager gm;
    public PowerUp powerupScript;
    public DataToSave ds;
    bool TurnOver = false; //for callling turn done function only once
    public GameObject CoinPopUp;
    public UnityEvent AfterDeathEvent;
    public AudioSource SFX_audiosource;
    private void Start()
    {
        SFX_audiosource = GameObject.FindGameObjectWithTag("SFXAudioSource").GetComponent<AudioSource>();
        //Set offsets to weapon DotLocation
        offsets = inventoryWeapon[equippiedWeaponIndex].DotLocations;

        // int equip Waeapon Index handles current weapon
        equippiedWeaponIndex = 0;
        weaponManagerScript = WeaponManager.GetComponent<WeaponManager>();

        //Change UI according to current weapon
        weaponManagerScript.equipWeapon(inventoryWeapon[equippiedWeaponIndex], inventoryWeapon[equippiedWeaponIndex + 1]);
        weaponManagerScript.ChangeAmmo(inventoryWeapon[equippiedWeaponIndex].ammo, inventoryWeapon[equippiedWeaponIndex].color);
        CurrentWeapon2D.sprite = inventoryWeapon[equippiedWeaponIndex].gunSprite;

        StartCoroutine(DelayinDotSpawn());

    }

    #region Movement
    bool DotsSpawned = false;
    public void SpawnDots()
    {
        #region EnemyMoves

        //Get all Enemy inside the Scene and call Movement
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (g.GetComponent<Enemy>() != null)//To Porevent IOFE
            {
                g.GetComponent<Enemy>().Movement();

            }
        }
        #endregion
        StartCoroutine(DelayinDotSpawn());
    }
    IEnumerator DelayinDotSpawn()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(.3f);
        if (!DotsSpawned)
        {
            spawnLocation = new Vector3(PlayerMiddlePoint.transform.position.x, PlayerMiddlePoint.transform.position.y, PlayerMiddlePoint.transform.position.z - 2);
            //Loop through offsets and spawn GameObjects
            for (int i = 0; i < offsets.Length; i++)
            {
                GameObject dot = Instantiate(MovementDots, spawnLocation + offsets[i], Quaternion.identity);
                dot.GetComponent<SpriteRenderer>().color = inventoryWeapon[equippiedWeaponIndex].color;
            }
            DotsSpawned = true;
        }
    }
    public void ReduceDot()
    {
        //This function is called by DotScript when Clicked on a Dot. It Access the Weapon Manager and call ReduceAmmo function
        weaponManagerScript.ReduceAmmo();

    }
    public void DeleteAllDots()
    {
        //Destroy All the dotsOn Screen
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Dot"))
        {
            Destroy(e);
        }
        DotsSpawned = false;

        powerupScript.TurnDone();

    }
    public void MovetoLoc(Vector3 pos)
    {
        powerupScript.StartTurn();
        desiredPos = pos;
        DeleteAllDots();
        anim.SetTrigger("Move");
    }
    public void Move()
    {
        transform.position = desiredPos;
        StartCoroutine(ShowFlash());
    }
    IEnumerator ShowFlash()
    {
        Flash.SetActive(true);
        yield return new WaitForSeconds(.5f);
        Flash.SetActive(false);


        //This script do everything needed after the end of turn
    }
    #endregion

    #region InventoryManager

    public void AddWeapon(Weapon w)
    {
        Time.timeScale = 1;
        // Use this to add a function to add new weapon to your arsenal
        inventoryWeapon.Add(w);
    }
    public void ChangeCurrentWeapon()
    {
        //using a coroutine to add a delay of 0.02. This allows enemy script to detect any changes in player config after it reached its 
        //final position
        //Fixes the problem of color system not working on the last ammo
        StartCoroutine(DelayedSwitch());
        //Call this function in WeaponManager when you run out of ammo to change Weapon
    }
    IEnumerator DelayedSwitch()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.02f);
        //Check whether There is a next weapon or not
        if (inventoryWeapon.Count > equippiedWeaponIndex + 1)//To avoid IndexOutofExpectation
        {
            //If yes change to that
            equippiedWeaponIndex++;
        } else
        {
            //If no Switch to 0
            equippiedWeaponIndex = 0;
        }

        //Check whether there is a weapon to be shown in next weapon section
        if (inventoryWeapon.Count > equippiedWeaponIndex + 1)//To avoid IndexOutofExpectation
        {
            //If yes switch next weapon to actual next weapon
            weaponManagerScript.equipWeapon(inventoryWeapon[equippiedWeaponIndex], inventoryWeapon[equippiedWeaponIndex + 1]);
        } else
        {
            //If no switch next weapon to 0
            weaponManagerScript.equipWeapon(inventoryWeapon[equippiedWeaponIndex], inventoryWeapon[0]);
        }
        //Call The Functions here
        weaponManagerScript.ChangeAmmo(inventoryWeapon[equippiedWeaponIndex].ammo, inventoryWeapon[equippiedWeaponIndex].color);
        offsets = inventoryWeapon[equippiedWeaponIndex].DotLocations;
        CurrentWeapon2D.sprite = inventoryWeapon[equippiedWeaponIndex].gunSprite;
    }
    #endregion

    #region Attacks
    public void PlayerAttack(Enemy e)
    {
        //Player Attacks Instead of moving
        powerupScript.StartTurn();
        //Take Enemy to mess up with it Do the Same things in Enemy Attack function
        if (e.enemyColor == inventoryWeapon[equippiedWeaponIndex].weaponColor)
        {
            //Enemy Dead
            //PLay Burst Animation
            anim.SetTrigger("Attack");

            StartCoroutine(DelayedEnemy(e.gameObject));
            //Get all Enemy inside the Scene and call Movement
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (g.GetComponent<Enemy>() != null)//To Porevent IOFE
                {
                    g.GetComponent<Enemy>().Movement();

                }
            }
            StartCoroutine(ShowFlash());
            //powerupScript.TurnDone();
        }
        else
        {
            //Play Die Animation
            Lost();
        }
        weaponManagerScript.ChangeAmmo(inventoryWeapon[equippiedWeaponIndex].ammo, inventoryWeapon[equippiedWeaponIndex].color);

    }
    public void KillEnemy(GameObject g)
    {//Useless Now

        //Save pos and GameObject here so that they can be accesed after destroying the enemy
        Vector3 pos = g.transform.position;
        GameObject e = g.GetComponent<Enemy>().enemyExplode;
        Destroy(g);

        //Give Rewards
        ds.SetExperience(g.GetComponent<Enemy>().RewardExp);
        ds.SetCoin(g.GetComponent<Enemy>().RewardCoin);

        //Instantiate effect and destroy it
        GameObject explodeVFX = Instantiate(e, pos, Quaternion.identity);
        Destroy(explodeVFX, anim.GetCurrentAnimatorClipInfo(0).Length + 0.10f);

        //Spawn Blood Stain
        GameObject.Find("BloodStainSpawner").GetComponent<SpawnBlood>().StainBlood(pos);
    }
    public void Lost()
    {
        AfterDeathEvent.Invoke();
        //Called in Enemy Script to die if player is in range of enemy
        gm.ShowLosePanel();
        Destroy(gameObject);
    }
    IEnumerator DelayedEnemy(GameObject g)
    {
        g.GetComponent<Enemy>().AfterDeathEvent.Invoke();
        SFX_audiosource.clip = g.GetComponent<Enemy>().die_SFX;
        SFX_audiosource.Play();


        yield return new WaitForSeconds(0.001f);

        //Save pos and GameObject here so that they can be accesed after destroying the enemy
        Vector3 pos = g.transform.position;
        GameObject e = g.GetComponent<Enemy>().enemyExplode;

        yield return new WaitForSeconds(0.001f);

        Destroy(g);
        //Give Rewards
        ds.SetExperience(g.GetComponent<Enemy>().RewardExp);
        ds.SetCoin(g.GetComponent<Enemy>().RewardCoin);

        GameObject cText = Instantiate(CoinPopUp, transform.position, Quaternion.identity);
        cText.GetComponent<TextMeshPro>().text = g.GetComponent<Enemy>().RewardCoin.ToString();

        //Instantiate effect and destroy it
        GameObject explodeVFX = Instantiate(e, pos, Quaternion.identity);
        Destroy(explodeVFX, anim.GetCurrentAnimatorClipInfo(0).Length + 0.10f);

        //Spawn Blood Stain
        GameObject.Find("BloodStainSpawner").GetComponent<SpawnBlood>().StainBlood(pos);

        StopAllCoroutines();
    }
    #endregion

}
