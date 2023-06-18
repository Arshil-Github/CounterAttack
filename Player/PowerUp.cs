using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    GameObject[] powerups;

    public GameObject Grenade;
    public GameObject GrenadeDeployEffect;
    public int NumberOfGrenades;
    public Text GrenadeNumberText;

    public Weapon RocketLauncher;
    public GameObject RLDeployEffect;
    public int NumberOfRockets;
    public Text RocketNumberText;
    // Update is called once per frame
    public ParticleSystem rain;
    public GameObject pf_MoveParticleSystem;

    PlayerMovement pm;

    [Header("PowerUpUI")]
    public GameObject BuyPowerUp;
    public GameObject PurchaseCoins;
    public int costOfRockets;

    public Animator Canvas;
    AudioSource audiosource;
    public AudioClip move_SFX;

    public UnityEvent aftermove;
    private void Start()
    {
        audiosource = gameObject.GetComponent<AudioSource>();

        GrenadeNumberText.text = NumberOfGrenades.ToString();
        pm = gameObject.GetComponent<PlayerMovement>();

        NumberOfGrenades = SaveSystem.Load().grenades;
        GrenadeNumberText.text = NumberOfGrenades.ToString();

        NumberOfRockets = SaveSystem.Load().rocketLauncher;
        RocketNumberText.text = NumberOfRockets.ToString();
    }
    public void StartTurn()
    {
        rain.Play();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject g in enemies)
        {
            g.GetComponent<Enemy>().killOtherEnemy();
        }

        Instantiate(pf_MoveParticleSystem, transform.position, Quaternion.identity);

        audiosource.clip = move_SFX;
        audiosource.Play();

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("moveEffects"))
        {
            g.GetComponent<ParticleSystem>().Play();
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("stopAnim"))
        {
            g.GetComponent<Animator>().speed = 1;
        }

    }
    public void TurnDone()
    {
        //This is to detect turn over and do all the powerup stuff
        //All after turn events are to be called here
        powerups = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach(GameObject e in powerups)
        {
            if(e.GetComponent<Grenade>() != null)
            {
                e.GetComponent<Grenade>().ActivateGrenade();
            }
        }

        GameObject[] rewardChests = GameObject.FindGameObjectsWithTag("chest");
        foreach (GameObject e in rewardChests)
        {
            if (e.GetComponent<RewardChest>() != null)
            {
                e.GetComponent<RewardChest>().ChestCheck();
            }
        }
 
        aftermove.Invoke();
        //gameObject.GetComponent<DataToSave>().Savedata();
        GameObject.Find("GameManager").GetComponent<GameManager>().ReduceRewardbarValue();
        StartCoroutine(TimeDelayStop());
    }
    IEnumerator TimeDelayStop()
    {
        yield return new WaitForSeconds(1.5f);
        rain.Pause();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("moveEffects"))
        {
            g.GetComponent<ParticleSystem>().Pause();
        }
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("stopAnim"))
        {
            g.GetComponent<Animator>().speed = 0;
        }
        StopAllCoroutines();
    }
    public void DeployGrenade()
    {
        if(NumberOfGrenades != 0)
        {
            Instantiate(Grenade, transform.position, Quaternion.identity);
            NumberOfGrenades -= 1;
            GrenadeNumberText.text = NumberOfGrenades.ToString();
            Canvas.SetTrigger("GrenadePopUp");
            isPowerUpOn = false;

        }
        //Change Grenades in SaveSystem
        DataToSave ds = new DataToSave();
        ds = SaveSystem.Load();
        ds.grenades = NumberOfGrenades;
        SaveSystem.SavePlayer(ds);

    }

    public bool isPowerUpOn = false;
    public void OpenPowerUps() {
        // To be called by Powerup Button - If open play closeing animation
        //If closed play opening anim
        if (!isPowerUpOn)
        {
            Canvas.SetTrigger("OpenPowerUp");
            isPowerUpOn = true;
        }
        else
        {
            Canvas.SetTrigger("ClosePowerUp");
            isPowerUpOn = false;
        }
    }
    public void EquipRocketLauncher()
    {
        if (NumberOfRockets > 0)
        {
            #region ChangeToRocket
            //1. Define pm from PlayerMovement
            //2. Do the same stuff that is done when equip weapon but dont add to weaponInventory list
            //3. delete and respawn dots 
            //4. Do a turn
            RocketLauncher.color = pm.inventoryWeapon[pm.equippiedWeaponIndex].color;
            //Change UI according to current weapon
            pm.weaponManagerScript.equipWeapon(RocketLauncher, pm.inventoryWeapon[pm.equippiedWeaponIndex]);
            pm.weaponManagerScript.ChangeAmmo(RocketLauncher.ammo, pm.inventoryWeapon[pm.equippiedWeaponIndex].color);
            pm.CurrentWeapon2D.sprite = RocketLauncher.gunSprite;

            pm.DeleteAllDots();
            pm.SpawnDots();

            pm.offsets = RocketLauncher.DotLocations;
            #endregion

            NumberOfRockets -= 1;
            RocketNumberText.text = NumberOfRockets.ToString();
        }
        else
        {
            BuyPowerUp.SetActive(true);
            BuyPowerUp.transform.Find("PowerUPName").GetComponent<Text>().text = "Rocket";
            BuyPowerUp.transform.Find("coins").GetComponent<Text>().text = costOfRockets.ToString();
        }

        DataToSave ds = new DataToSave();
        ds = SaveSystem.Load();
        ds.rocketLauncher = NumberOfRockets;
        SaveSystem.SavePlayer(ds);
    }
    public void BuyRocket()
    {
        if (SaveSystem.Load().coins >= costOfRockets)
        {
            DataToSave ds = new DataToSave();
            ds = SaveSystem.Load();
            ds.SetCoin(costOfRockets);
            SaveSystem.SavePlayer(ds);
        }
        else
        {
            PurchaseCoins.SetActive(true);
        }
    }
    public void ChangeMoveEffect(UnityEvent e)
    {
        aftermove = e;
    }
}
