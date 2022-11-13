using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{

    private static MonoBehaviour monoInstance;

    [RuntimeInitializeOnLoadMethod]
    private static void Initializer()
    {
        monoInstance = new GameObject($"[{nameof(CoroutineHandler)}]").AddComponent<CoroutineHandler>();
        DontDestroyOnLoad(monoInstance.gameObject);
    }
    public new static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return monoInstance.StartCoroutine(coroutine);
    }
    public new static void StopCoroutine(IEnumerator coroutine)
    {
        monoInstance.StopCoroutine(coroutine);
    }


    //IEnumerator enumerator = null;
    //private void Coroutine(IEnumerator coroutine)
    //{
    //    enumerator = coroutine;
    //    StartCoroutine(coroutine);
    //}

    //void Update()
    //{
    //    if(enumerator != null)
    //    {
    //        if(enumerator.Current == null)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}
    //public void Stop()
    //{
    //    StopCoroutine(enumerator.ToString());
    //    Destroy(gameObject);
    //}

    //public static CoroutineHandler Start_Coroutine(IEnumerator coroutine)
    //{
    //    GameObject obj = new GameObject("CoroutineHandler");
    //    CoroutineHandler handler = obj.AddComponent<CoroutineHandler>();
    //    if(handler)
    //    {
    //        handler.Coroutine(coroutine);
    //    }
    //    return handler;
    //}
}
