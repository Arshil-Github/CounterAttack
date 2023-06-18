using UnityEngine;

public class DotScript : MonoBehaviour
{
    public float minDistance;
    bool EnemyinRange = false;
    Enemy CollidedEnemy;
    LevelGeneration lg;
    public GameObject hit;
    private void Start()
    {
        #region DotLocationChecker
        /*//If The Dot is outside the desired area, Delete it
        lg = GameObject.Find("LevelGeneration").GetComponent<LevelGeneration>();
        float x = transform.position.x;
        if(x > lg.leftoffset & x < lg.rightoffset)
        {
            //Do Nothing
        }
        else
        {
            Destroy(gameObject);
        }
        float y = transform.position.y;
        if (y > lg.bottomoffset & y < lg.topoffset)
        {
            //Do Nothing
        }
        else
        {
            Destroy(gameObject);
        }*/

        Collider2D[] overlapingGM = Physics2D.OverlapCircleAll(transform.position, .1f);

        foreach (Collider2D c in overlapingGM)
        {
            if (c.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
                return;
            }
        }

        #endregion
        //Check the distance between gameObject and every Enemy
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //if even one enemy is in the given range STOP else continue to look
            Vector2 i = gameObject.transform.position - g.transform.position;
            if (Mathf.Abs(i.x) <= minDistance && Mathf.Abs(i.y) <= minDistance)
            {
                EnemyinRange = true;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            }
            else if (!EnemyinRange)
            {
                EnemyinRange = false;
            }
        }



    }
    private void OnMouseDown()
    {
        
         Stuff();
    }
    public void Update()
    {
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
            touchpos.z = transform.position.z;
            Vector2 k = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y) - new Vector2(touchpos.x, touchpos.y);
            if (Mathf.Abs(k.x) <= minDistance && Mathf.Abs(k.y) <= minDistance && touch.phase == TouchPhase.Began)
            {
                Debug.Log("Well Donw Boi");
                Stuff();
            }

        }*/
    }
    private void OnDrawGizmos()
    {
       //UnityEditor.Handles.color = Color.yellow;

       //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, minDistance);
    }
    public void Stuff()
    {
        EnemyinRange = false;
        Time.timeScale = 1;
        //Check the distance between gameObject and every Enemy
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            //if even one enemy is in the given range STOP else continue to look
            Vector2 i = gameObject.transform.position - g.transform.position;
            if (Mathf.Abs(i.x) <= minDistance && Mathf.Abs(i.y) <= minDistance)
            {
                EnemyinRange = true;
                CollidedEnemy = g.GetComponent<Enemy>();
            }
            else if (!EnemyinRange)
            {
                EnemyinRange = false;
            }
        }

        //On MOuse Click
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Remove Dots
        player.GetComponent<PlayerMovement>().ReduceDot();
        if (!EnemyinRange)
        {
            //Change Player Position
            player.GetComponent<PlayerMovement>().MovetoLoc(gameObject.transform.position);
        }
        else
        {
            player.GetComponent<PlayerMovement>().PlayerAttack(CollidedEnemy);
        }
        //player.GetComponent<PlayerMovement>().SpawnDots();
        //Check for GunSummon Range
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("GunSpawner"))
        {
            g.GetComponent<GunSummon>().CheckForPlayer();
        }
        GameObject hitEffect = Instantiate(hit, transform.position, Quaternion.identity);
        Destroy(hitEffect, .1f);
    }
}
