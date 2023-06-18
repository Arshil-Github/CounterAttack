using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posto2 : MonoBehaviour
{
    // This Script Round off the transform.position value to the neasrest 2
    void Start()
    {
        //Round Off position to nearest 2
        float x = transform.position.x % 2;
        float y = transform.position.y % 2;

        transform.position = new Vector2(transform.position.x - x, transform.position.y - y);
    }

}
