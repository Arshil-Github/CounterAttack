using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject losePanel;
    public GameObject[] uiToDisableOnLose;
    public GameObject[] uiToEnableOnLose;

    public Text coinNumber;
    public GameObject loadingText;

    public int max_rewardSteps;
    public int currentSteps;
    public int firstGradeRewardStep;
    public int secondGradeRewardStep;
    public int thirdGradeReward;
    public Slider Rewardbar;
    //Basicly handles all simple task
    void Start()
    {
        Rewardbar.maxValue = max_rewardSteps;
        StartCoroutine(StartDelayTimeStop());
        Rewardbar.value = max_rewardSteps;
    }
    public void ReduceRewardbarValue()
    {
        currentSteps += 1;
        Rewardbar.value -= 1;
    }
    public void ShowLosePanel()
    {
        //call this on dieing
        losePanel.SetActive(true);
        
        //Play A PlayDirector
        //Disable and enable all the object specified in 10, 11
        foreach(GameObject g in uiToDisableOnLose)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in uiToEnableOnLose)
        {
            g.SetActive(true);
        }
        //Give Rewards

        if (max_rewardSteps - currentSteps >= firstGradeRewardStep)
        {
            Debug.Log("Give First grade Award");
        }
        else if (max_rewardSteps - currentSteps >= secondGradeRewardStep && max_rewardSteps - currentSteps < firstGradeRewardStep)
        {
            Debug.Log("Give Second grade Award");
        }
        else if (max_rewardSteps - currentSteps >= thirdGradeReward && max_rewardSteps - currentSteps < secondGradeRewardStep)
        {
            Debug.Log("Give Third grade Award");
        }

        GameObject.Find("Experience").GetComponent<ExperienceManager>().EnterEndScreen();
    }
    IEnumerator StartDelayTimeStop()
    {
        yield return new WaitForSeconds(GameObject.Find("LevelLoader").GetComponent<levelloader>().transitionTime + .5f);
        /*Time.timeScale = 0;*/
    }
    public void NextScene()
    {
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(0);

        loadingText.SetActive(true);
    }
    public void MoveToScene(int index)
    {
        loadingText.SetActive(true);
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(index);
    }
    public void Replay()
    {
        loadingText.SetActive(true);
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ChangeCoinText(int number)
    {
        //Call this function in GlobalObject
        coinNumber.text = number.ToString();
    }
}
