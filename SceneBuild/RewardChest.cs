using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RewardChest : MonoBehaviour
{
    public int coin;
    public float radius;
    public UnityEvent afterCollect;

    // Update is called once per frame
    public void ChestCheck()
    {
        foreach (Collider2D g in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if (g.CompareTag("Player"))
            {
                g.GetComponent<PlayerMovement>().ds.SetCoin(coin);
                GameObject cText = Instantiate(g.GetComponent<PlayerMovement>().CoinPopUp, transform.position, Quaternion.identity);
                cText.GetComponent<TextMeshPro>().text = coin.ToString();

                afterCollect.Invoke();

                Destroy(gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        //UnityEditor.Handles.color = Color.red;

        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, radius);
    }
    public void NextScene(int i)
    {
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(i);
    }
}
