using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    //Used in PlayerMovement Script for Attacking
    public Vector2 DetectionRange;
    public Vector2 AttackRange;
    public float DistanceTravel;
    public int RewardCoin;
    public int RewardExp;
    public string enemyColor;
    public Animator EnemyAnimator;
    public string ChargeTrigger;
    public string NormalTrigger;
    public string PosChangeTrigger;
    public GameObject enemyExplode;

    //JustForTesting
    public SpriteRenderer sp;
    public Color normalColor;

    GameObject player;
    public bool attackCharged;
    Weapon playerWeapon;
    Vector3 posToBeTransported;

    AudioSource audioSource;
    public AudioClip move_SFX;
    public AudioClip charge_SFX;
    public AudioClip die_SFX;

    public Vector3[] DotLocations;
    public GameObject EnemyDots;

    public GameObject pf_ChargeEffect;
    GameObject current_ChargeEffect;

    public UnityEvent AfterDeathEvent;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (attackCharged)
        {
            ChargeAttack();
            EnemyAnimator.SetBool(NormalTrigger, false);
        }
        StartCoroutine(SpawnEnemyDots());
    }

    #region EnemyAttack(UselessNow)
    //Attacking now in PLayerMovement so this is useless
    IEnumerator Attack()
    {

        // One Second Delay so that all the attack calc happens when the player reaches its position
        yield return new WaitForSeconds(0.01f);
        //Find Distance between player and gameObject
        Vector2 distanceBtw = player.transform.position - transform.position;

        //Absolute value of distanceBtw
        distanceBtw.x = Mathf.Abs(distanceBtw.x);
        distanceBtw.y = Mathf.Abs(distanceBtw.y);

        if(distanceBtw.y <= AttackRange.y && distanceBtw.x <= AttackRange.x)
        {
            if (playerWeapon.weaponColor != enemyColor)
            {
                //Player Defeated
                Destroy(player);
            }
            else if(playerWeapon.weaponColor == enemyColor)
            {
                if(!attackCharged)
                {
                    //Player Defeated
                    Destroy(player);
                }
                else
                {
                    //Enemy Defeated
                    /*GlobalObject.Instance.coins += RewardCoin;
                    GlobalObject.Instance.xpPoints += RewardExp;
                    */
                    Debug.Log("Awarded");
                    Destroy(gameObject);
                }
            }
        }
        StopAllCoroutines();
    }
    #endregion

    public void Movement()
    {

       //For One by One Attack
        if(attackCharged)
        {
            //MoveTowards the Player
            #region MoveTowardsPlayer
            //Find Distance between player and gameObject
            Vector2 distanceBtw = player.transform.position - transform.position;

            //StartCoroutine(Attack());
            if (Mathf.Abs(distanceBtw.y) <= DetectionRange.y && Mathf.Abs(distanceBtw.x) <= DetectionRange.x)
            {   //Move Towards Player
                //If both are non zero then choose one on Random

                Vector2 Pos1 = Vector2.MoveTowards(transform.position, player.transform.position, DistanceTravel);

                if (Mathf.Abs(distanceBtw.y) != 0 && Mathf.Abs(distanceBtw.x) != 0)
                {
                    //Use sign of float DistanceTravel as direction
                    int i = Random.Range(0, 2);
                    if (i == 0)
                    {
                        // If DistanceBtw y is negative distanceTeavel is negative i.e direction is reversed
                        if (distanceBtw.y < 0)
                        {
                            DistanceTravel = DistanceTravel * (-1);
                        }

                        ChangePostobeTransported(new Vector2(transform.position.x, transform.position.y + DistanceTravel));

                        //Reset Direction i.e. sign of DistanceTravel
                        DistanceTravel = Mathf.Abs(DistanceTravel);
                    }
                    if (i == 1)
                    {
                        // If DistanceBtw x is negative distanceTeavel is negative i.e direction is reversed
                        if (distanceBtw.x < 0)
                        {
                            DistanceTravel = DistanceTravel * (-1);
                        }

                        ChangePostobeTransported(new Vector2(transform.position.x + DistanceTravel, transform.position.y));

                        //Reset Direction i.e. sign of DistanceTravel
                        DistanceTravel = Mathf.Abs(DistanceTravel);
                    }
                }
                else
                {
                    //if perpendicular just move towards it
                    ChangePostobeTransported(Pos1);
                }
                //Change to Normal
                EnemyAnimator.SetBool(NormalTrigger, true);

                attackCharged = false;
            }
        }
        else //This make sures that enemies outside detectionRange Become Normal
        {
            Vector2 distanceBtw = player.transform.position - transform.position;

            if (Mathf.Abs(distanceBtw.y) <= DetectionRange.y && Mathf.Abs(distanceBtw.x) <= DetectionRange.x)
            {//InRange of player
                //Charge Attack
                ChargeAttack();
                //StartCoroutine(Attack());
                attackCharged = true;
                EnemyAnimator.SetBool(NormalTrigger, false);
            }
            else
            {
                attackCharged = false;
                //Change to Normal
                EnemyAnimator.SetBool(NormalTrigger, true);
            }
        }
        #endregion
        //Keep a check on Player Weapon
        playerWeapon = player.GetComponent<PlayerMovement>().inventoryWeapon[player.GetComponent<PlayerMovement>().equippiedWeaponIndex];
    }
    void ChangePostobeTransported(Vector2 pos)
    {
        Vector2 originalPos = transform.position;
        #region CheckOLocation
        //If The Dot is outside the desired area, Delete it
       /* LevelGeneration lg = GameObject.Find("LevelGeneration").GetComponent<LevelGeneration>();
        float x = pos.x;
        if (x > lg.leftoffset & x < lg.rightoffset)
        {
            posToBeTransported = pos;
            EnemyAnimator.SetTrigger(PosChangeTrigger);
        }
        else
        {
            transform.position = originalPos;
            ChangePostobeTransported(new Vector2(-pos.x, pos.y));
            return;
        }
        float y = pos.y;
        if (y > lg.bottomoffset & y < lg.topoffset)
        {
            posToBeTransported = pos;
            EnemyAnimator.SetTrigger(PosChangeTrigger);
        }
        else
        {
            transform.position = originalPos;
            ChangePostobeTransported(new Vector2(-pos.x, pos.y));
            return;
        }*/



        #endregion
        posToBeTransported = pos;
        EnemyAnimator.SetTrigger(PosChangeTrigger);
    }
    public void ChargeAttack()
    {
        PlaySound(charge_SFX);

        current_ChargeEffect = Instantiate(pf_ChargeEffect, transform.position, Quaternion.identity);

        //Do some kind of Charge Animation
        EnemyAnimator.SetTrigger(ChargeTrigger);

        foreach (GameObject a in GameObject.FindGameObjectsWithTag("EnemyDot"))
        {
            Destroy(a);
        }
        StartCoroutine(SpawnEnemyDots());
    }
    public void PlaySound(AudioClip ac)
    {
        audioSource.clip = ac;
        audioSource.Play();
    }
    public void ChangePosition()
    {
        Destroy(current_ChargeEffect);

        Vector2 originalPos = transform.position;
        //Using a seperate method so that it can be called as a event in AnimationClip
        transform.position = posToBeTransported;
        foreach (Collider2D g in Physics2D.OverlapCircleAll(transform.position, .15f))
        {
            if (g.CompareTag("Obstacle"))
            {
                transform.position = originalPos;
                Movement();
                return;
            }
            else
            {
            }
        }

        // GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().DelayinDotSpawn();
    }
    public void IsPlayerNear()
    {
        //Check whether there is player or not
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Vector2 distanceBtw = player.transform.position - transform.position;
            
            if (Mathf.Abs(distanceBtw.y) <= .5 && Mathf.Abs(distanceBtw.x) <= .5)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Lost();
            }
        }
        
    }
    IEnumerator SpawnEnemyDots()
    {
        foreach(GameObject a in GameObject.FindGameObjectsWithTag("EnemyDot"))
        {
            Destroy(a);
        }
        yield return new WaitForSeconds(0.2f);
        foreach(Vector3 v in DotLocations)
        {
            Instantiate(EnemyDots, transform.position + v, Quaternion.identity);
        }
    }
    public void killOtherEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach(GameObject g in enemies)
        {
            Vector2 distanceBtw = g.transform.position - transform.position;
            if (Mathf.Abs(distanceBtw.y) <= .5 && Mathf.Abs(distanceBtw.x) <= .5)
            {
                if (g == this.gameObject)
                {
                    return;
                }
                else
                {
                    player.GetComponent<PlayerMovement>().KillEnemy(gameObject);
                }
            }
        }

    }
    private void OnDrawGizmos()
    {
        //UnityEditor.Handles.color = Color.red;

        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, DetectionRange.x);
        //UnityEditor.Handles.DrawWireCube(transform.position, 2 * DetectionRange);
    }
}
