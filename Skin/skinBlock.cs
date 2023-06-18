using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skinBlock : MonoBehaviour
{
    public Skin skin;
    public Color equipColor;

    private void Start()
    {
        
    }
    public void EquipDeEquip()
    {
        FindObjectOfType<SkinStorage>().currentSkin = skin;
        foreach(skinBlock s in FindObjectOfType<skinManager>().GetComponent<skinManager>().skinBlocksInScene)
        {
            s.ChangeButton();
        }
    }
    public void notOwnedUI()
    {
        transform.Find("Disabled").gameObject.SetActive(true);
        transform.Find("Equip").GetComponent<Button>().interactable = false;
        transform.Find("Equip/equiptext").GetComponent<Text>().text = "Locked";

        transform.Find("Disabled/SkinPieces").GetComponent<Text>().text = skin.skinPiecesHave + " / " + skin.skinPiecesneeded;
    }
    public void ChangeButton()
    {
        if(FindObjectOfType<SkinStorage>().currentSkin == skin)
        {
            //Here Show Equip Button
            transform.Find("Equip").GetComponent<Button>().interactable = false;
            transform.Find("Equip/equiptext").GetComponent<Text>().text = "Equippied";
        }
        else
        {
            //Here Disable Button
            transform.Find("Equip").GetComponent<Button>().interactable = true;
            transform.Find("Equip").transform.Find("equiptext").GetComponent<Text>().text = "Equip";
        }
    }
}
