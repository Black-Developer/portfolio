using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region Singleton
    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }
    #endregion

    DataManager _data = new DataManager();
    ResourceManager _resouce = new ResourceManager();
    public static DataManager Data { get { return Instance._data; } }
    public static ResourceManager Resource { get { return Instance._resouce; } }
    

    void Start()
    {
        Init();
    }
    private void OnApplicationQuit()
    {
        Clear();
        Debug.Log("게임 종료");
    }


    static void Init()
    {
        if (s_instance != null) return;
        GameObject go = GameObject.Find("GameManager");
        if (go == null)
        {
            go = new GameObject { name = "GameManager" };
            go.AddComponent<Managers>();
        }
        DontDestroyOnLoad(go);
        s_instance = go.GetComponent<Managers>();
        s_instance._data.Init();
    }
    public static void Clear()
    {
        s_instance._data.quitGame();
    }
}
