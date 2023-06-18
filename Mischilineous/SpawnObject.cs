using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, objects.Length);
        Instantiate(objects[r], transform.position, Quaternion.identity);
    }

}
