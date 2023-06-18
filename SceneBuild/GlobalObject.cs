using UnityEngine;
using UnityEngine.UI;
public class GlobalObject : MonoBehaviour
{
    #region GlobalCrap
    //For making this a global Script
    public static GlobalObject Instance;
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

}
