using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform poseTR;
    public Transform poseTL;
    public Transform poseBR;
    public Transform poseBL;


    public GameObject[] roomsTR;
    public GameObject[] roomsTL;
    public GameObject[] roomsBR;
    public GameObject[] roomsBL;

    //For Making all the objects stay within this area
    public float rightoffset;
    public float leftoffset;
    public float topoffset;
    public float bottomoffset;

    // Start is called before the first frame update
    void Start()
    {
        // Generate Random Numberes for each side and generate that side Prefab
        int a = Random.Range(0, roomsTR.Length);
        Instantiate(roomsTR[a], poseTR.position, Quaternion.identity);

        int b = Random.Range(0, roomsTL.Length);
        Instantiate(roomsTL[a], poseTL.position, Quaternion.identity);

        int c = Random.Range(0, roomsBR.Length);
        Instantiate(roomsBR[a], poseBR.position, Quaternion.identity);

        int d = Random.Range(0, roomsBL.Length);
        Instantiate(roomsBL[a], poseBL.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
