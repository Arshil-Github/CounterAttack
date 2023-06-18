using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public int level = 0;
    public int PercentBtwLevel = 20;
    public int xpPoints = 0;//Stores the total xpGained in a level
    public int intialXPPoints; //Stores xp before the level so that it could be added on end screen Always Out of Xpneeded 
    public int Initialxpneeded = 100;
    public Text levelText;
    public Text currentLevelUI;
    public Text nextlevelUI;
    public Slider experienceBar;
    public float fillTime;
    public float fillAmount;
    // Start is called before the first frame update
    void Start()
    {
        experienceBar.value = 0;
        experienceBar.maxValue = Initialxpneeded;
    }
    public void increaseXp(int xp)
    {
        //Add the given xp
        xpPoints += xp;

    }
    public void EnterEndScreen()
    {
        StartCoroutine(FillOnEndScreen());
    }
    public IEnumerator FillOnEndScreen()
    {
        //This is for setting up the bar
        experienceBar.value = intialXPPoints;
        experienceBar.maxValue = Initialxpneeded;

        //Add xp to initial if inital = xp needed - value = 0 && xp -= xp needed - initial 

        
        for (int i  = xpPoints; i > 0; i--)
        {
            experienceBar.value += 1;

            yield return new WaitForSeconds(fillTime);

            if (experienceBar.value >= experienceBar.maxValue)
            {
                experienceBar.value = 0;
                //Increase level - Increase level needed - set xP 0 - Rinse and repeat
                level++;
                SetLevelNumberUI();
                Initialxpneeded += (PercentBtwLevel / 100) * Initialxpneeded;
                experienceBar.maxValue = Initialxpneeded;
            }
        }


        //StartCoroutine(FillSlider());

    }
    public void SetLevelNumberUI()
    {
        currentLevelUI.text = level.ToString();
        nextlevelUI.text = (level + 1).ToString();
    }
}
