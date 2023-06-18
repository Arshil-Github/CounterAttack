using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBlock : MonoBehaviour
{
    public Chest chest;
    public GameObject fx_chestOpen;

    public void OpenChest()
    {
        GameObject fx_effect = Instantiate(fx_chestOpen, transform.position, Quaternion.identity);

    }
}
