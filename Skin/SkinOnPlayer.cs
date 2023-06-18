using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinOnPlayer : MonoBehaviour
{
    public SpriteRenderer sr;
    // Start is called before the first frame update
    public void ChangeSkin(Skin s)
    {
        sr.sprite = s.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
