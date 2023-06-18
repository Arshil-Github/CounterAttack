using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinStorage : MonoBehaviour
{
    #region GlobalCrap
    //For making this a global Script
    public static SkinStorage Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Skin currentSkin;
    public List<Skin> allSkins;
    public List<Skin> ownedSkins;
    private void Start()
    {
        if(FindObjectOfType<SkinOnPlayer>() != null)
        {
            Debug.Log("Activated");
            FindObjectOfType<SkinOnPlayer>().ChangeSkin(currentSkin);
        }
    }
}
