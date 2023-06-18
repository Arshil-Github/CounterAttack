using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int turns;
    public float radius;
    public GameObject explodeEffect;
    List<GameObject> EnemiesInRange;

    public void ActivateGrenade()
    {
        turns = turns - 1;
        Debug.Log(turns);
        if(turns == 0)
        {
            Explosion();
        }
    }
    void Explosion()
    {
        //Check the distance between gameObject and every Enemy
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //if even one enemy is in the given range STOP else continue to look
            Vector2 i = gameObject.transform.position - g.transform.position;
            if (Mathf.Abs(i.x) <= radius && Mathf.Abs(i.y) <= radius)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().KillEnemy(g);
            }
        }

        Destroy(gameObject);

        GameObject explodeVFX = Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(explodeVFX, 2f);
    }
}
