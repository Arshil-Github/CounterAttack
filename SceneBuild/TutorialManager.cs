using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject WeaponUI;
    public GameObject CoinUI;
    public GameObject PowerUpUI;

    public GameObject Fog1;
    public GameObject Fog2;
    public GameObject Fog3;
    public GameObject Player;

    public GameObject UI_BG;
    public GameObject[] UI_tuts;

    public string[] dialogue;
    public Text DialogueUI;

    public Enemy Enemy1;
    public GameObject Hand1;
    public GameObject Instruction3;
    // Start is called before the first frame update

    bool InstructToKillEnemy1 = false;
    void Start()
    {
        DialogueUI.text = dialogue[0];
        //UI_tuts[0].SetActive(false);
    }
    public void Stage1()
    {
        DialogueUI.text = dialogue[1];
        UI_BG.SetActive(true);
        CoinUI.SetActive(true);
        UI_tuts[1].SetActive(true);

        InstructToKillEnemy1 = true;
    }
    public void Stage2()
    {
        DialogueUI.text = dialogue[2];
        Fog1.SetActive(false);
        WeaponUI.SetActive(true);
    }
    public void Stage3()
    {
        DialogueUI.text = dialogue[3];
        UI_BG.SetActive(true);
        UI_tuts[2].SetActive(true);
    }
    public void Stage4()
    {
        DialogueUI.text = dialogue[4];
        Fog2.SetActive(false);
    }
    public void Stage5()
    {
        DialogueUI.text = dialogue[5];
        UI_BG.SetActive(true);
        UI_tuts[3].SetActive(true);
        Fog3.SetActive(false);
    }
    // Update is called once per frame
    public void Reload()
    {
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
    }
    public void Update()
    {
        if(InstructToKillEnemy1 == true)
        {
            if (Enemy1.gameObject != null)
            {
                foreach (Collider2D g in Physics2D.OverlapCircleAll(Enemy1.transform.position, 2f))
                {

                    if (g.CompareTag("Player") && Enemy1.attackCharged == false)
                    {
                        Hand1.SetActive(true);
                        Instruction3.SetActive(true);

                        Hand1.transform.position = new Vector2(Enemy1.transform.position.x + 1f, Enemy1.transform.position.y - 1.5f);
                    }
                    else if (Enemy1.attackCharged == true)
                    {
                        Hand1.SetActive(false);
                        Instruction3.SetActive(false);
                    }
                }
            }
        }
    }

}
