using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �ε�
//����
//����

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        
        return Resources.Load<T>(path);
    }
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"../Prefabs/{path}");
        if(prefab == null)
        {
            Debug.Log($"������ �ε忡 ������ : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }
    public void Destroy(GameObject go, float time = 0.0f)
    {
        if (go == null) return;
        Object.Destroy(go, time);
    }
}
