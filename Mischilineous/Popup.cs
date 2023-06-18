using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    TextMeshPro text;
    Color textColor;
    float disappeartimer;
    private void Awake()
    {
        disappeartimer = .2f;
        text = gameObject.GetComponent<TextMeshPro>();
        textColor = text.color;
    }
    private void Update()
    {
        float moveYSpeed = 10f;
        transform.position += new Vector3(0, moveYSpeed * Time.deltaTime);

        disappeartimer -= Time.deltaTime;
        if(disappeartimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            text.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
