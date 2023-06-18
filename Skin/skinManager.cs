using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class skinManager : MonoBehaviour
{
    public skinBlock[] skinBlocksInScene;

    private void Start()
    {
        skinBlocksInScene = FindObjectsOfType<skinBlock>();

        foreach(skinBlock s in skinBlocksInScene)
        {
            s.transform.Find("Sprite").GetComponent<Image>().sprite = s.skin.sprite;
            s.transform.Find("Name").GetComponent<Text>().text = s.skin.name;

            if (!SkinStorage.Instance.ownedSkins.Contains(s.skin))
            {
                s.notOwnedUI();
            }

            s.transform.Find("Equip").GetComponent<Button>().onClick.AddListener(s.EquipDeEquip);
        }
    }
    public void HomeButton()
    {
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(0);
    }
}
