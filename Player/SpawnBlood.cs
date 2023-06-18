using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlood : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bloodStain;
    public void StainBlood(Vector2 pos)
    {
        Instantiate(bloodStain, pos, Quaternion.identity);
    }
}
