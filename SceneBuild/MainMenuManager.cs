using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Background;
    public float BG_bluredAlpha;
    public float BG_NormalAlpha;

    public GameObject LoadingText;

    public void OpenPanel()
    {
        //Call this on button click This blurs the background
        Color o = Background.GetComponent<SpriteRenderer>().color;
        Background.GetComponent<SpriteRenderer>().color = new Color(o.r, o.g, o.b, BG_bluredAlpha);
    }
    public void ClosePanel()
    {
        //Call this on Closing the panel 
        Color o = Background.GetComponent<SpriteRenderer>().color;
        Background.GetComponent<SpriteRenderer>().color = new Color(o.r, o.g, o.b, BG_NormalAlpha);
    }
    public void PlayButton()
    {
        LoadingText.SetActive(true);
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(3);
    }
    public void ShopButton()
    {
        LoadingText.SetActive(true);
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(1);
    }
}
